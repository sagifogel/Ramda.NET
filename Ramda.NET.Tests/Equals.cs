using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;
using System.Dynamic;
using System.Collections.Generic;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Equals
    {
        class Point : IEquatable<Point>
        {
            public int X { get; private set; }
            public int Y { get; private set; }

            public Point(int x, int y) {
                X = x;
                Y = y;
            }

            public bool Equals(Point other) {
                if (other == null) {
                    return false;
                }

                return X == other.X && Y == other.Y;
            }

            public override int GetHashCode() {
                return X.GetHashCode() ^ Y.GetHashCode();
            }
        }

        class ColorPoint : Point, IEquatable<ColorPoint>
        {
            public string Color { get; private set; }

            public ColorPoint(int x, int y, string color) : base(x, y) {
                Color = color;
            }

            public override bool Equals(object other) {
                return base.Equals(other as ColorPoint);
            }

            public bool Equals(ColorPoint other) {
                if (other == null) {
                    return false;
                }

                return base.Equals(other) && Color == other.Color;
            }

            public override int GetHashCode() {
                return base.GetHashCode() ^ Color.GetHashCode();
            }
        }

        class Left : IEquatable<Left>
        {
            public object Value { get; private set; }

            public Left(object x) {
                Value = x;
            }

            public override bool Equals(object obj) {
                return Equals(obj as Left);
            }

            public bool Equals(Left other) {
                if (other == null) {
                    return false;
                }

                return R.Equals(Value, other.Value);
            }

            public override int GetHashCode() {
                return Value.GetHashCode();
            }
        }

        class Right : IEquatable<Right>
        {
            public object Value { get; private set; }

            public Right(object x) {
                Value = x;
            }

            public override bool Equals(object obj) {
                return Equals(obj as Right);
            }

            public bool Equals(Right other) {
                if (other == null) {
                    return false;
                }

                return R.Equals(Value, other.Value);
            }

            public override int GetHashCode() {
                return Value.GetHashCode();
            }
        }

        [TestMethod]
        public void Equals_Tests_For_Deep_Equality_Of_Its_Operands() {
            var a = new object[0];
            var b = a;

            Assert.IsTrue(R.Equals(100, 100));
            Assert.IsFalse(R.Equals(100, "100"));
            Assert.IsTrue(R.Equals(new object[0], new object[0]));
            Assert.IsTrue(R.Equals(a, b));
        }

        [TestMethod]
        public void Equals_Considers_Equal_Boolean_Primitives_Equal() {
            Assert.IsTrue(R.Equals(true, true));
            Assert.IsTrue(R.Equals(false, false));
            Assert.IsFalse(R.Equals(true, false));
            Assert.IsFalse(R.Equals(false, true));
        }

        [TestMethod]
        public void Equals_Considers_Equal_Number_Primitives_Equal() {
            Assert.IsTrue(R.Equals(0, 0));
            Assert.IsFalse(R.Equals(0, 1));
            Assert.IsFalse(R.Equals(1, 0));
        }

        [TestMethod]
        public void Equals_Considers_Equal_String_Primitives_Equal() {
            Assert.IsTrue(R.Equals("", ""));
            Assert.IsFalse(R.Equals("", "x"));
            Assert.IsFalse(R.Equals("x", ""));
            Assert.IsTrue(R.Equals("foo", "foo"));
            Assert.IsFalse(R.Equals("foo", "bar"));
            Assert.IsFalse(R.Equals("bar", "foo"));
        }

        [TestMethod]
        public void Equals_Considers_Equivalent_String_Objects_Equal() {
            var emptyCharArray = string.Empty.ToCharArray();

            Assert.IsTrue(R.Equals(new string(emptyCharArray), new string(emptyCharArray)));
            Assert.IsFalse(R.Equals(new string(new[] { ' ' }), new string(new[] { 'x' })));
            Assert.IsFalse(R.Equals(new string(new[] { 'x' }), new string(emptyCharArray)));
            Assert.IsTrue(R.Equals(new string(new[] { 'f', 'o', 'o' }), new string(new[] { 'f', 'o', 'o' })));
            Assert.IsFalse(R.Equals(new string(new[] { 'f', 'o', 'o' }), new string(new[] { 'b', 'a', 'r' })));
            Assert.IsFalse(R.Equals(new string(new[] { 'b', 'a', 'r' }), new string(new[] { 'f', 'o', 'o' })));
        }

        [TestMethod]
        public void Equals_Handles_Objects() {
            Assert.IsTrue(R.Equals(new { }, new { }));
            Assert.IsTrue(R.Equals(new { A = 1, B = 2 }, new { A = 1, B = 2 }));
            Assert.IsTrue(R.Equals(new { A = 2, B = 3 }, new { B = 3, A = 2 }));
            Assert.IsFalse(R.Equals(new { A = 2, B = 3 }, new { A = 3, B = 3 }));
            Assert.IsFalse(R.Equals(new { A = 2, B = 3, C = 1 }, new { A = 2, B = 3 }));
        }

        [TestMethod]
        public void Equals_Considers_Equivalent_Error_Objects_Equal() {
            Assert.IsTrue(R.Equals(new Exception("XXX"), new Exception("XXX")));
            Assert.IsFalse(R.Equals(new Exception("XXX"), new Exception("YYY")));
            Assert.IsFalse(R.Equals(new Exception("XXX"), new DivideByZeroException("XXX")));
            Assert.IsFalse(R.Equals(new Exception("XXX"), new DivideByZeroException("YYY")));
        }

        [TestMethod]
        public void Equals_Handles_Regex() {
            Assert.IsTrue(R.Equals(new Regex("\\s"), new Regex("\\s")));
            Assert.IsFalse(R.Equals(new Regex("\\s"), new Regex("\\d")));
            Assert.IsFalse(R.Equals(new Regex("(?is)a"), new Regex("(?si)a")));
            Assert.IsFalse(R.Equals(new Regex("(?i)a"), new Regex("(?si)a")));
        }

        [TestMethod]
        public void Equals_Handles_Lists() {
            Assert.IsFalse(R.Equals(new[] { 1, 2, 3 }, new[] { 1, 3, 2 }));
        }

        [TestMethod]
        public void Equals_Handles_Recursive_Data_Structures() {
            object nestA;
            object nestB;
            object nestC;
            dynamic c = new ExpandoObject();
            dynamic d = new ExpandoObject();
            var f = new object[1];
            var e = new object[1];

            d.v = d;
            c.v = c;
            e[0] = e;
            f[0] = f;
            nestA = new { A = new object[] { 1, 2, new { C = 1 } }, B = 1 };
            nestB = new { A = new object[] { 1, 2, new { C = 1 } }, B = 1 };
            nestC = new { A = new object[] { 1, 2, new { C = 2 } }, B = 1 };

            Assert.IsTrue(R.Equals(c, d));
            Assert.IsTrue(R.Equals(e, f));
            Assert.IsTrue(R.Equals(nestA, nestB));
            Assert.IsFalse(R.Equals(nestA, nestC));
        }

        [TestMethod]
        public void Equals_Handles_Dates() {
            Assert.IsTrue(R.Equals(new DateTime(0), new DateTime(0)));
            Assert.IsTrue(R.Equals(new DateTime(1), new DateTime(1)));
            Assert.IsFalse(R.Equals(new DateTime(0), new DateTime(1)));
            Assert.IsFalse(R.Equals(new DateTime(1), new DateTime(0)));
        }

        [TestMethod]
        public void Equals_Compares_Map_Objects_By_Value() {
            var emptyDictionary = new Dictionary<object, object>();
            var dictionary1 = new Dictionary<int, object> { [1] = "a" };
            var dictionary2 = new Dictionary<int, object> { [1] = "b" };
            var dictionary3 = new Dictionary<int, object> { [1] = "a", [2] = new Dictionary<int, string> { [3] = "c" } };
            var dictionary4 = new Dictionary<int[], int[]> { [new int[] { 1, 2, 3 }] = new int[] { 4, 5, 6 } };

            Assert.IsTrue(R.Equals(emptyDictionary, new Dictionary<object, object>()));
            Assert.IsFalse(R.Equals(emptyDictionary, dictionary1));
            Assert.IsFalse(R.Equals(dictionary1, emptyDictionary));
            Assert.IsTrue(R.Equals(dictionary1, new Dictionary<int, object> { [1] = "a" }));
            Assert.IsFalse(R.Equals(dictionary1, dictionary2));
            Assert.IsTrue(R.Equals(dictionary3, new Dictionary<int, object> { [1] = "a", [2] = new Dictionary<int, string> { [3] = "c" } }));
            Assert.IsFalse(R.Equals(dictionary3, new Dictionary<int, object> { [1] = "a", [2] = new Dictionary<int, string> { [3] = "d" } }));
            Assert.IsTrue(R.Equals(dictionary4, new Dictionary<int[], int[]> { [new int[] { 1, 2, 3 }] = new int[] { 4, 5, 6 } }));
            Assert.IsFalse(R.Equals(dictionary4, new Dictionary<int[], int[]> { [new int[] { 1, 2, 3 }] = new int[] { 7, 8, 9 } }));
        }

        [TestMethod]
        public void Equals_Compares_Set_Objects_By_Value() {
            var emptySet = new HashSet<object>();
            var set1 = new HashSet<int> { 1 };
            var set2 = new HashSet<object> { 1, new HashSet<object> { 2, new HashSet<object> { 3 } } };
            var set3 = new HashSet<int[]> { new int[] { 1, 2, 3 }, new int[] { 4, 5, 6 } };

            Assert.IsTrue(R.Equals(emptySet, new HashSet<object>()));
            Assert.IsFalse(R.Equals(emptySet, set1));
            Assert.IsFalse(R.Equals(set1, emptySet));
            Assert.IsTrue(R.Equals(set2, new HashSet<object> { 1, new HashSet<object> { 2, new HashSet<object> { 3 } } }));
            Assert.IsFalse(R.Equals(set1, new HashSet<object> { 1, new HashSet<object> { 2, new HashSet<object> { 4 } } }));
            Assert.IsTrue(R.Equals(set3, new HashSet<int[]> { new int[] { 1, 2, 3 }, new int[] { 4, 5, 6 } }));
            Assert.IsFalse(R.Equals(set3, new HashSet<int[]> { new int[] { 1, 2, 3 }, new int[] { 7, 8, 9 } }));
        }

        [TestMethod]
        [Description("Equals_Dispatches_To_\"Equals\"_Method_Recursively")]
        public void Equals_Dispatches_To_Equals_Method_Recursively() {
            Assert.IsTrue(R.Equals(new Left(new[] { 42 }), new Left(new[] { 42 })));
            Assert.IsFalse(R.Equals(new Left(new[] { 42 }), new Left(new[] { 43 })));
            Assert.IsFalse(R.Equals(new Left(42), new { Value = 42 }));
            Assert.IsFalse(R.Equals(new { Value = 42 }, new Left(42)));
            Assert.IsFalse(R.Equals(new Left(42), new Right(42)));
            Assert.IsFalse(R.Equals(new Right(42), new Left(42)));
            Assert.IsTrue(R.Equals(new[] { new Left(42) }, new[] { new Left(42) }));
            Assert.IsFalse(R.Equals(new[] { new Left(42) }, new[] { new Right(42) }));
            Assert.IsFalse(R.Equals(new[] { new Right(42) }, new[] { new Left(42) }));
            Assert.IsTrue(R.Equals(new[] { new Right(42) }, new[] { new Right(42) }));
        }

        [TestMethod]
        public void Equals_Is_Commutative() {
            Assert.IsFalse(R.Equals(new Point(2, 2), new ColorPoint(2, 2, "red")));
            Assert.IsFalse(R.Equals(new ColorPoint(2, 2, "red"), new Point(2, 2)));
        }

        [TestMethod]
        public void Equals_Is_Curried() {
            var isA = R.Equals(new object[0]);

            Assert.IsTrue(isA(new object[0]));
        }
    }
}
