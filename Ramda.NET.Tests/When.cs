using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class When
    {
        [TestMethod]
        public void When_Calls_The_WhenTrue_Function_If_The_Validator_Returns_A_Truthy_Value() {
            Assert.AreEqual(R.When(R.Is(typeof(int)), R.Add(1))(10), 11);
        }

        [TestMethod]
        public void When_Returns_The_Argument_Unmodified_If_The_Validator_Returns_A_Falsy_Value() {
            Assert.AreEqual(R.When(R.Is(typeof(int)), R.Add(1))("hello"), "hello");
        }

        [TestMethod]
        public void When_Returns_A_Curried_Function() {
            var ifIsNumber = R.When(R.Is(typeof(int)));

            Assert.AreEqual(ifIsNumber(R.Add(1))(15), 16);
            Assert.AreEqual(ifIsNumber(R.Add(1))("hello"), "hello");
        }
    }
}
