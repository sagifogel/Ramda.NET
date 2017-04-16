using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Dynamic;
using System.Collections.Generic;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class ForEach
    {
        private static bool dispatched;
        private static readonly Action<object> fn = (obj) => { };
        private readonly object[] list = new object[] { new { X = 1, Y = 2 }, new { X = 100, Y = 200 }, new { X = 300, Y = 400 }, new { X = 234, Y = 345 } };

        private class DummyList
        {
            public void ForEach(Action<object> callback) {
                dispatched = true;
                Assert.AreEqual(callback, fn);
            }
        }

        [TestMethod]
        public void ForEach_Performs_The_Passed_In_Function_On_Each_Element_Of_The_List() {
            var sideEffect = new Dictionary<int, int>();
            var expected = new Dictionary<int, int> {
                [1] = 2,
                [100] = 200,
                [300] = 400,
                [234] = 345
            };

            R.ForEach<dynamic>(elem => sideEffect[elem.X] = elem.Y, list);

            DynamicAssert.AreEqual(sideEffect, expected);
        }

        [TestMethod]
        public void ForEach_Returns_The_Original_List() {
            var s = string.Empty;

            Assert.AreEqual(R.ForEach<dynamic>(obj => s += obj.X, list), list);
            Assert.AreEqual("1100300234", s);
        }

        [TestMethod]
        public void ForEach_Handles_Empty_List() {
            var list = new int[0];

            CollectionAssert.AreEqual(R.ForEach(x => Math.Pow(x, 2), list), list);
        }

        [TestMethod]
        [Description("ForEach_Dispatches_To_\"ForEach\"_Method")]
        public void ForEach_Dispatches_To_ForEach_Method() {
            R.ForEach(fn, new DummyList());

            Assert.IsTrue(dispatched);
        }

        [TestMethod]
        public void ForEach_Is_Curried() {
            var xStr = string.Empty;
            var xe = R.ForEach((int x) => xStr += (x.ToString() + " "));

            xe(new[] { 1, 2, 4 });

            Assert.IsInstanceOfType(xe, typeof(DynamicDelegate));
            Assert.AreEqual(xStr, "1 2 4 ");
        }
    }
}
