using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MyPrismSample.Annotations;

namespace MyPrismSample.Module
{
    public class Author : INotifyPropertyChanged
    {
        private string _authorName;
        private string _bookTitle;

        public string AuthorName
        {
            get { return _authorName; }
            set
            {
                if (value == _authorName) return;
                _authorName = value;
                OnPropertyChanged();
            }
        }

        public string BookTitle
        {
            get { return _bookTitle; }
            set
            {
                if (_bookTitle == value) return;
                _bookTitle = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
