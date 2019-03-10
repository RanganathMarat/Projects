using Prism.Modularity;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PrismApplication.Interfaces
{
    /// <summary>
    /// MOCK
    /// TODO: To be replaced by actual Workflow Task implementation
    /// </summary>
    public class WorkflowTask : ITask, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        public string Id
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsSelected
        {
            get;
            set;
        }

        protected void RaisePropertyChanged([CallerMemberName]string memberName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(memberName));
            }
        }
    }

    /// <summary>
    /// Interface for the Worflow Task
    /// </summary>
    public interface ITask
    {
        /// <summary>
        /// Id of the Task
        /// </summary>
        string Id
        { get; set; }

        /// <summary>
        /// Name of the Task
        /// </summary>
        string Name
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool IsSelected
        {
            get;
            set;
        }
    }

    /// <summary>
    /// MOCK
    /// TODO: To be replaced by actual task id
    /// </summary>
    public static class WorkflowTaskId
    {
        /// <summary>
        /// 
        /// </summary>
        public const string Prepare = "Prepare";

        /// <summary>
        /// 
        /// </summary>
        public const string Planning = "Planning";

        /// <summary>
        /// 
        /// </summary>
        public const string Review = "Review";

        /// <summary>
        /// 
        /// </summary>
        public const string Analysis = "Analysis";

        /// <summary>
        /// 
        /// </summary>
        public const string Print = "Print";

        /// <summary>
        /// 
        /// </summary>
        public const string None = "";
    }

    public interface ITaskPlugin : IModule, IDisposable
    {
        /// <summary>
        /// <see cref="ITask"/> to which this plugin belongs.
        /// </summary>
        ITask Task
        { get; }

        /// <summary>
        /// Loads the plugin control with new sessionid
        /// </summary>
        /// <param name="sessionId">
        /// session id to load
        /// </param>
        void Load(string sessionId);

        /// <summary>
        /// Unloads any previous session related info
        /// </summary>
        /// <param name="sessionId">
        /// session id to unload
        /// </param>
        void UnLoad(string sessionId);
    }
}
