using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Add
    {
        [TestMethod]
        public void Add_Adds_Together_Two_Numbers() {
            Assert.AreEqual(R.Add(3, 7), 10);
        }

        [TestMethod]
        public void Add_Coerces_Its_Arguments_To_Numbers() {
            Assert.AreEqual(R.Add(true, false), 1);
        }

        [TestMethod]
        public void Add_Is_Curried() {
            var incr = R.Add(1);

            Assert.AreEqual(incr(42), 43);
        }
    }
}
