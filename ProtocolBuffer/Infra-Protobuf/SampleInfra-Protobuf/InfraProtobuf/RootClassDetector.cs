#region  Copyright 2014 Koninklijke Philips N.V.
// 
// All rights are reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written permission of the copyright owner.
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Philips.PmsMR.Platform.Logging;

namespace Philips.PmsMR.Protobuf.ModelReflection
{
    internal class RootClassDetector
    {
        public RootClassDetector(Type[] allTypes, string dataModelNamespace, Type extensibleRoot)
        {
            ParentlessRoots = new List<Type>();
            DerivedTypes = new List<DerivedTypeInfo>();
            EnumRoots = new List<Type>();
            this.extensibleRoot = extensibleRoot;
            FilterClasses(allTypes, dataModelNamespace);
        }

        /// <summary>
        /// Classes, which do not derive from extensible root classes.
        /// </summary>
        /// <remarks>
        /// These classes and their children cannot be polymorphic.
        /// For example, message envelopes are such classes.
        /// Children get the fields from their ancestor classes
        /// and inheritance is not used.
        /// Note that a class that is parentless or inherits from
        /// a parentless root can still contain data classes that
        /// are polymorphic.
        /// </remarks>
        public IList<Type> ParentlessRoots
        {
            get;
            private set;
        }

        /// <summary>
        /// Classes, which derive directly, or indirectly, from extensible roots
        /// </summary>
        public IList<DerivedTypeInfo> DerivedTypes
        {
            get;
            private set;
        }

        /// <summary>
        /// Enums
        /// </summary>
        public IList<Type> EnumRoots
        {
            get;
            private set;
        }

        /// <summary>
        /// Tries to find the base class that inherits from the root extension.
        /// </summary>
        /// <param name="derivedType"></param>
        /// <returns>the full namespace of the base class, postfixed with the base class name</returns>
        public string GetDerivedNamespace(Type derivedType)
        {
            string extensibleRootNamespace = extensibleRoot.Namespace;
            string rootNamespace = null;
            var tmp = derivedType;
            while (tmp != typeof(object) && tmp != null)
            {
                rootNamespace = tmp.FullName;
                if (tmp.BaseType == extensibleRoot)
                {
                    break;
                }
                tmp = tmp.BaseType;
            }

            if (!String.IsNullOrEmpty(rootNamespace) && !String.IsNullOrEmpty(extensibleRootNamespace))
            {
                if (rootNamespace.StartsWith(extensibleRootNamespace, StringComparison.InvariantCulture))
                {
                    return rootNamespace;
                }
            }

            throw new ArgumentException("Invalid argument provided, type " + derivedType + " is not inherited/namespaced from " + extensibleRoot);
        }

        /// <summary>
        /// Filter out all non-root classes from the given datamodel namespace.
        /// </summary>
        /// <remarks>
        /// Root class is a class that does not have a parent class, or is a base class and inherits from Protobuf.Extensible
        /// </remarks>
        /// <param name="allTypes"></param>
        /// <param name="dataModelNamespace"></param>
        private void FilterClasses(
            IEnumerable<Type> allTypes,
            string dataModelNamespace)
        {
            bool usesDatamodelNamespace = !String.IsNullOrEmpty(dataModelNamespace);
            if (!usesDatamodelNamespace)
            {
                trace.Info("Root Class Detector will not use a datamodel namespace");
            }

            var parentlessRoots = new Dictionary<Type, bool>();
            foreach (var type in allTypes)
            {
                if (type == null)
                {
                    throw new ArgumentException("Null type provided in the type array");
                }

                if (!type.IsClass && !type.IsEnum)
                {
                    trace.Info("Ignoring a non-class {0}", type.FullName);
                    continue;
                }

                var namespaceText = type.Namespace;
                if (namespaceText == null)
                {
                    // Uninteresting, type does not even have a namespace!
                    continue;
                }

                if (usesDatamodelNamespace && !namespaceText.StartsWith(dataModelNamespace, StringComparison.InvariantCulture))
                {
                    continue;
                }

                if (type.IsClass && (!type.IsPublic || type.IsAbstract))
                {
                    continue;
                }

                if (type.IsEnum)
                {
                    EnumRoots.Add(type);
                    continue;
                }

                var derivationChain = new Stack<Type>();
                var tmpType = type;
                while (tmpType != typeof(object))
                {
                    derivationChain.Push(tmpType);
                    if (tmpType.BaseType == typeof(object) || tmpType == extensibleRoot)
                    {
                        parentlessRoots[tmpType] = true;
                        break;
                    }
                    tmpType = tmpType.BaseType;
                }

                if (!parentlessRoots.ContainsKey(type))
                {
                    DerivedTypes.Add(new DerivedTypeInfo(type, derivationChain));
                }
            }
            ParentlessRoots = parentlessRoots.Keys.ToList();
        }

        private readonly Type extensibleRoot;
        private static readonly TraceMessage trace = new TraceMessage("ExternalControl", "ClientManager");

    }
}
