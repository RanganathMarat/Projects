using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UserControlAntiPattern.ViewModel
{
    public class PersonViewModel : INotifyPropertyChanged
    {
        Person person = null;
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public PersonViewModel()
        {
            person = new Person();
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
                NotifyPropertyChanged("FirstName");
            }
        }

    }


    class Person
    {

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
    }

}
