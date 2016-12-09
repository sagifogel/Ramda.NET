using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Always
    {
        [TestMethod]
        public void Always_Returns_A_Function_That_Returns_The_Object_Initially_Supplied() {
            var theMeaning = R.Always(42);

            Assert.AreEqual(theMeaning(), 42);
            Assert.AreEqual(theMeaning(10), 42);
            Assert.AreEqual(theMeaning(false), 42);
        }

        [TestMethod]
        public void Always_Works_With_Various_Types() {
            var obj = new { a = 1, b = 2 };
            var now = new DateTime(1776, 6, 4);

            Assert.AreEqual(R.Always(false)(), false);
            Assert.AreEqual(R.Always("abc")(), "abc");
            Assert.AreEqual(R.Always(new { a = 1, b = 2 })(), new { a = 1, b = 2 });
            Assert.AreEqual(R.Always(obj)(), obj);
            Assert.AreEqual(R.Always(now)(), new DateTime(1776, 6, 4));
        }

        [TestMethod]
        public void Always_Returns_Initial_Argument() {
            var a = 10;
            var f = R.Always(a);

            Assert.AreEqual((int)f(), a);
            Assert.AreEqual((int)f(10), a);
        }
    }
}
