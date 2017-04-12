using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Dynamic;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class AssocPath
    {
        [TestMethod]
        public void AssocPath_Makes_A_Shallow_Clone_Of_An_Object_Overriding_Only_What_Is_Necessary_For_The_Path() {
            var obj1 = new { a = new { b = 1, c = 2, d = new { e = 3 } }, f = new { g = new { h = 4, i = new[] { 5, 6, 7 }, j = new { k = 6, l = 7 } } }, m = 8 };
            var expando = obj1.ToDynamic();
            object obj2 = R.AssocPath(new object[] { "f", "g", "i", 1 }, 42, obj1);
            var expando2 = obj2.ToDynamic();

            expando.f.g.i[1] = 42;

            DynamicAssert.AreEqual(expando, expando2);
        }

        [TestMethod]
        public void AssocPath_Is_The_Equivalent_Of_Clone_And_SetPath_If_The_Property_Is_Not_On_The_Original() {
            var obj1 = new { a = 1, b = new { c = 2, d = 3 }, f = 4, e = 5 };
            var expando = obj1.ToDynamic();
            object obj2 = R.AssocPath(new object[] { "x", 0, "y" }, 42, obj1);
            IDictionary<string, object> x = new ExpandoObject();
            IDictionary<string, object> zero = new ExpandoObject();

            expando.x = x;
            x["0"] = zero;
            zero["y"] = 42;

            DynamicAssert.AreEqual(expando, obj2);
        }

        [TestMethod]
        public void AssocPath_Is_Curried() {
            var z = new { x = 42 };
            var obj1 = new { a = new { b = 1, c = 2, d = new { e = 3 } }, f = new { g = new { h = 4, i = 5, j = new { k = 6, l = 7 } } }, m = 8 };
            dynamic expected = new { a = new { b = 1, c = 2, d = new { e = 3 } }, f = new { g = new { h = 4, i = new { x = 42 }, j = new { k = 6, l = 7 } } }, m = 8 };
            var f = R.AssocPath(new[] { "f", "g", "i" });
            var g = f(z);

            DynamicAssert.AreEqual(f(z, obj1), expected);
            DynamicAssert.AreEqual(g(obj1), expected);
        }

        [TestMethod]
        public void AssocPath_Empty_Path_Replaces_The_The_Whole_Object() {
            Assert.AreEqual((int)R.AssocPath(new object[0], 3, new { a =  1, b =  2}), 3);
        }
    }
}
