using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class DefaultTo
    {
        dynamic DefaultTo42 = R.DefaultTo(42);

        [TestMethod]
        public void DefaultTo_Returns_The_Default_Value_If_Input_Is_Null() {
            Assert.AreEqual(42, DefaultTo42(R.Null));
        }

        [TestMethod]
        public void DefaultTo_Returns_The_Input_Value_If_It_Is_Not_Null() {
            Assert.AreEqual("a real value", DefaultTo42("a real value"));
        }

        [TestMethod]
        public void DefaultTo_Returns_The_Input_Value_Even_If_It_Is_Considered_Falsy() {
            Assert.AreEqual(string.Empty, DefaultTo42(string.Empty));
            Assert.AreEqual(0, DefaultTo42(0));
            Assert.AreEqual(false, DefaultTo42(false));
            CollectionAssert.AreEqual(new object[0], DefaultTo42(new object[0]));
        }

        [TestMethod]
        public void DefaultTo_Can_Be_Called_With_Both_Arguments_Directly() {
            Assert.AreEqual(42, R.DefaultTo(42, R.Null));
            Assert.AreEqual("a real value", R.DefaultTo(42, "a real value"));
        }
    }
}
