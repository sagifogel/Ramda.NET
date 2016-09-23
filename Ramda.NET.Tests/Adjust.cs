using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Adjust
    {
        [TestMethod]
        public void Adjust_Applies_The_Given_Function_To_The_Value_At_The_Given_Index_Of_The_Supplied_Array() {
            CollectionAssert.AreEqual(R.Adjust(R.Add(1), 2, new int[] { 0, 1, 2, 3 }), new[] { 0, 1, 3, 3 });
        }

        [TestMethod]
        public void Adjust_Offsets_Negative_Indexes_From_The_End_Of_The_Array() {
            CollectionAssert.AreEqual(R.Adjust(R.Add(1), -3, new[] { 0, 1, 2, 3 }), new[] { 0, 2, 2, 3 });
        }

        [TestMethod]
        public void Adjust_Returns_The_Original_Array_If_The_Supplied_Index_Is_Out_Of_Bounds() {
            var list = new[] { 0, 1, 2, 3 };

            CollectionAssert.AreEqual(R.Adjust(R.Add(1), 4, list), list);
            CollectionAssert.AreEqual(R.Adjust(R.Add(1), -5, list), list);
        }

        [TestMethod]
        public void Adjust_Does_Not_Mutate_The_Original_Array() {
            var list = new[] { 0, 1, 2, 3 };

            CollectionAssert.AreEqual(R.Adjust(R.Add(1), 2, list), new[] { 0, 1, 3, 3 });
            CollectionAssert.AreEqual(list, new[] { 0, 1, 2, 3 });
        }

        [TestMethod]
        public void Adjust_Curries_The_Arguments() {
            CollectionAssert.AreEqual(R.Adjust(R.Add(1))(2)(new[] { 0, 1, 2, 3 }), new[] { 0, 1, 3, 3 });
        }

        [TestMethod]
        public void Adjust_Accepts_An_ArrayLike_Object() {
            CollectionAssert.AreEqual(R.Adjust(R.Add(1), 2, Arguments(0, 1, 2, 3)), new[] { 0, 1, 3, 3 });
        }

        private static int[] Arguments(params int[] arguments) {
            return arguments;
        }
    }
}
