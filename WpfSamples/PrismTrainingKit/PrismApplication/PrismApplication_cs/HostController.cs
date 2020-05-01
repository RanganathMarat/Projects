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
    public class HostController : IDisposable
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
        /// 
        /// </summary>
        private IPatientService _serviceProxy;
        /// <summary>
        /// 
        /// </summary>
        private ThemeManager _themeManager;
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
        public HostController(IUnityContainer container,
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

            // Components
            _themeManager = container.Resolve<ThemeManager>();

            // Proxies (If Any)
            InitializeProxies();

            // Subscribe to the events (if any).
            this._eventAggregator.GetEvent<MoveToAcqEvent>().Subscribe(OnMoveToAcqRequested, true);
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
                    if (_eventAggregator != null)
                    {
                        _eventAggregator.GetEvent<MoveToAcqEvent>().Unsubscribe(OnMoveToAcqRequested);
                    }
                    _disposed = true;
                }
            }
        }

        /// <summary>
        /// Initializes all proxies
        /// </summary>
        private void InitializeProxies()
        {
            _serviceProxy = _container.Resolve<IPatientService>();
            _serviceProxy.NewSession += (o,e) =>
            {
                _eventAggregator.GetEvent<SessionChangedEvent>().Publish((string)o);
            };
        }

        /// <summary>
        /// Event Handler
        /// </summary>
        private void OnMoveToAcqRequested(object obj)
        {
            _serviceProxy.MoveToAcq();
            _themeManager.ApplyTheme(ThemeManager.Theme.Dark);
        }
    }
}

#region Revision History
// dd-mmm-yyyy  Name
//              Initial version
#endregion Revision History
