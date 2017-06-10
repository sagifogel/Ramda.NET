using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Dynamic;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class ToString
    {
        class Point
        {
            public int X { get; set; }
            public int Y { get; set; }

            public Point(int x, int y) {
                X = x;
                Y = y;
            }
        }

        class Point2 : Point
        {
            public Point2(int x, int y) : base(x, y) {
            }

            public override string ToString() {
                return $"new Point({X}, {Y})";
            }
        }

        [TestMethod]
        [Description("ToString_Returns_The_String_Representation_Of_Null")]
        public void ToString_Returns_The_String_Representation_Of_R_Null() {
        }

        [TestMethod]
        public void ToString_Returns_The_String_Representation_Of_A_Number_Primitive() {
            Assert.AreEqual(R.ToString(0), "0");
            Assert.AreEqual(R.ToString(1.23), "1.23");
            Assert.AreEqual(R.ToString(-1.23), "-1.23");
            Assert.AreEqual(R.ToString(1e+23), "1E+23");
            Assert.AreEqual(R.ToString(-1e+23), "-1E+23");
            Assert.AreEqual(R.ToString(1e-23), "1E-23");
            Assert.AreEqual(R.ToString(-1e-23), "-1E-23");
        }

        [TestMethod]
        public void ToString_Returns_The_String_Representation_Of_A_String_Primitive() {
            Assert.AreEqual(R.ToString("abc"), "\"abc\"");
            Assert.AreEqual(R.ToString("x \"y\" z"), "\"x \\\"y\\\" z\"");
            Assert.AreEqual(R.ToString("\" \""), "\"\\\" \\\"\"");
            Assert.AreEqual(R.ToString("\b \b"), "\"\\b \\b\"");
            Assert.AreEqual(R.ToString("\f \f"), "\"\\f \\f\"");
            Assert.AreEqual(R.ToString("\n \n"), "\"\n \n\"");
            Assert.AreEqual(R.ToString("\r \r"), "\"\\r \\r\"");
            Assert.AreEqual(R.ToString("\t \t"), "\"\\t \\t\"");
            Assert.AreEqual(R.ToString("\v \v"), "\"\\v \\v\"");
            Assert.AreEqual(R.ToString("\0 \0"), "\"\\0 \\0\"");
        }

        [TestMethod]
        public void ToString_Returns_The_String_Representation_Of_A_Boolean_Object() {
            Assert.AreEqual(R.ToString(new Boolean()), "False");
        }

        [TestMethod]
        [Description("ToString_Returns_The_String_Representation_Of_A_Number_Object")]
        public void ToString_Returns_The_String_Representation_Of_An_Integer_Object() {
            Assert.AreEqual(R.ToString(new Int32()), "0");
        }

        [TestMethod]
        public void ToString_Returns_The_String_Representation_Of_A_String_Object() {
            Assert.AreEqual(R.ToString(new string("abc".ToCharArray())), "\"abc\"");
            Assert.AreEqual(R.ToString(new string("x \"y\" z".ToCharArray())), "\"x \\\"y\\\" z\"");
            Assert.AreEqual(R.ToString(new string("\" \"".ToCharArray())), "\"\\\" \\\"\"");
            Assert.AreEqual(R.ToString(new string("\b \b".ToCharArray())), "\"\\b \\b\"");
            Assert.AreEqual(R.ToString(new string("\f \f".ToCharArray())), "\"\\f \\f\"");
            Assert.AreEqual(R.ToString(new string("\n \n".ToCharArray())), "\"\n \n\"");
            Assert.AreEqual(R.ToString(new string("\r \r".ToCharArray())), "\"\\r \\r\"");
            Assert.AreEqual(R.ToString(new string("\t \t".ToCharArray())), "\"\\t \\t\"");
            Assert.AreEqual(R.ToString(new string("\v \v".ToCharArray())), "\"\\v \\v\"");
            Assert.AreEqual(R.ToString(new string("\0 \0".ToCharArray())), "\"\\0 \\0\"");
        }

        [TestMethod]
        public void ToString_Returns_The_String_Representation_Of_A_Date_Object() {
            Assert.AreEqual(R.ToString(DateTime.Parse("2001-02-03T04:05:06.000Z")), "2/3/2001 6:05:06 AM");
        }

        [TestMethod]
        public void ToString_Returns_The_String_Representation_Of_An_Array() {
            Assert.AreEqual(R.ToString(new object[0]), "[]");
            Assert.AreEqual(R.ToString(new[] { 1, 2, 3 }), "[1, 2, 3]");
            Assert.AreEqual(R.ToString(new object[] { 1, new object[] { 2, new[] { 3 } } }), "[1, [2, [3]]]");
            Assert.AreEqual(R.ToString(new[] { "x", "y" }), "[\"x\", \"y\"]");
        }

        [TestMethod]
        public void ToString_Returns_The_String_Representation_Of_A_Plain_Object() {
            Assert.AreEqual(R.ToString(new { }), "{}");
            Assert.AreEqual(R.ToString(new { FOO = 1, BAR = 2, BAZ = 3 }), "{\"BAR\": 2, \"BAZ\": 3, \"FOO\": 1}");
            Assert.AreEqual(R.ToString(new Dictionary<string, bool> { ["\"quoted\""] = true }), "{\"\\\"quoted\\\"\": True}");
            Assert.AreEqual(R.ToString(new { A = new { B = new { C = new { } } } }), "{\"A\": {\"B\": {\"C\": {}}}}");
        }

        [TestMethod]
        [Description("ToString_Treats_Instance_Without_Custom_\"ToString\"_Method_As_Plain_Object")]
        public void ToString_Treats_Instance_Without_Custom_ToString_Method_As_Plain_Object() {
            Assert.AreEqual(R.ToString(new Point(1, 2)), "{\"X\": 1, \"Y\": 2}");
        }

        [TestMethod]
        [Description("ToString_Dispatches_To_Custom_\"ToString\"_Method")]
        public void ToString_Dispatches_To_Custom_ToString_Method() {
            var Just = new Func<object, Just>(_Maybe.Just);

            Assert.AreEqual(R.ToString(new Point2(1, 2)), "new Point(1, 2)");
            Assert.AreEqual(R.ToString(Just(42)), "Just(42)");
            Assert.AreEqual(R.ToString(Just(new[] { 1, 2, 3 })), "Just([1, 2, 3])");
            Assert.AreEqual(R.ToString(Just(Just(Just("")))), "Just(Just(Just(\"\")))");
        }

        [TestMethod]
        [Description("ToString_Handles_Object_With_No_\"ToString\"_Method")]
        public void ToString_Handles_Object_With_No_ToString_Method() {
            var a = new { };
            var b = new { X = 1, Y = 2 };

            Assert.AreEqual(R.ToString(a), "{}");
            Assert.AreEqual(R.ToString(b), "{\"X\": 1, \"Y\": 2}");
        }

        [TestMethod]
        public void ToString_Handles_Circular_References() {
            var a = new object[1];
            a[0] = a;

            Assert.AreEqual(R.ToString(a), "[<Circular>]");

            dynamic o = new ExpandoObject();
            o.O = o;
            Assert.AreEqual(R.ToString(o), "{\"O\": <Circular>}");

            var b = new object[2] { "bee", null };
            var c = new object[2] { "see", null };
            b[1] = c;
            c[1] = b;
            Assert.AreEqual(R.ToString(b), "[\"bee\", [\"see\", <Circular>]]");
            Assert.AreEqual(R.ToString(c), "[\"see\", [\"bee\", <Circular>]]");

            dynamic p = new ExpandoObject();
            dynamic q = new ExpandoObject();
            p.Q = q;
            q.P = p;

            Assert.AreEqual(R.ToString(p), "{\"Q\": {\"P\": <Circular>}}");
            Assert.AreEqual(R.ToString(q), "{\"P\": {\"Q\": <Circular>}}");

            var x = new object[1];
            dynamic y = new ExpandoObject();

            x[0] = y;
            y.X = x;
            Assert.AreEqual(R.ToString(x), "[{\"X\": <Circular>}]");
            Assert.AreEqual(R.ToString(y), "{\"X\": [<Circular>]}");
        }
    }
}
