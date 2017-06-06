using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Split
    {
        [TestMethod]
        public void Split_Splits_A_String_Into_An_Array() {
            CollectionAssert.AreEqual(R.Split(".", "a.b.c.xyz.d"), new[] { "a", "b", "c", "xyz", "d" });
        }

        [TestMethod]
        public void Split_The_Split_String_Can_Be_Arbitrary() {
            CollectionAssert.AreEqual(R.Split("at", "The Cat in the Hat sat on the mat"), new[] { "The C", " in the H", " s", " on the m", "" });
        }
    }
}
