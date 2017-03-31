using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Dec
    {
        [TestMethod]
        public void Dec_Decrements_Its_Argument() {
            Assert.AreEqual(R.Dec(-1), -2);
            Assert.AreEqual(R.Dec(0), -1);
            Assert.AreEqual(R.Dec(1), 0);
            Assert.AreEqual(R.Dec(12.34), 11.34);
            unchecked {
                Assert.AreEqual(R.Dec(int.MinValue), int.MaxValue);
            }
        }
    }
}
