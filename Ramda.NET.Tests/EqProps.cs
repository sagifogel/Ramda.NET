using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class EqProps
    {
        [TestMethod]
        public void EqProps_Reports_Whether_Two_Objects_Have_The_Same_Value_For_A_Given_Property() {
            Assert.AreEqual(R.EqProps("Name", new { Name = "fred", Age = 10 }, new { Name = "fred", Age = 12 }), true);
            Assert.AreEqual(R.EqProps("Name", new { Name = "fred", Age = 10 }, new { Name = "franny", Age = 10 }), false);
        }

        [TestMethod]
        [Description("EqProps_Has_R.Equals_Semantics")]
        public void EqProps_Has_R_Equals_Semantics() {
            object nullObject = R.Null;

            Assert.AreEqual(R.EqProps("value", new { value = nullObject }, new { value = nullObject }), true);
            Assert.AreEqual(R.EqProps("value", new { value = new Just(new[] { 42 }) }, new { value = new Just(new[] { 42 }) }), true);
        }

        [TestMethod]
        public void EqProps_Is_Curried() {
            var sameName = R.EqProps("Name");

            Assert.AreEqual(sameName(new { Name = "fred", Age = 10 }, new { Name = "fred", Age = 12 }), true);
        }
    }
}
