using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Insert
    {
        private readonly string[] list = new[] { "a", "b", "c", "d", "e" };

        [TestMethod]
        public void Insert_Inserts_An_Element_Into_The_Given_List() {
            CollectionAssert.AreEqual(R.Insert<string>(2, "x", list), new[] { "a", "b", "x", "c", "d", "e" });
        }

        [TestMethod]
        public void Insert_Inserts_Another_List_As_An_Element() {
            NestedCollectionAssert.AreEqual(R.Insert(2, new[] { "s", "t" }, list), new object[] { "a", "b", new[] { "s", "t" }, "c", "d", "e" });
        }

        [TestMethod]
        public void Insert_Appends_To_The_End_Of_The_List_If_The_Index_Is_Too_Large() {
            CollectionAssert.AreEqual(R.Insert<string>(8, "z", list), new[] { "a", "b", "c", "d", "e", "z" });
        }

        [TestMethod]
        public void Insert_Is_Curried() {
            CollectionAssert.AreEqual(R.Insert(8)("z")(list), new[] { "a", "b", "c", "d", "e", "z" });
            CollectionAssert.AreEqual(R.Insert(8, "z")(list), new[] { "a", "b", "c", "d", "e", "z" });
        }
    }
}
