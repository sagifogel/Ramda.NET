using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Is
    {
        class Foo
        {
        }

        class Bar : Foo
        {

        }

        [TestMethod]
        [Description("Is_Works_With_Built-in_Types")]
        public void Is_Works_With_Built_In_Types() {
            Assert.IsTrue(R.Is(typeof(object[]), new object[0]));
            Assert.IsTrue(R.Is(typeof(bool), true));
            Assert.IsTrue(R.Is(typeof(DateTime), new DateTime()));
            Assert.IsTrue(R.Is(typeof(Action), new Action(() => { })));
            Assert.IsTrue(R.Is(typeof(int), 1));
            Assert.IsTrue(R.Is(typeof(object), new object()));
            Assert.IsTrue(R.Is(typeof(Regex), new Regex("(?:)")));
            Assert.IsTrue(R.Is(typeof(string), string.Empty));
        }

        [TestMethod]
        [Description("Is_Works_With_User-Defined_Types")]
        public void Is_Works_With_User_Defined_Types() {
            var foo = new Foo();
            var bar = new Bar();

            Assert.IsTrue(R.Is(typeof(Foo), foo));
            Assert.IsTrue(R.Is(typeof(Bar), bar));
            Assert.IsTrue(R.Is(typeof(Foo), bar));
            Assert.IsFalse(R.Is(typeof(Bar), foo));
        }

        [TestMethod]
        public void Is_Is_Curried() {
            var isArray = R.Is(typeof(Array));

            Assert.IsInstanceOfType(R.Is(typeof(string)), typeof(DynamicDelegate));
            Assert.IsTrue(R.Is(typeof(string))("s"));
            Assert.IsTrue(isArray(new object[0]));
            Assert.IsFalse(isArray(new { }));
        }

        [TestMethod]
        public void Is_Considers_Almost_Everything_An_Object() {
            var foo = new Foo();
            var isObject = R.Is(typeof(object));

            Assert.IsTrue(isObject(foo));
            Assert.IsTrue(isObject(new Action(() => { })));
            Assert.IsTrue(isObject(new object[0]));
            Assert.IsTrue(isObject(string.Empty));
            Assert.IsTrue(isObject(new object()));
            Assert.IsTrue(isObject(new Regex("(?:)")));
            Assert.IsFalse(isObject(R.@null));
        }

        [TestMethod]
        public void Is_Does_Not_Coerce() {
            var foo = new Foo();
            var isObject = R.Is(typeof(object));

            Assert.IsFalse(R.Is(typeof(bool), 1));
            Assert.IsFalse(R.Is(typeof(int), "1"));
            Assert.IsFalse(R.Is(typeof(int), false));
        }

        [TestMethod]
        public void Is_Recognizes_Primitives_As_Their_Object_Equivalents() {
            var foo = new Foo();
            var isObject = R.Is(typeof(object));

            Assert.IsTrue(R.Is(typeof(bool), false));
            Assert.IsTrue(R.Is(typeof(int), 0));
            Assert.IsTrue(R.Is(typeof(string), ""));
        }

        [TestMethod]
        public void Is_Does_Consider_Primitives_To_Be_Instances_Of_Object() {
            Assert.IsTrue(R.Is(typeof(object), false));
            Assert.IsTrue(R.Is(typeof(object), 0));
            Assert.IsTrue(R.Is(typeof(object), ""));
        }
    }
}
