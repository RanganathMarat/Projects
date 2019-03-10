using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismApplication.Interfaces
{
    /// <summary>
    /// Dummy
    /// </summary>
    public interface IPatientService
    {
        /// <summary>
        /// 
        /// </summary>
        event EventHandler NewSession;

        /// <summary>
        /// 
        /// </summary>
        event EventHandler NewExamName;

        /// <summary>
        /// 
        /// </summary>
        event EventHandler TaskListUpdated;

        /// <summary>
        /// 
        /// </summary>
        void MoveToAcq();

        /// <summary>
        /// 
        /// </summary>
        IEnumerable<ITask> TaskList
        {
            get;
            set;
        }
    }
}
