using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Unless
    {
        [TestMethod]
        public void Unless_Calls_The_WhenFalse_Function_If_The_Validator_Returns_A_Falsy_Value() {
            CollectionAssert.AreEqual(R.Unless(R.IsArrayLike(R.__), R.Of(R.__))(10), new[] { 10 });
        }

        [TestMethod]
        public void Unless_Returns_The_Argument_Unmodified_If_The_Validator_Returns_A_Truthy_Value() {
            CollectionAssert.AreEqual(R.Unless(R.IsArrayLike(R.__), R.Of(R.__))(new[] { 10 }), new[] { 10 });
        }

        [TestMethod]
        public void Unless_Returns_A_Curried_Function() {
            CollectionAssert.AreEqual(R.Unless(R.IsArrayLike(R.__))(R.Of(R.__))(10), new[] { 10 });
            CollectionAssert.AreEqual(R.Unless(R.IsArrayLike(R.__))(R.Of(R.__))(new[] { 10 }), new[] { 10 });
        }
    }
}
