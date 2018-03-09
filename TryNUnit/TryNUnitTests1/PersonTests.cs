using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryNUnit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace TryNUnit.Tests
{
    [TestClass()]
    public class PersonTests
    {
        [TestMethod()]
        public void GetFullNameTest1()
        {
            Person P = new Person();
            P.FirstName = "Bla";
            P.LastName = "Bla2";
            string expected = "BlaBla2";
            Assert.AreEqual(P.GetFullName(), expected, "Failed");
        }
    }
}
