using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Replace
    {
        [TestMethod]
        public void Replace_Replaces_Substrings_Of_The_Input_String() {
            Assert.AreEqual(R.Replace(new Regex("1"), "one", "1 two three"), "one two three");
        }

        [TestMethod]
        public void Replace_Replaces_Regex_Matches_Of_The_Input_String() {
            Assert.AreEqual(R.Replace(new Regex("\\d+"), "num", "1 2 three"), "num num three");
        }

        [TestMethod]
        public void Replace_Is_Curried_Up_To_3_Arguments() {
            var regex = new Regex(string.Empty);
            var replaceSemicolon = R.Replace(";");
            var removeSemicolon = replaceSemicolon(string.Empty);

            Assert.IsInstanceOfType(R.Replace(regex), typeof(DynamicDelegate));
            Assert.IsInstanceOfType(R.Replace(regex, string.Empty), typeof(DynamicDelegate));
            Assert.AreEqual(removeSemicolon("return 42;"), "return 42");
        }
    }
}
