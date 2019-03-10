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
using System.Collections.Generic;

namespace PrismApplication
{
    /// <summary>
    /// Class to manage all your Views
    /// </summary>
    public class TaskModuleController : IDisposable
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
        private IList<ITaskPlugin> _pluginList;
        /// <summary>
        /// 
        /// </summary>
        private IList<ITaskPlugin> _toDispose;
        /// <summary>
        /// 
        /// </summary>
        private string _sessionId = string.Empty;
        /// <summary>
        /// ViewModel
        /// </summary>
        private TasklistViewModel _viewModel;
        /// <summary>
        /// ViewModel
        /// </summary>
        private TaskHeaderView _headerView;
        /// <summary>
        /// ViewModel
        /// </summary>
        private TaskContainerView _containerView;
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
        public TaskModuleController(IUnityContainer container,
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

            // Create ViewModel
            this._viewModel = _container.Resolve<TasklistViewModel>();
            // Bind both Views
            this._containerView = container.Resolve<TaskContainerView>();
            _containerView.DataContext = _viewModel;
            this._headerView = container.Resolve<TaskHeaderView>();
            _headerView.DataContext = _viewModel;
            _pluginList = new List<ITaskPlugin>();

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
            _proxy.TaskListUpdated += (o, e) =>
            {
                IEnumerable<ITask> newList = _proxy.TaskList;
                ClearTaskList();
                CreateTaskList(newList);
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

        private void CreateTaskList(IEnumerable<ITask> newList)
        {
            _pluginList.Clear();
            _viewModel.TaskList.Clear();
            foreach (var task in newList)
            {
                ITaskPlugin taskPlugin = new DefaultTaskModule(_container, _regionManager, task);
                taskPlugin.Initialize();

                _viewModel.TaskList.Add(task);
                _pluginList.Add(taskPlugin);

                taskPlugin.Load(_sessionId);
            }
            //2.3. Update default selected task (Mocked to 0)
            _viewModel.SelectedTask = 0;
        }

        private void ClearTaskList()
        {
            //[1] Reset controller fields
            _viewModel.TaskList.Clear();
            foreach (ITaskPlugin pluginModule in _pluginList)
            {
                pluginModule.Dispose();
            }
            _pluginList.Clear();
            // 2.3 Reset default selected task
            _viewModel.SelectedTask = -1;
        }

        //private void CreateTaskList(IEnumerable<ITask> newList)
        //{
        //    _pluginList.Clear();
        //    _viewModel.TaskList.Clear();
        //    foreach (var task in newList)
        //    {
        //        ITaskPlugin taskPlugin;
        //        //var oldPlugin = _toDispose.FirstOrDefault(plugin => plugin.Task.Id == task.Id);
        //        // check if plugin for current task already exists in 'dispose' list
        //        //if (oldPlugin != null)
        //        //{
        //        //    // Unload and remove the old plugin from the 'dispose' list
        //        //    oldPlugin.UnLoad(_sessionId);
        //        //    _toDispose.Remove(oldPlugin);
        //        //    // Recylce the old plugin as new plugin
        //        //    taskPlugin = oldPlugin;
        //        //}
        //        //else
        //        //{
        //            // create new if plugin does not exist
        //            taskPlugin = new DefaultTaskModule(_container, _regionManager, task);
        //            taskPlugin.Initialize();
        //        //}
        //        // Add both task/plugin to List
        //        _viewModel.TaskList.Add(task);
        //        _pluginList.Add(taskPlugin);
        //        taskPlugin.Load(_sessionId);
        //    }
        //    //2.3. Update default selected task (Mocked to 0)
        //    _viewModel.SelectedTask = 0;

        //    // Finally dispose the old un-recycled (leftovers) Task Plugins
        //    foreach (var taskPlugin in _toDispose)
        //    {
        //        IDisposable disposable = (IDisposable) taskPlugin;
        //        if (disposable != null)
        //        {
        //            // Disposes the control itself
        //            taskPlugin.Dispose();
        //        }
        //    }
        //    _toDispose.Clear();
        //}

        //private void ClearTaskList()
        //{
        //    //[1] Reset controller fields
        //    _viewModel.TaskList.Clear();
        //    _toDispose = new List<ITaskPlugin>();
        //    foreach (ITaskPlugin pluginModule in _pluginList)
        //    {
        //        _toDispose.Add(pluginModule);
        //    }
        //    _pluginList.Clear();
        //    // 2.3 Reset default selected task
        //    _viewModel.SelectedTask = -1;

        //    // Note: _toDispose is kept for recycling later
        //}
    }
}

#region Revision History
// dd-mmm-yyyy  Name
//              Initial version
#endregion Revision History
