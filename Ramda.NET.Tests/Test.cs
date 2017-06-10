using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test_Returns_True_If_String_Matches_Pattern() {
            Assert.IsTrue(R.Test("^x", "xyz"));
        }

        [TestMethod]
        public void Test_Returns_False_If_String_Does_Not_Match_Pattern() {
            Assert.IsFalse(R.Test("^y", "xyz"));
        }
    }
}
