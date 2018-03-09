using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyProperty
{
    class MyControlViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        private void NotifyPropertyChanged (string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private string textBoxString;

        public string TextBoxString
        {
            get { return textBoxString; }
            set { textBoxString = value; NotifyPropertyChanged("TextBoxString"); }
        }

        private string labelString;

        public string LabelString
        {
            get { return labelString; }
            set { labelString = value; NotifyPropertyChanged("LabelString"); }
        }

    }
}
