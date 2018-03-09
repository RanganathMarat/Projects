#region Copyright Koninklijke Philips Electronics N.V. 2014
//
// All rights are reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
// FILENAME: CellInfoDTO.cs
//
#endregion

using System.Reflection;
using ProtoBuf;


namespace ProtocolBuf12.ECViewModel
{
    /// <summary>
    /// Data Structure class which holds the information like
    /// Data object value, Description ( used as tooltip in the UI ) and canChange property for the DataObject.
    /// </summary>
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class CellInfoDTO : DTOBase
    {
        /// <summary>
        /// Data Object 
        /// </summary>
        private object value = null;
        /// <summary>
        /// Description about the data object
        /// </summary>
        private string description = string.Empty;
        /// <summary>
        /// Indicates whther the data object can be changed or not.
        /// </summary>
        private bool canChange = true;
        /// <summary>
        /// Holds the state of the lifetime of the object.
        /// </summary>
        private bool disposed = false;
        /// <summary>
        /// Default Constructor
        /// </summary>
        public CellInfoDTO()
        {            
        }
        /// <summary>
        /// Overloaded Constructor for Objects with has all 3 values set
        /// </summary>
        public CellInfoDTO(object data, string desc, bool allowedtoChange)
        {
            Value = data;
            Description = desc;
            CanChange = allowedtoChange;
        }

        /// <summary>
        /// Overloaded Constructor for Objects with has only data object and description
        /// </summary>
        public CellInfoDTO(object data, string desc)
        {
            Value = data;
            Description = desc;            
        }

        /// <summary>
        /// Overloaded Constructor for Objects with has only data object and can change property
        /// </summary>
        public CellInfoDTO(object data,  bool allowedtoChange)
        {
            Value = data;            
            CanChange = allowedtoChange;
        }
        /// <summary>
        /// Overloaded Constructor for Objects with has only data object.
        /// </summary>
        public CellInfoDTO(object data)
        {
            Value = data;            
        }
        /// <summary>
        /// Get/Set for the Data object Value
        /// </summary>
        public object Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
                RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
            }
        }
        /// <summary>
        /// Get/Set for the Data object Can change property
        /// </summary>
        public bool CanChange
        {
            get
            {
                return canChange;
            }
            set
            {
                canChange = value;
                RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
            }
        }
        /// <summary>
        /// Get/Set for the Data object Description
        /// </summary>
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
                RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
            }
        }
        /// <summary>
        /// Cleans up the resources
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Value = null;
                }
                disposed = true;
            }
            base.Dispose(disposing);
        }
    }
}
#region Revision History
// 22-Jun-2014  Chandralatha
//              Initial version
// 24-Jun-2014  Chandralatha
//              Implemented IDiposable interface and release of resource.
#endregion Revision History