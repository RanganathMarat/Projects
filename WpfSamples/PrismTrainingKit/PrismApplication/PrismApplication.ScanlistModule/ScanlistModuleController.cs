#region Copyright Koninklijke Philips Electronics N.V. 2016
//
// All rights are reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
// FILENAME: filename.cs
//
#endregion

using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;
using PrismApplication.Interfaces;
using System;

namespace PrismApplication
{
    /// <summary>
    /// Class to manage all your Views
    /// </summary>
    public class ScanlistModuleController : IDisposable
    {
        #region Prism Related Fields
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;
        #endregion #region Prism Related Fields

        // Your fields/components/proxies comes here : 
        #region Private Fields
        /// <summary>
        /// 
        /// </summary>
        private bool _disposed;
        /// <summary>
        /// 
        /// </summary>
        private string _sessionId = string.Empty;
        /// <summary>
        /// ViewModel
        /// </summary>
        private ScanlistViewModel _viewModel;
        /// <summary>
        /// View
        /// </summary>
        private ScanlistView _scanlistView;
        /// <summary>
        /// Some Proxy
        /// </summary>
        private IPatientService _proxy;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="container">
        /// Container for Dependency Injection
        /// </param>
        /// <param name="regionManager">
        /// Manager that manages UI regions
        /// </param>
        /// <param name="eventAggregator">
        /// Aggregator for all Events
        /// </param>
        public ScanlistModuleController(IUnityContainer container,
                                    IRegionManager regionManager,
                                    IEventAggregator eventAggregator)
        {
            if (container == null) throw new ArgumentNullException("container");
            if (regionManager == null) throw new ArgumentNullException("regionManager");
            if (eventAggregator == null) throw new ArgumentNullException("eventAggregator");

            // Prism related components
            this._container = container;
            this._regionManager = regionManager;
            this._eventAggregator = eventAggregator;

            // View/ViewModel Bind
            this._viewModel = _container.Resolve<ScanlistViewModel>();
            this._scanlistView = _container.Resolve<ScanlistView>();
            _scanlistView.DataContext = _viewModel;

            // Proxies (If Any)
            InitializeProxies();

            // Subscribe to all events (if any).
            this._eventAggregator.GetEvent<SessionChangedEvent>().Subscribe(OnSessionChanged, true);
        }

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose pattern
        /// </summary>
        /// <param name="disposing">
        /// Information if the object is being disposed by GC or user triggered.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!_disposed)
                {
                    // Unsubscribe all events
                    if (_eventAggregator != null)
                    {
                        _eventAggregator.GetEvent<SessionChangedEvent>().Unsubscribe(OnSessionChanged);
                    }

                    // Stop/Unsubscribe Proxy
                    _proxy = null;

                    _disposed = true;
                }
            }
        }

        /// <summary>
        /// Initializes all proxies
        /// </summary>
        private void InitializeProxies()
        {
            _proxy = _container.Resolve<IPatientService>();
            _proxy.NewExamName += (o, e) =>
            {
                _viewModel.ExamCardName = (string)o;
            };
        }

        /// <summary>
        /// EventHandler
        /// </summary>
        private void OnSessionChanged(string sessionId)
        {
            _sessionId = sessionId;
            // Do something
        }
    }
}

#region Revision History
// dd-mmm-yyyy  Name
//              Initial version
#endregion Revision History
