using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class IsNil
    {
        [TestMethod]
        [Description("IsNil_Tests_A_Value_For_\"null\"_Or_\"undefined\"")]
        public void IsNil_Tests_A_Value_For_Null() {
            Assert.IsTrue(R.IsNil(R.Null));
            Assert.IsFalse(R.IsNil(new object[0]));
            Assert.IsFalse(R.IsNil(new { }));
            Assert.IsFalse(R.IsNil(0));
            Assert.IsFalse(R.IsNil(string.Empty));
        }
    }
}
