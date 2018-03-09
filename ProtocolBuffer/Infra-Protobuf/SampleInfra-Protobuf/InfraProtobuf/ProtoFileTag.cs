using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Philips.PmsMR.Protobuf.DataModel
{
    /// <summary>
    /// A generic implemenation for a datamodel that has been defined by a protofile.
    /// </summary>
    /// <remarks>
    /// A sample implementation. Feel free to derive from this class.
    /// </remarks>
    public class ProtoFileTag : IProtoFileTag
    {
        /// <summary>
        /// Converter from known message enumerators to message-id mappings.
        /// </summary>
        public class DefaultNameConverter {

            /// <summary>
            /// Format of the enum name that identifies the message class.
            /// </summary>
            public enum MessageNameFormatType
            {
                /// <summary>
                /// Enumerator name is the class name written in all-caps, prefixed with 'MESSAGE_TYPE_', and with underscore separating the words.
                /// </summary>
                /// <remarks>
                /// For example, MESSAGE_TYPE_THIS_IS_TEST_NAME refers to a message class ThisIsTestName.
                /// </remarks>
                ProtobufStyle,
                /// <summary>
                /// Enumerator name is the class name + postfix 'Id' string.
                /// </summary>
                /// <remarks>
                /// For example, ThisIsTestNameId refers to a message class ThisIsTestName.
                /// </remarks>
                IdPostfixStyle
            }

            /// <summary>
            /// Initializing ctor.
            /// </summary>
            /// <param name="enumeratorType">Enumerator, which name maps to message class names, and whose value maps to the message id of the corresponding class</param>
            /// <param name="enumFormatStyle">Format style for converting enumerator names to class names</param>
            public DefaultNameConverter(Type enumeratorType, MessageNameFormatType enumFormatStyle)
            {
                this.enumFormatStyle = enumFormatStyle;
                var names = Enum.GetNames(enumeratorType);
                var values = Enum.GetValues(enumeratorType);
                for (int index = 0;index < names.Length;++index) {
                    messageToIdMap[ConvertEnumToClass(names[index])] = (int)(values.GetValue(index));
                }
            }
            /// <summary>
            /// Map types to ids
            /// </summary>
            /// <param name="types"></param>
            /// <returns></returns>
            public IDictionary<int, Type> MapTypesToIds(Type[] types)
            {
                var map = new Dictionary<int, Type>();
                foreach (var type in types)
                {
                    int id;
                    if (messageToIdMap.TryGetValue(type.Name, out id))
                    {
                        map.Add(id, type);
                    }
                }
                return map;
            }

            private string ConvertEnumToClass(string enumName)
            {
                switch (enumFormatStyle)
                {
                    case MessageNameFormatType.IdPostfixStyle:
                        if (!enumName.EndsWith(IdPostfix))
                        {
                            throw new ArgumentException("Invalidly formed enum name, postfix is not " + IdPostfix + " in " + enumName, "enumName");
                        }
                        return enumName.Substring(0, enumName.Length - IdPostfix.Length);
                    case MessageNameFormatType.ProtobufStyle:
                    default:
                        throw new NotImplementedException("TODO: " + enumFormatStyle);
                }
            }

            private readonly Dictionary<string, Int32> messageToIdMap = new Dictionary<string, int>();
            private readonly MessageNameFormatType enumFormatStyle;
            private const string IdPostfix = "Id";
        }

        /// <summary>
        /// Maps message types to integer ids.
        /// </summary>
        /// <remarks>
        /// Note that not all types are necessarily mappable to ids. 
        /// E.g., auxiliary structures that are contained in messages.
        /// </remarks>
        /// <param name="types"></param>
        /// <returns></returns>
        public delegate IDictionary<int, Type> MapTypesToIdsFunc(Type[] types);


        /// <summary>
        /// Initializing ctor.
        /// </summary>
        /// <remarks>
        /// This constructor extracts the types from the provided assembly
        /// by filtering the public types with the provided package namespace.
        /// </remarks>
        /// <param name="assembly">Assembly containing generated types</param>
        /// <param name="packageNamespace">Protobuf package-namespace, which is used for automatically detecting the types</param>
        /// <param name="defaultConverter"></param>
        public ProtoFileTag(System.Reflection.Assembly assembly, string packageNamespace, DefaultNameConverter defaultConverter)
            : this(
                assembly, packageNamespace,
                types => defaultConverter.MapTypesToIds(types))
        {
        }

        /// <summary>
        /// Initializing ctor.
        /// </summary>
        /// <remarks>
        /// This constructor extracts the types from the provided assembly
        /// by filtering the public types with the provided package namespace.
        /// </remarks>
        /// <param name="assembly">Assembly, which contains the datamodel types</param>
        /// <param name="packageNamespace">Protobuf package-namespace, which is used for automatically detecting the types</param>
        /// <param name="typeConverter">Type to int conversion mapping</param>
        public ProtoFileTag(System.Reflection.Assembly assembly, string packageNamespace, MapTypesToIdsFunc typeConverter)
        {
            if (String.IsNullOrEmpty(packageNamespace))
            {
                throw new ArgumentException("This constructor expects a package namespace for the protobuf model, now none was given", "packageNamespace");
            }

            types = assembly.GetTypes().Where(
                x => x.IsPublic && 
                     !String.IsNullOrEmpty(x.Namespace) && 
                     x.Namespace.StartsWith(packageNamespace, StringComparison.InvariantCulture)).ToArray();

            idToTypeMap = typeConverter(types);
        }

        #region IProtoFileTag Members
        /// <summary>
        /// Returns primtive types
        /// </summary>
        /// <returns></returns>
        public virtual Type[] GetTypes()
        {
            return types;
        }
        /// <summary>
        /// Returns details about the data model
        /// </summary>
        public virtual DataModelOptionTypes DataModelOptions
        {
            get
            {
                return DataModelOptionTypes.GeneratedFromProtoFile;
            }
        }
        /// <summary>
        /// Maps ids with types.
        /// </summary>
        /// <returns></returns>
        public virtual IDictionary<int, Type> GetIdsAndTypes()
        {
            return idToTypeMap;
        }
        #endregion
        /// <summary>
        /// Array of primtive types.
        /// </summary>
        protected readonly Type[] types;
        /// <summary>
        /// Maps ids with types
        /// </summary>
        protected readonly IDictionary<int, Type> idToTypeMap;
    }
}
