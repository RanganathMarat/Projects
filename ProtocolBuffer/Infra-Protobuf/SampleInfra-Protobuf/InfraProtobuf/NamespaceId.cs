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
    /// Namespace identifier.
    /// </summary>
    /// <remarks>
    /// Reflected types can be placed under multiple namespace ids.
    /// The namespace ids determine the uniqueness constraints for the integer ids in the types.
    /// </remarks>
    public class NamespaceId : IEquatable<NamespaceId>
    {
        /// <summary>
        /// Base class for a unique item (field or class name).
        /// </summary>
        public abstract class UniquedItem
        {
            /// <summary>
            /// Initializing ctor.
            /// </summary>
            /// <param name="name"></param>
            /// <param name="fullName"></param>
            protected UniquedItem(string name, string fullName)
            {
                Name = name;
                FullName = fullName;
            }

            /// <summary>
            /// Item name without actual namespace
            /// </summary>
            public string Name { get; private set; }

            /// <summary>
            /// Full name, includes actual namespace
            /// </summary>
            public string FullName { get; private set; }

            /// <summary>
            /// Identifies the item as obsolete.
            /// </summary>
            /// <remarks>Obsolete items shall not be used anymore</remarks>
            public bool IsObsolete { get; set; }
        }

        internal class ClassItem : UniquedItem
        {
            public ClassItem(Type type) : base(type.Name, type.FullName) { }
        }

        internal class FieldItem : UniquedItem {
            public FieldItem(FieldExtractor.FieldInfo info, Type usingType, Type declaringType, Type fieldType, bool isEnumField)
                : base(info.Name, usingType.FullName + "." + info.Name) {
                UsingType = usingType;
                DeclaringType = declaringType;
                FieldType = fieldType;
                IsEnum = isEnumField;
                }

            public Type UsingType { get; private set; }

            public Type DeclaringType { get; private set; }

            public Type FieldType { get; private set; }

            public bool IsEnum { get; private set; }
        }

        /// <summary>
        /// Uniqueness of the namespaced id is checked against the namespace and id, fullItemName is ignored.
        /// </summary>
        /// <param name="namespaceString"></param>
        /// <param name="item">Item identified by the namespace and id combo</param>
        /// <param name="id"></param>
        internal NamespaceId(string namespaceString, Int32 id, UniquedItem item)
        {
            Namespace = namespaceString;
            Item = item;
            Id = id;

            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
        }

        /// <summary>
        /// Get/Set property of the namespace
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// Item that has been identified with the namespace+id combo
        /// </summary>
        public UniquedItem Item { get; set; }

        /// <summary>
        /// Integer id of the namespace.
        /// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// Overriding ToString()
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Namespace-id for " + Namespace + ", id is " + Id + " for uniqued " + (Item != null ? Item.Name : "<null>");
        }

        /// <summary>
        /// Equality check.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var typed = obj as NamespaceId;
            if (typed == null)
            {
                return false;
            }
            return Equals(typed);
        }

        /// <summary>
        /// Hash generation.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        #region IEquatable<NamespaceId> Members

        /// <summary>
        /// Two namespace ids are equal if their namespace names are equal and integer ids are equal.
        /// The name is not used in comparison.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(NamespaceId other)
        {
            // For enum fields, we just need to test global name clashes (In C++, enums are in the global namespace)
            var fieldItem = Item as FieldItem;
            if (fieldItem != null && fieldItem.IsEnum)
            {
                var otherItem = other.Item;
                if (otherItem != null)
                {
                    return String.Compare(fieldItem.Name, otherItem.Name, StringComparison.InvariantCulture) == 0;
                }
                return false;
            }

            return 
                String.Compare(Namespace, other.Namespace, StringComparison.InvariantCulture) == 0 &&
                Id == other.Id;

        }

        #endregion
    }
}
