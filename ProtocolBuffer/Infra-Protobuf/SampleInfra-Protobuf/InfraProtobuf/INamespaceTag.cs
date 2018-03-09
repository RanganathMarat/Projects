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

namespace Philips.PmsMR.Protobuf.DataModel
{

    /// <summary>
    /// Identifier for a protobuf data model that can use a namespace for filtering types.
    /// </summary>
    public interface INamespaceTag : IDataModelTag
    {
        /// <summary>
        /// Root namespace for the datamodel.
        /// </summary>
        /// <remarks>
        /// <p>
        ///     If namespace collapsing (globally unique field and message ids) are not used,
        ///     this can be null.
        /// </p>
        /// <p>
        ///     If namespace is provided, it is also used to filter the typelist to silently ignore
        ///     those types that are not under the RootNamespace.
        /// </p>
        /// </remarks>
        string RootNamespace { get; }

        /// <summary>
        /// Namespaces that are collapsed when uniqueness constraints are examined.
        /// </summary>
        /// <remarks>
        /// <para>
        ///     Some applications, e.g., legacy therapy code, require that all ids are unique,
        ///     so that a trivial conversion to the legacy data dictionaries (e.g., UUID-tagged dictionaries)
        ///     can be made. This encompasses both class and field names within given namespaces.
        ///     To enable this kind of global collapsing at all levels of the class hierarchy,
        ///     provide a non-null RootNamespace.
        /// </para>
        /// <para>
        ///     Protobuf itself allows a more efficient use of uniqueness that needs to be enforced
        ///     only for field names within a class, or for wire-protocol message class names, 
        ///     or for class names within polymorphic inheritance chain.
        ///     Wire-protocol uniqueness can be ascertained by providing all the necessary unique types
        ///     to IDataModel.GetTypes-call (regardless of the namespace).
        ///     If RootNamespace is null, this legacy-support value is not used.
        ///     If RootNamespace is non-null, but collapsing is not needed, provide an empty string array.
        /// </para>
        /// </remarks>
        IEnumerable<string> CollapsedNamespaces { get; }

        /// <summary>
        /// Base type for all polymorphic data classes.
        /// </summary>
        /// <remarks>
        /// If polymorphic classes are not used, this can be default(ExtensibleRootInfo).
        /// </remarks>
        ExtensibleRootInfo ExtensibleRootInfo { get; }

    }
}
