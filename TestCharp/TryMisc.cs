﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Runtime.Remoting.Messaging;

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

        internal static void TryObservableCollection()
        {
            IReadOnlyList<double> list = new List<double>{2,2,2};
            string bla = "First";
            ObservableCollection<Person> collection = new ObservableCollection<Person>();
            collection.CollectionChanged +=
                (sender, e) => { Console.WriteLine($"{((Person)(e.NewItems[0])).FirstName}"); };
            var person = new Person(){FirstName = "Alistair", LastName = "Maclean"};
            collection.Add(person);
            collection[0].FirstName = "NotAlistair";
        }
    }

    #region TryOverride
    internal interface IWhatever
    {
        void Display();
        void Silly();
    }

    internal abstract class WhateverAbstract : IWhatever
    {
        protected string Name { get; set; }

        protected void SetName()
        {
            Name = "AbstractTemporary";}

        public virtual void Display()
        {
            Console.WriteLine("Inside ABSTRACT::Display");
        }

        public abstract void Silly();
    }
    internal enum TestEnum { One = 1, Two };
    internal class WhateverConcrete : WhateverAbstract
    {
        
        public override void Silly()
        {
            Console.WriteLine("CONCRETE::Silly()");
        }

        public override void Display()
        {
            SetName();
            Console.WriteLine($"Inside CONCRETE::Display() - {Name}");
        }
    }

    internal class WhateverDoubleConcrete : WhateverConcrete
    {
        public WhateverDoubleConcrete(string name)
        {
            Name = name;
        }
    }
    #endregion

    #region TryObeservableCollection

    internal class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    #endregion

}