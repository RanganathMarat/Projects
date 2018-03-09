using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryNUnit;
using Xunit;
namespace TryNUnit.Tests
{
    public class PersonTests
    {
        [Fact()]
        public void GetFullNameTest2()
        {
            Person P = new Person();
            P.FirstName = "Bla";
            P.LastName = "Bla2";
            string expected = "BlaBla2";
            Assert.Contains(expected, P.GetFullName());
        }
    }
}
