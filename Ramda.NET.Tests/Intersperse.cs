using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Intersperse
    {
        public class Dispersible : IDispersible<string, string>
        {
            public string Intersperse(string seperator) {
                return $"override {seperator}";
            }
        }

        [TestMethod]
        public void Interposes_Interposes_A_Separator_Between_List_Items() {
            CollectionAssert.AreEqual(R.Intersperse("n", new[] { "ba", "a", "a" }), new[] { "ba", "n", "a", "n", "a" });
            CollectionAssert.AreEqual(R.Intersperse("bar", new[] { "foo" }), new[] { "foo" });
            CollectionAssert.AreEqual(R.Intersperse("bar", new object[0]), new object[0]);
        }

        [TestMethod]
        public void Interposes_Dispatches() {
            Assert.AreEqual(R.Intersperse("x", new Dispersible()), "override x");
        }
    }
}
