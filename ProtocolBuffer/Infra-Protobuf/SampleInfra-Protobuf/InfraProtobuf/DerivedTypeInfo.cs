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
    internal class DerivedTypeInfo
    {
        public DerivedTypeInfo(Type derivedType, IEnumerable<Type> derivationChain) {
            DerivedType = derivedType;
            DerivationChain = derivationChain;
        }

        public Type DerivedType { get; private set; }

        /// <summary>
        /// The first item is the root class (ExtensibleRoot or parentless class), the last item is the derived type itself.
        /// </summary>
        public IEnumerable<Type> DerivationChain { get; private set; }
    };
}
