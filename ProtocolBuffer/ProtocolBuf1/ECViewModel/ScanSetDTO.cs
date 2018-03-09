#region Copyright Koninklijke Philips Electronics N.V. 2014
//
// All rights are reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
// FILENAME: ScanSetDTO.cs
//
#endregion

using System.Collections.ObjectModel;
using System.Reflection;
using ProtoBuf;

namespace ProtocolBuf12.ECViewModel
{
    /// <summary>
    /// 
    /// </summary>
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class ScanSetDTO : ElementBase
    {
        /// <summary>
        /// Contains a list of execution sequences
        /// </summary>
        private ObservableCollection<ElementBase> executionSequence = new ObservableCollection<ElementBase>();
        /// <summary>
        /// Denotes if the scanset is in edit Mode
        /// </summary>
        private bool editMode;
        /// <summary>
        /// Child elements as execution sequences
        /// </summary>
        [ViewModelToModel("ScanSet", "ExecutionSteps", KnownType = typeof(ExecutionSequenceDTO))]
        public override ObservableCollection<ElementBase> ChildElements
        {
            get
            {
                return executionSequence;
            }
        }
             /// <summary>
        /// Denotes if the scanset is in edit mode
        /// </summary>
        public bool EditMode
        {
            get
            {
                return editMode;
            }
            set
            {
                if (editMode != value)
                {
                    editMode = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));;
                }
            }
        }
    }
}

#region Revision History
// 2014-May-12  Anand K R
//              Initial version
#endregion Revision History