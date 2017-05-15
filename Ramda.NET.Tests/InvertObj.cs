using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Dynamic;
using System.Collections;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class InvertObj
    {
        [TestMethod]
        public void InvertObj_Takes_A_List_Or_Object_And_Returns_An_Object() {
            Assert.IsInstanceOfType(R.InvertObj(new object[0]), typeof(ExpandoObject));
            Assert.IsInstanceOfType(R.InvertObj(new { }), typeof(ExpandoObject));
        }

        [TestMethod]
        public void InvertObj_Returns_An_Empty_Object_When_Applied_To_A_Primitive() {
            DynamicAssert.AreEqual(R.Invert(42), new ExpandoObject());
            DynamicAssert.AreEqual(R.Invert("abc"), new ExpandoObject());
        }

        [TestMethod]
        [Description("InvertObj_Returns_An_Empty_Object_When_Applied_To_Null/Undefined")]
        public void Invert_Returns_An_Empty_Object_When_Applied_To_Null() {
            DynamicAssert.AreEqual(R.InvertObj(R.@null), new ExpandoObject());
        }

        [TestMethod]
        [Description("InvertObj_Returns_The_Input's_Values_As_Keys, _And_Keys_As_Values_Of_An_Array")]
        public void Invert_Returns_The_Inputs_Values_As_Keys_And_Keys_As_Values_Of_An_Array() {
            DynamicAssert.AreEqual(R.InvertObj(new[] { "A", "B", "C" }), new { A = "0", B = "1", C = "2" });
            DynamicAssert.AreEqual(R.InvertObj(new { X = "A", Y = "B", Z = "C" }), new { A = "X", B = "Y", C = "Z" });
        }

        [TestMethod]
        public void InvertObj_Prefers_The_Last_Key_Found_When_Handling_Keys_With_The_Same_Value() {
            DynamicAssert.AreEqual(R.InvertObj(new[] { "A", "B", "A" }), new { A = "2", B = "1" });
            DynamicAssert.AreEqual(R.InvertObj(new { X = "A", Y = "B", Z = "A", _ID = "A" }), new { A = "_ID", B = "Y" });
        }

        [TestMethod]
        public void Invert_Is_Not_Destructive() {
            var input = new { X = "A", Y = "B", Z = "A", _ID = "A" };

            R.InvertObj(input);
            DynamicAssert.AreEqual(input, new { X = "A", Y = "B", Z = "A", _ID = "A" });
        }
    }
}
