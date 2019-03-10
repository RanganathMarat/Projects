using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestCharp
{
    internal class TryObservableCollection
    {
        private readonly ObservableCollection<string> _statusMessages = new ObservableCollection<string>();
        private static int _count = 0;

        public ObservableCollection<string> StatusMessages
        {
            get { return _statusMessages; }
        }

        public void AddToCollection()
        {
            Console.WriteLine("Adding...");
            StatusMessages.Add("Entry" + _count.ToString());
        }

        public void RemoveFromCollection()
        {
            if (_statusMessages.Count > 0)
            {
                StatusMessages.RemoveAt(_statusMessages.Count - 1);
            }
        }

        public void SubscribeToCollectionChanged()
        {
            StatusMessages.CollectionChanged +=
                (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) =>
                    Console.WriteLine("Change" + " " + e.Action);
        }

    }
}
