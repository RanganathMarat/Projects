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
    class ProtoFileMappingResult : IMappingResult
    {
        public ProtoFileMappingResult(DataModel.IProtoFileTag tag)
        {
            DataModelTag = tag;
            var typeToId = new Dictionary<Type, int>();
            var idToType = new Dictionary<int, Type>();
            foreach (var pair in tag.GetIdsAndTypes())
            {
                typeToId.Add(pair.Value, pair.Key);
                idToType.Add(pair.Key, pair.Value);
            }
            TypeToIntMap = typeToId;
            IdToTypeMap = idToType;

            // For proto-file results, the input types are also the output types.
            // It is the responsibility of the proto file provider to give us all the necessary types.
            types = tag.GetTypes();
        }

        #region IMappingResult
        public IDictionary<Int32, Type> IdToTypeMap { get; private set; }

        public IDictionary<Type, Int32> TypeToIntMap { get; private set; }

        public Type[] GetTypes()
        {
            return types;
        }

        public DataModel.IDataModelTag DataModelTag { get; private set; }
        #endregion

        private readonly Type[] types;
    }

}
