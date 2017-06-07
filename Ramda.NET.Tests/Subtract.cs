using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Subtract
    {
        [TestMethod]
        public void Subtract_Subtracts_Two_Numbers() {
            Assert.AreEqual(R.Subtract(22, 7), 15);
        }

        [TestMethod]
        public void Subtract_Coerces_Its_Arguments_To_Numbers() {
            Assert.AreEqual(R.Subtract("1", "2"), -1);
            Assert.AreEqual(R.Subtract(1, "2"), -1);
            Assert.AreEqual(R.Subtract(true, false), 1);
            Assert.AreEqual(R.Subtract(R.@null, R.@null), 0);
        }

        [TestMethod]
        public void Subtract_Is_Curried() {
            var ninesCompl = R.Subtract(9);

            Assert.AreEqual(ninesCompl(6), 3);
        }

        [TestMethod]
        [Description("Subtract_Behaves_Right_Curried_When_Passed_\"R.__\"_For_Its_First_Argument")]
        public void Subtract_Behaves_Right_Curried_When_Passed_R__Placeholder_For_Its_First_Argument() {
            var minus5 = R.Subtract(R.__, 5);

            Assert.AreEqual(minus5(17), 12);
        }
    }
}
