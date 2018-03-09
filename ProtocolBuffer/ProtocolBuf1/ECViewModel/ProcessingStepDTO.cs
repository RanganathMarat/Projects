#region Copyright Koninklijke Philips Electronics N.V. 2014
//
// All rights are reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
// FILENAME: ProcessingStepDTO.cs
//
#endregion

using System;
using System.Collections.ObjectModel;
using System.Reflection;
using ProtoBuf;

namespace ProtocolBuf12.ECViewModel
{
    /// <summary>
    /// A class which denotes a Processing step or an output
    /// </summary>
    // [TODO-Ranga]
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class ProcessingStepDTO : StepDTO //, IProcessingInputOutput
    {
        /// <summary>
        /// Progress information of scanning
        /// </summary>
        private int stepProgress;
        /// <summary>
        /// Name of the processing step
        /// </summary>
        private CellInfoDTO name = new CellInfoDTO();
        /// <summary>
        /// Indicates if the processing step can be manually started or not.
        /// </summary>
        private CellInfoDTO currentSetting = new CellInfoDTO();
        /// <summary>
        /// State of the processing step.
        /// </summary>
        private CellInfoDTO currentState = new CellInfoDTO();
        /// <summary>
        /// List of valid settings to be shown for the step.
        /// </summary>
        private ObservableCollection<SettingsCell> settingsList;
        /// <summary>
        /// Processing type information
        /// </summary>
        private string packageType;
        /// <summary>
        /// To be filled only when the processing step contains 
        /// only one outputdescriptor
        /// </summary>
        private string scanNumber;
        /// <summary>
        /// List of outputs / child elements which are associated with this step.
        /// The list can only be of type <see cref="IProcessingInputOutput"/>
        /// </summary>
        private ObservableCollection<ElementBase> outputs = new ObservableCollection<ElementBase>();
        /// <summary>
        /// Input mode is enabled or not
        /// </summary>
        private bool inputModeEnabled;
        /// <summary>
        /// Input mode is selected or not.
        /// </summary>
        private bool inputModeSelected;
        /// <summary>
        /// Input mode which is visible or not.
        /// </summary>
        private bool inputModeVisible;
        /// <summary>
        /// 
        /// </summary>
        private bool swOptionAvailable;
        /// <summary>
        /// Current editing field
        /// </summary>
        private EditFields currentEditField;
        /// <summary>
        /// State of the lifetime of the object
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Name of the processing step
        /// </summary>
        [ViewModelToModel("ProcessingStep", "Name")]
        public override CellInfoDTO Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name != value)
                {
                    name = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }

        /// <summary>
        /// Indicates if the processing step can be manually started or not.
        /// </summary>
        public CellInfoDTO CurrentSetting
        {
            get
            {
                return currentSetting;
            }
            set
            {
                if (currentSetting != value)
                {
                    currentSetting = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }

        /// <summary>
        /// List of outputs / child elements which are associated with this step.
        /// The list can only be of type <see cref="IProcessingInputOutput"/>
        /// </summary>
        public override ObservableCollection<ElementBase> ChildElements
        {
            get
            {
                return outputs;
            }            
        }

        /// <summary>
        /// what kind of package the processing step is associated to.
        /// </summary>
        public string PackageType
        {
            get
            {
                return packageType;
            }
            set
            {
                if (packageType != value)
                {
                    packageType = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }

        /// <summary>
        /// Processing step state information.
        /// </summary>
        public override CellInfoDTO CurrentState
        {
            get
            {
                return currentState;
            }
            set
            {
                if (currentState != value)
                {
                    currentState = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }

        /// <summary>
        /// List of valid settings to be shown for the step.
        /// </summary>
        public ObservableCollection<SettingsCell> SettingsList
        {
            get
            {
                return settingsList;
            }
            set
            {
                if (settingsList != value)
                {
                    settingsList = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }
        /// <summary>
        /// Scan number to be filled only if processingstep
        /// contains only one output descriptor.
        /// </summary>
        public override string ScanNumber
        {
            get
            {
                return scanNumber;
            }
            set
            {
                if (scanNumber != value)
                {
                    scanNumber = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }

        /// <summary>
        /// Check if input mode needs to be shown or not.
        /// </summary>
        public override bool InputModeEnabled
        {
            get
            {
                return inputModeEnabled;
            }
            set
            {
                if (inputModeEnabled != value)
                {
                    inputModeEnabled = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }

        /// <summary>
        /// Check if input mode needs to be shown or not.
        /// </summary>
        public override bool InputModeSelected
        {
            get
            {
                return inputModeSelected;
            }
            set
            {
                // Set the value even if it is same
                inputModeSelected = value;
                RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
            }
        }

        /// <summary>
        /// Check if input mode needs to be shown or not.
        /// </summary>
        public override bool InputModeVisible
        {
            get
            {
                return inputModeVisible;
            }
            set
            {
                if (inputModeVisible != value)
                {
                    inputModeVisible = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }

        /// <summary>
        /// Check if input mode needs to be shown or not.
        /// </summary>
        public bool SWOptionAvailable
        {
            get
            {
                return swOptionAvailable;
            }
            set
            {
                if (swOptionAvailable != value)
                {
                    swOptionAvailable = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }

        /// <summary>
        /// Current edit field.
        /// </summary>
        public EditFields CurrentEditField
        {
            get
            {
                return currentEditField;
            }
            set
            {
                if (currentEditField != value)
                {
                    currentEditField = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }

        /// <summary>
        /// Not to be used by ECEditor or ECDatabaseBrowser view
        /// Shows the acquisition progress
        /// </summary>
        [ViewModelToModel("ProcessingStep", "Progress")]
        public override int PrimaryProgress
        {
            get
            {
                return stepProgress;
            }
            set
            {
                if (stepProgress != value)
                {
                    stepProgress = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }

        /// <summary>
        /// Not to be used by ECEditor or ECDatabasebrowser view.
        /// Denotes the reconstruction progress
        /// </summary>
        public override int SecondaryProgress
        {
            get
            {
                return 0;
            }
            set
            {
                // Do Nothing
                throw new NotImplementedException("SecondaryProgress not implemented for ProcessingStep");
            }
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (settingsList != null)
                    {
                        settingsList.Clear();
                        settingsList = null;
                    }
                    if (Name != null)
                    {
                        Name.Dispose();
                        Name = null;
                    }
                    if (CurrentState != null)
                    {
                        CurrentState.Dispose();
                        CurrentState = null;
                    }
                    if (CurrentSetting != null)
                    {
                        CurrentSetting.Dispose();
                        CurrentSetting = null;
                    }
                    disposed = true;
                }
            }
            base.Dispose(true);
        }
    }
}

#region Revision History
// 2014-May-12  Anand K R
//              Initial version
// 2014-Jun-24  Chandralatha
//              Implemented IDiposable interface and release of resource.
#endregion Revision History