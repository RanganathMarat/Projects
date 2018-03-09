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

namespace Philips.PmsMR.Protobuf.Serialization {

    /// <summary>
    /// Serialization of .NET classes into protobuf bytes - deserialization from bytes to .NET class instances.
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// Converts a datamodel object into bytes and stores them in the provided stream.
        /// </summary>
        /// <param name="dataModelObject"></param>
        /// <param name="stream"></param>
        void Serialize(object dataModelObject, System.IO.Stream stream);

        /// <summary>
        /// Deserializes a datamodel object from the given stream.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        object Deserialize(System.IO.Stream stream);
    }
}
