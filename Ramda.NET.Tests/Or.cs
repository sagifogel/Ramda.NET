using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Or
    {
        [TestMethod]
        [Description("Or_Compares_Two_Values_With_Js_&&")]
        public void Or_Compares_Two_Values_With_And_Operator() {
            Assert.IsTrue(R.Or(true, true));
            Assert.IsTrue(R.Or(true, false));
            Assert.IsTrue(R.Or(false, true));
            Assert.IsFalse(R.Or(false, false));
        }

        [TestMethod]
        public void Or_Is_Curried() {
            Assert.IsFalse(R.Or(false)(false));
            Assert.IsTrue(R.Or(false)(true));
        }
    }
}
