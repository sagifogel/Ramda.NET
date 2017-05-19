using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Partition
    {
        private Func<int, bool> pred = n => n % 2 != 0;

        [TestMethod]
        public void Partition_Splits_A_List_Into_Two_Lists_According_To_A_Predicate() {
            NestedCollectionAssert.AreEqual(R.Partition(pred, new object[0]), new object[] { new object[0], new object[0] });
            NestedCollectionAssert.AreEqual(R.Partition(pred, new[] { 0, 2, 4, 6 }), new object[] { new object[0], new[] { 0, 2, 4, 6 } });
            NestedCollectionAssert.AreEqual(R.Partition(pred, new[] { 1, 3, 5, 7 }), new object[] { new[] { 1, 3, 5, 7 }, new object[0] });
            NestedCollectionAssert.AreEqual(R.Partition(pred, new[] { 0, 1, 2, 3 }), new object[] { new[] { 1, 3 }, new object[] { 0, 2 } });
        }

        [TestMethod]
        public void Partition_Works_With_Objects() {
            NestedCollectionAssert.AreEqual(R.Partition(pred, new { }), new object[] { new { }, new { } });
            NestedCollectionAssert.AreEqual(R.Partition(pred, new { A = 0, B = 2, C = 4, D = 6 }), new object[] { new { }, new { A = 0, B = 2, C = 4, D = 6 } });
            NestedCollectionAssert.AreEqual(R.Partition(pred, new { A = 1, B = 3, C = 5, D = 7 }), new object[] { new { A = 1, B = 3, C = 5, D = 7 }, new { } });
            NestedCollectionAssert.AreEqual(R.Partition(pred, new { A = 0, B = 1, C = 2, D = 3 }), new object[] { new { B = 1, D = 3 }, new { A = 0, C = 2 } });
        }

        [TestMethod]
        public void Partition_Is_Curried() {
            var polarize = R.Partition((Func<object, bool>)Convert.ToBoolean);

            NestedCollectionAssert.AreEqual(polarize(new object[] { true, 0, 1, null }), new[] { new object[] { true, 1 }, new object[] { 0, R.@null } });
        }
    }
}
