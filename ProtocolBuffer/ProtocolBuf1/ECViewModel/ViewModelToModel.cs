#region Copyright Koninklijke Philips Electronics N.V. 2014
//
// All rights are reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
// FILENAME: ViewModelToModel.cs
//
#endregion

#region system namespaces
using System;
using ProtoBuf;

#endregion

namespace ProtocolBuf12.ECViewModel
{
    /// <summary>
    /// Custom attribute class to define view model to model mapping
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Property,AllowMultiple=true)]
    public class ViewModelToModel : System.Attribute
    {
        private string modelname;
        /// <summary>
        /// Represents the Model Name that view model maps to
        /// </summary>
        public string Modelname
        {
            get { return modelname; }
            set { modelname = value; }
        }
        private Type knownType;
        /// <summary>
        /// 
        /// </summary>
        public Type KnownType
        {
            get { return knownType; }
            set { knownType = value; }
        }

        private string propertyName;
        /// <summary>
        /// Represents the Property name of the model that view model  property maps to
        /// </summary>
        public string PropertyName
        {
            get { return propertyName; }
            set { propertyName = value; }
        }
        private string convertorName;
        /// <summary>
        /// Indicates the type of convertor to be used for mapping.
        /// </summary>
        public string ConvertorName
        {
            get { return convertorName; }
            set { convertorName = value; }
        }
        /// <summary>
        /// DEfault constructor
        /// </summary>
        /// <param name="name"></param>
        public ViewModelToModel(string name)
        {
            this.modelname = name;
            propertyName = string.Empty;
        }
        /// <summary>
        /// DEfault constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="propname"></param>
        public ViewModelToModel(string name, string propname)
        {
            this.modelname = name;
            propertyName = propname;
        }
       
        /// <summary>
        /// Intializes the model to view model attribute to use convertor as well.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="propname"></param>
        /// <param name="convertorType"></param>
        public ViewModelToModel(string name, string propname, string convertorType)
        {
            this.modelname = name;
            propertyName = propname;
            convertorName = convertorType;
        }
    }
}
#region Revision History
// 2014-May-15  Chandralatha
//              Initial version
#endregion Revision History