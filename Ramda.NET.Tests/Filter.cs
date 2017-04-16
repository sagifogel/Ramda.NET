using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Filter
    {
        private Func<int, bool> gt100 = x => x > 100;
        private Func<int, bool> even = x => x % 2 == 0;

        [TestMethod]
        public void Filter_Reduces_An_Array_To_Those_Matching_A_Filter() {
            CollectionAssert.AreEqual(R.Filter(even, new[] { 1, 2, 3, 4, 5 }), new[] { 2, 4 });
        }

        [TestMethod]
        public void Filter_Returns_An_Empty_Array_If_No_Element_Matches() {
            CollectionAssert.AreEqual(R.Filter(gt100, new[] { 1, 9, 99 }), new int[0]);
        }

        [TestMethod]
        public void Filter_Returns_An_Empty_Array_If_Asked_To_Filter_An_Empty_Array() {
            CollectionAssert.AreEqual(R.Filter(gt100, new int[0]), new int[0]);
        }

        [TestMethod]
        public void Filter_Filters_Objects() {
            var positive = new Func<int, bool>(x => x > 0);

            DynamicAssert.AreEqual(R.Filter(positive, new { }), new { });
            DynamicAssert.AreEqual(R.Filter(positive, new { X = 0, Y = 0, Z = 0 }), new { });
            DynamicAssert.AreEqual(R.Filter(positive, new { X = 1, Y = 0, Z = 0 }), new { X = 1 });
            DynamicAssert.AreEqual(R.Filter(positive, new { X = 1, Y = 2, Z = 0 }), new { X = 1, Y = 2 });
            DynamicAssert.AreEqual(R.Filter(positive, new { X = 1, Y = 2, Z = 3 }), new { X = 1, Y = 2, Z = 3 });
        }

        [TestMethod]
        [Description("Filter_Dispatches_To_Passed-in_Non-Array_Object_With_A_\"Filter\"_Method")]
        public void Filter_Dispatches_To_Passed_In_Non_Array_Object_With_A_Filter_Method() {
            var func = new { Filter = new Func<Func<string, string>, string>(f => f("called func.Filter")) };

            Assert.AreEqual(R.Filter(new Func<string, string>(s => s), func), "called func.Filter");
        }

        [TestMethod]
        public void Filter_Is_Curried() {
            var onlyEven = R.Filter(even);

            CollectionAssert.AreEqual(onlyEven(new[] { 1, 2, 3, 4, 5, 6, 7 }), new[] { 2, 4, 6 });
        }
    }
}
