using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Philips.PmsMR.Protobuf.DataModel
{
    /// <summary>
    /// Identifier for a data model that is generated from a .proto file.
    /// </summary>
    public interface IProtoFileTag : IDataModelTag
    {
        /// <summary>
        /// Contained protobuf-convertible types.
        /// </summary>
        /// <remarks>
        /// In addition to message types, the message ids are needed to correctly identify messages
        /// when the messages are being serialized or deserialized.
        /// </remarks>
        /// <returns>Array of message id and type pairs, where types that are valid protobuf message classes.</returns>
        IDictionary<int, Type> GetIdsAndTypes();
    }
}
