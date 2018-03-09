#region Copyright Koninklijke Philips Electronics N.V. 2014
//
// All rights are reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
// FILENAME: ExamCardPropertyDTO.cs
//
#endregion

#region System Namespaces
using System.Reflection;
using ProtoBuf;
#endregion

namespace ProtocolBuf12.ECViewModel
{    
    /// <summary>
    /// Confirm review type of the review required type
    /// </summary>
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class ConfirmReviewTypeDTO : DTOBase
    {
        /// <summary>
        /// Key of the review type
        /// </summary>
        private string key;
        /// <summary>
        /// Value of the review type
        /// </summary>
        private string val;

        /// <summary>
        /// Key to be displayed of the review type
        /// </summary>
        [ViewModelToModel("ECModel.ConfirmReviewType", "PackageName")]
        public string Key
        {
            get
            {
                return key;
            }
            set
            {
                if (key != value)
                {
                    key = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }
        /// <summary>
        /// Value to be displayed of the review type
        /// </summary>
        [ViewModelToModel("ECModel.ConfirmReviewType", "ReviewType")]
        public string Value
        {
            get
            {
                return val;
            }
            set
            {
                if (val != value)
                {
                    val = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }
    }
}

#region Revision History
// 2014-May-12  Anand K R
//              Initial version
// 2014-Jun-22  Chandralatha
//              Defined mapping between source and destination
#endregion Revision History