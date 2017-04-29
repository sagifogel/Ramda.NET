using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class IsEmpty
    {
        public object[] Arguments(params object[] args) {
            return args;
        }

        [TestMethod]
        public void IsEmpty_Returns_False_For_Null() {
            Assert.IsFalse(R.IsEmpty(R.Null));
        }

        [TestMethod]
        public void IsEmpty_Returns_True_For_Empty_String() {
            Assert.IsTrue(R.IsEmpty(""));
            Assert.IsFalse(R.IsEmpty(" "));
        }

        [TestMethod]
        public void IsEmpty_Returns_True_For_Empty_Arguments_Object() {
            Assert.IsTrue(R.IsEmpty(Arguments()));
            Assert.IsFalse(R.IsEmpty(Arguments(0)));
        }

        [TestMethod]
        public void IsEmpty_Returns_False_For_Every_Other_Value() {
            Assert.IsFalse(R.IsEmpty(0));
            Assert.IsFalse(R.IsEmpty(new[] { string.Empty }));
        }
    }
}
