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
    /// Mapping results from model creation.
    /// </summary>
    /// <remarks>
    /// When a C# classes are reflected into a data model,
    /// the association between integer ids and types is made.
    /// The results can then be accessed via this class.
    /// </remarks>
    class ReflectedMappingResult : IMappingResult
    {
        /// <summary>
        /// Default ctor.
        /// </summary>
        public ReflectedMappingResult() { }

        /// <summary>
        /// Initializing ctor.
        /// </summary>
        /// <param name="typeMap"></param>
        /// <param name="derivationChain"></param>
        /// <param name="tag">Identifier for the data model</param>
        public ReflectedMappingResult(
            IDictionary<NamespaceId, IList<Type>> typeMap,
            IDictionary<Type, IEnumerable<Type>> derivationChain,
            DataModel.INamespaceTag tag)
        {
            TypeMap = typeMap;
            DerivationChain = derivationChain;
            DataModelTag = tag;
            namespaceTag = tag as DataModel.INamespaceTag;

            var allClassTypes = from pair in typeMap
                                let namespaceId = pair.Key
                                where namespaceId.Item is NamespaceId.ClassItem
                                let classType = pair.Value.Single()
                                select new
                                {
                                    Id = namespaceId.Id,
                                    ClassType = classType,
                                    RequiresExplicitType =
                                        derivationChain.ContainsKey(classType) &&
                                        derivationChain[classType].First() == tag.ExtensibleRootInfo.ExtensibleRoot
                                };

            TypeToIdMap = allClassTypes.ToDictionary(x => x.ClassType,
                y => new KeyValuePair<Int32, IdentificationType>(y.Id, y.RequiresExplicitType ? IdentificationType.RequiresIdField : IdentificationType.ImplicitlyIdentified));
            IdToTypeMap = allClassTypes.ToDictionary(x => x.Id, y => y.ClassType);

            TypeIdFieldInfo = FindTypeIdFieldInfo();
        }

        #region IMapping
        /// <summary>
        /// Integer id-to-reflected type map.
        /// </summary>
        public IDictionary<Int32, Type> IdToTypeMap { get; private set; }

        /// <summary>
        /// Type-to-integer-id map.
        /// </summary>
        public IDictionary<Type, Int32> TypeToIntMap
        {
            get { 
                var typeToIdMap = TypeToIdMap;
                if (typeToIdMap == null)
                {
                    return null;
                }
                return TypeToIdMap.ToDictionary(x => x.Key, y => y.Value.Key); 
            }
        }

        public Type[] GetTypes()
        {
            var typeMap = TypeMap;
            if (typeMap == null)
            {
                return null;
            }
            return typeMap.SelectMany(x => x.Value).Distinct().ToArray();
        }

        /// <summary>
        /// Namespace scope for reflection task.
        /// </summary>
        public DataModel.IDataModelTag DataModelTag { get; private set; }
        #endregion

        /// <summary>
        /// Identification needed for a reflected type.
        /// </summary>
        public enum IdentificationType
        {
            /// <summary>
            /// Either by wire-protocol or by not being extensible.
            /// </summary>
            ImplicitlyIdentified,
            /// <summary>
            /// An explicit id field that needs to be filled.
            /// </summary>
            RequiresIdField
        }

        /// <summary>
        /// Namespace-to-reflected types map.
        /// </summary>
        public IDictionary<NamespaceId, IList<Type>> TypeMap { get; private set; }

        /// <summary>
        /// Reflected type-to-integer id map.
        /// </summary>
        public IDictionary<Type, KeyValuePair<Int32, IdentificationType>> TypeToIdMap { get; private set; }

        /// <summary>
        /// For extendable data types, this provides the inheritance chain.
        /// </summary>
        /// <remarks>
        /// The first item in the enumeration is the base class of the whole chain.
        /// The last item is the type itself.
        /// </remarks>
        public IDictionary<Type, IEnumerable<Type>> DerivationChain { get; private set; }

        /// <summary>
        /// Field identifier for typeid-Field in all derived data classes.
        /// </summary>
        public KeyValuePair<Int32, string>? TypeIdFieldInfo { get; private set; }

        private KeyValuePair<Int32, string>? FindTypeIdFieldInfo()
        {
            if (namespaceTag != null & (namespaceTag.DataModelOptions & Philips.PmsMR.Protobuf.DataModel.DataModelOptionTypes.UsesPolymorphicMessages) != 0)
            {
                var rootType = namespaceTag.ExtensibleRootInfo.ExtensibleRoot;
                var idFieldName = namespaceTag.ExtensibleRootInfo.ActualTypeIdField.Name;

                var all = from pair in TypeMap
                          let namespaceId = pair.Key
                          where pair.Value.Contains(rootType) && namespaceId.Item.Name == idFieldName
                          select namespaceId.Id;
                return new KeyValuePair<int, string>(all.First(), idFieldName);
            }
            return null;
        }

        DataModel.INamespaceTag namespaceTag;
    }
}
