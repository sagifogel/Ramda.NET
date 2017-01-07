using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Concat
    {
        public class Z1
        {
            public string X { get; } = "Z1";

            public string Concat(Z2 that) => $"{X} {that.X}";
        }

        public class Z2
        {
            public string X { get; } = "Z2";
        }

        [TestMethod]
        public void Concat_Adds_Combines_The_Elements_Of_The_Two_Lists() {
            CollectionAssert.AreEqual((ICollection)R.Concat(new[] { "a", "b" }, new[] { "c", "d" }), new[] { "a", "b", "c", "d" });
            CollectionAssert.AreEqual((ICollection)R.Concat(new object[0], new[] { "c", "d" }), new[] { "c", "d" });
        }

        [TestMethod]
        public void Concat_Works_On_Strings() {
            Assert.AreEqual(R.Concat("foo", "bar"), "foobar");
            Assert.AreEqual(R.Concat("x", ""), "x");
            Assert.AreEqual(R.Concat("", "x"), "x");
            Assert.AreEqual(R.Concat("", ""), "");
        }

        [TestMethod]
        [Description("Concat_Delegates_To_Non-String_Object_With_A_Concat_Method,_As_Second_Param")]
        public void Concat_Delegates_To_Non_String_Object_With_A_Concat_Method_As_Second_Param() {
            Assert.AreEqual(R.Concat(new Z1(), new Z2()), "Z1 Z2");
        }


        [TestMethod]
        public void Concat_Is_Curried() {
            var conc123 = R.Concat(new[] { 1, 2, 3 });

            CollectionAssert.AreEqual(conc123(new[] { 4, 5, 6 }), new[] { 1, 2, 3, 4, 5, 6 });
        }


        [TestMethod]
        [Description("Concat_Is_Curried_Like_A_Binary_Operator, _That_Accepts_An_Initial_Placeholder")]

        public void Concat_Is_Curried_Like_A_Binary_Operator_That_Accepts_An_Initial_Placeholder() {
            var appendBar = R.Concat(R.__, "bar");

            Assert.IsInstanceOfType(appendBar, typeof(DynamicDelegate));
            Assert.AreEqual(appendBar("foo"), "foobar");
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [Description("Concat_Throws_If_Attempting_To_Combine_An_Array_With_A_Non-array")]
        public void Concat_Throws_If_Attempting_To_Combine_An_Array_With_A_Non_Array() {
            R.Concat(new[] { 1 }, 2);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [Description("Concat_Throws_If_Not_An_Array,_String,_Or_Object_With_A_Concat_Method")]
        public void Concat_Throws_If_Not_An_Array_String_Or_Object_With_A_Concat_Method() {
            R.Concat(new { }, new { });
        }
    }
}
