#region Copyright Koninklijke Philips Electronics N.V. 2014
//
// All rights are reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
// FILENAME: ExecutionSequenceDTO.cs
//
#endregion

using System.Collections.ObjectModel;
using System.Reflection;
using ProtoBuf;

namespace ProtocolBuf12.ECViewModel
{   
    /// <summary>
    /// Denotes each row item in a scanset
    /// This is a combination of ExecutionStep and SingleScan since the UI
    /// comprises of data collected by both the classes for building the row.
    /// </summary>
    [ProtoContract(ImplicitFields=ImplicitFields.AllPublic)]
    [ProtoInclude(110, typeof(ProcessingStepDTO))]
    public class ExecutionSequenceDTO : StepDTO
    {
        /// <summary>
        /// List of all processing steps associated with Singlescan
        /// Has to be of type <see cref="IProcessingInputOutput"/>
        /// </summary>
        private ObservableCollection<ElementBase> processingSteps = new ObservableCollection<ElementBase>();
        /// <summary>
        /// Type of data to be shown on the UI during edit mode
        /// "Philips.PmsMR.Acquisition.AcqGlo.DynamicScanDelayMode"
        /// </summary>
        private CellInfoDTO scanDelayMode = new CellInfoDTO();
        /// <summary>
        /// Name of the processing step
        /// </summary>
        private CellInfoDTO name = new CellInfoDTO();
        /// <summary>
        /// Check if contrast is available or not.
        /// </summary>
        private bool contrast;
        /// <summary>
        /// contrast injection protocol data
        /// </summary>
        private CipDTO contrastInjectionProtocol;
        /// <summary>
        /// Contrast injection if enabled or not.
        /// </summary>
        private bool contrastInjectionEnabled;
        /// <summary>
        /// Current settings which is shown in the UI / what is selected by the user.
        /// </summary>
        private CellInfoDTO currentSetting = new CellInfoDTO();
        /// <summary>
        /// 
        /// </summary>
        private EditFields currentEditField;
        /// <summary>
        /// List of valid settings cell that is available to the user to modify
        /// </summary>
        private ObservableCollection<SettingsCell> settingsList;
        /// <summary>
        /// Input mode to be enabled or not
        /// </summary>
        private bool inputModeEnabled;
        /// <summary>
        /// Input mode which is checked.
        /// </summary>
        private bool inputModeSelected;
        /// <summary>
        /// Input mode which is visible or not.
        /// </summary>
        private bool inputModeVisible;
        /// <summary>
        /// Input mode to be enabled or not
        /// </summary>
        private bool showExtendedParameter;
        /// <summary>
        /// determines if the step needs Manual start or not
        /// </summary>
        private bool manualStart;
        /// <summary>
        /// Check if the execution step is the first one or subsequent step.
        /// </summary>
        private bool isFirst;
        /// <summary>
        /// Check if the scan has a breathold 
        /// </summary>
        private bool breathold;
        /// <summary>
        /// Scan delay provided for the execution step.
        /// </summary>
        private CellInfoDTO scanDelay = new CellInfoDTO();
        /// <summary>
        /// Denotes if the table will move or not
        /// </summary>
        private CellInfoDTO tableWillMove = new CellInfoDTO();
        /// <summary>
        /// Progress information of scanning
        /// </summary>
        private int acquisitionProgress;
        /// <summary>
        /// Progress information of reconstruction
        /// </summary>
        private int reconstructionProgress;
        /// <summary>
        /// Denotes the time taken by the step.
        /// </summary>
        private double stepDuration;
        /// <summary>
        /// Number of dynamics associated with the execution step.
        /// </summary>
        private CellInfoDTO numberOfDynamics = new CellInfoDTO();
        /// <summary>
        /// Attained SED value
        /// </summary>
        private double attainedSED;
        /// <summary>
        /// Predicted SED value
        /// </summary>
        private double predictedSED;
        /// <summary>
        /// Laterality associated with the singlescan
        /// </summary>
        private CellInfoDTO laterality = new CellInfoDTO();
        /// <summary>
        /// denotes if the singlescan is readonly or not
        /// </summary>
        private bool readOnly;
        /// <summary>
        /// Smart geometry type
        /// </summary>
        private string smartGeoType;
        /// <summary>
        /// Smart scout type
        /// </summary>
        private string smartScoutType;
        /// <summary>
        /// Geometry name associated with singlescan
        /// </summary>
        private CellInfoDTO geometryId = new CellInfoDTO();
        /// <summary>
        /// Denoted the propagate coverage menu information
        /// </summary>
        private bool reuseStackSizes;
        /// <summary>
        /// Transfer information associated with single scan
        /// </summary>
        private TransferPropertyDTO transferProperty;
        /// <summary>
        /// Geolink ID associated with singlescan
        /// </summary>
        private CellInfoDTO geoLinkId = new CellInfoDTO();
        /// <summary>
        /// Denotes if the singlescan is a prescan or not
        /// </summary>
        private bool isPreScan;
        /// <summary>
        /// 
        /// </summary>
        private bool isInEditMode;
        /// <summary>
        /// 
        /// </summary>
        private bool isPlannedScan;
        /// <summary>
        /// State of singlescan
        /// </summary>
        private CellInfoDTO currentState = new CellInfoDTO();
        /// <summary>
        /// Acquisition number, reconstruction number.
        /// Combined value of the above mentioned items.
        /// </summary>
        private string scanNumber;
        /// <summary>
        /// Anatomy information
        /// </summary>
        private string anatomy;
        /// <summary>
        /// Antomical region associated with singlescan
        /// </summary>
        private string anatomicalRegion;        
        /// <summary>
        /// Sar level information
        /// </summary>
        private bool highSarPnsLevel;
        /// <summary>
        /// State of the lifetime of the object
        /// </summary>
        private bool disposed = false;
        /// <summary>
        /// Check if contrast is available or not.
        /// </summary>
        [ViewModelToModel("ExecutionStep", "Contrast")]
        public bool Contrast
        {
            get
            {
                return contrast;
            }
            set
            {
                if (contrast != value)
                {
                    contrast = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }

        /// <summary>
        /// Contrast injection protocol data
        /// </summary>
        [ViewModelToModel("ExecutionStep","ContrastInjectionProtocol")]
        public CipDTO ContrastInjectionProtocol
        {
            get
            {
                return contrastInjectionProtocol;
            }
            set
            {
                if (contrastInjectionProtocol != value)
                {
                    contrastInjectionProtocol = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }

        /// <summary>
        /// Contrast injection if enabled or not.
        /// </summary>
        [ViewModelToModel("ExecutionStep",ConvertorName="ContrastInjectionEnabledConvertor")]
        public bool ContrastInjectionEnabled
        {
            get
            {
                return contrastInjectionEnabled;
            }
            set
            {
                if (contrastInjectionEnabled != value)
                {
                    contrastInjectionEnabled = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }

        /// <summary>
        /// Current settings which is shown in the UI / what is selected by the user.
        /// </summary>
        [ViewModelToModel("ExecutionStep.Scan", ConvertorName="SettingCellConvertor")]
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
        /// Check if the step needs Manual start or not
        /// </summary>
        [ViewModelToModel("ExecutionStep", "ManualStart")]
        public bool ManualStart
        {
            get
            {
                return manualStart;
            }
            set
            {
                if (manualStart != value)
                {
                    manualStart = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }
        /// <summary>
        /// Checks if the executionstep is the first dynamic step or 
        /// subsequent dynamic step
        /// </summary>
         [ViewModelToModel("ExecutionStep", "IsFirst")]
        public bool IsFirst
        {
            get
            {
                return isFirst;
            }
            set
            {
                if (isFirst != value)
                {
                    isFirst = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }

        /// <summary>
        /// Check if Breathhold is enabled or not for the scan
        /// </summary>
        [ViewModelToModel("ExecutionStep", "BreathHold")]
        public bool BreathHold
        {
            get
            {
                return breathold;
            }
            set
            {
                if (breathold != value)
                {
                    breathold = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }

        /// <summary>
        /// Type of data to be shown on the UI during edit mode
        /// "Philips.PmsMR.Acquisition.AcqGlo.DynamicScanDelayMode"
        /// </summary>
        [ViewModelToModel("ExecutionStep", "ScanDelayMode", "ScanDelayModeConvertor")]
        public CellInfoDTO  ScanDelayMode
        {
            get
            {
                return scanDelayMode;
            }
            set
            {
                if (scanDelayMode != value)
                {
                    scanDelayMode = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }

       

        /// <summary>
        /// Delay to be given for the scan
        /// </summary>
        [ViewModelToModel("ExecutionStep", "ScanDelay", "ScanDelayConvertor")]
        public CellInfoDTO ScanDelay
        {
            get
            {
                return scanDelay;
            }
            set
            {
                if (scanDelay != value)
                {
                    scanDelay = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }

       

        /// <summary>
        /// Number of dynamics to be displayed during edit mode.
        /// </summary>
        [ViewModelToModel("ExecutionStep", "NumberOfDynamics","NumberOfDynamicsConvertor")]
        public CellInfoDTO NumberOfDynamics
        {
            get
            {
                return numberOfDynamics;
            }
            set
            {
                if (numberOfDynamics != value)
                {
                    numberOfDynamics = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }

        
        /// <summary>
        /// To be used later. Currently not shown in UI.
        /// Denotes the time taken by the execution step.
        /// </summary>
        [ViewModelToModel("ExecutionStep", "StepDuration")]
        public double StepDuration
        {
            get
            {
                return stepDuration;
            }
            set
            {
                if (stepDuration != value)
                {
                    stepDuration = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }

        /// <summary>
        /// Not to be used by ECEditor or ECDatabaseBrowser view
        /// Shows the acquisition progress
        /// </summary>
        [ViewModelToModel("ExecutionStep", "AcquisitionProgress")]
        public override int PrimaryProgress
        {
            get
            {
                return acquisitionProgress;
            }
            set
            {
                if (acquisitionProgress != value)
                {
                    acquisitionProgress = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }

        /// <summary>
        /// Not to be used by ECEditor or ECDatabasebrowser view.
        /// Denotes the reconstruction progress
        /// </summary>
       [ViewModelToModel("ExecutionStep", "ReconstructionProgress")]
        public override int SecondaryProgress
        {
            get
            {
                return reconstructionProgress;
            }
            set
            {
                if (reconstructionProgress != value)
                {
                    reconstructionProgress = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }

        /// <summary>
        /// Denotes if the table movement is required or not
        /// </summary>

       [ViewModelToModel("ExecutionStep", "TableWillMove")]
       public CellInfoDTO TableWillMove
        {
            get
            {
                return tableWillMove;
            }
            set
            {
                if (tableWillMove != value)
                {
                    tableWillMove = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }
        #region SingleScan members

        /// <summary>
        /// Set this property whenever the examcard has validAnatomy,
        /// Is not first
        /// or if examcard itself is ReadOnly
        /// </summary>
        public bool ReadOnly
        {
            get
            {
                return readOnly;
            }
            set
            {
                if (readOnly != value)
                {
                    readOnly = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }

        /// <summary>
        /// Name of the singlescan.
        /// </summary>
        [ViewModelToModel("ExecutionStep.Scan", "Name","ScanNameConvertor")]
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
        /// Smart plan type if the single scan is a smart singlescan
        /// [DefaultValue(SmartPlanType.AWPLAN_SMARTPLAN_TYPE_NONE)]
        /// </summary>
       [ViewModelToModel("ExecutionStep.Scan", "SmartGeoType","EnumToStringConvertor")]
        public string SmartGeoType
        {
            get
            {
                return smartGeoType;
            }
            set
            {
                if (smartGeoType != value)
                {
                    smartGeoType = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }

        /// <summary>
        /// Smart scout type associated with the singlescan
        /// </summary>
        [ViewModelToModel("ExecutionStep.Scan", "SmartScoutType", "EnumToStringConvertor")]
        public string SmartScoutType
        {
            get
            {
                return smartScoutType;
            }
            set
            {
                if (smartScoutType != value)
                {
                    smartScoutType = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }

        /// <summary>
        /// Name of the geometry associated with singlescan
        /// </summary>
       [ViewModelToModel("ExecutionStep",ConvertorName="GeometryConvertor")]
        public CellInfoDTO GeometryId
        {
            get
            {
                return geometryId;
            }
            set
            {
                if (geometryId != value)
                {
                    geometryId = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }

        /// <summary>
        /// Denotes if propagate coverage is switched ON or not
        /// </summary>
        [ViewModelToModel("ExecutionStep.Scan", "ReuseStackSizes")]
        public bool ReuseStackSizes
        {
            get
            {
                return reuseStackSizes;
            }
            set
            {
                if (reuseStackSizes != value)
                {
                    reuseStackSizes = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }

        /// <summary>
        /// Get/Set the transfer related information of SingleScan
        /// </summary>
        [ViewModelToModel("ExecutionStep.Scan")]
        public TransferPropertyDTO TransferProperty
        {
            get
            {
                return transferProperty;
            }
            set
            {
                if(transferProperty != value)
                {
                    transferProperty = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }

        /// <summary>
        /// Get/Set the geo link information of the singlescan
        /// </summary>
        [ViewModelToModel("ExecutionStep.Scan", "GeoLinkID", "GeoLinkConvertor")]
        public CellInfoDTO GeoLinkId
        {
            get
            {
                return geoLinkId;
            }
            set
            {
                if (geoLinkId != value)
                {
                    geoLinkId = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }

        /// <summary>
        /// Denotes if the scan is a prescan or not
        /// </summary>
        [ViewModelToModel("ExecutionStep.Scan", "IsPreScan")]
        public bool IsPreScan
        {
            get
            {
                return isPreScan;
            }
            set
            {
                if (isPreScan != value)
                {
                    isPreScan = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }

        /// <summary>
        /// CurrentState of the singlescan
        /// </summary>
        [ViewModelToModel("ExecutionStep.Scan", "CurrentState", "ScanStateConvertor")]
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
        /// For simplicity sake combined Acq Number and Rec Number and
        /// kept as single property
        /// Even though multiple reconstruction numbers are generated, only
        /// one will be used to be shown on the UI.
        /// </summary>
        [ViewModelToModel("ExecutionStep.Scan", "ScanNumber","ScanNumberConvertor")]
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
        /// Anatomy information associated with the singlescan
        /// </summary>
        [ViewModelToModel("ExecutionStep.Scan", "Anatomy")]
        public string Anatomy
        {
            get
            {
                return anatomy;
            }
            set
            {
                if (anatomy != value)
                {
                    anatomy = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }

        /// <summary>
        /// Anatomical region information associated with the singlescan
        /// </summary>
        [ViewModelToModel("ExecutionStep.Scan", "AnatomicalRegion")]
        public string AnatomicalRegion
        {
            get
            {
                return anatomicalRegion;
            }
            set
            {
                if (anatomicalRegion!= value)
                {
                    anatomicalRegion = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }

        /// <summary>
        /// Attained SED value for the singlescan.
        /// </summary>
       [ViewModelToModel("ExecutionStep.Scan", "AttainedSED")]
        public double AttainedSED
        {
            get
            {
                return attainedSED;
            }
            set
            {
                if (attainedSED != value)
                {
                    attainedSED = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }

        /// <summary>
        /// Predicted SED value for the singlescan
        /// </summary>
         [ViewModelToModel("ExecutionStep.Scan", "PredictedSED")]
        public double PredictedSED
        {
            get
            {
                return predictedSED;
            }
            set
            {
                if (predictedSED != value)
                {
                    predictedSED = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }

        /// <summary>
        /// Laterality information associated with SingleScn
        /// </summary>
        [ViewModelToModel("ExecutionStep.Scan", "Laterality","LateralityConvertor")]
        public CellInfoDTO Laterality
        {
            get
            {
                return laterality;
            }
            set
            {
                if (laterality != value)
                {
                    laterality = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }
                   
        /// <summary>
        /// Sar level information
        /// </summary>
        [ViewModelToModel("ExecutionStep.Scan",ConvertorName="SarPnsConvertor")]
        public bool HighSarPnsLevel
        {
            get
            {
                return highSarPnsLevel;
            }
            set
            {
                if (highSarPnsLevel != value)
                {
                    highSarPnsLevel = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }

        /// <summary>
        /// List of valid settings cell that is available to the user to modify
        /// </summary>
       [ViewModelToModel("ExecutionStep",ConvertorName="SettingsListConvertor")]
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
        /// Check if input mode needs to be checked or not.
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
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4)); ;
                }
            }
        }

        /// <summary>
        /// Check if edit mode row needs to be shown or not.
        /// </summary>
        public bool ShowExtendedParameter
        {
            get
            {
                return showExtendedParameter;
            }
            set
            {
                if (showExtendedParameter != value)
                {
                    showExtendedParameter = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }
        
        #endregion SingleScan members

        /// <summary>
        /// List of all outputs associated with singlescan
        /// </summary>
        public override ObservableCollection<ElementBase> ChildElements
        {
            get
            {
                return processingSteps;
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
        /// Check if the row is in edit mode
        /// </summary>
        public bool IsPlannedScan
        {
            get
            {
                return isPlannedScan;
            }
            set
            {
                if (isPlannedScan != value)
                {
                    isPlannedScan = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }

        /// <summary>
        /// Check if the row is in edit mode
        /// </summary>
        public bool IsInEditMode
        {
            get
            {
                return isInEditMode;
            }
            set
            {
                if (isInEditMode != value)
                {
                    isInEditMode = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
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
                    if (contrastInjectionProtocol != null)
                    {
                        contrastInjectionProtocol.Dispose();
                        contrastInjectionProtocol = null;
                    }
                    if (settingsList != null)
                    {
                        settingsList.Clear();
                        settingsList = null;
                    }
                    if (ScanDelayMode != null)
                    {
                        ScanDelayMode.Dispose();
                        ScanDelayMode = null;
                    }
                    if (Name != null)
                    {
                        Name.Dispose();
                        Name = null;
                    }
                    if (GeoLinkId != null)
                    {
                        GeoLinkId.Dispose();
                        GeoLinkId = null;
                    }
                    if (TransferProperty != null)
                    {
                        TransferProperty.Dispose();
                        TransferProperty = null;
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
                    if (GeometryId != null)
                    {
                        GeometryId.Dispose();
                        GeometryId = null;
                    }
                    if (TableWillMove != null)
                    {
                        TableWillMove.Dispose();
                        TableWillMove = null;
                    }
                    if (NumberOfDynamics != null)
                    {
                        NumberOfDynamics.Dispose();
                        NumberOfDynamics = null;
                    }
                    if (ScanDelay != null)
                    {
                        ScanDelay.Dispose();
                        ScanDelay = null;
                    }
                    if (Laterality != null)
                    {
                        Laterality.Dispose();
                        Laterality = null;
                    }
                    disposed = true;
                }
            }
            base.Dispose(true);
        }
    }

    /// <summary>
    /// Base class for All Steps  which can be represented as a row.
    /// - ExecutionSequence, ProcessingStep, OutputDescriptor.
    /// </summary>
    public abstract class StepDTO : ElementBase
    {
        /// <summary>
        /// Represents the scanNumber 
        /// </summary>
        public abstract string ScanNumber
        {
            get;
            set;
        }

        /// <summary>
        /// Represents the information to be shown related to the name of the step.
        /// </summary>
        public abstract CellInfoDTO Name
        {
            get;
            set;
        }

        /// <summary>
        /// Represents the information to be shown related to state of the step.
        /// </summary>
        public abstract CellInfoDTO CurrentState
        {
            get;
            set;
        }

        /// <summary>
        /// Check if input mode needs to be shown or not.
        /// </summary>
        public abstract bool InputModeEnabled
        {
            get;
            set;
        }

        /// <summary>
        /// Check if input mode needs to be checked or not.
        /// </summary>
        public abstract bool InputModeSelected
        {
            get;
            set;
        }

        /// <summary>
        /// Check if input mode checkbox needs to be shown or not
        /// </summary>
        public abstract bool InputModeVisible
        {
            get;
            set;
        }

        /// <summary>
        /// Display the secondary progress
        /// </summary>
        public abstract int SecondaryProgress
        {
            get;
            set;
        }
        /// <summary>
        /// Display the primary progress.
        /// </summary>
        public abstract int PrimaryProgress
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Geometry identifier which is based on the name, orientation and smart status
    /// </summary>
    public class GeometryDTO : DTOBase
    {
        /// <summary>
        /// Name of the geometry
        /// </summary>
        private string name;
        /// <summary>
        /// Smart plan learn status
        /// </summary>
        private int smartPlanLearnStatus = -1;
        /// <summary>
        /// 
        /// </summary>
        private SmartPlanType smartPlanType = SmartPlanType.AWPLAN_SMARTPLAN_TYPE_NONE;        
        /// <summary>
        /// Orientation of the slice
        /// </summary>
        private GeometryOrientation orientation = GeometryOrientation.Undefined;
        /// <summary>
        /// Tooltip associated with smart plan 
        /// </summary>
        private string smartPlanDescription;
        /// <summary>
        /// Tooltip geometry description        
        /// </summary>
        private string geometryDescription;
        /// <summary>
        /// Name of the geometry
        /// </summary>
        public string Name
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
        /// Smart plan learn status
        /// </summary>
        public int SmartPlanLearnStatus
        {
            get
            {
                return smartPlanLearnStatus;
            }
            set
            {
                if (smartPlanLearnStatus != value)
                {
                    smartPlanLearnStatus = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public SmartPlanType SmartPlanType
        {
            get 
            { 
                return smartPlanType;
            }
            set 
            { 
                smartPlanType = value;
                RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
            }
        }
        /// <summary>
        /// Orientation of the slice
        /// </summary>
        public GeometryOrientation Orientation
        {
            get
            {
                return orientation;
            }
            set
            {
                if (orientation != value)
                {
                    orientation = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }

        /// <summary>
        /// Smart plan status description
        /// </summary>
        public string SmartPlanDescription
        {
            get
            {
                return smartPlanDescription;
            }
            set
            {
                if (smartPlanDescription != value)
                {
                    smartPlanDescription = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }

        /// <summary>
        /// Geometry description.
        /// </summary>
        public string GeometryDescription
        {
            get
            {
                return geometryDescription;
            }
            set
            {
                if (geometryDescription != value)
                {
                    geometryDescription = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (!(obj is GeometryDTO))
            {
                return false;
            }
            GeometryDTO ob = (GeometryDTO)obj;
            // Return true if the fields match:
            if ((string.Compare(ob.Name.Trim(), this.Name.Trim(), true) == 0) &&
                (ob.SmartPlanType == this.SmartPlanType)&&
                (ob.Orientation == this.Orientation))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
 #region Revision History
// 2014-May-12  Anand K R
//              Initial version
// 2014-Jun-24  Chandralatha
//              Implemented IDiposable interface and release of resource.
#endregion Revision History