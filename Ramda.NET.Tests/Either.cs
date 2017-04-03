using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Either
    {
        private Func<int, bool> gt10 = n => n > 10;
        private Func<int, bool> even = n => n % 2 == 0;

        [TestMethod]
        [Description("Either_Combines_Two_Boolean-Returning_Functions_Into_One")]
        public void Either_Combines_Two_Boolean_Returning_Functions_Into_One() {

            var f = R.Either(even, gt10);

            Assert.AreEqual(f(8), true);
            Assert.AreEqual(f(13), true);
            Assert.AreEqual(f(7), false);
        }

        [TestMethod]
        public void Either_Accepts_Functions_That_Take_Multiple_Parameters() {
            Func<int, int, int, bool> between = (a, b, c) => a < b && b < c; ;
            Func<int, int, int, bool> total20 = (a, b, c) => a + b + c == 20;
            var f = R.Either(between, total20);

            Assert.AreEqual(f(4, 5, 8), true);
            Assert.AreEqual(f(12, 2, 6), true);
            Assert.AreEqual(f(7, 5, 1), false);
        }

        [TestMethod]
        public void Either_Does_Not_Evaluate_The_Second_Expression_If_The_First_One_Is_True() {
            Func<bool> T = () => true;
            var effect = "not evaluated";
            Action Z = () => effect = "Z got evaluated";

            R.Either(T, Z)();
            Assert.AreEqual(effect, "not evaluated");
        }

        [TestMethod]
        public void Either_Is_Curried() {
            var evenOr = R.Either(even);

            Assert.IsInstanceOfType(evenOr(gt10), typeof(DynamicDelegate));
            Assert.AreEqual(evenOr(gt10)(11), true);
            Assert.AreEqual(evenOr(gt10)(9), false);
        }
    }
}
