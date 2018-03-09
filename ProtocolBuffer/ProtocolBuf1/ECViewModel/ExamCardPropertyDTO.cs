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
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using ProtoBuf;
#endregion

namespace ProtocolBuf12.ECViewModel
{
    /// <summary>
    /// UI attributes required for Examcard Property
    /// </summary>
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class ExamCardPropertyDTO : DTOBase
    {
        #region Private Members
        /// <summary>
        /// Name of the examcard
        /// </summary>
        private string name;
        /// <summary>
        /// Anatomy selected for examcard
        /// </summary>
        private string anatomy;
        /// <summary>
        /// List of Anatomies supported
        /// </summary>
        private ObservableCollection<string> anatomiesList = new ObservableCollection<string>();
        /// <summary>
        /// SubAnatomy selected for examcard
        /// </summary>
        private string anatomicRegion;
        /// <summary>
        /// List of anatomic regions for the specified anantomy
        /// </summary>
        private ObservableCollection<string> anatomicRegionsList;
        /// <summary>
        /// Laterality selected for examcard
        /// </summary>
        private string laterality = string.Empty;
        /// <summary>
        /// List of possible lateralities
        /// </summary>
        private ObservableCollection<SelectableItemDTO> lateralities = new ObservableCollection<SelectableItemDTO>();
        /// <summary>
        /// List of geometries associated with the examcard
        /// </summary>
        private ObservableCollection<GeometryDTO> namedGeometries;
        /// <summary>
        /// List of review required packages and their type.
        /// </summary>
        private ObservableCollection<ConfirmReviewTypeDTO> reviewRequired;
        /// <summary>
        /// Check to denote if laterality is in conflict or not.
        /// </summary>
        private bool lateralityConflict;
        /// <summary>
        /// Denotes if posterior hold back is true or not.
        /// </summary>
        private bool posteriorHoldBack;
        /// <summary>
        /// DockMode which is of type "Philips.PmsMR.Platform.ScannerContext.DockMode"
        /// converted to string
        /// </summary>
        private string dockMode;
        /// <summary>
        /// Heart rate as defined in the examcard
        /// </summary>
        private int heartRate;
        /// <summary>
        /// Bool to denote if the auto fill in heart rate is auto filled or not
        /// </summary>
        private bool autoFillInHeartRate;
        /// <summary>
        /// Bool to denote if the auto fill in heart rate is allowed or not
        /// </summary>
        private bool autoFillInHeartRateAllowed;
        /// <summary>
        /// Align overlap value
        /// </summary>
        private float alignOverlap = 30.0f;
        /// <summary>
        /// Geo link to be propagated or not.
        /// </summary>
        private bool geoLinkPropagation;
        /// <summary>
        /// Table to be used or not
        /// </summary>
        private bool tableUsage;
        /// <summary>
        /// Indicates whther the examcard is intended for paediatric patients or not
        /// </summary>
        private bool paediatric;
        /// <summary>
        /// Indicates the patient weight
        /// </summary>
        private double patientWeight;
        /// <summary>
        /// Unit of patient wight
        /// </summary>
        private string weightUnit;
        /// <summary>
        /// ExamCard is of type smart or normal examcard
        /// </summary>
        private bool smartType;
        /// <summary>
        /// List of configured coils
        /// </summary>
        private ObservableCollection<SelectableItemDTO> configuredCoils = new ObservableCollection<SelectableItemDTO>();
        /// <summary>
        /// Patient orientation for the examcard
        /// </summary>
        private ObservableCollection<SelectableItemDTO> patientOrientation =
            new ObservableCollection<SelectableItemDTO>();
        /// <summary>
        /// Gets the total predicted SED of the examcard
        /// </summary>
        private double predictedSED;
        /// <summary>
        /// Gets a value indicating the time to complete all scans in the
        /// examcard (excluding those are aborted or are invalid).
        /// </summary>
        private int examDuration;
        /// <summary>
        /// State of the lifetime of the object
        /// </summary>
        private bool disposed;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets/Sets the name of examcard
        /// </summary>
        [ViewModelToModel("ExamCard", "Name")]
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
        /// Gets/Sets the sub-anatomy of examcard
        /// </summary>
        [ViewModelToModel("ExamCard", "AnatomicRegion", "AnatomicalRegionConverter")]
        public string AnatomicRegion
        {
            get
            {
                return anatomicRegion;
            }
            set
            {
                if (anatomicRegion != value)
                {
                    anatomicRegion = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }
        /// <summary>
        /// Gets / Sets the anatomy of examcard
        /// </summary>
        [ViewModelToModel("ExamCard", "Anatomy", "AnatomyConverter")]
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
        /// Gets/Sets the list of anatomic regions for the specified anantomy
        /// </summary>
        [ViewModelToModel("ExamCard", "Anatomy", "AnatomyRegionListConverter")]
        public ObservableCollection<string> AnatomicRegionsList
        {
            get
            {
                return anatomicRegionsList;
            }
            set
            {
                if (anatomicRegionsList != value)
                {
                    anatomicRegionsList = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }       
        /// <summary>
        /// Gets/Sets the list of anatomies supported
        /// </summary>
        public ObservableCollection<string> AnatomiesList
        {
            get
            {
                return anatomiesList;
            }
            set
            {
                if (anatomiesList != value)
                {
                    anatomiesList = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }
        /// <summary>
        /// Gets/Sets the Laterality of examcard
        /// </summary>
        [ViewModelToModel("ExamCard", "Laterality")]
        public string Laterality
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
        /// Gets/Sets the laterlities list
        /// </summary>        
        public ObservableCollection<SelectableItemDTO> Lateralities
        {
            get
            {
                return lateralities;
            }
            set
            {
                if (lateralities != value)
                {
                    lateralities = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }
        /// <summary>
        /// Gets/Sets the review types associated with an examcard
        /// </summary>
        [ViewModelToModel("ExamCard", "ReviewRequired", KnownType = typeof(ECModel.ConfirmReviewType))]
        public ObservableCollection<ConfirmReviewTypeDTO> ReviewRequired
        {
            get
            {
                return reviewRequired;
            }
            set
            {
                if (reviewRequired != value)
                {
                    reviewRequired = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }
        /// <summary>
        /// Gets/Sets the lateraliy conflict of the  examcard
        /// </summary>
        [ViewModelToModel("ExamCard", "LateralityConflict")]
        public bool LateralityConflict
        {
            get
            {
                return lateralityConflict;
            }
            set
            {
                if (lateralityConflict != value)
                {
                    lateralityConflict = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }
        /// <summary>
        /// Gets/Sets the list of associated named geometries for the examcard
        /// </summary>
        [ViewModelToModel("ExamCard", "Geometries","NamedGeometriesConvertor")]
        public ObservableCollection<GeometryDTO> NamedGeometries
        {
            get
            {
                return namedGeometries;
            }
            set
            {
                if (namedGeometries != value)
                {
                    namedGeometries = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }
        /// <summary>
        /// Gets/Sets whether the posterior hold back is true or false
        /// </summary>
        [ViewModelToModel("ExamCard", "PosteriorHoldBack")]
        public bool PosteriorHoldBack
        {
            get
            {
                return posteriorHoldBack;
            }
            set
            {
                if (posteriorHoldBack != value)
                {
                    posteriorHoldBack = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }
        /// <summary>
        /// DockMode which is of type "Philips.PmsMR.Platform.ScannerContext.DockMode"
        /// converted to string
        /// </summary>
        [ViewModelToModel("ExamCard", "DockingMode", "EnumToStringConvertor")]
        public string DockingMode
        {
            get
            {
                return dockMode;
            }
            set
            {
                if (dockMode != value)
                {
                    dockMode = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }
        /// <summary>
        /// Gets Patient orientation which is converted from type "Philips.PmsMR.Platform.ScannerContext.PatientPosition"
        /// </summary>
        [ViewModelToModel("ExamCard", "PatientOrientation", "PatientOrientationConverter")]
        [ViewModelToModel("ExamCard", "Laterality", "PatientOrientationConverter")]
        [ViewModelToModel("ExamCard", "DockingMode", "PatientOrientationConverter")]
        public ObservableCollection<SelectableItemDTO> PatientOrientation
        {
            get
            {
                return patientOrientation;
            }
            set
            {
                if (patientOrientation != value)
                {
                    patientOrientation = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }
        /// <summary>
        /// Gets/Sets the heart rate defined in the examcard
        /// </summary>
        [ViewModelToModel("ExamCard", "HeartRate")]
        public int HeartRate
        {
            get
            {
                return heartRate;
            }
            set
            {
                if (heartRate != value)
                {
                    heartRate = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }
        /// <summary>
        /// Auto fill in heart rate.
        /// </summary>
        [ViewModelToModel("ExamCard", "AutoFillInHeartRate")]
        public bool AutoFillInHeartRate
        {
            get
            {
                return autoFillInHeartRate;
            }
            set
            {
                if (autoFillInHeartRate != value)
                {
                    autoFillInHeartRate = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }
        /// <summary>
        /// Gets/Sets whether the Auto fill in heart rate is allowed or not
        /// </summary>
        [ViewModelToModel("ExamCard", "AutoFillInHeartRateAllowed")]
        public bool AutoFillInHeartRateAllowed
        {
            get
            {
                return autoFillInHeartRateAllowed;
            }
            set
            {
                if (autoFillInHeartRateAllowed != value)
                {
                    autoFillInHeartRateAllowed = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }
        /// <summary>
        /// Gets/Sets the Align overlap
        /// </summary>
        [ViewModelToModel("ExamCard", "AlignOverlap")]
        public float AlignOverlap
        {
            get
            {
                return alignOverlap;
            }
            set
            {
                if (alignOverlap != value)
                {
                    alignOverlap = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }
        /// <summary>
        /// Gets/Sets the Geo-link propagation value
        /// </summary>
        [ViewModelToModel("ExamCard", "GeoLinkPropagation")]
        public bool GeoLinkPropagation
        {
            get
            {
                return geoLinkPropagation;
            }
            set
            {
                if (geoLinkPropagation != value)
                {
                    geoLinkPropagation = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }
        /// <summary>
        /// Gets/Sets whether the table to be used or not
        /// </summary>
        [ViewModelToModel("ExamCard", "TableUsage", "TableUsuageConvertor")]
        public bool TableUsage
        {
            get
            {
                return tableUsage;
            }
            set
            {
                if (tableUsage != value)
                {
                    tableUsage = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }
        /// <summary>
        /// Gets/Sets whether the examcard is intended to be used for paediatric
        /// patients or not.
        /// </summary>
        [ViewModelToModel("ExamCard", "IsPaediatric")]
        public bool Paediatric
        {
            get
            {
                return paediatric;
            }
            set
            {
                if (paediatric != value)
                {
                    paediatric = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }
        /// <summary>
        /// Gets/Sets the weight of the patient.
        /// </summary>
        [ViewModelToModel("ExamCard", "PatientWeight")]
        public double PatientWeight
        {
            get
            {
                return patientWeight;
            }
            set
            {
                if (patientWeight != value)
                {
                    patientWeight = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }
        /// <summary>
        /// Gets/Sets the unit of weight(Kgs or Lbs).
        /// </summary>
        public string WeightUnit
        {
            get
            {
                return weightUnit;
            }
            set
            {
                if (value != weightUnit)
                {
                    weightUnit = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }
        /// <summary>
        /// Denotes if the examcard is a smart examcard or normal examcard.
        /// </summary>
        [ViewModelToModel("ExamCard", ConvertorName = "SmartTypeConvertor")]
        public bool SmartType
        {
            get
            {
                return smartType;
            }
            set
            {
                if (smartType != value)
                {
                    smartType = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }
        /// <summary>
        /// Gets/Sets the list of coils configured in the system
        /// </summary>
        [ViewModelToModel("ExamCard", "SelectedCoils", "ConfiguredCoilsConverter")]
        public ObservableCollection<SelectableItemDTO> ConfiguredCoils
        {
            get
            {
                return configuredCoils;
            }
            set
            {
                if (configuredCoils != value)
                {
                    configuredCoils = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }
        /// <summary>
        /// Gets / sets the total predicted SED of the examcard
        /// </summary>
        [ViewModelToModel("ExamCard", "PredictedSED")]
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
        /// Gets / sets the duration of examcard
        /// </summary>
        [ViewModelToModel("ExamCard", "ScanDuration")]
        public int ExamDuration
        {
            get
            {
                return examDuration;
            }
            set
            {
                if (examDuration != value)
                {
                    examDuration = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Public Constructor
        /// </summary>
        public ExamCardPropertyDTO()
        {
            // Initialize the anatomies list
            ObservableCollection<string> list = new ObservableCollection<string>();
            List<string> anatomies = AnatomiesAnatomicalRegionHelper.GetAnatomies();
            foreach (string anatomy in anatomies)
            {
                list.Add(anatomy);
            }
            AnatomiesList = list;
            // Initialize the lateralities
            ObservableCollection<SelectableItemDTO> lateralityList = new ObservableCollection<SelectableItemDTO>();
            IList<string> lateralities = ResourceUtility.GetLateralities();
            foreach (string laterality in lateralities)
            {
                SelectableItemDTO dto = new SelectableItemDTO();
                dto.ID = laterality;
                dto.Caption = ResourceUtility.GetLateralityDisplayToText(laterality);
                lateralityList.Add(dto);
            }
            Lateralities = lateralityList;

            using (GeneralConfiguration generalConfiguration = new GeneralConfiguration())
            {
                weightUnit = generalConfiguration.PatientWeightUnit.ToString();
            }
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (namedGeometries != null)
                    {
                        foreach (GeometryDTO child in namedGeometries)
                        {
                            child.Dispose();
                        }
                    }
                    if (reviewRequired != null)
                    {
                        foreach (ConfirmReviewTypeDTO child in reviewRequired)
                        {
                            child.Dispose();
                        }
                    }
                    if (anatomiesList != null)
                    {
                        anatomiesList.Clear();
                        anatomiesList = null;
                    }
                    if (anatomicRegionsList != null)
                    {
                        anatomicRegionsList.Clear();
                        anatomicRegionsList = null;
                    }
                    if (ConfiguredCoils != null)
                    {
                        foreach (SelectableItemDTO child in ConfiguredCoils)
                        {
                            child.Dispose();
                        }
                        ConfiguredCoils.Clear();
                        ConfiguredCoils = null;
                    }
                    if (patientOrientation != null)
                    {
                        foreach (SelectableItemDTO child in patientOrientation)
                        {
                            child.Dispose();
                        }
                        patientOrientation.Clear();
                        patientOrientation = null;
                    }
                    if (lateralities != null)
                    {
                        foreach (SelectableItemDTO child in lateralities)
                        {
                            child.Dispose();
                        }
                        lateralities.Clear();
                        lateralities = null;
                    }
                }
                disposed = true;
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}

#region Revision History
// 2014-May-12  Anand K R
//              Initial version
// 2014-Jun-22  Chandralatha
//              Defined mapping between source and destination
// 2014-Jun-23  prasad v n
//              Renamed SubAnatomy to AnatomicRegion.
//              Added AnatomisList and AnatomicRegionsList.
// 2014-Jun-24  Chandralatha
//              Implemented IDiposable interface and release of resource.
// 2014-Jun-26  prasad v n
//              Added configured coils list
#endregion Revision History