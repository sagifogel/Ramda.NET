using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class IsArrayLike
    {
        [TestMethod]
        public void IsArrayLike_Is_True_For_Arrays() {
            Assert.IsTrue(R.IsArrayLike(new object[0]));
            Assert.IsTrue(R.IsArrayLike(new[] { 1, 2, 3, 4 }));
            Assert.IsTrue(R.IsArrayLike(new object[] { null }));
        }

        [TestMethod]
        [Description("IsArrayLike_Is_True_For_Arguments")]
        public void IsArrayLike_Is_True_For_Params() {
            Assert.IsTrue(Test());
            Assert.IsTrue(Test(1, 2, 3));
            Assert.IsTrue(Test(R.@null));
        }

        private bool Test(params object[] arguments) {
            return R.IsArrayLike(arguments);
        }

        [TestMethod]
        public void IsArrayLike_Is_False_For_Strings() {
            Assert.IsFalse(R.IsArrayLike(string.Empty));
            Assert.IsFalse(R.IsArrayLike("abcdefg"));
        }

        [TestMethod]
        [Description("IsArrayLike_Is_True_For_Arbitrary_Objects_With_Numeric_Length,_If_Extreme_Indices_Are_Defined")]
        public void IsArrayLike_Is_True_For_Arbitrary_Objects_With_Numeric_Length_If_Extreme_Indices_Are_Defined() {
            var obj1 = new Dictionary<string, object> { ["Length"] = 0 };
            var obj2 = new Dictionary<string, object> { ["0"] = "something", ["Length"] = 0 };
            var obj3 = new Dictionary<string, object> { ["0"] = null, ["Length"] = 0 };
            var obj4 = new Dictionary<string, object> { ["0"] = "zero", ["1"] = "one", ["Length"] = 2 };
            var obj5 = new Dictionary<string, object> { ["0"] = "zero", ["Length"] = 2 };
            var obj6 = new Dictionary<string, object> { ["1"] = "one", ["Length"] = 2 };

            Assert.IsTrue(R.IsArrayLike(obj1));
            Assert.IsTrue(R.IsArrayLike(obj2));
            Assert.IsTrue(R.IsArrayLike(obj3));
            Assert.IsTrue(R.IsArrayLike(obj4));
            Assert.IsFalse(R.IsArrayLike(obj5));
            Assert.IsFalse(R.IsArrayLike(obj6));
        }

        [TestMethod]
        public void IsArrayLike_Is_False_For_Everything_Else() {
            Assert.IsFalse(R.IsArrayLike(R.@null));
            Assert.IsFalse(R.IsArrayLike(1));
            Assert.IsFalse(R.IsArrayLike(new { }));
            Assert.IsFalse(R.IsArrayLike(false));
            Assert.IsFalse(R.IsArrayLike(new Action(() => { })));
        }
    }
}
