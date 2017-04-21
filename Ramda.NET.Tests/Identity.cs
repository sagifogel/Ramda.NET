using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Identity
    {
        [TestMethod]
        public void Identity_Returns_Its_First_Argument() {
            Assert.AreEqual(R.Identity(R.Null), R.Null);
            Assert.AreEqual(R.Identity("foo"), "foo");
        }

        [TestMethod]
        public void Identity_Has_Length_1() {
            Assert.AreEqual(R.Identity(R.__).Length, 1);
        }
    }
}
