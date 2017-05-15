using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Dynamic;
using System.Collections;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Invert
    {
        [TestMethod]
        public void Invert_Takes_A_List_Or_Object_And_Returns_An_Object_Of_Lists() {
            var inverted = (IDictionary<string, object>)R.Invert(new[] { 0 });
            var keys = R.Keys(inverted);

            Assert.IsInstanceOfType(R.Invert(new object[0]), typeof(ExpandoObject));
            Assert.IsInstanceOfType(R.Invert(new { }), typeof(ExpandoObject));
            Assert.IsTrue(R.Is(typeof(string[]), inverted[keys[keys.Length - 1]]));
        }

        [TestMethod]
        public void Invert_Returns_An_Empty_Object_When_Applied_To_A_Primitive() {
            DynamicAssert.AreEqual(R.Invert(42), new ExpandoObject());
            DynamicAssert.AreEqual(R.Invert("abc"), new ExpandoObject());
        }

        [TestMethod]
        [Description("Invert_Returns_An_Empty_Object_When_Applied_To_Null/Undefined")]
        public void Invert_Returns_An_Empty_Object_When_Applied_To_Null() {
            DynamicAssert.AreEqual(R.Invert(R.@null), new ExpandoObject());
        }

        [TestMethod]
        [Description("Invert_Returns_The_Input's_Values_As_Keys,_And_Keys_As_Values_Of_An_Array")]
        public void Invert_Returns_The_Inputs_Values_As_Keys_And_Keys_As_Values_Of_An_Array() {
            DynamicAssert.AreEqual(R.Invert(new[] { "A", "B", "C" }), new { A = new[] { "0" }, B = new[] { "1" }, C = new[] { "2" } });
            DynamicAssert.AreEqual(R.Invert(new { X = "A", Y = "B", Z = "C" }), new { A = new[] { "X" }, B = new[] { "Y" }, C = new[] { "Z" } });
        }

        [TestMethod]
        public void Invert_Puts_Keys_That_Have_The_Same_Value_Into_The_Appropriate_An_Array() {
            IDictionary<string, object> inverted = R.Invert(new { X = "A", Y = "B", Z = "A", _ID = "A" });
            var res = inverted["A"];

            Assert.IsTrue(R.IndexOf("X", res) >= 0);
            DynamicAssert.AreEqual(R.Invert(new[] { "A", "B", "A" }), new { A = new[] { "0", "2" }, B = new[] { "1" } });
            Assert.IsTrue(R.IndexOf("Z", res) >= 0);
            Assert.IsTrue(R.IndexOf("_ID", res) >= 0);
            CollectionAssert.AreEqual((ICollection)inverted["B"], new[] { "Y" });
        }

        [TestMethod]
        public void Invert_Is_Not_Destructive() {
            var input = new { X = "A", Y = "B", Z = "A", _ID = "A" };

            R.Invert(input);
            DynamicAssert.AreEqual(input, new { X = "A", Y = "B", Z = "A", _ID = "A" });
        }
    }
}
