#region  Copyright 2015 Koninklijke Philips N.V.
// 
// All rights are reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written permission of the copyright owner.
#endregion

using System;
using System.Linq;
using System.Collections.Generic;

namespace Philips.PmsMR.Protobuf.DataModel
{

	/// <summary>
    /// A generic implemenation for a datamodel that has been defined within a specific namespace in a single assembly.
    /// </summary>
    /// <remarks>
    /// A sample implementation. Feel free to derive from this class.
    /// By default, the implementation will retrieve all types within the containing namespace
    /// of the inheriting class.
    /// </remarks>
    public abstract class NamespaceTag : INamespaceTag
    {

        #region INamespaceTag Methods
		/// <summary>
        /// INamespaceTag Impl.
		/// </summary>
        public virtual string RootNamespace
        {
            get { return GetType().Namespace; }
        }

		/// <summary>
        /// INamespaceTag Impl.
		/// </summary>
        public virtual System.Collections.Generic.IEnumerable<string> CollapsedNamespaces
        {
            get { return new string[0]; }
        }

		/// <summary>
        /// INamespaceTag Impl.
		/// </summary>
        public virtual ExtensibleRootInfo ExtensibleRootInfo
        {
            get { return default(ExtensibleRootInfo); }
        }

		/// <summary>
        /// INamespaceTag Impl.
		/// </summary>
		/// <returns></returns>
        public virtual System.Type[] GetTypes()
        {
            return GetPublicTypesFromNamespace(GetType().Assembly, RootNamespace, new HashSet<Type> { GetType() });
        }

		/// <summary>
        /// INamespaceTag Impl.
		/// </summary>
        public virtual DataModelOptionTypes DataModelOptions
        {
            get { return DataModelOptionTypes.None; }
        }
        #endregion

		/// <summary>
		/// Auxiliary reflection support.
		/// </summary>
		/// <param name="assembly"></param>
		/// <param name="containingNamespace"></param>
        /// <param name="excludedTypes">Public types in the containing namespace that are not to be included.</param>
		/// <returns></returns>
        public static System.Type[] GetPublicTypesFromNamespace(System.Reflection.Assembly assembly, string containingNamespace, HashSet<Type> excludedTypes)
        {
            return assembly.GetTypes().Where(x => x.Namespace.StartsWith(containingNamespace) && x.IsPublic && !excludedTypes.Contains(x)).ToArray();
        }
    }

}