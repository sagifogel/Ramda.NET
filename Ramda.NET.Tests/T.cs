using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class T
    {
        [TestMethod]
        public void T_Always_Return_True() {
            Assert.IsTrue(R.T());
            Assert.IsTrue(R.T(10));
            Assert.IsTrue(R.T(true));
        }
    }
}
