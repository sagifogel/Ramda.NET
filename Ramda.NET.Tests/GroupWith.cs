using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Dynamic;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class GroupWith
    {
        [TestMethod]
        public void GroupWith_Splits_The_List_Into_Groups_According_To_The_Grouping_Function() {
            NestedCollectionAssert.AreEqual(R.GroupWith(R.Equals(R.__), new[] { 1, 2, 2, 3 }), new object[] { new[] { 1 }, new[] { 2, 2 }, new[] { 3 } });
            NestedCollectionAssert.AreEqual(R.GroupWith(R.Equals(R.__), new[] { 1, 1, 1, 1 }), new object[] { new[] { 1, 1, 1, 1 } });
            NestedCollectionAssert.AreEqual(R.GroupWith(R.Equals(R.__), new[] { 1, 2, 3, 4 }), new object[] { new[] { 1 }, new[] { 2 }, new[] { 3 }, new[] { 4 } });
        }

        [TestMethod]
        public void GroupWith_Returns_An_Empty_Array_If_Given_An_Empty_Array() {
            CollectionAssert.AreEqual(R.GroupWith(R.Equals(R.__), new object[0]), new object[0]);
        }

        [TestMethod]
        public void GroupWith_Can_Be_Turned_Into_The_Original_List_Through_Concatenation() {
            var list = new[] { 1, 1, 2, 3, 4, 4, 5, 5 };

            CollectionAssert.AreEqual(R.Unnest(R.GroupWith(R.Equals(R.__), list)), list);
            CollectionAssert.AreEqual(R.Unnest(R.GroupWith(R.Complement(R.Equals(R.__)), list)), list);
            CollectionAssert.AreEqual(R.Unnest(R.GroupWith(R.T, list)), list);
            CollectionAssert.AreEqual(R.Unnest(R.GroupWith(R.F, list)), list);
        }

        [TestMethod]
        public void GroupWith_Also_Works_On_Strings() {
            CollectionAssert.AreEqual(R.GroupWith(R.Equals(R.__))("Mississippi"), new[] { "M", "i", "ss", "i", "ss", "i", "pp", "i" });
        }
    }
}
