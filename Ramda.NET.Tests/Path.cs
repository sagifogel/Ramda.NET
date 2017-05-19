using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Path
    {
        private dynamic deepObject = new { A = new { B = new { C = "C" } }, FalseVal = false, NullVal = (object)null, ArrayVal = new[] { "Arr" } };

        [TestMethod]
        [Description("Path_Takes_A_Path_And_An_Object_And_Returns_The_Value_At_The_Path_Or_Undefined")]
        public void Path_Takes_A_Path_And_An_Object_And_Returns_The_Value_At_The_Path_Or_Ramda_Null() {
            var obj = new {
                A = new {
                    B = new {
                        C = 100,
                        D = 200
                    },
                    E = new {
                        F = new[] { 100, 101, 102 },
                        G = "G"
                    },
                    H = "H"
                },
                I = "I",
                J = new[] { "J" }
            };

            Assert.AreEqual(R.Path(new[] { "A", "B", "C" }, obj), 100);
            Assert.AreEqual(R.Path(new string[0], obj), obj);
            Assert.AreEqual(R.Path(new object[] { "A", "E", "F", 1 }, obj), 101);
            Assert.AreEqual(R.Path(new object[] { "J", 0 }, obj), "J");
        }

        [TestMethod]
        [Description("Path_Gets_A_Deep_Property's_Value_From_Objects")]
        public void Path_Gets_A_Deep_Propertys_Value_From_Objects() {
            Assert.AreEqual(R.Path(new[] { "A", "B", "C" }, deepObject), "C");
            Assert.AreEqual(R.Path(new[] { "A" }, deepObject), deepObject.A);
        }

        [TestMethod]
        [Description("Path_Returns_Undefined_For_Items_Not_Found")]
        public void Path_Returns_R_Null_For_Items_Not_Found() {
            Assert.AreEqual(R.Path(new[] { "A", "B", "Foo" }, deepObject), R.@null);
            Assert.AreEqual(R.Path(new[] { "bar" }, deepObject), R.@null);
            Assert.AreEqual(R.Path(new[] { "A", "B", }, new { A = (object)null }), R.@null);
        }

        [TestMethod]
        public void Path_Works_With_Falsy_Items() {
            var toString = typeof(bool).GetMethod("ToString", Type.EmptyTypes);
            Assert.AreEqual(R.Path(new[] { "ToString" }, false).Method, toString);
        }
    }
}
