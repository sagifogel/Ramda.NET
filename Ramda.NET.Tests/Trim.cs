using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Trim
    {
        [TestMethod]
        public void Trim_Trims_A_String() {
            Assert.AreEqual(R.Trim("   xyz  "), "xyz");
        }
    }
}
