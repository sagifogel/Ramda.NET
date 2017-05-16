using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class NthArg
    {
        [TestMethod]
        public void NthArg_Returns_A_Function_Which_Returns_Its_Nth_Argument() {
            Assert.AreEqual(R.NthArg<string>(0)("foo", "bar"), "foo");
            Assert.AreEqual(R.NthArg<string>(1)("foo", "bar"), "bar");
        }

        [TestMethod]
        public void NthArg_Accepts_Negative_Offsets() {
            Assert.AreEqual(R.NthArg<string>(-1)("foo", "bar"), "bar");
            Assert.AreEqual(R.NthArg<string>(-2)("foo", "bar"), "foo");
            Assert.AreEqual(R.NthArg<string>(-3)("foo", "bar"), R.@null);
        }

        [TestMethod]
        [Description("NthArg_Returns_A_Function_With_Length_N_+_1_When_N_>=_0")]
        public void NthArg_Returns_A_Function_With_Length_N_Plus_1_When_N_Is_Greater_Or_Equals_To_0() {
            Assert.AreEqual(R.NthArg<string>(0).Length, 1);
            Assert.AreEqual(R.NthArg<string>(1).Length, 2);
            Assert.AreEqual(R.NthArg<string>(2).Length, 3);
            Assert.AreEqual(R.NthArg<string>(3).Length, 4);
        }

        [TestMethod]
        [Description("NthArg_Returns_A_Function_With_Length_1_When_N_<_0")]
        public void NthArg_Returns_A_Function_With_Length_1_When_N_Is_Lower_Than_0() {
            Assert.AreEqual(R.NthArg<string>(-1).Length, 1);
            Assert.AreEqual(R.NthArg<string>(-2).Length, 1);
            Assert.AreEqual(R.NthArg<string>(-3).Length, 1);
        }

        [TestMethod]
        public void NthArg_Returns_A_Curried_Function() {
            Assert.AreEqual(R.NthArg<string>(1)("foo", "bar"), R.NthArg<string>(1)("foo")("bar"));
            Assert.AreEqual(R.NthArg<string>(2)("foo", "bar", "baz"), R.NthArg<string>(2)("foo")("bar")("baz"));
        }
    }
}
