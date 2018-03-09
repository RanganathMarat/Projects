using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TryBinding
{
    public class PersonViewModel : INotifyPropertyChanged
    {
        #region Command Implementation
        private ICommand mergeName;

        public ICommand MergeName
        {
            get
            { return mergeName; }
            set { mergeName = value; }
        }

        #endregion
        Person person = null;
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if( PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public PersonViewModel()
        {
            person = new Person();
            //Initialize command object
            this.mergeName = new DelegateCommand((obj) => { this.FullName = this.FirstName + " " + this.LastName; }, (obj) => { return true; });
        }

        public string FirstName
        {
            get
            {
                return person.FirstName;
            }
            set
            {
                person.FirstName = value;
                //Thread.Sleep(50000);
                NotifyPropertyChanged("FirstName");
            }
        }

        public string LastName
        {
            get
            {
                return person.LastName;
            }
            set
            {
                person.LastName = value;
                NotifyPropertyChanged("LastName");
            }
        }

        public string FullName
        {
            get
            {
                return person.FullName;
            }
            set
            {
                person.FullName = value;
                NotifyPropertyChanged("FullName");
            }

        }
    }

    class DelegateCommand : ICommand
    {
        private Action<object> executeDelegate;
        private Func<object, bool> canExecuteDelegate;

        public DelegateCommand(Action<object> executeDelegate, Func<object, bool> canExecuteDelegate)
        {
            this.canExecuteDelegate = canExecuteDelegate;
            this.executeDelegate = executeDelegate;
        }

        public bool CanExecute(object parameter)
        {
            return canExecuteDelegate(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            executeDelegate(parameter);
        }
    }
}
