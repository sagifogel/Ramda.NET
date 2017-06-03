using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Sequence
    {
        [TestMethod]
        public void Sequence_Operates_On_A_List_Of_Lists() {
            NestedCollectionAssert.AreEqual(R.Sequence(R.Of(R.__), new object[0]), new object[] { new object[0] });
            //NestedCollectionAssert.AreEqual(R.Sequence(R.Of(R.__), new object[] { new object[0], new[] { 1, 2, 3, 4 } }), new int[0]);
            //NestedCollectionAssert.AreEqual(R.Sequence(R.Of(R.__), new object[] { new[] { 1 }, new[] { 2, 3, 4 } }), new object[] { new[] { 1, 2 }, new[] { 1, 3 }, new[] { 1, 4 } });
            //NestedCollectionAssert.AreEqual(R.Sequence(R.Of(R.__), new object[] { new[] { 1, 2 }, new[] { 3, 4 } }), new object[] { new[] { 1, 3 }, new[] { 1, 4 }, new[] { 2, 3 }, new[] { 2, 4 } });
            //NestedCollectionAssert.AreEqual(R.Sequence(R.Of(R.__), new object[] { new[] { 1, 2, 3 }, new[] { 4 } }), new object[] { new[] { 1, 4 }, new[] { 2, 4 }, new[] { 3, 4 } });
            //NestedCollectionAssert.AreEqual(R.Sequence(R.Of(R.__), new object[] { new[] { 1, 2, 3, 4 }, new int[0] }), new int[0]);
        }
    }
}
