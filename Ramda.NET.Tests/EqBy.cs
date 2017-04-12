using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class EqBy
    {
        [TestMethod]
        public void EqBy_Determines_Whether_Two_Values_Map_To_The_Same_Value_In_The_Codomain() {
            Assert.AreEqual(R.EqBy(Math.Abs, 5, 5), true);
            Assert.AreEqual(R.EqBy(Math.Abs, 5, -5), true);
            Assert.AreEqual(R.EqBy(Math.Abs, -5, 5), true);
            Assert.AreEqual(R.EqBy(Math.Abs, -5, -5), true);
            Assert.AreEqual(R.EqBy(Math.Abs, 42, 99), false);
        }

        [TestMethod]
        [Description("EqBy_Has_R.Equals_Semantics")]
        public void EqBy_Has_R_Equals_Semantics() {
            object nullObject = R.Null;
            var identity = R.Identity(R.__);

            Assert.AreEqual(R.EqBy(identity, new { value = nullObject }, new { value = nullObject }), true);
            Assert.AreEqual(R.EqBy(identity, new { value = new Just(new[] { 42 }) }, new { value = new Just(new[] { 42 }) }), true);
        }
    }
}
