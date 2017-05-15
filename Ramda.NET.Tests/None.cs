using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class None
    {
        class Test
        {
            public string X { get; set; }
        }

        private Func<bool> T = () => true;
        private Func<int, bool> even = n => n % 2 == 0;

        [TestMethod]
        public void None_Returns_True_If_No_Elements_Satisfy_The_Predicate() {
            Assert.IsTrue(R.None(even, new[] { 1, 3, 5, 7, 9, 11 }));
        }

        [TestMethod]
        public void None_Returns_False_If_Any_Element_Satisfies_The_Predicate() {
            Assert.IsFalse(R.None(even, new[] { 1, 3, 5, 7, 8, 11 }));
        }

        [TestMethod]
        public void None_Returns_True_For_An_Empty_List() {
            Assert.IsTrue(R.None(T, new int[0]));
        }

        [TestMethod]
        public void None_Works_With_More_Complex_Objects() {
            var xs = new object[] { new Test { X = "abcd" }, new Test { X = "adef" }, new Test { X = "fghiajk" } };
            Func<Test, bool> len3 = o => o.X.Length == 3;
            Func<Test, bool> hasA = o => o.X.IndexOf("a") >= 0;

            Assert.IsTrue(R.None(len3, xs));
            Assert.IsFalse(R.None(hasA, xs));
        }

        [TestMethod]
        public void None_Is_Curried() {
            Assert.IsFalse(R.None(even)(new[] { 1, 3, 5, 6, 7, 9 }));
        }
    }
}
