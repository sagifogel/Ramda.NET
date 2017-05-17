using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Omit
    {
        private object obj = new { A = 1, B = 2, C = 3, D = 4, E = 5, F = 6 };

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

        [TestMethod]
        public void Omit_Copies_An_Object_Omitting_The_Listed_Properties() {
            DynamicAssert.AreEqual(R.Omit(new[] { "A", "C", "F" }, obj), new { B = 2, D = 4, E = 5 });
        }

        [TestMethod]
        public void Omit_Includes_Prototype_Properties() {
            var obj = new F(30);

            obj.V = 10;
            obj.W = 20;
            DynamicAssert.AreEqual(R.Omit(new[] { "W", "X", "Y" }, obj), new { V = 10, Z = 50 });
        }

        [TestMethod]
        public void Omit_Is_Curried() {
            var skipAB = R.Omit(new[] { "A", "B" });

            DynamicAssert.AreEqual(skipAB(obj), new { C = 3, D = 4, E = 5, F = 6 });
        }
    }
}
