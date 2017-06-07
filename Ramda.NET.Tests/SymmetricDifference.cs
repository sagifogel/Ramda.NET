using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class SymmetricDifference
    {
        private readonly int[] M = new[] { 1, 2, 3, 4 };
        private readonly int[] N = new[] { 3, 4, 5, 6 };
        private readonly int[] Z = new[] { 3, 4, 5, 6, 10 };
        private readonly int[] M2 = new[] { 1, 2, 3, 4, 1, 2, 3, 4 };
        private readonly int[] N2 = new[] { 3, 3, 4, 4, 5, 5, 6, 6 };
        private readonly int[] Z2 = new[] { 1, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 8 };

        [TestMethod]
        public void SymmetricDifference_Finds_The_Set_Of_All_Elements_In_The_First_Or_Second_List_But_Not_Both() {
            CollectionAssert.AreEqual(R.SymmetricDifference(M, N), new[] { 1, 2, 5, 6 });
        }

        [TestMethod]
        public void SymmetricDifference_Does_Not_Allow_Duplicates_In_The_Output_Even_If_The_Input_Lists_Had_Duplicates() {
            CollectionAssert.AreEqual(R.SymmetricDifference(M2, N2), new[] { 1, 2, 5, 6 });
        }

        [TestMethod]
        [Description("SymmetricDifference_Has_R.Equals_Semantics")]
        public void SymmetricDifference_Has_R_Equals_Semantics() {
            Assert.AreEqual(R.SymmetricDifference(new[] { R.@null }, new[] { R.@null }).Length, 0);
            Assert.AreEqual(R.SymmetricDifference(new[] { new Just(new[] { 42 }) }, new[] { new Just(new[] { 42 }) }).Length, 0);
        }

        [TestMethod]
        public void SymmetricDifference_Works_For_Arrays_Of_Different_Lengths() {
            CollectionAssert.AreEqual(R.SymmetricDifference(Z, Z2), new[] { 10, 1, 2, 7, 8 });
            CollectionAssert.AreEqual(R.SymmetricDifference(Z2, Z), new[] { 1, 2, 7, 8, 10 });
        }

        [TestMethod]
        [Description("SymmetricDifference_Will_Not_Create_A_\"Sparse\"_Array")]
        public void SymmetricDifference_Will_Not_Create_A_Sparse_Array() {
            CollectionAssert.AreEqual(R.SymmetricDifference(M2, M), new int[0]);
            CollectionAssert.AreEqual(R.SymmetricDifference(M, M2), new int[0]);
        }

        [TestMethod]
        public void SymmetricDifference_Is_Curried() {
            Assert.IsInstanceOfType(R.SymmetricDifference(new[] { 1, 2, 3 }), typeof(DynamicDelegate));
            CollectionAssert.AreEqual(R.SymmetricDifference(new[] { 1, 2, 3 })(new[] { 1, 3 }), new[] { 2 });
        }
    }
}
