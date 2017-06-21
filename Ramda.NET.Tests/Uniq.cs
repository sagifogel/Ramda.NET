using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Uniq
    {
        [TestMethod]
        [Description("Uniq_Returns_A_Set_From_Any_Array_(i.e._Purges_Duplicate_Elements)")]
        public void Uniq_Returns_A_Set_From_Any_Array() {
            var list = new[] { 1, 2, 3, 1, 2, 3, 1, 2, 3 };

            CollectionAssert.AreEqual(R.Uniq(list), new[] { 1, 2, 3 });
        }

        [TestMethod]
        public void Uniq_Keeps_Elements_From_The_Left() {
            CollectionAssert.AreEqual(R.Uniq(new[] { 1, 2, 3, 4, 1 }), new[] { 1, 2, 3, 4 });
        }

        [TestMethod]
        public void Uniq_Returns_An_Empty_Array_For_An_Empty_Array() {
            CollectionAssert.AreEqual(R.Uniq(new int[0]), new int[0]);
        }

        [TestMethod]
        [Description("Uniq_Has_R.Equals_Semantics")]
        public void Uniq_Has_R_Equals_Semantics() {
            var arr = new[] { 1 };
            var justArr = new[] { 42 };

            Assert.AreEqual(R.Uniq(new[] { 0, -0 }).Length, 1);
            Assert.AreEqual(R.Uniq(new[] { R.@null, R.@null }).Length, 1);
            Assert.AreEqual(R.Uniq(new object[] { arr, arr }).Length, 1);
            Assert.AreEqual(R.Uniq(new[] { new Just(justArr), new Just(justArr) }).Length, 1);
        }

        [TestMethod]
        [Description("Uniq_Handles_Null_And_Undefined_Elements")]
        public void Uniq_Handles_R_Null_Elements() {
            CollectionAssert.AreEqual(R.Uniq(new object[] { null, R.@null, R.@null, null }), new[] { null, R.@null });
        }

        [TestMethod]
        public void Uniq_Uses_Reference_Equality_For_Functions() {
            var add = R.Add(R.__);
            var identity = R.Identity(R.__);

            Assert.AreEqual(R.Uniq(new[] { add, identity, add, identity, add, identity }).Length, 2);
        }
    }
}
