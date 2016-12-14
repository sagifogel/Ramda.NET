﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Dynamic;
using FluentAssertions;
using System.Collections.Generic;
using System.Collections;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Clone
    {
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
        public void Clone_Deep_Clone_Objects_Deep_Clone_Arrays_Clones_Shallow_Arrays() {
            var list = new List<int> { 1, 2, 3 };
            var clone = R.Clone(list);

            list.RemoveAt(list.Count - 1);

            CollectionAssert.AreEqual((ICollection)clone, new List<int> { 1, 2, 3 });
        }


        [TestMethod]
        public void Clone_Deep_Clone_Objects_Deep_Clone_Arrays_Clones_Deep_Arrays() {
            var list = new object[] { 1, new[] { 1, 2, 3 }, new object[] { new object[] { new[] { 5 } } } };
            var cloned = (IList)R.Clone(list);
            var third = (IList)list[2];
            var clonedthirdItem = (IList)cloned[2];

            Assert.AreNotSame(cloned[2], list[2]);
            Assert.IsTrue(((ICollection)third[0]).SequenceEqual((ICollection)clonedthirdItem[0]));
        }

        [TestMethod]
        public void Clone_Deep_Clone_Objects_Deep_Clone_Functions_Keep_Reference_To_Function() {
            Func<int, int> fn = x => x + x;
            var list = new object[] { new { a = fn } };
            var clone = R.Clone(list);

            Assert.AreEqual(clone[0].a(10), 20);
            Assert.AreSame(((dynamic)list[0]).a, clone[0].a);
        }
    }
}
