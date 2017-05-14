using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Modulo
    {
        [TestMethod]
        public void Modulo_Divides_The_First_Param_By_The_Second_And_Returns_The_Remainder() {
            Assert.AreEqual(R.Modulo(100, 2), 0);
            Assert.AreEqual(R.Modulo(100, 3), 1);
            Assert.AreEqual(R.Modulo(100, 17), 15);
        }

        [TestMethod]
        public void Modulo_Is_Curried() {
            var hundredMod = R.Modulo(100);

            Assert.IsInstanceOfType(hundredMod, typeof(DynamicDelegate));
            Assert.AreEqual(hundredMod(2), 0);
            Assert.AreEqual(hundredMod(3), 1);
            Assert.AreEqual(hundredMod(17), 15);
        }

        [TestMethod]
        [Description("Modulo_Behaves_Right_Curried_When_Passed_\"R.__\"_For_Its_First_Argument")]
        public void Modulo_Behaves_Right_Curried_When_Passed_Placeholder_For_Its_First_Argument() {
            var isOdd = R.Modulo(R.__, 2);

            Assert.IsInstanceOfType(isOdd, typeof(DynamicDelegate));
            Assert.AreEqual(isOdd(3), 1);
            Assert.AreEqual(isOdd(198), 0);
        }

        [TestMethod]
        [Description("Modulo_Preserves_Javascript-Style_Modulo_Evaluation_For_Negative_Numbers")]
        public void Modulo_Preserves_Javascript_Style_Modulo_Evaluation_For_Negative_Numbers() {
            Assert.AreEqual(R.Modulo(-5, 4), -1);
        }
    }
}
