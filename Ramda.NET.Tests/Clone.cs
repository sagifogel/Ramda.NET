using System;
using System.Dynamic;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Clone
    {
        class ArbitraryClone : ICloneable, IEquatable<ArbitraryClone>
        {
            public int Value { get; set; }

            public ArbitraryClone(int x) {
                Value = x;
            }

            public object Clone() {
                return new ArbitraryClone(Value);
            }

            public override bool Equals(object obj) {
                return Equals((ArbitraryClone)obj);
            }

            public bool Equals(ArbitraryClone other) {
                if (other != null) {
                    return Value == other.Value;
                }

                return false;
            }

            public override int GetHashCode() {
                return Value.GetHashCode();
            }
        }

        public class Obj
        {
            public int X { get; set; }
        }

        public class Test
        {
            public object c { get; set; }
        }

        public class Test2
        {
            public object a { get; set; }
        }

        public class Test3
        {
            public object b { get; set; }
        }

        [TestMethod]
        [Description("Clone_Deep_Clone_Integers,_Strings_And_Booleans_Clones_Integers")]
        public void Clone_Deep_Clone_Integers_Strings_And_Booleans_Clones_Integers() {
            Assert.AreEqual(R.Clone(-4), -4);
            Assert.AreEqual(R.Clone(9007199254740991), 9007199254740991);
        }

        [TestMethod]
        [Description("Clone_Deep_Clone_Integers,_Strings_And_Booleans_Clones_Floats")]
        public void Clone_Deep_Clone_Integers_Strings_And_Booleans_Clones_Floats() {
            Assert.AreEqual(R.Clone(-4.5), -4.5);
            Assert.AreEqual(R.Clone(0d), 0d);
        }

        [TestMethod]
        [Description("Clone_Deep_Clone_Integers,_Strings_And_Booleans_Clones_Strings")]
        public void Clone_Deep_Clone_Integers_Strings_And_Booleans_Clones_Strings() {
            Assert.AreEqual(R.Clone("ramda"), "ramda");
        }

        [TestMethod]
        [Description("Clone_Deep_Clone_Integers,_Strings_And_Booleans_Clones_Booleans")]
        public void Clone_Deep_Clone_Integers_Strings_And_Booleans_Clones_Booleans() {
            Assert.AreEqual(R.Clone(true), true);
        }


        [TestMethod]
        public void Clone_Deep_Clone_Objects_Clones_Shallow_Object() {
            var obj = new { a = 1, b = "ramda", c = true, d = new DateTime(2013, 12, 25) };
            object clone = R.Clone(obj);
            dynamic expando = clone.ToExpando();
            dynamic dynamicObj = obj.ToExpando();

            dynamicObj.c = false;
            dynamicObj.d = dynamicObj.d.AddDays(6);
            Assert.IsTrue(((ExpandoObject)expando).ContentEquals(new { a = 1, b = "ramda", c = true, d = new DateTime(2013, 12, 25) }.ToExpando()));
        }

        [TestMethod]
        public void Clone_Deep_Clone_Objects_Clones_Deep_Object() {
            var obj = new { a = new { b = new { c = "ramda" } } };
            ExpandoObject cloned = ((object)R.Clone(obj)).ToDynamic();

            Assert.IsTrue(cloned.ContentEquals(obj.ToExpando()));
        }

        [TestMethod]
        public void Clone_Clones_Objects_With_Circular_References() {
            dynamic clone;
            dynamic dynamicX;
            var x = new Test { c = (object)null };
            var y = new Test2 { a = x };
            var z = new Test3 { b = y };

            x.c = z;
            dynamicX = x;
            clone = (Test)R.Clone(x);

            Assert.AreNotSame(x, clone);
            Assert.AreNotSame(x.c, clone.c);
            Assert.AreNotSame(((dynamic)x).c.b, clone.c.b);
            Assert.AreNotSame(((dynamic)x).c.b.a, clone.c.b.a);
            Assert.AreNotSame(((dynamic)x).c.b.a.c, clone.c.b.a.c);
            Assert.AreNotSame(R.Keys(clone), R.Keys(x));
            Assert.AreNotSame(R.Keys(clone.c), R.Keys(x.c));
            Assert.AreNotSame(R.Keys(clone.c.b), R.Keys(dynamicX.c.b));
            Assert.AreNotSame(R.Keys(clone.c.b.a), R.Keys(dynamicX.c.b.a));
            Assert.AreNotSame(R.Keys(clone.c.b.a.c), R.Keys(dynamicX.c.b.a.c));

            dynamicX.c.b = 1;
            Assert.AreNotEqual(dynamicX.c.b, clone.c.b);
        }

        [TestMethod]
        public void Clone_Deep_Clone_Objects_Clone_Instances() {
            var obj = new Obj { X = 10 };
            var clone = R.Clone(obj);

            Assert.AreEqual(obj.X, 10);
            Assert.AreEqual(clone.X, 10);
            Assert.AreNotSame(obj, clone);

            obj.X = 11;
            Assert.AreEqual(obj.X, 11);
            Assert.AreEqual(clone.X, 10);
        }

        [TestMethod]
        public void Clone_Deep_Clone_Arrays_Clones_Shallow_Arrays() {
            var list = new List<int> { 1, 2, 3 };
            var clone = R.Clone(list);

            list.RemoveAt(list.Count - 1);

            CollectionAssert.AreEqual((ICollection)clone, new List<int> { 1, 2, 3 });
        }


        [TestMethod]
        public void Clone_Deep_Clone_Arrays_Clones_Deep_Arrays() {
            var list = new object[] { 1, new[] { 1, 2, 3 }, new object[] { new object[] { new[] { 5 } } } };
            var cloned = (IList)R.Clone(list);
            var third = (IList)list[2];
            var clonedthirdItem = (IList)cloned[2];

            Assert.AreNotSame(cloned[2], list[2]);
            Assert.IsTrue(((ICollection)third[0]).SequenceEqual((ICollection)clonedthirdItem[0]));
        }

        [TestMethod]
        public void Clone_Deep_Clone_Functions_Keep_Reference_To_Function() {
            Func<int, int> fn = x => x + x;
            var list = new object[] { new { a = fn } };
            var clone = R.Clone(list);

            Assert.AreEqual(clone[0].a(10), 20);
            Assert.AreSame(((dynamic)list[0]).a, clone[0].a);
        }

        [TestMethod]
        [Description("Clone_Deep_Built-in_Types_Clones_Date_Object")]
        public void Clone_Deep_Built_In_Types_Clones_Date_Object() {
            var date = new DateTime(2014, 11, 14, 23, 59, 59, DateTimeKind.Local);
            var clone = R.Clone(date);

            Assert.AreNotSame(date, clone);
            Assert.AreEqual(clone, new DateTime(2014, 11, 14, 23, 59, 59, DateTimeKind.Local));
            Assert.AreEqual(clone.DayOfWeek, DayOfWeek.Friday);
        }

        [TestMethod]
        [Description("Clone_Deep_Built-in_Types_Regex_Object")]
        public void Clone_Deep_Built_in_Types_Clones_Regex_Object() {
            var typeofRegex = typeof(Regex);
            Func<Regex, string> extractPattern = regex => {
                return (string)typeofRegex.GetField("pattern", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(regex);
            };

            R.ForEach(regex => {
                var clone = (Regex)R.Clone(regex);

                Assert.AreNotSame(clone, regex);
                Assert.IsInstanceOfType(clone, typeofRegex);
                Assert.AreEqual(clone.Options, regex.Options);
                Assert.AreEqual(extractPattern(clone), extractPattern(regex));
            }, new[] { new Regex("x"), new Regex("x", RegexOptions.IgnoreCase), new Regex("x", RegexOptions.Multiline), new Regex("x", RegexOptions.IgnoreCase | RegexOptions.Multiline) });
        }

        [TestMethod]
        public void Clone_Deep_Clone_Deep_Nested_Mixed_Objects_Clones_Array_With_Objects() {
            var list = new object[] { new { a = new { b = 1 } }, new object[] { new { c = new { d = 1 } } } };
            var clone = R.Clone(list);

            ((IList)list[1])[0] = null;
            Assert.IsTrue(((ICollection)clone).SequenceEqual(new object[] { new { a = new { b = 1 } }, new object[] { new { c = new { d = 1 } } } }));
        }

        [TestMethod]
        public void Clone_Deep_Clone_Deep_Nested_Mixed_Objects_Clones_Array_With_Mutual_Ref_Object() {
            var obj = new Test2 { a = 1 };
            var list = new object[] { new { b = obj }, new { b = obj } };
            var clone = R.Clone(list);
            var dynamicList = (dynamic)list;
            var excpected = new { a = 1 }.ToExpando();

            Assert.AreEqual(dynamicList[0].b, dynamicList[1].b);
            Assert.AreEqual(clone[0].b, clone[1].b);
            Assert.AreNotEqual(clone[0].b, dynamicList[0].b);
            Assert.AreNotEqual(clone[1].b, dynamicList[1].b);
            Assert.IsTrue(((object)clone[0].b).ToExpando().ContentEquals(excpected));
            Assert.IsTrue(((object)clone[1].b).ToExpando().ContentEquals(excpected));
            obj.a = 2;
            Assert.IsTrue(((object)clone[0].b).ToExpando().ContentEquals(excpected));
            Assert.IsTrue(((object)clone[1].b).ToExpando().ContentEquals(excpected));
        }

        [TestMethod]

        public void Clone_Deep_Clone_Edge_Cases_Nulls_And_Empty_Objects_And_Arrays() {
            var obj = new { };
            var list = new object[0];

            Assert.AreEqual(R.Clone(R.Null), new Nothing());
            Assert.IsTrue(((object)R.Clone(obj)).ToExpando().ContentEquals(obj.ToExpando()));
            Assert.AreNotSame(R.Clone(list), list);
        }

        [TestMethod]
        [Description("Clone_Let_`R.Clone`_Use_An_Arbitrary_User_Defined_`Clone`_Method")]
        public void Clone_Let_Clone_Use_An_Arbitrary_User_Defined_Clone_Method() {
            var obj = new ArbitraryClone(42);
            var arbitraryClonedObj = R.Clone(obj);

            Assert.AreEqual(arbitraryClonedObj, new ArbitraryClone(42));
            Assert.IsInstanceOfType(arbitraryClonedObj, typeof(ArbitraryClone));
        }
    }
}
