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

namespace Philips.PmsMR.Protobuf.ModelReflection
{
    /// <summary>
    /// TypePopulator fills in the namespaced integer id - type mappings
    /// </summary>
    internal class TypePopulator
    {
        public TypePopulator(DataModel.INamespaceTag namespaceTag)
        {
            this.namespaceTag = namespaceTag;
            collapsedNamespaces = new Dictionary<string, IEnumerable<string>>();
            if (!String.IsNullOrEmpty(namespaceTag.RootNamespace) && namespaceTag.CollapsedNamespaces != null && namespaceTag.CollapsedNamespaces.Any())
            {
                collapsedNamespaces[namespaceTag.RootNamespace] = namespaceTag.CollapsedNamespaces;
            }
        }

        public void CreateMapping(Type[] types, IDictionary<NamespaceId, IList<Type>> map, IDictionary<Type, IEnumerable<Type>> derivedChainMap)
        {
            if ((namespaceTag.DataModelOptions & Philips.PmsMR.Protobuf.DataModel.DataModelOptionTypes.UsesPolymorphicMessages) != 0)
            {
                ValidateExtensibleRootInfo(namespaceTag.ExtensibleRootInfo);
            }

            var modelNamespace = namespaceTag.RootNamespace;
            var extensibleRoot = namespaceTag.ExtensibleRootInfo.ExtensibleRoot;
            var rootDetector = new RootClassDetector(types, modelNamespace, extensibleRoot);

            var parentlessRoots = rootDetector.ParentlessRoots;
            var idManager = new IdManager(map);

            ValidateEnums(rootDetector.EnumRoots);

            idManager.InsertClassEntries(modelNamespace, rootDetector.EnumRoots);

            // All parentless classes go to the root model namespace, we need to tell them apart in the wire protocol
            // (ids must be unique).
            idManager.InsertClassEntries(modelNamespace, parentlessRoots);

            // All derived types need to be unique in their hierarchy.
            idManager.InsertClassEntries(collapsedNamespaces, rootDetector.DerivedTypes.Select(x => x.DerivedType), rootDetector.GetDerivedNamespace);

            foreach (var type in rootDetector.EnumRoots)
            {
                idManager.InsertFieldEntries(collapsedNamespaces, false, type);
            }
            foreach (var type in parentlessRoots)
            {
                // Fields may, or may not, need to be unique within a datamodel namespace -
                // Collapsed namespaces (dictionary values) identify those types that need to be unique within the given datamodel namespace (dictionary key)
                idManager.InsertFieldEntries(collapsedNamespaces, false, type);
            }
            foreach (var derivedTypeInfo in rootDetector.DerivedTypes)
            {
                var derivationChain = derivedTypeInfo.DerivationChain;
                derivedChainMap[derivedTypeInfo.DerivedType] = derivationChain;
                // If we use hierarchical message polymorphism for classes derived from extensible root in this data model:
                // For derived types, we define only those fields that have been declared in the derived type -
                // Protobuf.NET does not replicate the parent fields into the derived data during serialization.
                bool declaredOnly = derivationChain.First() == extensibleRoot ?
                    (namespaceTag.DataModelOptions & DataModel.DataModelOptionTypes.UsesPolymorphicMessages) != 0 :
                    false;
                idManager.InsertFieldEntries(collapsedNamespaces, declaredOnly, derivedTypeInfo.DerivedType);
            }
        }

        /// <summary>
        /// Ensure that all enum key strings and values are unique.
        /// </summary>
        /// <remarks>
        /// C++ code cannot handle enums that only differ by the containing enum name (and/or value).
        /// Also reassignment of the same integer to two different keys can be unintentional and should be disallowed.
        /// </remarks>
        /// <param name="list"></param>
        internal static void ValidateEnums(IList<Type> list)
        {
            var usedKeys = new Dictionary<string, Type>(StringComparer.InvariantCulture);
            foreach (var enumType in list) 
            {
                if (Enum.GetUnderlyingType(enumType) != typeof(Int32))
                {
                    throw new ModelViolationException(enumType, "Underlying enum type is not a signed integer!");
                }
                var values = Enum.GetValues(enumType).Cast<int>().ToArray();
                var names = Enum.GetNames(enumType);
                if (values.Distinct().Count() != values.Length) 
                {
                    HashSet<int> usedValues = new HashSet<int>();
                    foreach (var name in names) 
                    {
                        var value = (int)Enum.Parse(enumType, name);
                        if (usedValues.Contains(value))
                        {
                            throw new ModelViolationException(enumType, "Same integer value used more than once: " + name);
                        }
                        else
                        {
                            usedValues.Add(value);
                        }
                    }
                    throw new ModelViolationException(enumType, "Unexpected number of non-distinct values");
                }
           
                foreach (var enumKey in names) {
                    if (usedKeys.ContainsKey(enumKey))
                    {
                        throw new ModelViolationException(enumType, "Ambiguous key '" + enumKey + "' found in " + enumType.Name + ", also defined in " + usedKeys[enumKey].FullName);
                    }
                    usedKeys[enumKey] = enumType;
                }
            }
        }

        private void ValidateExtensibleRootInfo(DataModel.ExtensibleRootInfo info)
        {
            if (!info.ExtensibleRoot.IsPublic)
            {
                throw new ModelViolationException(info.ExtensibleRoot, "Extensible root is not public");
            }
            if (info.ActualTypeIdField.FieldType != typeof(Int32))
            {
                throw new ModelViolationException(info.ExtensibleRoot, "ActualTypeIdField is not an integer: " + info.ActualTypeIdField.Name);
            }
            if (!info.ActualTypeIdField.IsPublic)
            {
                throw new ModelViolationException(info.ExtensibleRoot, "ActualTypeIdField is not public: " + info.ActualTypeIdField.Name);
            }
        }

        private readonly DataModel.INamespaceTag namespaceTag;

        private readonly Dictionary<string, IEnumerable<string>> collapsedNamespaces;
    }
}
