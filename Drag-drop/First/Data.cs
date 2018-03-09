using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First
{
    class Data : INotifyPropertyChanged
    {

        private int entryCounter = 0;
        private int entryCounter2 = 0;
        private string mouseState = null;

        public int EntryCounter
        {
            get
            {
                return entryCounter;
            }

            set
            {
                entryCounter = value;
                NotifyPropertyChanged("EntryCounter");
            }
        }

        public int EntryCounter2
        {
            get
            {
                return entryCounter2;
            }

            set
            {
                this.entryCounter2 = value;
                NotifyPropertyChanged("EntryCounter2");
            }
        }

        public string MouseState
        {
            get
            {
                return mouseState;
            }

            set
            {
                mouseState = value;
                NotifyPropertyChanged("MouseState");
            }
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            if( PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
