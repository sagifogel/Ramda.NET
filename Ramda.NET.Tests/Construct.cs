using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;
using System.Reflection;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Construct
    {
        class Foo
        {
            public Foo() {

            }

            public Foo(object a0, object a1, object a2, object a3, object a4, object a5, object a6, object a7, object a8, object a9, object a10) {

            }
        }

        class Rectangle
        {
            public int Width;
            public int Height;

            public Rectangle(int w, int h) {
                Width = w;
                Height = h;
            }

            public Rectangle() : this(1, 1) {
            }

            public int Area() {
                return Width * Height;
            }
        }

        [TestMethod]
        [Description("Construct_Turns_A_Constructor_Function_Into_One_That_Can_Be_Called_Without_`new`")]
        public void Construct_Turns_A_Constructor_Function_Into_One_That_Can_Be_Called_Without_New() {
            dynamic r1;
            dynamic word;
            var rect = R.Construct((int w, int h) => new Rectangle(w, h));
            var regex = R.Construct((string pattern, RegexOptions options) => new Regex(pattern, options));
            var prop = typeof(Regex).GetField("pattern", BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

            r1 = rect(3, 4);
            word = regex("word", RegexOptions.IgnoreCase);

            Assert.IsInstanceOfType(r1, typeof(Rectangle));
            Assert.AreEqual(r1.Width, 3);
            Assert.AreEqual(r1.Area(), 12);
            Assert.IsInstanceOfType(word, typeof(Regex));
            Assert.AreEqual(prop.GetValue(word), "word");
            Assert.AreEqual(word.Options, RegexOptions.IgnoreCase);
        }

        [TestMethod]
        public void Construct_Can_Be_Used_To_Create_Date_Object() {
            var date = R.Construct((int y, int m, int d, int h, int mi, int s, int ms) => new DateTime(y, m, d, h, mi, s, ms))(1984, 3, 26, 0, 0, 0, 0);

            Assert.IsInstanceOfType(date, typeof(DateTime));
            Assert.AreEqual(date.Year, 1984);
        }

        [TestMethod]
        public void Construct_Supports_Constructors_With_No_Arguments() {
            var foo = R.Construct(() => new Foo());

            Assert.IsInstanceOfType(foo(), typeof(Foo));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Construct_Does_Not_Support_Constructor_With_Greater_Than_Ten_Arguments() {
            dynamic construct = new Func<object, object, object, object, object, object, object, object, object, object, object, Foo>((a0, a1, a2, a3, a4, a5, a6, a7, a8, a9, a10) => new Foo(a0, a1, a2, a3, a4, a5, a6, a7, a8, a9, a10));

            var foo = R.Construct(construct);
        }

        [TestMethod]
        public void Construct_Returns_A_Curried_Function() {
            var rect = R.Construct((int w, int h) => new Rectangle(w, h));
            var regex = R.Construct((string pattern, RegexOptions options) => new Regex(pattern, options));
            var prop = typeof(Regex).GetField("pattern", BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var rect3 = rect(3);
            var r1 = rect3(4);
            var word = regex("word");
            var complete = word(RegexOptions.IgnoreCase);

            Assert.IsInstanceOfType(r1, typeof(Rectangle));
            Assert.AreEqual(r1.Width, 3);
            Assert.AreEqual(r1.Height, 4);
            Assert.AreEqual(r1.Area(), 12);
            Assert.IsInstanceOfType(complete, typeof(Regex));
            Assert.AreEqual(prop.GetValue(complete), "word");
            Assert.AreEqual(complete.Options, RegexOptions.IgnoreCase);
        }
    }
}
