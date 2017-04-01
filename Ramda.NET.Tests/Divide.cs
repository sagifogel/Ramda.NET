using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Divide
    {
        [TestMethod]
        public void Divide_Divides_Two_Numbers() {
            Assert.AreEqual(R.Divide(28, 7), 4);
        }

        [TestMethod]
        public void Divide_Is_Curried() {
            var into28 = R.Divide(28);

            Assert.AreEqual(into28(7), 4);
        }

        [TestMethod]
        [Description("Divide_Behaves_Right_Curried_When_Passed_\"R.__\"_For_Its_First_Argument")]
        public void Divide_Behaves_Right_Curried_When_Passed_Placeholder_For_Its_First_Argument() {
            var half = R.Divide(R.__, 2);

            Assert.AreEqual(half(40), 20);
        }
    }
}
