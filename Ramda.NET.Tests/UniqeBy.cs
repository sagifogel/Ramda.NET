using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class UniqeBy
    {
        [TestMethod]
        public void UniqBy_Returns_A_Set_From_Any_Array_Based_On_Predicate() {
            CollectionAssert.AreEqual(R.UniqBy<int, int>(Math.Abs, new[] { -2, -1, 0, 1, 2 }), new[] { -2, -1, 0 });
        }

        [TestMethod]
        public void UniqBy_Keeps_Elements_From_The_Left() {
            CollectionAssert.AreEqual(R.UniqBy(Math.Abs, new[] { -1, 2, 4, 3, 1, 3 }), new[] { -1, 2, 4, 3 });
        }

        [TestMethod]
        public void UniqBy_Returns_An_Empty_Array_For_An_Empty_Array() {
            CollectionAssert.AreEqual(R.UniqBy(R.Identity(R.__), new int[0]), new int[0]);
        }

        [TestMethod]
        [Description("UniqBy_Has_R.Equals_Semantics")]
        public void Uniq_Has_R_Equals_Semantics() {
            var justArr = new[] { 42 };

            Assert.AreEqual(R.UniqBy(R.Identity(R.__), new[] { 0, -0 }).Length, 1);
            Assert.AreEqual(R.UniqBy(R.Identity(R.__), new[] { R.@null, R.@null }).Length, 1);
            Assert.AreEqual(R.UniqBy(R.Identity(R.__), new[] { new Just(justArr), new Just(justArr) }).Length, 1);
        }
    }
}
