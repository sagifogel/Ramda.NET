using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class InsertAll
    {
        private readonly string[] list = new[] { "a", "b", "c", "d", "e" };

        [TestMethod]
        public void InsertAll_Inserts_A_List_Of_Elements_Into_The_Given_List() {
            CollectionAssert.AreEqual(R.InsertAll(2, new[] { "x", "y", "z" }, list), new[] { "a", "b", "x", "y", "z", "c", "d", "e" });
        }

        [TestMethod]
        public void InsertAll_Appends_To_The_End_Of_The_List_If_The_Index_Is_Too_Large() {
            CollectionAssert.AreEqual(R.InsertAll<string>(8, new[] { "p", "q", "r" }, list), new[] { "a", "b", "c", "d", "e", "p", "q", "r" });
        }

        [TestMethod]
        public void InsertAll_Is_Curried() {
            CollectionAssert.AreEqual(R.InsertAll(8)(new[] { "p", "q", "r" }, list), new[] { "a", "b", "c", "d", "e", "p", "q", "r" });
        }
    }
}
