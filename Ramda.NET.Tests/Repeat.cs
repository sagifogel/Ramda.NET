using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Repeat
    {
        [TestMethod]
        public void Repeat_Returns_A_Lazy_List_Of_Identical_Values() {
            CollectionAssert.AreEqual(R.Repeat(0, 5), new[] { 0, 0, 0, 0, 0 });
        }

        [TestMethod]
        [Description("Repeat_Can_Accept_Any_Value,_Including_\"Null\"")]
        public void Repeat_Can_Accept_Any_Value_Including_R_Null() {
            CollectionAssert.AreEqual(R.Repeat(R.@null, 3), new[] { R.@null, R.@null, R.@null });
        }

        [TestMethod]
        public void Repeat_Is_Curried() {
            var makeFoos = R.Repeat("foo");

            CollectionAssert.AreEqual(makeFoos(0), new string[0]);
            CollectionAssert.AreEqual(makeFoos(3), new [] { "foo", "foo", "foo" });
        }
    }
}
