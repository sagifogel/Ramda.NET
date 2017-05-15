using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Intersection
    {

        private readonly int[] M = new[] { 1, 2, 3, 4 };
        private readonly int[] N = new[] { 3, 4, 5, 6 };
        private readonly int[] M2 = new[] { 1, 2, 3, 4, 1, 2, 3, 4 };
        private readonly int[] N2 = new[] { 3, 3, 4, 4, 5, 5, 6, 6 };

        [TestMethod]
        public void Intersection_Combines_Two_Lists_Into_The_Set_Of_Common_Elements() {
            CollectionAssert.AreEqual(R.Intersection(M, N), new[] { 3, 4 });
        }

        [TestMethod]
        public void Intersection_Does_Not_Allow_Duplicates_In_The_Output_Even_If_The_Input_Lists_Had_Duplicates() {
            CollectionAssert.AreEqual(R.Intersection(M2, N2), new[] { 3, 4 });
        }

        [TestMethod]
        [Description("Intersection_Has_R.Equals_Semantics")]
        public void Intersection_Has_R_Equals_Semantics() {
            Assert.AreEqual(R.Intersection(new [] { 0 }, new[] { -0 }).Length, 1);
            Assert.AreEqual(R.Intersection(new [] { R.@null }, new[] { R.@null }).Length, 1);
            Assert.AreEqual(R.Intersection(new[] { new Just(new[] { 42 } ) }, new[] { new Just(new[] { 42 }) }).Length, 1);
        }
    }
}
