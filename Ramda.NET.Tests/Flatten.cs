using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Dynamic;
using System.Collections.Generic;
using System.Linq;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Flatten
    {
        private readonly object[] nest = new object[] { 1, new[] { 2 }, new object[] { 3, new[] { 4, 5 }, 6, new object[] { new object[] { new[] { 7 }, 8 } } }, 9, 10 };

        [TestMethod]
        public void Flatten_Turns_A_Nested_List_Into_One_Flat_List() {
            var nest2 = new object[] { new object[] { new object[] { new object[] { 3 } }, 2, 1 }, 0, new object[] { new object[] { -1, -2 }, -3 } };

            CollectionAssert.AreEqual(R.Flatten(nest), new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            CollectionAssert.AreEqual(R.Flatten(nest2), new[] { 3, 2, 1, 0, -1, -2, -3 });
            CollectionAssert.AreEqual(R.Flatten(new[] { 1, 2, 3, 4, 5 }), new[] { 1, 2, 3, 4, 5 });
        }

        [TestMethod]
        public void Flatten_Is_Not_Destructive() {
            CollectionAssert.AreNotEqual(R.Flatten(nest), nest);
        }

        [TestMethod]
        public void Flatten_Handles_Ridiculously_Large_Inputs() {
            var nulls = Enumerable.Range(0, 1000000).Select(i => R.@null).ToArray();

            Assert.AreEqual(R.Flatten(new object[] { nulls , R.Range(0, 56000), 5, 1, 3 }).Length, 1056003);
        }

        [TestMethod]
        [Description("Flatten_Handles_Array-Like_Objects")]
        public void Flatten_Handles_Array_Like_Objects() {
            var o = new Dictionary<string, object> {
                ["Length"] = 3,
                ["1"] = new object[0],
                ["0"] = new object[] { 1, 2, new[] { 3 } },
                ["2"] = new object[] { "a", "b", "c", new[] { "d", "e" } }
            };

            CollectionAssert.AreEqual(R.Flatten(o), new object[] { 1, 2, 3, "a", "b", "c", "d", "e" });
        }

        [TestMethod]
        [Description("Flatten_Handles_Array-Like_Objects")]
        public void Flatten_Handles_Dictionaries_With_Object_Keys() {
            var o = new Dictionary<object, object> {
                ["Length"] = 3,
                [1] = new object[0],
                [0] = new object[] { 1, 2, new[] { 3 } },
                [2] = new object[] { "a", "b", "c", new[] { "d", "e" } }
            };

            CollectionAssert.AreEqual(R.Flatten(o), new object[] { 1, 2, 3, "a", "b", "c", "d", "e" });
        }

        [TestMethod]
        public void Flatten_Flattens_An_Array_Of_Empty_Arrays() {
            CollectionAssert.AreEqual(R.Flatten(new object[] { new object[0], new object[0], new object[0]}), new object[0]);
            CollectionAssert.AreEqual(R.Flatten(new object[0]), new object[0]);
        }
    }
}
