using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Reject
    {
        private readonly Func<int, bool> even = x => x % 2 == 0;

        [TestMethod]
        public void Reject_Reduces_An_Array_To_Those_Not_Matching_A_Filter() {
            CollectionAssert.AreEqual(R.Reject(even, new[] { 1, 2, 3, 4, 5 }), new[] { 1, 3, 5 });
        }

        [TestMethod]
        public void Reject_Returns_An_Empty_Array_If_No_Element_Matches() {
            CollectionAssert.AreEqual(R.Reject((int x) => x < 100, new[] { 1, 9, 99 }), new int[0]);
        }

        [TestMethod]
        public void Reject_Returns_An_Empty_Array_If_Asked_To_Filter_An_Empty_Array() {
            CollectionAssert.AreEqual(R.Reject((int x) => x > 100, new int[0]), new int[0]);
        }

        [TestMethod]
        public void Reject_Filters_Objects() {
            DynamicAssert.AreEqual(R.Reject(R.Equals(0), new { }), new { });
            DynamicAssert.AreEqual(R.Reject(R.Equals(0), new { X = 0, Y = 0, Z = 0 }), new { });
            DynamicAssert.AreEqual(R.Reject(R.Equals(0), new { X = 1, Y = 0, Z = 0 }), new { X = 1 });
            DynamicAssert.AreEqual(R.Reject(R.Equals(0), new { X = 1, Y = 2, Z = 0 }), new { X = 1, Y = 2 });
            DynamicAssert.AreEqual(R.Reject(R.Equals(0), new { X = 1, Y = 2, Z = 3 }), new { X = 1, Y = 2, Z = 3 });
        }

        [TestMethod]
        [Description("Reject_Dispatches_To_\"Filter\"_Method")]
        public void Reject_Dispatches_To_Filter_Method() {
            var m = new Just(42);

            Assert.AreEqual(R.Filter(R.T, m), m);
            Assert.AreEqual(R.Filter(R.F, m), _Nothing.Nothing());
            Assert.AreEqual(R.Reject(R.T, m), _Nothing.Nothing());
            Assert.AreEqual(R.Reject(R.F, m), m);
        }

        [TestMethod]
        public void Reject_Is_Curried() {
            var odd = R.Reject(even);

            CollectionAssert.AreEqual(odd(new[] { 1, 2, 3, 4, 5, 6, 7 }), new[] { 1, 3, 5, 7 });
        }
    }
}
