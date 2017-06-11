using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Transpose
    {
        [TestMethod]
        public void Transpose_Returns_An_Array_Of_Two_Arrays() {
            var input = new object[][] { new object[] { "a", 1 }, new object[] { "b", 2 }, new object[] { "c", 3 } };

            NestedCollectionAssert.AreEqual(R.Transpose(input), new object[] { new[] { "a", "b", "c" }, new[] { 1, 2, 3 } });
        }

        [TestMethod]
        public void Transpose_Skips_Elements_When_Rows_Are_Shorter() {
            var actual = R.Transpose(new int[][] { new[] { 10, 11 }, new[] { 20 }, new int[0], new[] { 30, 31, 32 } });
            var expected = new int[][] { new[] { 10, 20, 30 }, new[] { 11, 31 }, new[] { 32 } };

            NestedCollectionAssert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void Transpose_Copes_With_Empty_Arrays() {
            CollectionAssert.AreEqual(R.Transpose(new int[0][]), new int[0]);
        }

        [TestMethod]
        [Description("Transpose_Copes_With_True,_False,_Null_&_Undefined_Elements_Of_Arrays")]
        public void Transpose_Copes_With_True_False_R_Null_Elements_Of_Arrays() {
            var actual = R.Transpose(new object[][] { new object[] { true, false, R.@null, null }, new object[] { R.@null, R.@null, false, true } });
            var expected = new object[][] { new object[] { true, R.@null }, new object[] { false, R.@null }, new object[] { R.@null, false }, new object[] { R.@null, true } };

            NestedCollectionAssert.AreEqual(actual, expected);
        }
    }
}
