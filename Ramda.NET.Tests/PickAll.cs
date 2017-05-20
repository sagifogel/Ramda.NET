using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class PickAll
    {
        private dynamic obj = new { A = 1, B = 2, C = 3, D = 4, E = 5, F = 6 };

        [TestMethod]
        public void PickAll_Copies_The_Named_Properties_Of_An_Object_To_The_New_Object() {
            DynamicAssert.AreEqual(R.PickAll(new[] { "A", "C", "F" }, obj), new { A = 1, C = 3, F = 6 });
        }

        [TestMethod]
        public void PickAll_Includes_Properties_Not_Present_On_The_Input_Object() {
            DynamicAssert.AreEqual(R.PickAll(new[] { "A", "C", "G" }, obj), new { A = 1, C = 3, G = R.@null });
        }

        [TestMethod]
        public void PickAll_Is_Curried() {
            var copyAB = R.PickAll(new[] { "A", "B" });

            DynamicAssert.AreEqual(copyAB(obj), new { A = 1, B = 2 });
        }
    }
}
