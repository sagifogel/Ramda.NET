using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ramda.NET.Tests
{
    public class AbstractConstruct
    {
        public class Foo
        {
            public Foo() {
            }

            public Foo(object a0, object a1, object a2, object a3, object a4, object a5, object a6, object a7, object a8, object a9, object a10) {
            }
        }

        public class Rectangle
        {
            public int Rect;
            public int Width;
            public int Height;
            public string[] Colors;

            public Rectangle(int w, int h) : this(w, h, null) {
            }

            public Rectangle(int r, params string[] colors) : this(0, 0, colors) {
                Rect = r;
            }

            public Rectangle(int w, int h, params string[] colors) {
                Width = w;
                Height = h;
                Colors = colors;
            }

            public Rectangle() : this(1, 1) {
            }

            public int Area() {
                return Width * Height;
            }

            public double PieArea() {
                return Math.PI * Math.Pow(Rect, 2);
            }
        }
    }
}
