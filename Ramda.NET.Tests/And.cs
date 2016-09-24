using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class And
    {
        [TestMethod]
        public void And_Compares_Two_Values_With_conditional_AND_Operator() {
            Assert.AreEqual(R.And(true, true), true);
            Assert.AreEqual(R.And(true, false), false);
            Assert.AreEqual(R.And(false, true), false);
            Assert.AreEqual(R.And(false, false), false);
        }

        [TestMethod]
        public void And_Is_Curried() {
            var halfTruth = R.And(true);

            Assert.AreEqual(halfTruth(false), false);
            Assert.AreEqual(halfTruth(true), true);
        }
    }
}
