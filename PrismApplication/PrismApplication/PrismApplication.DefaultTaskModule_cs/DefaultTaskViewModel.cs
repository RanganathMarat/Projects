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
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PrismApplication
{
    /// <summary>
    /// Dummy ViewModel for your View
    /// </summary>
    public class DefaultTaskViewModel:INotifyPropertyChanged
    {
        private string _sessionId = "NoPateint";
        private string _message = Guid.NewGuid().ToString();
        private IEventAggregator _eventAggregator;

        public event PropertyChangedEventHandler PropertyChanged;

        public DefaultTaskViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public string SessionId
        {
            get { return _sessionId; }
            set { _sessionId = value; RaisePropertyChanged(); }
        }

        public string Message
        {
            get { return _message; }
            set { _message = value; RaisePropertyChanged(); }
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
