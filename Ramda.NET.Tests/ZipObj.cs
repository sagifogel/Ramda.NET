using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class ZipObj
    {
        [TestMethod]
        public void ZipObj_Combines_An_Array_Of_Keys_With_An_Array_Of_Values_Into_A_Single_Object() {
            DynamicAssert.AreEqual(R.ZipObj(new[] { "A", "B", "C" }, new[] { 1, 2, 3 }), new { A = 1, B = 2, C = 3 });
        }

        [TestMethod]
        public void ZipObj_Ignores_Extra_Values() {
            DynamicAssert.AreEqual(R.ZipObj(new[] { "A", "B", "C" }, new[] { 1, 2, 3, 4, 5, 6, 7 }), new { A = 1, B = 2, C = 3 });
        }

        [TestMethod]
        public void ZipObj_Ignores_Extra_Keys() {
            DynamicAssert.AreEqual(R.ZipObj(new[] { "A", "B", "C", "D", "E", "F" }, new[] { 1, 2, 3 }), new { A = 1, B = 2, C = 3 });
        }

        [TestMethod]
        public void ZipObj_Last_One_In_Wins_When_There_Are_Duplicate_Keys() {
            DynamicAssert.AreEqual(R.ZipObj(new[] { "A", "B", "C", "A" }, new object[] { 1, 2, 3, "LAST" }), new { A = "LAST", B = 2, C = 3 });
        }
    }
}
