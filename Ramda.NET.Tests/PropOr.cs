using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class PropOr
    {
        class Person
        {
            public int Age { get; set; }
        }

        class Bob : Person
        {
        }

        private readonly object anon = new { Age = 99 };
        private readonly object fred = new { Name = "Fred", Age = 23 };
        private readonly dynamic nm = R.PropOr("Unknown", "Name");

        [TestMethod]
        public void PropOr_Returns_A_Function_That_Fetches_The_Appropriate_Property() {
            Assert.IsInstanceOfType(nm, typeof(DynamicDelegate));
            Assert.AreEqual(nm(fred), "Fred");
        }

        [TestMethod]
        public void PropOr_Returns_The_Default_Value_When_The_Property_Does_Not_Exist() {
            Assert.AreEqual(nm(anon), "Unknown");
        }

        [TestMethod]
        [Description("PropOr_Returns_The_Default_Value_When_The_Object_Is_Nil")]
        public void PropOr_Returns_The_Default_Value_When_The_Object_Is_Null() {
            Assert.AreEqual(nm(R.@null), "Unknown");
        }

        [TestMethod]
        public void PropOr_Does_Not_Return_Properties_From_The_Prototype_Chain() {
            var bob = new Bob();
            var res = R.PropOr(100, "Age", bob);

            Assert.AreEqual(res, 100);
        }
    }
}
