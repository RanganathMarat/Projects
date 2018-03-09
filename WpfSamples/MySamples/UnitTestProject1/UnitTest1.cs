using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TryBinding;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            PersonViewModel personVm = new PersonViewModel() { FirstName = "First", LastName = "Last", FullName = "" };
            personVm.MergeName.Execute(new object());
            string fullname = personVm.FullName;
            Assert.AreEqual("First Last", fullname);
        }
    }
}
