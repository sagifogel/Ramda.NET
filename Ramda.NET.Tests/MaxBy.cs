using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class MaxBy
    {
        [TestMethod]
        public void MaxBy_Returns_The_Larger_Value_As_Determined_By_The_Function() {
            Assert.AreEqual(R.MaxBy(n => n * n, -3, 2), -3);
            Assert.AreEqual(R.MaxBy(R.Prop("X"), new { X = 3, Y = 1 }, new { X = 5, Y = 10 }), new { X = 5, Y = 10 });
        }
    }
}
