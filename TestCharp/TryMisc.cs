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

        internal static void TryOverride()
        {
            IWhatever whatever = new WhateverConcrete();
            whatever.Display();
            whatever.Silly();
        }
    }

    internal interface IWhatever
    {
        void Display();
        void Silly();
    }

    internal abstract class WhateverAbstract : IWhatever
    {
        public virtual void Display()
        {
            Console.WriteLine("Inside ABSTRACT::Display");
        }

        public abstract void Silly();
    }

    internal class WhateverConcrete : WhateverAbstract
    {
        public override void Silly()
        {
            Console.WriteLine("CONCRETE::Silly()");
        }

        public override void Display()
        {
            Console.WriteLine("Inside CONCRETE::Display()");
        }
    }
}