using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Mean
    {   
        private int[] Arguments(params int[] argumnets) => argumnets;

        [TestMethod]
        public void Mean_Returns_Mean_Of_A_Nonempty_List() {
            Assert.AreEqual(R.Mean(new[] { 2 }), 2);
            Assert.AreEqual(R.Mean(new double[] { 2, 7 }), 4.5);
            Assert.AreEqual(R.Mean(new[] { 2, 7, 9 }), 6);
            Assert.AreEqual(R.Mean(new[] { 2, 7, 9, 10 }), 7);
        }

        [TestMethod]
        [ExpectedException(typeof(NaNException))]
        [Description("Mean_Returns_NaN_For_An_Empty_List")]
        public void Mean_Returns_Throws_NaNException_For_An_Empty_List() {
            R.Mean(new int[0]);
        }

        [TestMethod]
        [Description("Mean_Handles_Array-Like_Object")]
        public void Mean_Handles_Array_Like_Object() {
            Assert.AreEqual(R.Mean(Arguments(1, 2, 3)), 2);
        }
    }
}
