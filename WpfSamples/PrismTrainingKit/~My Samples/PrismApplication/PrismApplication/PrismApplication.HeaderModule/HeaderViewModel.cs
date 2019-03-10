#region Copyright Koninklijke Philips Electronics N.V. 2016
//
// All rights are reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
// FILENAME: filename.cs
//
#endregion

using Prism.Events;
using PrismApplication.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PrismApplication
{
    /// <summary>
    /// Dummy ViewModel for your View
    /// </summary>
    public class HeaderViewModel:INotifyPropertyChanged
    {
        private string _sessionId = "NoPateint";
        private IEventAggregator _eventAggregator;

        public event PropertyChangedEventHandler PropertyChanged;

        public HeaderViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public string SessionId
        {
            get { return _sessionId; }
            set { _sessionId = value; RaisePropertyChanged(); }
        }

        public void MoveToAcqCommand()
        {
            _eventAggregator.GetEvent<MoveToAcqEvent>().Publish(null);
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
