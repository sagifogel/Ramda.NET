using System;
using System.Dynamic;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Assoc
    {
        [TestMethod]
        public void Assoc_Makes_A_Shallow_Clone_Of_An_Object_Overriding_Only_The_Specified_Property() {
            var b = new { c = 2, d = 3 };
            var e = new { x = 42 };
            var dictionary = new Dictionary<string, object> {
                ["a"] = 1,
                ["b"] = b,
                ["e"] = e,
                ["f"] = 5
            };

            var obj1 = new { a = 1, b, e = 4, f = 5 };
            dynamic obj2 = R.Assoc("e", e, obj1);

            DynamicAssert.AreEqual(dictionary, obj2);
            Assert.AreEqual(obj2.a, obj1.a);
            Assert.AreEqual(obj2.b, obj1.b);
            Assert.AreEqual(obj2.f, obj1.f);
        }

        [TestMethod]
        public void Assoc_Is_The_Equivalent_Of_Clone_And_Set_If_The_Property_Is_Not_On_The_Original() {
            var f = 5;
            var a = 1;
            var z = new { x = 42 };
            var b = new { c = 2, d = 3 };
            var dictionary = new Dictionary<string, object> {
                ["a"] = a,
                ["b"] = b,
                ["e"] = 4,
                ["f"] = f
            };

            var obj1 = new { a, b, e = 4, f };
            var obj2 = R.Assoc("z", z, obj1);

            dictionary["z"] = z;

            DynamicAssert.AreEqual(dictionary, obj2);
            Assert.AreEqual(obj2.a, obj1.a);
            Assert.AreEqual(obj2.b, obj1.b);
            Assert.AreEqual(obj2.f, obj1.f);
        }

        [TestMethod]
        public void Assoc_Is_Curried() {
            var z = new { x = 42 };
            var b = new { c = 2, d = 3 };
            var dictionary = new Dictionary<string, object> {
                ["a"] = 1,
                ["b"] = b,
                ["e"] = 4,
                ["f"] = 5
            };

            var obj1 = new { a = 1, b, e = 4, f = 5 };
            dynamic f = R.Assoc("e");
            dynamic g = f(z);

            dictionary["e"] = z;

            DynamicAssert.AreEqual(dictionary, g(obj1));
        }
    }
}
