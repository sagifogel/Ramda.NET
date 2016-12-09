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

        [TestMethod]
        public void Add_Is_Commutative() {
            int a = 10;
            int b = 10;
            Assert.AreEqual(R.Add(a, b), R.Add(b, a));
        }

        [TestMethod]
        public void Add_Is_Transitive() {
            int a = 10;
            int b = 10;
            int c = 15;
            Assert.AreEqual(R.Add(a, R.Add(b, c)), R.Add(R.Add(a, b), c));
        }

        [TestMethod]
        public void Add_IsIdentity() {
            var a = 9;

            Assert.AreEqual(R.Add(a, 0), a);
            Assert.AreEqual(R.Add(0, a), a);
        }
    }
}
