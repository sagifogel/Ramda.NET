using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Difference
    {
        private int[] M = new[] { 1, 2, 3, 4 };
        private int[] N = new[] { 3, 4, 5, 6 };
        private int[] Z = new[] { 3, 4, 5, 6, 10 };
        private int[] M2 = new[] { 1, 2, 3, 4, 1, 2, 3, 4 };
        private int[] N2 = new[] { 3, 3, 4, 4, 5, 5, 6, 6 };
        private int[] Z2 = new[] { 1, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 8 };

        private class Just : IEquatable<Just>
        {
            public object Value { get; private set; }

            public Just(object value) {
                Value = value;
            }

            public override bool Equals(object obj) {
                return Equals(obj as Just);
            }

            public bool Equals(Just other) {
                if (other == null) {
                    return false;
                }

                return R.Equals(other.Value, Value);
            }

            public override int GetHashCode() {
                return Value.GetHashCode();
            }
        }

        [TestMethod]
        public void Difference_Finds_The_Set_Of_All_Elements_In_The_First_List_Not_Contained_In_The_Second() {
            CollectionAssert.AreEqual(R.Difference(M, N), new[] { 1, 2 });
        }

        [TestMethod]
        public void Difference_Does_Not_Allow_Duplicates_In_The_Output_Even_If_The_Input_Lists_Had_Duplicates() {
            CollectionAssert.AreEqual(R.Difference(M2, N2), new[] { 1, 2 });
        }

        [TestMethod]
        [Description("Difference_Has_R.equals_Semantics")]
        public void Difference_Has_R_Equals_Semantics() {
            Assert.AreEqual(R.Difference(new[] { new Just(new[] { 42 }) }, new[] { new Just(new[] { 42 }) }).Length, 0);
        }

        [TestMethod]
        public void Difference_Works_For_Arrays_Of_Different_Lengths() {
            CollectionAssert.AreEqual(R.Difference(Z, Z2), new[] { 10 });
            CollectionAssert.AreEqual(R.Difference(Z2, Z), new[] { 1, 2, 7, 8 });
        }


        [TestMethod]
        [Description("Difference_Will_Not_Create_A_\"Sparse\"_Array")]
        public void Difference_Will_Not_Create_A_Sparse_Array() {
            Assert.AreEqual(R.Difference(M2, new[] { 3 }).Length, 3);
        }

        [TestMethod]
        public void Difference_Returns_An_Empty_Array_If_There_Are_No_Different_Elements() {
            var comparee = new object[0];

            CollectionAssert.AreEqual(R.Difference(M2, M), comparee);
            CollectionAssert.AreEqual(R.Difference(M, M2), comparee);
            CollectionAssert.AreEqual(R.Difference<int>(new int[0], M2), comparee);
        }

        [TestMethod]
        public void Difference_Is_Curried() {
            Assert.IsInstanceOfType(R.Difference(new[] { 1, 2, 3 }), typeof(DynamicDelegate));
            CollectionAssert.AreEqual(R.Difference(new[] { 1, 2, 3 })(new[] { 1, 3 }), new[] { 2 });
        }
    }
}
