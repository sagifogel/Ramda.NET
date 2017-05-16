using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Nth
    {
        private string[] list = new[] { "foo", "bar", "baz", "quux" };

        [TestMethod]
        public void Nth_Accepts_Positive_Offsets() {
            Assert.AreEqual(R.Nth(0, list), "foo");
            Assert.AreEqual(R.Nth(1, list), "bar");
            Assert.AreEqual(R.Nth(2, list), "baz");
            Assert.AreEqual(R.Nth(3, list), "quux");
            Assert.AreEqual(R.Nth(4, list), R.@null);
            Assert.AreEqual(R.Nth(0, "abc"), "a");
            Assert.AreEqual(R.Nth(1, "abc"), "b");
            Assert.AreEqual(R.Nth(2, "abc"), "c");
            Assert.AreEqual(R.Nth(3, "abc"), "");
        }

        [TestMethod]
        public void Nth_Accepts_Negative_Offsets() {
            Assert.AreEqual(R.Nth(-1, list), "quux");
            Assert.AreEqual(R.Nth(-2, list), "baz");
            Assert.AreEqual(R.Nth(-3, list), "bar");
            Assert.AreEqual(R.Nth(-4, list), "foo");
            Assert.AreEqual(R.Nth(-5, list), R.@null);
            Assert.AreEqual(R.Nth(-1, "abc"), "c");
            Assert.AreEqual(R.Nth(-2, "abc"), "b");
            Assert.AreEqual(R.Nth(-3, "abc"), "a");
            Assert.AreEqual(R.Nth(-4, "abc"), "");
        }
    }
}
