using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Remove
    {
        [TestMethod]
        [Description("Remove_Splices_Out_A_Sub-List_Of_The_Given_List")]
        public void Remove_Splices_Out_A_Sub_List_Of_The_Given_List() {
            var list = new[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" };

            CollectionAssert.AreEqual(R.Remove(2, 5, list), new[] { "a", "b", "h", "i", "j" });
        }

        [TestMethod]
        [Description("Remove_Returns_The_Appropriate_Sublist_When_Start_==_0")]
        public void Remove_Returns_The_Appropriate_Sublist_When_Start_Equals_0() {
            var list = new[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" };
            CollectionAssert.AreEqual(R.Remove(0, 5, list), new[] { "f", "g", "h", "i", "j" });
            CollectionAssert.AreEqual(R.Remove(0, 1, list), new[] { "b", "c", "d", "e", "f", "g", "h", "i", "j" });
            CollectionAssert.AreEqual(R.Remove(0, list.Length, list), new string[0]);
        }

        [TestMethod]
        public void Remove_Removes_The_End_Of_The_List_If_The_Count_Is_Too_Large() {
            var list = new[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" };
            CollectionAssert.AreEqual(R.Remove(2, 20, list), new[] { "a", "b" });
        }

        [TestMethod]
        public void Remove_Retains_The_Entire_List_If_The_Start_Is_Too_Large() {
            var list = new[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" };

            CollectionAssert.AreEqual(R.Remove(13, 3, list), new[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" });
        }

        [TestMethod]
        public void Remove_Is_Curried() {
            var list = new[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" };

            CollectionAssert.AreEqual(R.Remove(13)(3)(list), new[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" });
            CollectionAssert.AreEqual(R.Remove(13, 3)(list), new[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" });
        }
    }
}
