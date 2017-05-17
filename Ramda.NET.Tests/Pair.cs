using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Pair
    {
        [TestMethod]
        [Description("Pair_Creates_A_Two-Element_Array")]
        public void Pair_Creates_A_Two_Element_Array() {
            CollectionAssert.AreEqual(R.Pair("foo", "bar"), new[] { "foo", "bar" });
            CollectionAssert.AreEqual(R.Pair("foo")("bar"), new[] { "foo", "bar" });
        }
    }
}
