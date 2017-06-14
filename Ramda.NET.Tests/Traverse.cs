using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Traverse
    {
        [TestMethod]
        public void Traverse_Operates_On_A_List_Of_Lists() {
            NestedCollectionAssert.AreEqual(R.Traverse(R.Of(R.__), R.Map(R.Add(10)), new int[0]), new int[][] { new int[0] });
            NestedCollectionAssert.AreEqual(R.Traverse(R.Of(R.__), R.Map(R.Add(10)), new int[][] { new int[0], new[] { 1, 2, 3, 4 } }), new int[0]);
            NestedCollectionAssert.AreEqual(R.Traverse(R.Of(R.__), R.Map(R.Add(10)), new int[][] { new[] { 1 }, new[] { 2, 3, 4 } }), new int[][] { new[] { 11, 12 }, new[] { 11, 13 }, new[] { 11, 14 } });
            NestedCollectionAssert.AreEqual(R.Traverse(R.Of(R.__), R.Map(R.Add(10)), new int[][] { new[] { 1, 2 }, new[] { 3, 4 } }), new int[][] { new[] { 11, 13 }, new[] { 11, 14 }, new[] { 12, 13 }, new[] { 12, 14 } });
            NestedCollectionAssert.AreEqual(R.Traverse(R.Of(R.__), R.Map(R.Add(10)), new int[][] { new[] { 1, 2, 3 }, new[] { 4 } }), new[] { new[] { 11, 14 }, new[] { 12, 14 }, new[] { 13, 14 } });
            NestedCollectionAssert.AreEqual(R.Traverse(R.Of(R.__), R.Map(R.Add(10)), new int[][] { new[] { 1, 2, 3, 4 }, new int[0] }), new int[0]);
        }

        [TestMethod]
        [Description("Traverse_Dispatches_To_\"Sequence\"_Method")]
        public void Traverse_Dispatches_To_Sequence_Method() {
            var Id = new Func<object, _Id>(_Id.Id);

            DynamicAssert.AreEqual(R.Traverse(Id, R.Map(R.Negate(R.__)), new[] { Id(1), Id(2), Id(3) }), Id(new[] { -1, -2, -3 }));
            CollectionAssert.AreEqual(R.Traverse(R.Of(R.__), R.Map(R.Negate(R.__)), Id(new[] { 1, 2, 3 })), new[] { Id(-1), Id(-2), Id(-3) });
        }
    }
}
