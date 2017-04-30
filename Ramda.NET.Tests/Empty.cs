using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Dynamic;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Empty
    {
        public class Just
        {
            private readonly int num;
            private readonly dynamic always = R.Always(-1);

            public Just(int num) {
                this.num = num;
            }

            public Nothing Empty() {
                return new Nothing();
            }

            public int LastIndexOf() {
                return always();
            }

        }
        public class Nothing
        {
            public Nothing Empty() {
                return new Nothing();
            }
        }

        [TestMethod]
        [Description("Empty_Dispatches_To_\"Empty\"_Method")]
        public void Empty_Dispatches_To_Empty_Method() {
            Assert.IsInstanceOfType(R.Empty(new Nothing()), typeof(Nothing));
            Assert.IsInstanceOfType(R.Empty(new Just(123)), typeof(Nothing));
        }


        [TestMethod]
        public void Empty_Returns_Empty_Array_Given_Array() {
            CollectionAssert.AreEqual(R.Empty(new[] { 1, 2, 3 }), new int[0]);
        }

        [TestMethod]
        public void Empty_Returns_Empty_Object_Given_Object() {
            DynamicAssert.AreEqual(R.Empty(new { x = 1, y = 2 }), new ExpandoObject());
        }

        [TestMethod]
        public void Empty_Returns_Empty_String_Given_String() {
            Assert.AreEqual(R.Empty("abc"), string.Empty);
            Assert.AreEqual(R.Empty(new string(new[] { 'a', 'b', 'c' })), string.Empty);
        }
    }
}
