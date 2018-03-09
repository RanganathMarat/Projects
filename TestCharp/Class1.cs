using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TestCharp.DatabaseEntities;

namespace TestCharp
{
    public class Class1
    {
        public decimal Calculate(decimal amount, int type, int years) 
        {
            // Check if invalid argument is passed. Check this first.
            if (type == 0 || type > 4)
            {
                throw new ArgumentException($"Incorrect type={type} passed");
            }

            // Initiliaze with a default discount
            decimal disc = 0.05m;
            disc = years > 5 ? disc : (decimal)years / 100;
            disc = disc * new DatabaseEntities().GetConstantFactorForType(type);

            decimal result = 0.0m;
            // A switch statement used for easy extensibility and better readibility.
            switch (type)
            {
                case 1:
                {
                    result = amount;
                    break;
                }
                case 2:
                {
                    result = (amount - (0.1m * amount)) * (1 - disc);
                    break;
                }
                case 3:
                {
                    result = (0.7m * amount) * (1 - disc);
                    break;
                }
                case 4:
                {
                    result = (amount - (0.5m * amount)) * (1 - disc);
                    break;
                }
                //default is unnecessary since range check is done earlier.
            }
            return result;
        }
    }

    public class DatabaseEntities
    {
        public decimal GetConstantFactorForType(int type)
        {
            //assuming this is a DB access code
            return 1;
        }

    }
}
