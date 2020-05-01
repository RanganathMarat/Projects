#region Copyright Koninklijke Philips Electronics N.V. 2016
//
// All rights are reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
// FILENAME: filename.cs
//
#endregion

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PrismApplication
{
    /// <summary>
    /// Dummy ViewModel for your View
    /// </summary>
    public class ScanlistViewModel:INotifyPropertyChanged
    {
        private string examCardName = "NoName";

        private string scanName = string.Empty;

        public event PropertyChangedEventHandler PropertyChanged;

        public string ScanName
        {
            get { return scanName; }
            set { scanName = value; RaisePropertyChanged(); }
        }

        public string ExamCardName
        {
            get { return examCardName; }
            set { examCardName = value; RaisePropertyChanged(); }
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
