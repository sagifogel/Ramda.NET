using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Props
    {
        private readonly object obj = new { A = 1, B = 2, C = 3, D = 4, E = 5, F = 6 };

        [TestMethod]
        public void Props_Returns_Empty_Array_If_No_Properties_Requested() {
            CollectionAssert.AreEqual(R.Props(new string[0], obj), new string[0]);
        }

        [TestMethod]
        public void Props_Returns_Values_For_Requested_Properties() {
            CollectionAssert.AreEqual(R.Props(new[] { "A", "E" }, obj), new[] { 1, 5 });
        }

        [TestMethod]
        public void Props_Preserves_Order() {
            CollectionAssert.AreEqual(R.Props(new[] { "F", "C", "E" }, obj), new[] { 6, 3, 5 });
        }

        [TestMethod]
        [Description("Props_Returns_Undefined_For_Nonexistent_Properties")]
        public void Props_Returns_R_Null_For_Nonexistent_Properties() {
            var ps = R.Props(new[] { "A", "nonexistent" }, obj);

            Assert.AreEqual(ps.Length, 2);
            Assert.AreEqual(ps[0], 1);
            Assert.AreEqual(ps[1], R.@null);
        }

        [TestMethod]
        public void Props_Is_Curried() {
            CollectionAssert.AreEqual(R.Props(new[] { "A", "B" })(obj), new[] { 1, 2 });
        }
    }
}
