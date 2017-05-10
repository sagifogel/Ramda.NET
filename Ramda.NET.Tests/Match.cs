using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Match
    {
        private readonly string matching = "B17-afn";
        private readonly string notMatching = "B1-afn";
        private readonly Regex re = new Regex(@"[A-Z]\d\d\-[a-zA-Z]+");

        [TestMethod]
        public void Match_Determines_Whether_A_String_Matches_A_Regex() {
            Assert.AreEqual(R.Match(re, matching).Count, 1);
            Assert.AreEqual(R.Match(re, notMatching).Count, 0);
        }

        [TestMethod]
        public void Match_Is_Curried() {
            var format = R.Match(re);

            Assert.AreEqual(format(matching).Count, 1);
            Assert.AreEqual(format(notMatching).Count, 0);
        }

        [TestMethod]
        public void Match_Defaults_To_A_Different_Empty_Array_Each_Time() {
            var first = R.Match(re, notMatching);
            var second = R.Match(re, notMatching);

            CollectionAssert.AreEqual(first, second);
        }
    }
}
