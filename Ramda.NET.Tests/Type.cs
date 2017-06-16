using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Type
    {
        [TestMethod]
        [Description("Type_\"Array\"_If_Given_An_Array_Literal")]
        public void Type_Array_If_Given_An_Array_Literal() {
            Assert.AreEqual(R.Type(new[] { 1, 2, 3 }), "Int32[]");
        }

        [TestMethod]
        [Description("Type_\"Object\"_If_Given_An_Object_Literal")]
        public void Type_Object_If_Given_An_Object_Literal() {
            Assert.AreEqual(R.Type(new { Batman = "na na na na na na na" }), "anonymous");
        }

        [TestMethod]
        [Description("Type_\"RegExp\"_If_Given_A_RegExp_Literal")]
        public void Type_RegExp_If_Given_A_RegExp() {
            Assert.AreEqual(R.Type(new Regex("[A-z]")), "Regex");
        }

        [TestMethod]
        [Description("Type_Number_If_Given_Numeric_Value")]
        public void Type_Int_If_Given_Int_Value() {
            Assert.AreEqual(R.Type(4), "Int32");
        }

        [TestMethod]
        public void Type_String_If_Given_String_Literal() {
            Assert.AreEqual(R.Type("Gooooodd Mornning Ramda!!"), "String");
        }

        [TestMethod]
        public void Type_String_If_Given_String_Object() {
            Assert.AreEqual(R.Type(new string("Gooooodd Mornning Ramda!!".ToCharArray())), "String");
        }

        [TestMethod]
        [Description("Type_\"Null\"_If_Given_The_Null_Value")]
        public void Type_Null_If_Given_The_R_Null_Value() {
            Assert.AreEqual(R.Type(R.@null), "null");
        }
    }
}
