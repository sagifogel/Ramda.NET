using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class F
    {
        [TestMethod]
        public void F_Always_Return_False() {
            Assert.IsFalse(R.F());
            Assert.IsFalse(R.F(10));
            Assert.IsFalse(R.F(true));
        }
    }
}
