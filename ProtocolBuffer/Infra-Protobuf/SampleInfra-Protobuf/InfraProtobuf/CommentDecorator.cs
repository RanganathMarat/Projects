#region  Copyright 2015 Koninklijke Philips N.V.
// 
// All rights are reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written permission of the copyright owner.
#endregion

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Philips.PmsMR.Protobuf.ModelReflection
{
	/// <summary>
	/// Decorate proto-file contents with comments from C# classes.
	/// </summary>
    class CommentDecorator
    {

        public CommentDecorator(string assemblyDoc) 
        {
            this.assemblyDoc = assemblyDoc;
        }

        /// <summary>
        /// Decorates the proto-file contents with C# comments.
        /// </summary>
        /// <param name="protoContents"></param>
        /// <param name="types"></param>
        /// <returns>Decorated proto-file contents</returns>
        public string DecorateWithComments(string protoContents, Type[] types)
        {
            if (String.IsNullOrEmpty(assemblyDoc))
            {
                return protoContents;
            }
            var allMembers = ExtractMemberElements();

            var typeComments = new Dictionary<string, CommentInfo>(StringComparer.InvariantCulture);
            foreach (var type in types)
            {
                if (type.IsClass || type.IsEnum)
                {
                    XElement element;
                    if (allMembers.TryGetValue("T:" + type.FullName, out element))
                    {
                        var comments = ExtractComments(element);
                        var messageComment = new CommentInfo
                        {
                            ItemName = type.Name,
                            ItemClassification = type.IsClass ? CommentInfo.ItemType.Message : CommentInfo.ItemType.Enum,
                            CommentCreator = comments
                        };
                        typeComments[type.Name] = messageComment;
                        PopulateFieldComments(type, allMembers, messageComment);
                    }
                    else
                    {
                        throw new ModelViolationException(type, "Cannot find documentation comments for the class");
                    }
                }
                else
                {
                    throw new ModelViolationException(type, "Unknown type classification, can only interpret classes and enums");
                }
            }

            return InjectComments(protoContents, typeComments);
        }

        internal IDictionary<string, XElement> ExtractMemberElements()
        {
            var doc = System.Xml.Linq.XDocument.Parse(assemblyDoc);
            var allMembers = doc.Descendants("doc").Descendants("members").Descendants("member").ToDictionary(x => x.Attribute("name").Value, StringComparer.InvariantCulture);
            return allMembers;
        }

        internal virtual CommentCreator ExtractComments(XElement memberElement)
        {
            return indentation =>
            {
                var builder = new StringBuilder();
                var summary = memberElement.Descendants("summary").Single().Value.Trim();
                AppendAsComments(summary, builder, indentation);
                var remarksElement = memberElement.Descendants("remarks").FirstOrDefault();
                string remarks;
                if (remarksElement != null)
                {
                    remarks = remarksElement.Value.Trim();
                    AppendAsComments(remarks, builder, indentation);
                }
                return builder.ToString();
            };
        }

        internal string InjectComments(string protoContents, IDictionary<string, CommentInfo> typeComments)
        {
            var injected = declarationFinder.Replace(protoContents, match => 
            {
                var protoName = match.Groups["name"].ToString();
                if (!protoName.Equals(Exporter.ReservedMessageIdType))
                {
                    CommentInfo info;
                    string newContents;
                    if (!typeComments.TryGetValue(protoName, out info))
                    {
                        throw new ModelViolationException(protoName, "Cannot find comments for protofile name");
                    }
                    newContents = InjectFieldComments(match.Groups["contents"].ToString(), info);
                    return "\r\n" + info.CommentCreator(0) + match.Groups["prefix"] + "{" + newContents + "}";
                }
                else
                {
                    return match.Groups["prefix"] + "{" + match.Groups["contents"] + "}";                    
                }
                
            });
            return injected;
        }

        internal string InjectFieldComments(string declarationContents, CommentInfo parentInfo)
        {
            var map = parentInfo.Fields.ToDictionary(x => x.ItemName);
			System.Text.RegularExpressions.Regex finder;
			switch (parentInfo.ItemClassification) {
				case CommentInfo.ItemType.Message:
					finder = messageFieldFinder;
                    break;
                case CommentInfo.ItemType.Enum:
                    finder = enumFieldFinder;
                    break;
                default:
					throw new NotImplementedException("Unimplemented item classification: " + parentInfo.ItemClassification);
			}
            var injected = finder.Replace(declarationContents, match => 
            {
				var name = match.Groups["name"].ToString();
                var indentation = match.Groups["indentation"].ToString();
				CommentInfo info;
                if (!map.TryGetValue(name, out info))
                {
					throw new ModelViolationException(name, "Cannot find comments for protofile field, parent was: " + parentInfo.ItemName);
                }
                return "\r\n" + info.CommentCreator(indentation.Length) + match.Groups["entry"];
            });
            return injected;
        }

        private static readonly System.Text.RegularExpressions.Regex declarationFinder = 
            new System.Text.RegularExpressions.Regex(@"(?<prefix>(enum|message)\s+(?<name>\S+)\s+)\{(?<contents>.*?)\}", 
                RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.Singleline);
        private static readonly System.Text.RegularExpressions.Regex messageFieldFinder =
            new System.Text.RegularExpressions.Regex(@"[\r\n]*(?<entry>(?<indentation>\s*)[^;]+\s+(?<name>\S+)(\s*=\s*\d+\s*(\[.*\]\s*)*;))",
                RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.Singleline);
        private static readonly System.Text.RegularExpressions.Regex enumFieldFinder =
            new System.Text.RegularExpressions.Regex(@"[\r\n]*(?<entry>(?<indentation>\s*)(?<name>\S+)(\s*=\s*\d+\s*($|;)))",
                RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.Singleline);

        internal void PopulateFieldComments(Type type, IDictionary<string, XElement> nameElementMap, CommentInfo messageInfo)
        {
            var reflectionFields = type.GetFields(BindingFlags.Public | BindingFlags.DeclaredOnly |
                    (type.IsEnum ? BindingFlags.Static : BindingFlags.Instance));
            var reflectionProperties = type.GetProperties(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance).Where(x => x.CanRead && x.CanWrite);
			var members = new List<CommentInfo>();
            foreach (var field in reflectionFields)
            {
                members.Add(CreateInfo("F:", field, nameElementMap));
            }
			foreach (var prop in reflectionProperties)
            {
                members.Add(CreateInfo("P:", prop, nameElementMap));
            }
            messageInfo.Fields = members.ToArray();
        }

        private void AppendAsComments(string rawText, StringBuilder builder, int indentation)
        {
            foreach (var line in splitter.Split(rawText))
            {
                builder.Append(new string(' ', indentation));
                builder.AppendLine("/// " + line.Trim());
            }
        }

        private CommentInfo CreateInfo(string namePrefix, MemberInfo member, IDictionary<string, XElement> nameElementMap)
        {
            var key = namePrefix + member.DeclaringType.FullName + "." + member.Name;
            XElement element;
            if (!nameElementMap.TryGetValue(key, out element))
            {
                throw new ModelViolationException(member.DeclaringType, "Cannot find documentation for member: " + member.Name);
            }
            var comment = ExtractComments(element);
            return new CommentInfo
            {
                CommentCreator = comment,
                ItemName = member.Name,
                ItemClassification = CommentInfo.ItemType.Field
            };
        }

        internal delegate string CommentCreator(int indentationSpaces);

        internal class CommentInfo
        {
            /// <summary>
            /// Item-name (without proto-namespace) being decorated with a comment.
            /// </summary>
            public string ItemName;

            /// <summary>
            /// Comment in proto-format.
            /// </summary>
            public CommentCreator CommentCreator;

            public enum ItemType
            {
                Message,
                Enum,
                Field,
            }

            public ItemType ItemClassification;

            public CommentInfo[] Fields;
        }

        private readonly string assemblyDoc;
        private static readonly System.Text.RegularExpressions.Regex splitter = new System.Text.RegularExpressions.Regex(@"\r?\n|\r");
    }
}
