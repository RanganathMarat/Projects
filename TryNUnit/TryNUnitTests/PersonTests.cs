using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryNUnit;
using NUnit.Framework;
namespace TryNUnit.Tests
{
    [TestFixture()]
    public class PersonTests
    {
        [Test()]
        public void GetFullNameTest()
        {
            Person P = new Person();
            P.FirstName = "Bla";
            P.LastName = "Bla2";
            string expected = "BlaBla2";
            Assert.AreEqual(P.GetFullName(), expected, "Failed");
        }

        [Test()]
        public void GetFullNameTest1()
        {
            Assert.Fail();
        }

    }
}
