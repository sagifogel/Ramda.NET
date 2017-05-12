using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Median
    {
        private int[] Arguments(params int[] argumnets) => argumnets;

        [TestMethod]
        [Description("Median_Returns_Middle_Value_Of_An_Odd-Length_List")]
        public void Median_Returns_Middle_Value_Of_An_Odd_Length_List() {
            Assert.AreEqual(R.Median(new[] { 2 }), 2);
            Assert.AreEqual(R.Median(new[] { 2, 9, 7 }), 7);
        }

        [TestMethod]
        [Description("Median_Returns_Mean_Of_Two_Middle_Values_Of_A_Nonempty_Even-Length_List")]
        public void Median_Returns_Mean_Of_Two_Middle_Values_Of_A_Nonempty_Even_Length_List() {
            Assert.AreEqual(R.Median(new[] { 7, 2d }), 4.5);
            Assert.AreEqual(R.Median(new[] { 7, 2, 10, 9 }), 8);
        }

        [TestMethod]
        [ExpectedException(typeof(NaNException))]
        [Description("Median_Returns_NaN_For_An_Empty_List")]
        public void Median_Throws_NaNException_For_An_Empty_List() {
            R.Median(new int[0]);
        }

        [TestMethod]
        [Description("Median_Handles_Array-Like_Object")]
        public void Median_Handles_Array_Like_Object() {
            Assert.AreEqual(R.Median(Arguments(1, 2, 3)), 2);
        }
    }
}
