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
using System.Reflection;
using Philips.PmsMR.ExternalControl.ModelReflection;
using Philips.PmsMR.Protobuf.DataModel;

namespace Philips.PmsMR.Protobuf.ModelReflection
{
    internal class IdManager
    {
        public IdManager(IDictionary<NamespaceId, IList<Type>> map)
            : this(map, CalculateId)
        {
        }

        internal IdManager(IDictionary<NamespaceId, IList<Type>> namespaceToTypesMap, Func<string, Int32> calculationFunc)
        {
            if (namespaceToTypesMap == null)
            {
                throw new ArgumentNullException("namespaceToTypesMap");
            }
            calculator = calculationFunc;
            map = namespaceToTypesMap;
        }

        /// <summary>
        /// Insert to the datamodel root namespace (actual namespace ignored)
        /// </summary>
        /// <remarks>
        /// Insertion for simple types, e.g., enumerator types and parentless root classes.
        /// </remarks>
        /// <param name="namespaceText">Root namespace, this can be null</param>
        /// <param name="types"></param>
        public void InsertClassEntries(string namespaceText, IEnumerable<Type> types)
        {
            InsertEntries(
                new Dictionary<string, IEnumerable<string>>(),
                types.Select(x => new TypeEntryWrapper(x)),
                x => namespaceText,
                x => new NamespaceId.ClassItem(x.WrapperType), x => x.WrapperType);
        }

        public void InsertClassEntries(IDictionary<string, IEnumerable<string>> collapsedNamespaces, IEnumerable<Type> types, Func<Type, string> namespaceExtractor)
        {
            InsertEntries(
                collapsedNamespaces,
                types.Select(x => new TypeEntryWrapper(x)),
                x => namespaceExtractor(x.WrapperType),
                x => new NamespaceId.ClassItem(x.WrapperType), x => x.WrapperType);
        }

        /// <summary>
        /// Inserts public fields from the given type.
        /// </summary>
        /// <remarks>
        /// If the type is within the given collapsed namespaces (in value-part of the provided map),
        /// it is to be moved to the key-namespace for checking against id conflicts
        /// </remarks>
        /// <param name="collapsedNamespaces"></param>
        /// <param name="declaredOnly">If set, inserts only fields declared in the given type (parent classes include the inherited fields)</param>
        /// <param name="type"></param>
        public void InsertFieldEntries(IDictionary<string, IEnumerable<string>> collapsedNamespaces, bool declaredOnly, Type type)
        {
            var fieldExtractor = new FieldExtractor(type, declaredOnly, collapsedNamespaces.Count > 0);
            InsertEntries(
                collapsedNamespaces,
                fieldExtractor.Fields,
                x => x.NamespaceOfDeclaringType,
                x => new NamespaceId.FieldItem(x, type, x.DeclaringType, x.FieldType, x.IsEnum), x => x.DeclaringType);
        }

        /// <summary>
        /// We generate a pseudorandom number from the given name
        /// </summary>
        /// <param name="nameId"></param>
        /// <returns>A valid google protobuf id</returns>
        public static Int32 CalculateId(string nameId)
        {
            // Name hash can be considered quite random for our purposes. Field names should be descriptive and rather long
            // In the unfortunate case of a clash within a message type, renaming the field slightly should do the trick.
            // We should also comply we good class design principles and keep the classes rather short (<< 16 fields or so)

            // here we use our own function to get Hashcode for string.
            // ExternalControl is built in 32-bit as MR code base. but Hifu built in 64-bit.
            // .Net function string.GetHashCode() returns different hashcodes for 32-bit and 64-bit.
            int hashCode = GetHashCode(nameId);

            // Normalize to positive values
            hashCode &= Int32.MaxValue;

            // Google protobuf limits the allowed range to 1..536870911 (reserved range 19000-19999)
            var val = (hashCode % (MaximumAllowedGoogleProtobufId - 10000)) + 1;
            if (val >= 19000)
            {
                // Skip the reserved range 19000-19999
                val += 10000;
            }

            EnsureValidId(nameId, val);

            return val;
        }

        public static void EnsureValidId(string nameId, int id)
        {
            if (id > MaximumAllowedGoogleProtobufId || id < 1 || (id >= 19000 && id <= 19999))
            {
                throw new ApplicationException("Hash calculation for " + nameId + " produced an invalid result: " + id);
            }
        }

        public void InsertEntries<T>(
            IDictionary<string, IEnumerable<string>> collapsedNamespaces,
            IEnumerable<T> entries,
            Func<T, string> namespaceExtractor,
            Func<T, NamespaceId.UniquedItem> itemExtractor,
            Func<T, Type> typeExtractor) where T : ICustomAttributeProvider, IEnumProvider
        {
            foreach (var entry in entries)
            {
                var namespaceText = namespaceExtractor(entry);
                var uniquedItem = itemExtractor(entry);
                var collapsedToRoots = from pair in collapsedNamespaces
                                       let rootNamespace = pair.Key
                                       where pair.Value.Any(x => namespaceText.StartsWith(x))
                                       select rootNamespace;
                string collapsedToRoot = collapsedToRoots.ElementAtOrDefault(0);
                if (collapsedToRoot != null)
                {
                    // The uniqueness should be examined in the given namespace
                    namespaceText = collapsedToRoot;
                }

                uniquedItem.IsObsolete = entry.GetCustomAttributes(typeof(ObsoleteDataAttribute), true).Any();

                bool skipUniquenessCheck = false;
                int entryId;
                var forcedIds = entry.GetCustomAttributes(typeof(ForcedIdAttribute), true).ToArray();
                if (forcedIds.Length > 0)
                {
                    // Use the hardcoded id
                    if (forcedIds.Length > 1)
                    {
                        throw new ModelViolationException(uniquedItem.FullName, "Multiple forced ids on " + uniquedItem.Name);
                    }
                    entryId = (forcedIds[0] as ForcedIdAttribute).OverridingId;
                }
                else if (entry.IsEnum)
                {
                    // Enums are not limited by allowed values
                    entryId = entry.EnumValue;
                    skipUniquenessCheck = true;
                }
                else
                {
                    // Generate the id from the name
                    entryId = calculator(uniquedItem.Name);
                }

                if (!skipUniquenessCheck)
                {
                    EnsureValidIdRange(uniquedItem.FullName, entryId);
                }
                var namespaceId = new NamespaceId(namespaceText, entryId, uniquedItem);
                var type = typeExtractor(entry);

                IList<Type> alreadyInsertedTypes;
                if (!skipUniquenessCheck && map.TryGetValue(namespaceId, out alreadyInsertedTypes))
                {
                    var alreadyInserted = map.Keys.First(x => x.Equals(namespaceId));
                    var alreadyInsertedField = alreadyInserted.Item as NamespaceId.FieldItem;
                    var newField = namespaceId.Item as NamespaceId.FieldItem;
                    if (alreadyInsertedField != null && newField != null &&
                        alreadyInsertedField.DeclaringType == newField.DeclaringType)
                    {
                        // In the case of inheritance of parentless class, we can just add this new usage scenario
                        // to the already inserted field.
                        alreadyInsertedTypes.Add(((NamespaceId.FieldItem)namespaceId.Item).UsingType);
                    }
                    else
                    {
                        var oldKey = map.Keys.First(x => x.Equals(namespaceId));
                        throw new ClashException(uniquedItem.FullName, oldKey.Item.FullName, namespaceId.Id);
                    }
                }
                else
                {
                    try
                    {
                        map.Add(namespaceId, new List<Type> { type });
                    }
                    catch (ArgumentException)
                    {
                        var builder = new StringBuilder();
                        IList<Type> types;
                        if (map.TryGetValue(namespaceId, out types))
                        {
                            foreach (var existingType in types)
                            {
                                builder.AppendLine(existingType.FullName);
                            }
                        }
                        throw new ModelViolationException(type.FullName, "Cannot add type " + type.FullName + " to map, namespace (" + namespaceId + ") has already been added with types - " + builder);
                    }
                }
            }
        }

        private void EnsureValidIdRange(string fullName, int protobufIntId)
        {
            if (protobufIntId < 1)
            {
                throw new ModelViolationException(fullName, "Invalid protobuf id, value cannot be below one: " + protobufIntId);
            }
            if (protobufIntId >= 19000 && protobufIntId <= 19999)
            {
                throw new ModelViolationException(fullName, "Invalid protobuf id, value cannot be in google-reserved range (19000-19999): " + protobufIntId);
            }
            if (protobufIntId > 536870911)
            {
                throw new ModelViolationException(fullName, "Invalid protobuf id, value cannot be larger than 536870911: " + protobufIntId);
            }
        }

        /// <summary>
        /// This is our private function to return hashcode for any string. We cannot use .net's string.GetHashCode function because
        /// From MSDN,The value returned by GetHashCode is platform-dependent. It differs on the 32-bit and 64-bit versions of the .NET Framework,
        /// and is not guaranteed not to change over time.
        /// </summary>
        private static int GetHashCode(string nameId)
        {
            unchecked
            {
                if (nameId == null)
                    return 0;
                var length = nameId.Length;
                int hash = length;
                for (int i = 0; i != length; ++i)
                    hash = (hash ^ nameId[i]) * 16777619;
                return hash;
            }
        }

        private class TypeEntryWrapper : ICustomAttributeProvider, IEnumProvider
        {
            public TypeEntryWrapper(Type type)
            {
                WrapperType = type;
            }

            public object[] GetCustomAttributes(bool inherit)
            {
                return WrapperType.GetCustomAttributes(inherit);
            }

            public object[] GetCustomAttributes(Type attributeType, bool inherit)
            {
                return WrapperType.GetCustomAttributes(attributeType, inherit);
            }

            public bool IsDefined(Type attributeType, bool inherit)
            {
                return WrapperType.IsDefined(attributeType, inherit);
            }

            public bool IsEnum
            {
                get { return false; }
            }

            public int EnumValue
            {
                get { throw new InvalidOperationException("Enum value is not provided by class type"); }
            }

            public Type WrapperType { get; private set; }
        }

        private readonly Func<string, Int32> calculator;
        private readonly IDictionary<NamespaceId, IList<Type>> map;
        private const int MaximumAllowedGoogleProtobufId = 536870911;
    }
}
