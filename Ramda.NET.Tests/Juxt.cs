using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Juxt
    {
        private readonly Func<string> bye = () => "bye";
        private readonly Func<string> hello = () => "hello";

        [TestMethod]
        public void Juxt_Works_With_No_Functions_And_No_Values() {
            CollectionAssert.AreEqual(R.Juxt(new object[0])(), new object[0]);
        }

        [TestMethod]
        public void Juxt_Works_With_No_Functions_And_Some_Values() {
            CollectionAssert.AreEqual(R.Juxt(new object[0])(2, 3), new object[0]);
        }

        [TestMethod]
        public void Juxt_Works_With_1_Function_And_No_Values() {
            CollectionAssert.AreEqual(R.Juxt(new[] { hello })(), new[] { "hello" });
        }

        [TestMethod]
        public void Juxt_Works_With_1_Function_And_1_Value() {
            CollectionAssert.AreEqual(R.Juxt(new[] { R.Add(3) })(2), new[] { 5 });
        }

        [TestMethod]
        public void Juxt_Works_With_1_Function_And_Some_Values() {
            CollectionAssert.AreEqual(R.Juxt(new[] { R.Multiply(R.__) })(2, 3), new[] { 6 });
        }

        [TestMethod]
        public void Juxt_Works_With_Some_Functions_And_No_Values() {
            CollectionAssert.AreEqual(R.Juxt(new[] { hello, bye })(), new[] { "hello", "bye" });
        }

        [TestMethod]
        public void Juxt_Works_With_Some_Functions_And_1_Value() {
            CollectionAssert.AreEqual(R.Juxt(new[] { R.Multiply(2), R.Add(3) })(2), new[] { 4, 5 });
        }

        [TestMethod]
        public void Juxt_Works_With_Some_Functions_And_Some_Value() {
            CollectionAssert.AreEqual(R.Juxt(new[] { R.Add(R.__), R.Multiply(R.__) })(2, 3), new[] { 5, 6 });
        }

        [TestMethod]
        public void Juxt_Retains_The_Highest_Arity() {
            var f = R.Juxt(new[] { R.NAry(1, R.T), R.NAry(3, R.T), R.NAry(2, R.T) });

            Assert.AreEqual(f.Length, 3);
        }

        [TestMethod]
        public void Juxt_Returns_A_Curried_Function() {
            CollectionAssert.AreEqual(R.Juxt(new[] { R.Multiply(R.__), R.Add(R.__) })(2)(3), new[] { 6, 5 });
        }
    }
}
