using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Length
    {
        private static object[] Arguments(params object[] arguments) {
            return arguments;
        }

        [TestMethod]
        public void Length_Returns_The_Length_Of_A_List() {
            Assert.AreEqual(R.Length<object>(new object[0]), 0);
            Assert.AreEqual(R.Length<string>(new[] { "a", "b", "c", "d" }), 4);
        }

        [TestMethod]
        public void Length_Returns_The_Length_Of_A_String() {
            Assert.AreEqual(R.Length(string.Empty), 0);
            Assert.AreEqual(R.Length("xyz"), 3);
        }

        [TestMethod]
        public void Length_Returns_The_Length_Of_A_Function() {
            Assert.AreEqual(R.Length(new Action(() => { })), 0);
            Assert.AreEqual(R.Length(new Func<int, int, int, int>((x, y, z) => z)), 3);
        }

        [TestMethod]
        public void Length_Returns_The_Length_Of_An_Arguments_Object() {
            Assert.AreEqual(R.Length<object>(Arguments()), 0);
            Assert.AreEqual(R.Length<object>(Arguments("x", "y", "z")), 3);
        }

        [TestMethod]
        [Description("Length_Returns_NaN_For_Value_Of_Unexpected_Type")]
        public void Length_Returns_Minus_One_For_Value_Of_Unexpected_Type() {
            Assert.AreEqual(R.Length(0), -1);
            Assert.AreEqual(R.Length(new { }), -1);
            Assert.AreEqual(R.Length(R.@null), -1);
        }

        [TestMethod]
        [Description("Length_Returns_NaN_For_Value_Of_Unexpected_Type")]
        public void Length_Returns_Minus_1_For_Length_Property_Of_Unexpected_Type() {
            Assert.AreEqual(R.Length(new { Length = string.Empty }), -1);
            Assert.AreEqual(R.Length(new { Length = "1.23" }), -1);
            Assert.AreEqual(R.Length(new { Length = (object)null }), -1);
            Assert.AreEqual(R.Length(new { Length = R.@null }), -1);
            Assert.AreEqual(R.Length(new { }), -1);
        }
    }
}
