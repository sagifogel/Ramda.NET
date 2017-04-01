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

            Assert.IsTrue(((ExpandoObject)R.Dissoc("B", anonymous)).ContentEquals(new { A = 1, C = 3 }.ToExpando()));
            Assert.IsTrue(((ExpandoObject)R.Dissoc("D", anonymous)).ContentEquals(new { A = 1, B = 2, C = 3 }.ToExpando()));
        }

        [TestMethod]
        [Description("Dissoc_Includes_Prototype_Properties")]
        public void Dissoc_Includes_Well_Typed_Properties() {
            var rect = new Rectangle(7, 6);
            var area = rect.Area;

            Assert.IsTrue(((ExpandoObject)R.Dissoc("Area", rect)).ContentEquals(new { Width = 7, Height = 6 }.ToExpando()));
            Assert.IsTrue(((ExpandoObject)R.Dissoc("Width", rect)).ContentEquals(new { Height = 6, Area = area }.ToExpando()));
            Assert.IsTrue(((ExpandoObject)R.Dissoc("Depth", rect)).ContentEquals(new { Width = 7, Height = 6, Area = area }.ToExpando()));
        }

        [TestMethod]
        public void Dissoc_Is_Curried() {
            Assert.IsTrue(((ExpandoObject)R.Dissoc("B")(new { A = 1, B = 2, C = 3 })).ContentEquals(new { A = 1, C = 3 }.ToExpando()));
        }
    }
}
