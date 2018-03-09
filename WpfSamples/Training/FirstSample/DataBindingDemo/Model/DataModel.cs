using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBindingDemo.Model
{
   public class DataModel :System.ComponentModel.INotifyPropertyChanged
    {
        private string state;
        private int number;

        public int Number
        {
            get { return number; }
            set
            {
                if (value != number)
                {
                    number = value;
                    OnPropertyChanged("Number");
                }
            }
        }



        public string State
        {
            get { return state; }
            set 
            {
                if (value != state)
                {
                    state = value;
                    OnPropertyChanged("State");
                }
            }
        }


        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
