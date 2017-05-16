using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Not
    {
        [TestMethod]
        public void Not_Reverses_Argument() {
            Assert.IsTrue(R.Not(false));
            Assert.IsFalse(R.Not(true));
            Assert.IsTrue(R.Not(R.Not(true)));
        }
    }
}
