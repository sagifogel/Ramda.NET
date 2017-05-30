using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class PropIs
    {
        [TestMethod]
        public void PropIs_Returns_True_If_The_Specified_Object_Property_Is_Of_The_Given_Type() {
            Assert.IsTrue(R.PropIs(typeof(int), "Value", new { Value = 1 }));
        }

        [TestMethod]
        public void PropIs_Returns_False_Otherwise() {
            Assert.IsFalse(R.PropIs(typeof(string), "Value", new { Value = 1 }));
            Assert.IsFalse(R.PropIs(typeof(string), "Value", new { }));
        }
    }
}
