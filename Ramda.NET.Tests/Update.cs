using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Update
    {
        public object[] Args(params object[] argumnets) => argumnets;

        [TestMethod]
        public void Update_Updates_The_Value_At_The_Given_Index_Of_The_Supplied_Array() {
            CollectionAssert.AreEqual(R.Update(2, 4, new[] { 0, 1, 2, 3 }), new[] { 0, 1, 4, 3 });
        }

        [TestMethod]
        public void Update_Offsets_Negative_Indexes_From_The_End_Of_The_Array() {
            CollectionAssert.AreEqual(R.Update(-3, 4, new[] { 0, 1, 2, 3 }), new[] { 0, 4, 2, 3 });
        }

        [TestMethod]
        public void Update_Returns_The_Original_Array_If_The_Supplied_Index_Is_Out_Of_Bounds() {
            var list = new[] { 0, 1, 2, 3 };

            CollectionAssert.AreEqual(R.Update(4, 4, list), list);
            CollectionAssert.AreEqual(R.Update(-5, 4, list), list);
        }

        [TestMethod]
        public void Update_Does_Not_Mutate_The_Original_Array() {
            var list = new[] { 0, 1, 2, 3 };

            CollectionAssert.AreEqual(R.Update(2, 4, list), new[] { 0, 1, 4, 3 });
            CollectionAssert.AreEqual(list, new[] { 0, 1, 2, 3 });
        }

        [TestMethod]
        public void Update_Curries_The_Arguments() {
            CollectionAssert.AreEqual(R.Update(2)(4)(new[] { 0, 1, 2, 3 }), new[] { 0, 1, 4, 3 });
        }

        [TestMethod]
        [Description("Update_Accepts_An_Array-Like_Object")]
        public void Update_Accepts_An_Array_Like_Object() {
            CollectionAssert.AreEqual(R.Update(2, 4, Args(0, 1, 2, 3)), new[] { 0, 1, 4, 3 });
        }
    }
}
