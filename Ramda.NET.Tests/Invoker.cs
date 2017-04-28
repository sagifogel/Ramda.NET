using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Invoker
    {
        public class ArrayConcater
        {
            public Array Value;

            public ArrayConcater(Array arr) {
                Value = arr;
            }

            public Array Concat(Array arr) {
                return Value.Concat(arr);
            }
        }

        private readonly dynamic concat2 = R.Invoker(2, "Concat");

        [TestMethod]
        public void Invoker_Returns_A_Function_With_Correct_Arity() {
            Assert.AreEqual(concat2.Length, 3);
        }

        [TestMethod]
        public void Invoker_Calls_The_Method_On_The_Object() {
            CollectionAssert.AreEqual(concat2(3, 4, new ArrayConcater(new[] { 1, 2 })), new[] { 1, 2, 3, 4 });
        }

        [TestMethod]
        public void Invoker_Throws_A_Descriptive_TypeError_If_Method_Does_Not_Exist() {
            try {
                R.Invoker(0, "Foo")(new { });
            }
            catch (MissingMethodException ex) {
                Assert.AreEqual(ex.Message, "target does not have a method named 'Foo'");
            }
        }

        [TestMethod]
        public void Invoker_Throws_A_Descriptive_TypeError_If_Method_Does_Not_Exist_On_AnonymousType() {
            try {
                R.Invoker(0, "Foo")(new { });
            }
            catch (MissingMethodException ex) {
                Assert.AreEqual(ex.Message, "target does not have a method named 'Foo'");
            }
        }

        [TestMethod]
        public void Invoker_Throws_A_Descriptive_TypeError_If_Method_Does_Not_Exist_On_Array() {
            try {
                R.Invoker(0, "Foo")(new[] { 1, 2, 3 });
            }
            catch (MissingMethodException ex) {
                Assert.AreEqual(ex.Message, "target does not have a method named 'Foo'");
            }
        }

        [TestMethod]
        public void Invoker_Throws_A_Descriptive_TypeError_If_Method_Does_Not_Exist_On_Array_With_Name_That_Exists_As_Property() {
            try {
                R.Invoker(0, "Length")(new[] { 1, 2, 3 });
            }
            catch (MissingMethodException ex) {
                Assert.AreEqual(ex.Message, "target does not have a method named 'Length'");
            }
        }

        [TestMethod]
        public void Invoker_Curries_The_Method_Call() {
            var expected = new[] { 1, 2, 3, 4 };
            var arrConcater = new ArrayConcater(new[] { 1, 2 });

            CollectionAssert.AreEqual(concat2(3)(4)(arrConcater), expected);
            CollectionAssert.AreEqual(concat2(3, 4)(arrConcater), expected);
            CollectionAssert.AreEqual(concat2(3)(4, arrConcater), expected);
        }
    }
}
