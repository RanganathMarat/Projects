using System;
using System.Globalization;

namespace TestCharp
{
    internal class TryMisc
    {
        internal static DateTime TryDateParse()
        {
            var timeAsString = "12:30 AM";
            if (string.IsNullOrEmpty(timeAsString))
            {
                return DateTime.MinValue;
            }
            DateTime dateTime = DateTime.MinValue;
            try
            {
                bool result = DateTime.TryParseExact(timeAsString, "h:mm tt", new DateTimeFormatInfo(),
                    DateTimeStyles.None, out dateTime);
                if (!result)
                {
                    // Log the failure
                }
            }
            catch (ArgumentException exception)
            {
                // Log the failure
            }
            return dateTime; 
        }
    }
}