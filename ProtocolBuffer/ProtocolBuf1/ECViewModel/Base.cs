using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reflection;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace ProtocolBuf12.ECViewModel
{
    /// <summary>
    /// Base class for all classes which raises property changed notification
    /// </summary>
    [ProtoContract(ImplicitFields=ImplicitFields.AllPublic)]
    [ProtoInclude(10, typeof(CellInfoDTO))]
    [ProtoInclude(20, typeof(CipDTO))]
    [ProtoInclude(30, typeof(ConfirmReviewTypeDTO))]
    [ProtoInclude(40, typeof(ExamCardPropertyDTO))]
    [ProtoInclude(50, typeof(SelectableItemDTO))]
    [ProtoInclude(60, typeof(TransferPropertyDTO))]
    public class DTOBase : INotifyPropertyChanged, IDisposable
    {
        private List<object> tag;

        private IAdapter adapter;
        /// <summary>
        /// Flag to indicate if the instance of this class is disposed
        /// </summary>                    
        private bool disposed = false;
        /// <summary>
        /// 
        /// </summary>
        private bool setValidation = false;
        /// <summary>
        /// 
        /// </summary>
        public bool Validation
        {
            get
            {
                return setValidation;
            }
            set
            {
                setValidation = value;
                RaisePropertyChanged("Validated");
            }
        }
        /// <summary>
        /// IAdapter instance-maps the current dto with the datamodel modified.
        /// </summary>
        public IAdapter Adapter
        {
            get { return adapter; }
            set { adapter = value; }
        }
        /// <summary>
        /// Holds the reference to the datamodel instance to which it maps to.
        /// </summary>
        public List<object> Tag
        {
            get
            {
                return tag;
            }
            set
            {
                tag = value;
                SubscribeEvents();
            }
        }

        /// <summary>
        /// Disposes the members
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            // Take ourself off of the Finalization queue to prevent 
            // finalization code for this object from executing a second time.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (Tag != null)
                    {
                        UnsubscribeEvents();
                        (Tag as IList).Clear();
                        Tag = null;
                    }

                    if (adapter != null)
                    {
                        adapter.ReleaseResource();
                        adapter = null;
                    }
                }
                disposed = true;
            }
        }

        #region INotifyPropertyChanged Members
        /// <summary>
        /// Property changed event to be notified to the UI
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        /// <summary>
        /// Raise the property changed event given the caller name.
        /// </summary>        
        protected void RaisePropertyChanged(string nameOfProperty)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(nameOfProperty));
            }
        }

        /// <summary>
        /// Subscriptions to the Property Changed and Collectionchanged events of Model
        /// </summary>
        private void SubscribeEvents()
        {
            UnsubscribeEvents();
            if ((this.Tag != null) && (Tag.Count > 0))
            {
                for (int i = 0; i < Tag.Count; i++)
                {
                    object srcObject = Tag[i];
                    if (srcObject is INotifyPropertyChanged)
                    {
                        (srcObject as INotifyPropertyChanged).PropertyChanged += OnPropertyChanged;
                    }
                    if (srcObject is INotifyCollectionChanged)
                    {
                        (srcObject as INotifyCollectionChanged).CollectionChanged += OnCollectionChanged;
                    }
                }
            }
        }

        /// <summary>
        /// Unsubscriptions to the Property Changed and Collectionchanged events of Model
        /// </summary>
        private void UnsubscribeEvents()
        {
            if ((this.Tag != null) && (Tag.Count > 0))
            {
                for (int i = 0; i < Tag.Count; i++)
                {
                    object srcObject = Tag[i];
                    if (srcObject is INotifyPropertyChanged)
                    {
                        (srcObject as INotifyPropertyChanged).PropertyChanged -= OnPropertyChanged;
                    }
                    if (srcObject is INotifyCollectionChanged)
                    {
                        (srcObject as INotifyCollectionChanged).CollectionChanged -= OnCollectionChanged;
                    }
                }
            }
        }

        /// <summary>
        ///  Event Handler for the Collectionchanged events of Model
        /// </summary>
        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                if (adapter != null)
                {
                    adapter.Map(sender, this, e.NewStartingIndex, AdaptingOptions.Add);
                }
            }
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                if (adapter != null)
                {
                    adapter.Map(sender, this, e.OldStartingIndex, AdaptingOptions.Remove);
                }
            }
        }

        /// <summary>
        /// Event Handler for the PropertyChanged events of Model
        /// </summary>
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (adapter != null)
            {
                if (e.PropertyName == "Validated")
                {
                    adapter.Map(sender, this, false);
                }
                else
                {
                    adapter.Map(sender, this, e.PropertyName);
                }
            }
        }
    }

    /// <summary>
    /// Base class for all elements which has collection of subelements
    /// </summary>
    [ProtoContract(ImplicitFields=ImplicitFields.AllPublic)]
    [ProtoInclude(100, typeof(ExamCardDTO))]
    [ProtoInclude(200, typeof(StepDTO))]
    [ProtoInclude(300, typeof(ScanSetDTO))]
    public abstract class ElementBase : DTOBase
    {
        /// <summary>
        /// Parent ElementBase
        /// </summary>
        private ElementBase parent = null;
        /// <summary>
        /// Enabled, always unless specified.
        /// </summary>
        private bool enabled = true;
        /// <summary>
        /// Selected information
        /// </summary>
        private bool selected = false;
        /// <summary>
        /// Expanded. 
        /// </summary>
        private bool expanded = false;
        /// <summary>
        /// Invisible
        /// </summary>
        private bool invisible = false;
        /// <summary>
        /// Disposed
        /// </summary>
        private bool disposed = false;
        /// <summary>
        /// Constructor
        /// </summary>
        public ElementBase()
        {
            if (ChildElements != null)
            {
                ChildElements.CollectionChanged += OnChildElementsCollectionChanged;
            }
        }

        /// <summary>
        /// Sub elements. To be used by all classes which contains 
        /// collection of subelements
        /// </summary>
        public abstract ObservableCollection<ElementBase> ChildElements
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Selected
        {
            get
            {
                return selected;
            }
            set
            {
                selected = value;
                RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Enabled
        {
            get
            {
                return enabled;
            }
            set
            {
                enabled = value;
                RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ElementBase Parent
        {
            get
            {
                return parent;
            }
            set
            {
                parent = value;
                RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Invisible
        {
            get
            {
                return invisible;
            }
            set
            {
                invisible = value;
                RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Expanded
        {
            get
            {
                return expanded;
            }
            set
            {
                expanded = value;
                RaisePropertyChanged(MethodBase.GetCurrentMethod().Name.Substring(4));
            }
        }

        /// <summary>
        /// Get Ancestor of the current object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetAncestor<T>() where T : ElementBase
        {
            ElementBase parentDTO = this.parent;

            while (parentDTO != null)
            {
                T parentAsT = parentDTO as T;
                if (parentAsT != null)
                {
                    return parentAsT;
                }
                parentDTO = parentDTO.Parent;
            }

            return null;
        }

        /// <summary>
        /// Get all ancestors in its hierarchy
        /// </summary>
        public List<ElementBase> GetAncestors()
        {
            List<ElementBase> parents = new List<ElementBase>();
            ElementBase parentDTO = this.parent;
            while (parentDTO != null)
            {
                parents.Add(parentDTO);
                parentDTO = parentDTO.Parent;
            }
            return parents;
        }

        /// <summary>
        /// Force property changed notification for a property
        /// </summary>
        public void ForcePropertyChangeNotification(string propertyName)
        {
            RaisePropertyChanged(propertyName);
        }

        /// <summary>
        /// Cleanup all the child elements
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Parent = null;

                    if (ChildElements != null)
                    {
                        ChildElements.CollectionChanged -= OnChildElementsCollectionChanged;
                        foreach (ElementBase child in ChildElements)
                        {
                            child.Dispose();
                        }
                        ChildElements.Clear();
                    }
                    disposed = true;
                }
            }
            base.Dispose(true);
        }

        /// <summary>
        /// Change the child elements parent.
        /// </summary>
        private void OnChildElementsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // First remove the parent reference in OldItems (if any)
            if (e.OldItems != null)
            {
                foreach (ElementBase element in e.OldItems)
                {
                    if (element.Parent != null)
                    {
                        element.Parent = null;
                    }
                }
            }
            // Then add the parent reference in NewItems (if any)
            if (e.NewItems != null)
            {
                foreach (ElementBase element in e.NewItems)
                {
                    element.Parent = this as ElementBase;
                }
            }
        }
    }
}
