using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class MathMod
    {
        [TestMethod]
        public void MathMod_Computes_The_True_Modulo_Function() {
            Assert.AreEqual(R.MathMod(-17, 5), 3);
        }

        [TestMethod]
        public void MathMod_Is_Curried() {
            var f = R.MathMod(29);

            Assert.AreEqual(f(6), 5);
        }

        [TestMethod]
        [Description("MathMod_Behaves_Right_Curried_When_Passed_\"R.__\"_For_Its_First_Argument")]
        public void MathMod_Behaves_Right_Curried_When_Passed_R_Placeholder_For_Its_First_Argument() {
            var mod5 = R.Modulo(R.__, 5);

            Assert.AreEqual(mod5(12), 2);
            Assert.AreEqual(mod5(8), 3);
        }
    }
}
