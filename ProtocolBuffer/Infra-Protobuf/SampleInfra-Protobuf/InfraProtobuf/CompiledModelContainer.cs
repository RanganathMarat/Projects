#region  Copyright 2013 Koninklijke Philips N.V.
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
    /// Container for the Protobuf.NET runtime type model and its exporter.
    /// </summary>
    internal class CompiledModelContainer
    {
        #region Private Fields

        private readonly ProtoBuf.Meta.RuntimeTypeModel model;
        private readonly Exporter exporter;

        #endregion

        /// <summary>
        /// Initializing ctor.
        /// </summary>
        /// <param name="protobufRuntimeTypeModel"></param>
        public CompiledModelContainer(ProtoBuf.Meta.RuntimeTypeModel protobufRuntimeTypeModel) {
            this.model = protobufRuntimeTypeModel;
            exporter = new Exporter(protobufRuntimeTypeModel);
        }

        /// <summary>
        /// Access to the runtime model.
        /// </summary>
        public ProtoBuf.Meta.RuntimeTypeModel RuntimeModel
        {
            get
            {
                return model;
            }
        }

        /// <summary>
        /// Exporter of the runtime model.
        /// </summary>
        public Exporter ModelExporter {
            get
            {
                return exporter;
            }
        }
    }
}
