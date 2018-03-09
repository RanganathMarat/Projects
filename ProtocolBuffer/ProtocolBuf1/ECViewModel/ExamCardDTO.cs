using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

using System.Collections.ObjectModel;
using System.Reflection;

namespace ProtocolBuf12.ECViewModel
{
    /// <summary>
    /// Class for showing UI elements of Examcards
    /// </summary>
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class ExamCardDTO : ElementBase
    {
        /// <summary>
        /// Examcard property details
        /// </summary>
        private ExamCardPropertyDTO examCardProperty = new ExamCardPropertyDTO();
        /// <summary>
        /// List of scansets
        /// </summary>
        private ObservableCollection<ElementBase> scansets = new ObservableCollection<ElementBase>();
        /// <summary>
        /// Name of the examcard object
        /// </summary>
        private string name;
        /// <summary>
        /// Description of the examcard
        /// </summary>
        private string examCardDescription;
        /// <summary>
        /// State of the lifetime of the object
        /// </summary>
        private bool disposed = false;
        /// <summary>
        /// Properties related to transfer
        /// </summary>
        [ViewModelToModel("ExamCard")]
        public ExamCardPropertyDTO ExamCardProperty
        {
            get
            {
                return examCardProperty;
            }
            set
            {
                if (examCardProperty != value)
                {
                    examCardProperty = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
                }
            }
        }

        /// <summary>
        /// Child elements are scansets in the case of an examcard
        /// </summary>
        [ViewModelToModel("ExamCard", "Instruments\\ScanSet", KnownType = typeof(ScanSetDTO))]
        public override ObservableCollection<ElementBase> ChildElements
        {
            get
            {
                return scansets;
            }
        }

        /// <summary>
        /// Name of the Examcard
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
        /// Denotes the extra information related to an examcard.
        /// </summary>
        [ViewModelToModel("ExamCard", "Name")]
        public string ExamCardDescription
        {
            get
            {
                return examCardDescription;
            }
            set
            {
                if (examCardDescription != value)
                {
                    examCardDescription = value;
                    RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
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
                    if (examCardProperty != null)
                    {
                        examCardProperty.Dispose();
                        examCardProperty = null;
                    }
                }
                disposed = true;
            }
            base.Dispose(disposing);
        }
    }
}
