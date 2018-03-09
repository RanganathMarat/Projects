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
    /// Factory is used for creating DataModel data instances and to map types to integer ids.
    /// </summary>
    /// <remarks>
    /// Once an object is created, it can be filled with valid data
    /// and serialized.
    /// </remarks>
    public class Factory
    {
        /// <summary>
        /// Initializing ctor
        /// </summary>
        /// <param name="mappingResult"></param>
        public Factory(IMappingResult mappingResult)
        {
            genericMappingResult = mappingResult;
            this.reflectedMappingResult = mappingResult as ReflectedMappingResult;
            namespaceTag = mappingResult.DataModelTag as DataModel.INamespaceTag;
        }

        /// <summary>
        /// Creator for data model objects.
        /// </summary>
        /// <remarks>
        /// Ensures that the type id fields are automatically filled for transmission.
        /// </remarks>
        /// <typeparam name="T">DataModel type</typeparam>
        /// <returns>Newly created data instance with ActualTypeId set</returns>
        public virtual T Create<T>() where T: class, new()
        {
            return CreateObject(() => new T(), typeof(T));
        }

        /// <summary>
        /// Creator for data model objects.
        /// </summary>
        /// <remarks>
        /// Ensures that the type id fields are automatically filled for transmission.
        /// </remarks>
        /// <param name="type">data model type</param>
        /// <returns></returns>
        public virtual object Create(Type type)
        {
            return CreateObject(() => Activator.CreateInstance(type), type);
        }

        private T CreateObject<T>(Func<T> objectCreator, Type dataModelType) {
            var rootNamespace = namespaceTag.RootNamespace;
            if (!dataModelType.Namespace.StartsWith(rootNamespace)) {
                throw new ArgumentException("Wrong namespace tag provided, " + dataModelType.FullName + " is not within " + rootNamespace);
            }
            var newObj = objectCreator();
            KeyValuePair<int, ReflectedMappingResult.IdentificationType> pair;
            if (!reflectedMappingResult.TypeToIdMap.TryGetValue(dataModelType, out pair))
            {
                throw new ArgumentException("Wrong data model type provided, type not found in mapping results: " + dataModelType);
            }
            if (pair.Value == ReflectedMappingResult.IdentificationType.RequiresIdField)
            {
                namespaceTag.ExtensibleRootInfo.ActualTypeIdField.SetValue(newObj, pair.Key);
            }
            return newObj;
        }

        /// <summary>
        /// Protobuf integer id for the given data model type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Int32 GetTypeId(Type type)
        {            
            if (reflectedMappingResult == null) {
                return genericMappingResult.TypeToIntMap[type];
            } else {
                return reflectedMappingResult.TypeToIdMap[type].Key;
            }
        }

        private readonly IMappingResult genericMappingResult;
        private readonly ReflectedMappingResult reflectedMappingResult;
        private readonly DataModel.INamespaceTag namespaceTag;
    }
}
