using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCharp
{
    public static class TryLinq
    {
        public static void Func()
        {
            int[] i = new int[] { 0, 1, 2 };
            ArrayList items = new ArrayList(4);
            items.Add("asdf");
            items.Add("bla1");

            // Use the where operator when you need to return a sequence that matches a certain predicate
            Console.WriteLine((from string name in items.Cast<string>() where name.Equals("bla") select name).FirstOrDefault<string>());
            Console.Write((items.Cast<string>()).Where(x => string.Equals(x, "bla")).FirstOrDefault());
            foreach (var name in (items.Cast<string>()).Where(x=>string.Equals(x,"bla")))
            {
                Console.WriteLine(name);
            }

            //Below makes a better usage with where operator
            // Select operator should be used when you want to project a given sequence into a different sequence.
            // Or even for creating a new type using the "new" operator
            foreach (var name in (items.Cast<string>()).Select<string, string>((x) => { return (string.Equals(x, "bla")) ? x : null; }))
            {
                Console.WriteLine(name);
            }

            
            //items.Distinct();
            i.Distinct();
        }
    }
}
