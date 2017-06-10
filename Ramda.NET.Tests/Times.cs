using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Times
    {
        [TestMethod]
        public void Times_Takes_A_Map_Func() {
            CollectionAssert.AreEqual(R.Times(R.Identity(R.__), 5), new[] { 0, 1, 2, 3, 4 });
            CollectionAssert.AreEqual(R.Times(x => x * 2, 5), new[] { 0, 2, 4, 6, 8 });
        }

        [TestMethod]
        public void Times_Is_Curried() {
            var mapid = R.Times(R.Identity(R.__));

            CollectionAssert.AreEqual(mapid(5), new[] { 0, 1, 2, 3, 4 });
        }

        [TestMethod]
        public void Times_Throws_If_Second_Argument_Is_Not_A_Valid_Array_Length() {
            try {
                R.Times(R.Identity(R.__), -1);
            }
            catch (ArgumentOutOfRangeException ex) {
                Assert.AreEqual(ex.Message, "Specified argument was out of the range of valid values.\r\nParameter name: n must be a non-negative number");
            }
        }
    }
}
