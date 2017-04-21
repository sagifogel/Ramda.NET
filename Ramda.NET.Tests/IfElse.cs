using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class IfElse
    {
        private readonly Func<int, int> t = a => a + 1;
        private readonly Func<object, object> identity = a => a;
        private readonly Func<object, bool> isArray = a => a.GetType().IsArray;
        private Func<object, bool> isInt = a => a.GetType().Equals(typeof(int));

        [TestMethod]
        public void IfElse_Calls_The_Truth_Case_Function_If_The_Validator_Returns_A_Truthy_Value() {
            Assert.AreEqual(R.IfElse(isInt, t, identity)(10), 11);
        }

        [TestMethod]
        public void IfElse_Calls_The_False_Case_Function_If_The_Validator_Returns_A_Falsy_Value() {
            Assert.AreEqual(R.IfElse(isInt, t, identity)("hello"), "hello");
        }

        [TestMethod]
        public void IfElse_Calls_The_True_Case_On_Array_Items_And_The_False_Case_On_Non_Array_Items() {
            var list = new object[] { new[] { 1, 2, 3, 4, 5 }, 10, new[] { 0, 1 }, 15 };
            var arrayToLength = R.Map(R.IfElse(isArray, R.Prop("Length"), identity));

            CollectionAssert.AreEqual(arrayToLength(list), new[] { 5, 10, 2, 15 });
        }

        [TestMethod]
        public void IfElse_Passes_The_Arguments_To_The_True_Case_Function() {
            var v = new Func<bool>(() => true);
            var onTrue = new Action<object, object>((a, b) => {
                Assert.AreEqual(a, 123);
                Assert.AreEqual(b, "abc");
            });

            R.IfElse(v, onTrue, identity)(123, "abc");
        }

        [TestMethod]
        public void IfElse_Passes_The_Arguments_To_The_False_Case_Function() {
            var v = new Func<bool>(() => true);
            var onFalse = new Action<object, object>((a, b) => {
                Assert.AreEqual(a, 123);
                Assert.AreEqual(b, "abc");
            });

            R.IfElse(v, identity, onFalse)(123, "abc");
        }

        [TestMethod]
        [Description("IfElse_Returns_A_Function_Whose_Arity_Equals_The_Max_Arity_Of_The_Three_Arguments_To_\"ifElse\"")]
        public void IfElse_Returns_A_Function_Whose_Arity_Equals_The_Max_Arity_Of_The_Three_Arguments_To_IfElse() {
            Func<int> a0 = () => 0;
            Func<int, int> a1 = x => x;
            Func<int, int, int> a2 = (x, y) => x + y;

            Assert.AreEqual(R.IfElse(a0, a1, a2).Length, 2);
            Assert.AreEqual(R.IfElse(a0, a2, a1).Length, 2);
            Assert.AreEqual(R.IfElse(a1, a0, a2).Length, 2);
            Assert.AreEqual(R.IfElse(a1, a2, a0).Length, 2);
            Assert.AreEqual(R.IfElse(a2, a0, a1).Length, 2);
            Assert.AreEqual(R.IfElse(a2, a1, a0).Length, 2);
        }

        [TestMethod]
        public void IfElse_Returns_A_Curried_Function() {
            var ifIsNumber = R.IfElse(isInt);
            var fn = R.IfElse(R.Gt(R.__), R.Subtract(R.__), R.Add(R.__));

            Assert.AreEqual(ifIsNumber(t, identity)(15), 16);
            Assert.AreEqual(ifIsNumber(t, identity)("hello"), "hello");
            Assert.AreEqual(fn(2)(7), 9);
            Assert.AreEqual(fn(2, 7), 9);
            Assert.AreEqual(fn(7)(2), 5);
            Assert.AreEqual(fn(7, 2), 5);
        }
    }
}
