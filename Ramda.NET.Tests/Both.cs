using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Both
    {
        private Func<bool> F = () => false;
        private Func<int, bool> gt10 = x => x > 10;
        private Func<int, bool> even = n => n % 2 == 0;
        private Func<int, int, int, bool> between = (a, b, c) => a < b && b < c;
        private Func<int, int, int, bool> total20 = (a, b, c) => a + b + c == 20;

        [TestMethod]
        [Description("Both_Combines_Two_Boolean-Returning_Functions_Into_One")]
        public void Both_Combines_Two_Boolean_Returning_Functions_Into_One() {
            var f = R.Both(even, gt10);

            Assert.AreEqual(f(8), false);
            Assert.AreEqual(f(13), false);
            Assert.AreEqual(f(14), true);
        }

        [TestMethod]
        public void Both_Accepts_Functions_That_Take_Multiple_Parameters() {
            var f = R.Both(between, total20);

            Assert.AreEqual(f(4, 5, 11), true);
            Assert.AreEqual(f(12, 2, 6), false);
            Assert.AreEqual(f(5, 6, 15), false);
        }

        [TestMethod]
        public void Both_Does_Not_Evaluate_The_Second_Expression_If_The_First_One_Is_False() {
            var effect = "not evaluated";
            Func<bool> Z = () => {
                effect = "Z got evaluated";
                return true;
            };

            R.Both(F, Z)();
            Assert.AreEqual(effect, "not evaluated");
        }

        [TestMethod]
        public void Both_Is_Curried() {
            var evenAnd = R.Both(even);

            Assert.IsInstanceOfType(evenAnd(gt10), typeof(DynamicDelegate));
            Assert.AreEqual(evenAnd(gt10)(11), false);
            Assert.AreEqual(evenAnd(gt10)(12), true);
        }
    }
}
