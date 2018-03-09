#region  Copyright 2014 Koninklijke Philips N.V.
// 
// All rights are reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written permission of the copyright owner.
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Philips.PmsMR.ExternalControl.ModelReflection;

namespace Philips.PmsMR.Protobuf.ModelReflection
{

    /// <summary>
    /// Reflector to extract field information from types.
    /// </summary>
    /// <remarks>
    /// Can be used for caching field information, if performance issues arise.
    /// </remarks>
    internal class FieldExtractor
    {
        /// <summary>
        /// Initializing ctor,
        /// </summary>
        /// <param name="info"></param>
        /// <param name="declaredOnly">If set, inherited fields are not extracted</param>
        /// <param name="collapsedNamespacesInUse">If set, use the declaring namespace as the field namespace. If reset, use the full name of the declaring class</param>
        public FieldExtractor(Type info, bool declaredOnly, bool collapsedNamespacesInUse)
        {
            type = info;
            this.declaredOnly = declaredOnly;
            this.collapsedNamespacesInUse = collapsedNamespacesInUse;
        }

        internal class FieldInfo : ICustomAttributeProvider, IEnumProvider
        {

            public FieldInfo(string namespaceOfDeclaringType, string name, Type declaringType, Type actualType, ICustomAttributeProvider attributeProvider)
            {
                NamespaceOfDeclaringType = namespaceOfDeclaringType;
                Name = name;
                DeclaringType = declaringType;
                FieldType = actualType;
                this.attributeProvider = attributeProvider;
            }

            public string NamespaceOfDeclaringType
            {
                get;
                private set;
            }
            public string Name
            {
                get;
                private set;
            }
            public Type DeclaringType
            {
                get;
                private set;
            }

            public Type FieldType
            {
                get;
                private set;
            }

            #region ICustomAttributeProvider
            public virtual object[] GetCustomAttributes(bool inherit)
            {
                return attributeProvider.GetCustomAttributes(inherit);
            }

            public virtual object[] GetCustomAttributes(Type attributeType, bool inherit)
            {
                return attributeProvider.GetCustomAttributes(attributeType, inherit);
            }

            public virtual bool IsDefined(Type attributeType, bool inherit)
            {
                return attributeProvider.IsDefined(attributeType, inherit);
            }
            #endregion

            #region IEnumProvider
            public virtual bool IsEnum
            {
                get { return false; }
            }

            public virtual int EnumValue
            {
                get
                {
                    throw new InvalidOperationException("Class field does not have an enum value: " + Name);
                }
                protected set
                {
                    throw new InvalidOperationException("Class field does not have an enum value: " + Name);
                }
            }
            #endregion IEnumProvider

            private readonly ICustomAttributeProvider attributeProvider;
        }

        internal class EnumFieldInfo : FieldInfo
        {

            public EnumFieldInfo(string namespaceOfDeclaringType, string name, Type declaringType, Type fieldType, int enumValue)
                : base(namespaceOfDeclaringType, name, declaringType, null, null)
            {
                EnumValue = enumValue;
            }

            #region ICustomAttributeProvider
            public override object[] GetCustomAttributes(bool inherit)
            {
                return new object[0];
            }

            public override object[] GetCustomAttributes(Type attributeType, bool inherit)
            {
                return new object[0];
            }

            public override bool IsDefined(Type attributeType, bool inherit)
            {
                return false;
            }
            #endregion

            #region IEnumProvider
            public override bool IsEnum
            {
                get { return true; }
            }

            public override int EnumValue { get; protected set; }
            #endregion IEnumProvider
        }

        public IEnumerable<FieldInfo> Fields
        {
            get
            {
                var reflectionFields = type.GetFields(BindingFlags.Public |
                    (declaredOnly ? BindingFlags.DeclaredOnly : 0) |
                    (type.IsEnum ? BindingFlags.Static : BindingFlags.Instance));
                var reflectionProperties = type.GetProperties(BindingFlags.Public |
                    (declaredOnly ? BindingFlags.DeclaredOnly : 0) |
                    BindingFlags.Instance).Where(x => x.CanRead && x.CanWrite);

                var fields = reflectionFields.Select(
                    x => type.IsEnum ?
                        new EnumFieldInfo(
                            x.DeclaringType.Namespace,
                            x.Name,
                            x.DeclaringType,
                            x.FieldType,
                            (int)x.GetValue(null)) :
                        new FieldInfo(
                            collapsedNamespacesInUse ? x.DeclaringType.Namespace : x.DeclaringType.FullName,
                            x.Name,
                            x.DeclaringType,
                            x.FieldType,
                            x));
                var getSetProperties = reflectionProperties.Select(
                    x => new FieldInfo(
                        collapsedNamespacesInUse ? x.DeclaringType.Namespace : x.DeclaringType.FullName,
                        x.Name,
                        x.DeclaringType,
                        x.PropertyType,
                        x));
                return fields.Concat(getSetProperties);
            }
        }

        private readonly Type type;
        private readonly bool declaredOnly;
        private readonly bool collapsedNamespacesInUse;
    }
}
