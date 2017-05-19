using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class PathOr
    {
        private dynamic deepObject = new { A = new { B = new { C = "C" } }, FalseVal = false, NullVal = (object)null, ArrayVal = new[] { "Arr" } };

        [TestMethod]
        public void PathOr_Takes_A_Path_And_An_Object_And_Returns_The_Value_At_The_Path_Or_The_Default_Value() {
            var obj = new { A = new { B = new { C = 100, D = 200 }, E = new { F = new[] { 100, 101, 102 }, G = "G" }, H = "H" }, I = "I", J = new[] { "J" } };

            Assert.AreEqual(R.PathOr("Unknown", new[] { "A", "B", "C" }, obj), 100);
            DynamicAssert.AreEqual(R.PathOr("Unknown", new object[0], obj), obj);
            Assert.AreEqual(R.PathOr("Unknown", new object[] { "A", "E", "F", 1 }, obj), 101);
            Assert.AreEqual(R.PathOr("Unknown", new object[] { "J", 0 }, obj), "J");
            Assert.AreEqual(R.PathOr("Unknown", new object[] { "J", 1 }, obj), "Unknown");
            Assert.AreEqual(R.PathOr("Unknown", new[] { "A", "B", "C" }, R.@null), "Unknown");
        }

        [TestMethod]
        [Description("PathOr_Gets_A_Deep_Property's_Value_From_Objects")]
        public void PathOr_Gets_A_Deep_Propertys_Value_From_Objects() {
            Assert.AreEqual(R.PathOr("Unknown", new[] { "A", "B", "C" }, deepObject), "C");
            Assert.AreEqual(R.PathOr("Unknown", new[] { "A" }, deepObject), deepObject.A);
        }

        [TestMethod]
        public void PathOr_Returns_The_Default_Value_For_Items_Not_Found() {
            Assert.AreEqual(R.PathOr("Unknown", new[] { "A", "B", "FOO" }, deepObject), "Unknown");
            Assert.AreEqual(R.PathOr("Unknown", new[] { "BAR" }, deepObject), "Unknown");
        }

        [TestMethod]
        [Description("PathOr_Returns_The_Default_Value_For_Null/Undefined")]
        public void PathOr_Returns_The_Default_Value_For_R_Null() {
            Assert.AreEqual(R.PathOr("Unknown", new[] { "ToString" }, R.@null), "Unknown");
        }

        [TestMethod]
        public void PathOr_Works_With_Falsy_Items() {
            var toString = typeof(bool).GetMethod("ToString", Type.EmptyTypes);

            Assert.AreEqual(R.PathOr("Unknown", new[] { "ToString" }, false).Method, toString);
        }

        [TestMethod]
        public void PathOr_Is_Curried() {
            Assert.AreEqual(R.PathOr("Unknown", new[] { "ArrayVal", "0" })(deepObject), "Arr");
        }
    }
}