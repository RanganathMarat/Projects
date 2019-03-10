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

namespace PrismApplication.Interfaces
{
    /// <summary>
    /// Defines an event to indicate Session Id change.
    /// The event payload is the SessionId.
    /// </summary>
    public class ExamChangedEvent : PubSubEvent<string>
    {
    }
}

#region Revision History
// dd-mmm-yyyy  Name
//              Initial version
#endregion Revision History