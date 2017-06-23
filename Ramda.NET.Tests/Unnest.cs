using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Unnest
    {
        [TestMethod]
        public void Unnest_Only_Flattens_One_Layer_Deep_Of_A_Nested_List() {
            var nest = new object[] { 1, new[] { 2 }, new object[] { 3, new[] { 4, 5 }, 6, new object[] { new object[] { new[] { 7 }, 8 } } }, 9, 10 };
            NestedCollectionAssert.AreEqual(R.Unnest(nest), new object[] { 1, 2, 3, new[] { 4, 5 }, 6, new object[] { new object[] { new[] { 7 }, 8 } }, 9, 10 });

            nest = new object[] { new object[] { new object[] { new[] { 3 } }, 2, 1 }, 0, new object[] { new[] { -1, -2 }, -3 } };
            NestedCollectionAssert.AreEqual(R.Unnest(nest), new object[] { new object[] { new[] { 3 } }, 2, 1, 0, new[] { -1, -2 }, -3 });
            NestedCollectionAssert.AreEqual(R.Unnest(new[] { 1, 2, 3, 4, 5 }), new[] { 1, 2, 3, 4, 5 });
        }

        [TestMethod]
        public void Unnest_Is_Not_Destructive() {
            var nest = new object[] { 1, new[] { 2 }, new object[] { 3, new[] { 4, 5 }, 6, new object[] { new object[] { new[] { 7 }, 8 } } }, 9, 10 };

            NestedCollectionAssert.AreNotEqual(R.Unnest(nest), nest);
        }

        [TestMethod]
        [Description("Unnest_Handles_Array-Like_Objects")]
        public void Unnest_Handles_Array_Like_Objects() {
            var o = new Dictionary<string, object> {
                ["Length"] = 3,
                ["1"] = new int[0],
                ["0"] = new object[] { 1, 2, new[] { 3 } },
                ["2"] = new object[] { "a", "b", "c", new[] { "d", "e" } }
            };

            NestedCollectionAssert.AreEqual(R.Unnest(o), new object[] { 1, 2, new[] { 3 }, "a", "b", "c", new[] { "d", "e" } });
        }

        [TestMethod]
        public void Unnest_Flattens_An_Array_Of_Empty_Arrays() {
            CollectionAssert.AreEqual(R.Unnest(new object[] { new int[0], new int[0], new int[0] }), new int[0]);
            CollectionAssert.AreEqual(R.Unnest(new int[0]), new int[0]);
        }

        [TestMethod]
        [Description("Unnest_Is_Equivalent_To_R.Chain(R.Identity(R.__))")]
        public void Unnest_Is_Equivalent_To_R_Chain_Of_R_Identity() {
            Func<_Nothing> Nothing = _Nothing.Nothing;
            Func<object, Just> Just = _Maybe.Just;

            Assert.AreEqual(R.Unnest(Nothing()), Nothing());
            Assert.AreEqual(R.Unnest(Just(Nothing())), Nothing());
            Assert.AreEqual(R.Unnest(Just(Just(Nothing()))), Just(Nothing()));
            Assert.AreEqual(R.Unnest(Just(Just(42))), Just(42));
            Assert.AreEqual(R.Unnest(Just(Just(Just(42)))), Just(Just(42)));
        }
    }
}
