using System;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class ConstructN : AbstractConstruct
    {
        [TestMethod]
        public void ConstructN_Turns_A_Constructor_Function_Into_A_Function_With_N_Arguments() {
            dynamic r1;
            dynamic pattern;
            var regex = R.ConstructN(1, new Func<string, Regex>(p => new Regex(p)));
            var rect = R.ConstructN(2, new Func<int, string, Rectangle>((int r, string color) => new Rectangle(r, color)));
            var prop = typeof(Regex).GetField("pattern", BindingFlags.Instance | BindingFlags.NonPublic);

            r1 = rect(1, "Red");
            pattern = regex("[a-z]");

            Assert.IsInstanceOfType(r1, typeof(Rectangle));
            Assert.AreEqual(r1.PieArea(), Math.PI);
            Assert.IsInstanceOfType(pattern, typeof(Regex));
            Assert.AreEqual(prop.GetValue(pattern), "[a-z]");
        }

        [TestMethod]
        public void ConstructN_Can_Be_Used_To_Create_Date_Object() {
            var date = R.ConstructN(7, new Func<int, int, int, int, int, int, int, DateTime>((y, m, d, h, mi, s, ms) => new DateTime(y, m, d, h, mi, s, ms)))(1984, 3, 26, 0, 0, 0, 0);

            Assert.IsInstanceOfType(date, typeof(DateTime));
            Assert.AreEqual(date.Year, 1984);
        }

        [TestMethod]
        public void ConstructN_Supports_Constructors_With_No_Arguments() {
            var foo = R.ConstructN(0, new Func<Foo>(() => new Foo()));

            Assert.IsInstanceOfType(foo(), typeof(Foo));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ConstructN_Does_Not_Support_Constructor_With_Greater_Than_Ten_Arguments() {
            dynamic construct = new Func<object, object, object, object, object, object, object, object, object, object, object, Foo>((a0, a1, a2, a3, a4, a5, a6, a7, a8, a9, a10) => new Foo(a0, a1, a2, a3, a4, a5, a6, a7, a8, a9, a10));

            var foo = R.ConstructN(11, construct);
        }

        [TestMethod]
        public void ConstructN_Returns_A_Curried_Function() {
            var rect = R.ConstructN(2, new Func<int, int, Rectangle>((int w, int h) => new Rectangle(w, h)));
            var regex = R.ConstructN(2, new Func<string, RegexOptions, Regex>((string pattern, RegexOptions options) => new Regex(pattern, options)));
            var prop = typeof(Regex).GetField("pattern", BindingFlags.Instance | BindingFlags.NonPublic);
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
