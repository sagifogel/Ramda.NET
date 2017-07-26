using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class HasIn : AbstractHas
    {
        [TestMethod]
        public void HasIn_Returns_A_Function_That_Checks_The_Appropriate_Property() {
            var nm = R.HasIn("Name");

            Assert.IsInstanceOfType(nm, typeof(DynamicDelegate));
            Assert.IsTrue(nm(fred));
            Assert.IsFalse(nm(anon));
        }

        [TestMethod]
        public void HasIn_Checks_Properties_From_The_Prototype_Chain() {
            var bob = new Person();

            Assert.IsTrue(R.HasIn("Age", bob));
        }

        [TestMethod]
        public void HasIn_Works_Properly_When_Called_With_Two_Arguments() {
            Assert.IsTrue(R.HasIn("Name", fred));
            Assert.IsFalse(R.HasIn("Name", anon));
        }
    }
}
