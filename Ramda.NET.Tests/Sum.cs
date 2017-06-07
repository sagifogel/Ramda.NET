using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Sum
    {
        [TestMethod]
        public void Sum_Adds_Together_The_Array_Of_Numbers_Supplied() {
            Assert.AreEqual(R.Sum(new[] { 1, 2, 3, 4 }), 10);
        }

        [TestMethod]
        public void Sum_Does_Not_Save_The_State_Of_The_Accumulator() {
            Assert.AreEqual(R.Sum(new[] { 1, 2, 3, 4 }), 10);
            Assert.AreEqual(R.Sum(new[] { 1 }), 1);
            Assert.AreEqual(R.Sum(new[] { 5, 5, 5, 5, 5 }), 25);
        }
    }
}
