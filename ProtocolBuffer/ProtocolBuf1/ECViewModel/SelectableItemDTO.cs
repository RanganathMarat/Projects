#region Copyright Koninklijke Philips Electronics N.V. 2014
//
// All rights are reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
// FILENAME: SelectableItemDTO.cs
//
#endregion

#region System Namespaces
using System.Reflection;
using ProtoBuf;
#endregion

namespace ProtocolBuf12.ECViewModel
{
    /// <summary>
    /// UI attributes required for representing a selectable item
    /// </summary>
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class SelectableItemDTO : DTOBase
    {
        #region Private Members
        /// <summary>
        /// Ident for the item
        /// </summary>
        private string ident = string.Empty;
        /// <summary>
        /// caption to be diaplayed
        /// </summary>
        private string caption = string.Empty;
        /// <summary>
        /// Specifies whether the item is in conflict
        /// </summary>
        private bool isInConflict = false;
        /// <summary>
        /// Specifies whether the item is selected or not
        /// </summary>
        private bool isSelected = false;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets/Sets the ident for the item
        /// </summary>
        public string ID
        {
            get
            {
                return ident;
            }
            set
            {
                if (ident != value)
                {
                    ident = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }
        /// <summary>
        /// Gets/Sets the caption of the item
        /// </summary>
        public string Caption
        {
            get
            {
                return caption;
            }
            set
            {
                if (caption != value)
                {
                    caption = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }
        /// <summary>
        /// Gets / sets whether the item is in conflict with others
        /// </summary>
        public bool IsInConflict
        {
            get
            {
                return isInConflict;
            }
            set
            {
                if (isInConflict != value)
                {
                    isInConflict = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }
        /// <summary>
        /// Gets / sets whether the item is selected or not
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }
        #endregion
    }
}

#region Revision History
// 27-Aug-2014  prasad v n
//              Initial version
#endregion
