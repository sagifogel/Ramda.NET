using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Union
    {
        private readonly int[] M = new[] { 1, 2, 3, 4 };
        private readonly int[] N = new[] { 3, 4, 5, 6 };

        [TestMethod]
        public void Union_Combines_Two_Lists_Into_The_Set_Of_All_Their_Elements() {
            CollectionAssert.AreEqual(R.Union(M, N), new[] { 1, 2, 3, 4, 5, 6 });
        }

        [TestMethod]
        public void Union_Is_Curried() {
            Assert.IsInstanceOfType(R.Union(M), typeof(DynamicDelegate));
            CollectionAssert.AreEqual(R.Union(M)(N), new[] { 1, 2, 3, 4, 5, 6 });
        }

        [TestMethod]
        [Description("Union_Has_R.Equals_Semantics")]
        public void Union_Has_R_Equals_Semantics() {
            Assert.AreEqual(R.Union(new[] { 0 }, new[] { -0 }).Length, 1);
            Assert.AreEqual(R.Union(new[] { R.@null, R.@null }).Length, 1);
            Assert.AreEqual(R.Union(new[] { new Just(new[] { 42 }), new Just(new[] { 42 }) }).Length, 1);
        }
    }
}
