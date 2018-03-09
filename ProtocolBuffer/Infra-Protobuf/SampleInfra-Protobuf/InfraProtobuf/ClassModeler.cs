#region  Copyright 2015 Koninklijke Philips N.V.
// 
// All rights are reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written permission of the copyright owner.
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Reflection;

namespace Philips.PmsMR.Protobuf.ModelReflection
{
    internal class ClassModeler
    {
        public ClassModeler(ProtoBuf.Meta.RuntimeTypeModel model, ReflectedMappingResult typeMapperResult)
        {
            this.model = model;
            this.typeMapperResult = typeMapperResult;
        }

        public ProtoBuf.Meta.MetaType AddToModel(Type netType, IList<KeyValuePair<Int32, NamespaceId.FieldItem>> fieldItems)
        {
            try
            {
                var type = model.Add(netType, false);

                MethodInfo beforeSerialization = null;
                MethodInfo afterSerialization = null;
                MethodInfo beforeDeserialization = null;
                MethodInfo afterDeserialization = null;
                foreach (var method in netType.GetMethods())
                {
                    beforeSerialization = method.IsDefined(typeof(OnSerializingAttribute), false) ? method : beforeSerialization;
                    afterSerialization = method.IsDefined(typeof(OnSerializedAttribute), false) ? method : afterSerialization;
                    beforeDeserialization = method.IsDefined(typeof(OnDeserializingAttribute), false) ? method : beforeDeserialization;
                    afterDeserialization = method.IsDefined(typeof(OnDeserializedAttribute), false) ? method : afterDeserialization;
                }
                if (beforeSerialization != null || afterSerialization != null || beforeDeserialization != null ||
                    afterDeserialization != null)
                {
                    type.SetCallbacks(beforeSerialization, afterSerialization, beforeDeserialization, afterDeserialization);
                }

                foreach (var pair in fieldItems)
                {
                    var fieldItem = pair.Value;
                    var valueMember = type.AddField(pair.Key, fieldItem.Name);
                    valueMember.IsPacked = IsTypePackingNeeded(fieldItem.FieldType);
                }

                return type;

            }
            catch (ArgumentException e)
            {
                throw new ArgumentException("Failed to add to " + netType.FullName, e);
            }
        }

        bool IsTypePackingNeeded(Type type)
        {
            if (type != null && type.IsArray)
            {
                return packedTypes.Contains(type.GetElementType());
            }
            return false;
        }

        private readonly ProtoBuf.Meta.RuntimeTypeModel model;
        private readonly ReflectedMappingResult typeMapperResult;
        private static readonly HashSet<Type> packedTypes = new HashSet<Type>
        {
            typeof(Byte),
            typeof(Int16),
            typeof(UInt16),
            typeof(short),
            typeof(ushort),
            typeof(Int32),
            typeof(Int64),
            typeof(UInt32),
            typeof(UInt64),
            typeof(float),
            typeof(double),
        };
    }
}
