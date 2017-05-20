using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Pick
    {
        class Base
        {
            public int Y { get; set; } = 40;
            public int Z { get; set; } = 50;
        }

        class F : Base
        {
            public int X { get; set; }
            public int V { get; set; }
            public int W { get; set; }

            public F(int x) {
                X = x;
            }
        }

        private dynamic obj = new Dictionary<string, int> {
            ["A"] = 1,
            ["B"] = 2,
            ["C"] = 3,
            ["D"] = 4,
            ["E"] = 5,
            ["F"] = 6,
            ["1"] = 7
        };

        [TestMethod]
        public void Pick_Copies_The_Named_Properties_Of_An_Object_To_The_New_Object() {
            DynamicAssert.AreEqual(R.Pick(new[] { "A", "C", "F" }, obj), new { A = 1, C = 3, F = 6 });
        }

        [TestMethod]
        public void Pick_Handles_Numbers_As_Properties() {
            DynamicAssert.AreEqual(R.Pick(new[] { "1" }, obj), new Dictionary<string, int> { ["1"] = 7 });
        }

        [TestMethod]
        public void Pick_Ignores_Properties_Not_Included() {
            DynamicAssert.AreEqual(R.Pick(new[] { "A", "C", "G" }, obj), new { A = 1, C = 3 });
        }

        [TestMethod]
        public void Pick_Retrieves_Prototype_Properties() {
            var obj = new F(30) { V = 10, W = 20 };

            DynamicAssert.AreEqual(R.Pick(new[] { "W", "X", "Y" }, obj), new { W = 20, X = 30, Y = 40 });
        }

        [TestMethod]
        public void Pick_Is_Curried() {
            var copyAB = R.Pick(new[] { "A", "B" });

            DynamicAssert.AreEqual(copyAB(obj), new { A = 1, B = 2 });
        }
    }
}
