#region Copyright Koninklijke Philips Electronics N.V. 2014
//
// All rights are reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
// FILENAME: TransferPropertyDTO.cs
//
#endregion

using System.Reflection;
using ProtoBuf;


namespace ProtocolBuf12.ECViewModel
{
    /// <summary>
    /// View Model which should be used to show the UI for advanced properties
    /// </summary>
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class TransferPropertyDTO : DTOBase
    {
        /// <summary>
        /// Holds whether the push to workstation label should be checked or not.
        /// </summary>
        private bool pushToWorkstation;
        /// <summary>
        /// Holds whether the push to workstation needs to be enabled or not.
        /// </summary>
        private bool enablePushToWorkstation;
        /// <summary>
        /// Holds whether is transfer property is enabled or not
        /// </summary>
        private bool isCandidateForTransfer;
        /// <summary>
        /// Slice order = Ascending or descending
        /// </summary>
        private SortOrder sliceOrder;
        /// <summary>
        /// Attributes to be shown in the UI
        /// </summary>
        private SortAttributes[] sortAttributes;
        /// <summary>
        /// Manage the state of the lifetime of object.
        /// </summary>
        private bool disposed = false;
        /// <summary>
        /// Holds whether the push to workstation label should be checked or not.
        /// </summary>
        [ViewModelToModel("ExecutionStep.Scan", "PushToWorkStation")]
        public bool PushToWorkstation
        {
            get
            {
                return pushToWorkstation;
            }
            set
            {
                if (pushToWorkstation != value)
                {
                    pushToWorkstation = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));;
                }
            }
        }

        /// <summary>
        ///  Holds whether the push to workstation needs to be enabled or not.
        /// </summary>
        public bool EnablePushToWorkstation
        {
            get
            {
                return enablePushToWorkstation;
            }
            set
            {
                if (enablePushToWorkstation != value)
                {
                    enablePushToWorkstation = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));;
                }
            }
        }

        /// <summary>
        /// Holds whether is transfer property is enabled or not
        /// </summary>
        [ViewModelToModel("ExecutionStep.Scan", "IsCandidateForTransfer")]
        public bool IsCandidateForTransfer
        {
            get
            {
                return isCandidateForTransfer;
            }
            set
            {
                if (value != isCandidateForTransfer)
                {
                    isCandidateForTransfer = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));;
                }
            }
        }

        /// <summary>
        /// Holds the value of how the slice order needs to be shown
        /// </summary>
        [ViewModelToModel("ExecutionStep.Scan.ViewingProcedure", "SliceOrder")]
        public SortOrder SliceOrder
        {
            get
            {
                return sliceOrder;
            }
            set
            {
                if (sliceOrder != value)
                {
                    sliceOrder = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));;
                }
            }
        }

        /// <summary>
        /// Array of sort attributes
        /// </summary>
        [ViewModelToModel("ExecutionStep.Scan.ViewingProcedure", "SortingAttributes")]
        public SortAttributes[] SortingAttributes
        {
            get
            {
                return sortAttributes;
            }
            set
            {
                if (sortAttributes != value)
                {
                    sortAttributes = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));;
                }
            }
        }

         /// <summary>
         /// Clean up any resources being used.
         /// </summary>
         /// <param name="disposing"></param>
         protected override void Dispose(bool disposing)
         {
             if (!disposed)
             {
                 if (disposing)
                 {
                     SortingAttributes = null;                     
                     disposed = true;
                 }
             }
             base.Dispose(true);
         }
    }
}

#region Revision History
// 2014-May-07  Anand K R
//              Initial version
// 2014-Jun-24  Chandralatha
//              Implemented IDiposable interface and release of resource.
#endregion Revision History
