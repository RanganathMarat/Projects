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
using Prism.Regions;
using PrismApplication.Interfaces;
using System;

namespace PrismApplication
{
    /// <summary>
    /// Description of this Module
    /// </summary>
    public class DefaultTaskModule : ITaskPlugin, IDisposable
    {
        private readonly IUnityContainer _container;
        private IRegionManager _regionManager;

        private bool _disposed;
        private DefaultTaskController _defaultTaskController;
        private readonly ITask _parentTask;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="container">
        /// Container for Dependency Injection
        /// </param>
        /// <param name="regionManager">
        /// Manager that manages UI regions
        /// </param>
        /// <param name="task">
        /// Parent ITask for this module
        /// </param> 
        public DefaultTaskModule(IUnityContainer container, IRegionManager regionManager, ITask task)
        {
            // Always create isolated child container
            _container = container.CreateChildContainer();
            _regionManager = regionManager;

            // Do other stuffs
            _parentTask = task;
            _container.RegisterInstance<ITask>(_parentTask, new ContainerControlledLifetimeManager());
        }

        /// <summary>
        /// IModule.Initialize for intitialization of the Module
        /// </summary>
        public void Initialize()
        {
            // 1. Register your Views
            var view = _container.Resolve<DefaultTaskView>();
            _container.RegisterInstance<DefaultTaskView>(view, new ContainerControlledLifetimeManager());
            _regionManager.RegisterViewWithRegion(RegionNames.TaskPluginRegion,
                                                       RegionHelper.GetInstance(view).GetContentDelegate);

            // 2. Initialize controller
            _defaultTaskController = _container.Resolve<DefaultTaskController>();

            // 3. Do other stuffs

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionId"></param>
        public void Load(string sessionId)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionId"></param>
        public void UnLoad(string sessionId)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public ITask Task
        {
            get
            {
                return _parentTask;
            }
        }

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.Collect(2);
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
                    var view = _container.Resolve<DefaultTaskView>();
                    var regionHelper = RegionHelper.GetInstance(view);
                    if (_regionManager != null)
                    {
                        // Remove all Views
                        _regionManager.Regions[RegionNames.TaskPluginRegion].Deactivate(view);
                        _regionManager.Regions[RegionNames.TaskPluginRegion].Remove(view);
                        _regionManager = null;
                    }
                    if (regionHelper != null)
                    {
                        regionHelper.Dispose();
                    }
                    _defaultTaskController.Dispose();
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
