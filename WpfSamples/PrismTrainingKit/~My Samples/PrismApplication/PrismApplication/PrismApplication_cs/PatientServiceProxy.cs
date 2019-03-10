#region Copyright Koninklijke Philips Electronics N.V. 2016
//
// All rights are reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
// FILENAME: filename.cs
//
#endregion

using PrismApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismApplication
{
    /// <summary>
    /// Dummy Proxy for PatientEngine
    /// </summary>
    public class PatientServiceProxy : IPatientService
    {
        private static int NumberOfInstances = 0;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler NewSession;
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler NewExamName;
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler TaskListUpdated;
        /// <summary>
        /// 
        /// </summary>
        public PatientServiceProxy()
        {
            System.Threading.Interlocked.Increment(ref NumberOfInstances);
            if (NumberOfInstances > 1)
            {
                throw new Exception("No Can't Do");
            }

            Task.Run(() =>
            {
                // Simulate push message after 10 seconds
                System.Threading.Thread.Sleep(3000);
            }).ContinueWith((task) =>
            { 
                if(NewSession != null)
                {
                    NewSession("Session ABC", EventArgs.Empty);
                }
            });
        }

        public IEnumerable<ITask> TaskList
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public void MoveToAcq()
        {
            if (NewExamName != null)
            {
                NewExamName("ExamName A", EventArgs.Empty);
            }

            IList<ITask> defaultTasks = new List<ITask>();
            defaultTasks.Add(new WorkflowTask() { Id = WorkflowTaskId.Prepare, Name = "Setup" });
            defaultTasks.Add(new WorkflowTask() { Id = WorkflowTaskId.Planning, Name = "Planning" });
            defaultTasks.Add(new WorkflowTask() { Id = WorkflowTaskId.Review, Name = "Review" });
            defaultTasks.Add(new WorkflowTask() { Id = WorkflowTaskId.Analysis, Name = "Analysis" });
            defaultTasks.Add(new WorkflowTask() { Id = WorkflowTaskId.Print, Name = "Print" });
            TaskList = defaultTasks;
            if (TaskListUpdated != null)
            {
                TaskListUpdated(this, EventArgs.Empty);
            }
        }
    }
}
