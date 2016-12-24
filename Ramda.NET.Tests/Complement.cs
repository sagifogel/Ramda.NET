using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Complement
    {
        [TestMethod]
        [Description("Complement_Creates_Boolean-returning_Function_That_Reverses_Another")]
        public void Complement_Creates_Boolean_Returning_Function_That_Reverses_Another() {
            Func<int, bool> even = x => x % 2 == 0;
            var f = R.Complement(even);

            Assert.AreEqual(f(8), false);
            Assert.AreEqual(f(13), true);
        }

        [TestMethod]
        public void Complement_Accepts_A_Function_That_Take_Multiple_Parameters() {
            Func<int, int, int, bool> between = (a, b, c) =>  a < b && b < c;
            var f = R.Complement(between);

            Assert.AreEqual(f(4, 5, 11), false);
            Assert.AreEqual(f(12, 2, 6), true);
        }
    }
}
