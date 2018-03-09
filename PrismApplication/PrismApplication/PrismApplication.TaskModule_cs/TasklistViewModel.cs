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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PrismApplication
{
    /// <summary>
    /// Dummy ViewModel for your View
    /// </summary>
    public class TasklistViewModel:INotifyPropertyChanged
    {
        private ObservableCollection<ITask> _taskList = new ObservableCollection<ITask>();

        private int _selectedTask = -1;

        public event PropertyChangedEventHandler PropertyChanged;
        
        public ObservableCollection<ITask> TaskList
        {
            get { return _taskList; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        public void SelectTaskCommand(ITask task)
        {
            int index = _taskList.IndexOf(task);
            SelectedTask = index;
        }

        /// <summary>
        /// 
        /// </summary>
        public int SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                _selectedTask = value;
                RaisePropertyChanged();
            }
        }

        protected void RaisePropertyChanged([CallerMemberName]string memberName = "")
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(memberName));
            }
        }
    }
}

#region Revision History
// dd-mmm-yyyy  Name
//              Initial version
#endregion Revision History
