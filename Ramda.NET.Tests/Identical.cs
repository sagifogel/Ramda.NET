using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Identical
    {
        private readonly object[] b;
        private readonly object[] a = new object[0];

        public Identical() {
            b = a;
        }

        [TestMethod]
        [Description("Identical_Has_Object.Is_Semantics")]
        public void Identical_Has_Object_Is_Semantics() {
            Assert.IsTrue(R.Identical(100, 100));
            Assert.IsTrue(R.Identical("string", "string"));
            Assert.IsFalse(R.Identical(new object[0], new object[0]));
            Assert.IsTrue(R.Identical(a, b));
            Assert.IsTrue(R.Identical(R.@null, R.@null));
            Assert.IsTrue(R.Identical(-0, 0));
            Assert.IsTrue(R.Identical(0, -0));
        }

        [TestMethod]
        public void Identical_Is_Curried() {
            var isA = R.Identical(a);

            Assert.IsFalse(isA(new object[0]));
        }
    }
}
