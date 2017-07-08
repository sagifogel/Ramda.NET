using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Dynamic;
using System.Collections.Generic;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class ForEachObjIndexed
    {
        private readonly object obj = new { X = 1, Y = 2, Z = 123 };

        [TestMethod]
        public void ForEachObjIndexed_Performs_The_Passed_In_Function_On_Each_Key_And_Value_Of_The_Object() {
            IDictionary<string, object> sideEffect = new ExpandoObject();
            R.ForEachObjIndexed((value, key, target) => { sideEffect[key] = value; }, obj);

            DynamicAssert.AreEqual(sideEffect, obj);
        }

        [TestMethod]
        public void ForEachObjIndexed_Returns_The_Original_Object() {
            var s = string.Empty;

            DynamicAssert.AreEqual(R.ForEachObjIndexed((value, key, target) => { s += value; }, obj), obj);
            Assert.AreEqual("12123", s);
        }

        [TestMethod]
        public void ForEachObjIndexed_Is_Curried() {
            var xStr = string.Empty;
            var xe = R.ForEachObjIndexed(new Action<int, string, object>((value, key, target) => { xStr += (value.ToString() + " "); }));

            xe(obj);

            Assert.IsInstanceOfType(xe, typeof(DynamicDelegate));
            Assert.AreEqual(xStr, "1 2 123 ");
        }
    }
}
