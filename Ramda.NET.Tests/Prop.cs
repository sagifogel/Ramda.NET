using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Prop
    {
        [TestMethod]
        public void Prop_Returns_A_Function_That_Fetches_The_Appropriate_Property() {
            var nm = R.Prop("Name");
            var fred = new { Name = "Fred", Age = 23 };

            Assert.IsInstanceOfType(nm, typeof(DynamicDelegate));
            Assert.AreEqual(nm(fred), "Fred");
        }
    }
}
