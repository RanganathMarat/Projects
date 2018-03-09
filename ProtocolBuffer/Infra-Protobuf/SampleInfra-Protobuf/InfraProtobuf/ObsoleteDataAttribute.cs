#region  Copyright 2014 Koninklijke Philips N.V.
// All rights are reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written permission of the copyright owner.
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Philips.PmsMR.Protobuf
{
    /// <summary>
    /// Flags a class or a field as obsolete - the field id is still reserved, but the field is not guaranteed to contain anything meaningful
    /// and should not be used.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class ObsoleteDataAttribute : Attribute
    {
    }

}
