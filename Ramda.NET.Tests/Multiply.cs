using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Multiply
    {
        [TestMethod]
        public void Multiply_Adds_Together_Two_Numbers() {
            Assert.AreEqual(R.Multiply(6, 7), 42);
        }

        [TestMethod]
        public void Multiply_Is_Curried() {
            var dbl = R.Multiply(2);

            Assert.AreEqual(dbl(15), 30);
        }
    }
}
