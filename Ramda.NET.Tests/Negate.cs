using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Negate
    {
        [TestMethod]
        public void Negate_Negates_Its_Argument() {
            Assert.AreEqual(R.Negate(int.MaxValue), int.MinValue + 1);
            Assert.AreEqual(R.Negate(-1), 1);
            Assert.AreEqual(R.Negate(1), -1);
        }
    }
}
