using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Inc
    {
        [TestMethod]
        public void Inc_Increments_Its_Argument() {
            Assert.AreEqual(R.Inc(-1), 0);
            Assert.AreEqual(R.Inc(0), 1);
            Assert.AreEqual(R.Inc(1), 2);
            Assert.AreEqual(R.Inc(12.34), 13.34);
            Assert.AreEqual(R.Inc(int.MaxValue), int.MinValue);
        }
    }
}
