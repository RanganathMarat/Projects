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
using Prism.Modularity;
using Prism.Regions;
using System;

namespace PrismApplication
{
    /// <summary>
    /// Description of this Module
    /// </summary>
    public class HostControllerModule : IModule, IDisposable
    {
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;

        private bool _disposed;
        private HostController _hostController;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="container">
        /// Container for Dependency Injection
        /// </param>
        /// <param name="regionManager">
        /// Manager that manages UI regions
        /// </param>
        public HostControllerModule(IUnityContainer container, IRegionManager regionManager)
        {
            // Always create isolated child container
            _container = container.CreateChildContainer();
            _regionManager = regionManager;

            // Do other stuffs
        }

        /// <summary>
        /// IModule.Initialize for intitialization of the Module
        /// </summary>
        public void Initialize()
        {
            // 1. Register your Views
            // No Views to Register...

            // 2. Initialize controller
            _hostController = _container.Resolve<HostController>();

            // 3. Do other stuffs

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
                    _hostController.Dispose();
                    // Can safely Dispose container here as this is child container
                    _container.Dispose();
                    _disposed = true;
                }
            }
        }
    }
}

#region Revision History
// dd-mmm-yyyy  Name
//              Initial version
#endregion Revision History
