using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Flip
    {
        Func<string, string, string, string> f = (a, b, c) => string.Format("{0} {1} {2}", a, b, c);

        [TestMethod]
        public void Flip_Returns_A_Function_Which_Inverts_The_First_Two_Arguments_To_The_Supplied_Function() {
            var g = R.Flip(f);

            Assert.AreEqual(f("a", "b", "c"), "a b c");
            Assert.AreEqual(g("a", "b", "c"), "b a c");
        }

        [TestMethod]
        public void Flip_Returns_A_Curried_Function() {
            var g = R.Flip(f)("a");

            Assert.AreEqual(g("b", "c"), "b a c");
        }
    }
}
