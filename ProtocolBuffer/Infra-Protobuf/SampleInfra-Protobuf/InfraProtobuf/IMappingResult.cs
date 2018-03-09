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
    /// Mapping between id integers and types.
    /// </summary>
    public interface IMappingResult
    {
        /// <summary>
        /// integer id-to-type map.
        /// </summary>
        /// <remarks>Null if mapping failed</remarks>
        IDictionary<Int32, Type> IdToTypeMap { get; }

        /// <summary>
        /// Type-to-integer-id map.
        /// </summary>
        /// <remarks>Null if mapping failed</remarks>
        IDictionary<Type, Int32> TypeToIntMap { get; }

        /// <summary>
        /// All types in the data model.
        /// </summary>
        /// <remarks>
        /// Note that some of the auxiliary types may not have an id.
        /// Therefore the types are not available in conversion maps.
        /// </remarks>
        /// <returns></returns>
        Type[] GetTypes();

        /// <summary>
        /// Namespace scope for reflection task.
        /// </summary>
        /// <remarks>Null if mapping failed</remarks>
        DataModel.IDataModelTag DataModelTag { get; }
    }
}
