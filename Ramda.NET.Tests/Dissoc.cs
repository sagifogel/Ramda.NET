using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Dynamic;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Dissoc
    {
        class Rectangle
        {
            public int Width { get; set; }
            public int Height { get; set; }

            public int Area {
                get {
                    return Width * Height;
                }
            }

            public Rectangle(int width, int height) {
                Width = width;
                Height = height;
            }
        }

        [TestMethod]
        public void Dissoc_Copies_An_Object_Omitting_The_Specified_Property() {
            var anonymous = new { A = 1, B = 2, C = 3 };

            DynamicAssert.AreEqual(R.Dissoc("B", anonymous), new { A = 1, C = 3 });
            DynamicAssert.AreEqual(R.Dissoc("D", anonymous), new { A = 1, B = 2, C = 3 });
        }

        [TestMethod]
        [Description("Dissoc_Includes_Prototype_Properties")]
        public void Dissoc_Includes_Well_Typed_Properties() {
            var rect = new Rectangle(7, 6);
            var area = rect.Area;

            DynamicAssert.AreEqual(R.Dissoc("Area", rect), new { Width = 7, Height = 6 });
            DynamicAssert.AreEqual(R.Dissoc("Width", rect), new { Height = 6, Area = area });
            DynamicAssert.AreEqual(R.Dissoc("Depth", rect), new { Width = 7, Height = 6, Area = area });
        }

        [TestMethod]
        public void Dissoc_Is_Curried() {
            DynamicAssert.AreEqual(R.Dissoc("B")(new { A = 1, B = 2, C = 3 }), new { A = 1, C = 3 });
        }
    }
}
