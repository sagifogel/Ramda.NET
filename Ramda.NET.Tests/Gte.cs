using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Gte
    {
        [TestMethod]
        public void Gte_Reports_Whether_One_Item_Is_Less_Than_Another() {
            Assert.IsFalse(R.Gte(3, 5));
            Assert.IsTrue(R.Gte(6, 4));
            Assert.IsTrue(R.Gte(7.0, 7.0));
            Assert.IsFalse(R.Gte("abc", "xyz"));
            Assert.IsTrue(R.Gte("abcd", "abc"));
        }

        [TestMethod]
        public void Gte_Is_Curried() {
            var lte20 = R.Gte(20);

            Assert.IsTrue(lte20(10));
            Assert.IsTrue(lte20(20));
            Assert.IsFalse(lte20(25));
        }

        [TestMethod]
        [Description("Gt_Behaves_Right_Curried_When_Passed_\"R.__\"_For_Its_First_Argument")]
        public void Gte_Behaves_Right_Curried_When_Passed_R_PlaceHolder_For_Its_First_Argument() {
            var gte20 = R.Gte(R.__, 20);

            Assert.IsFalse(gte20(10));
            Assert.IsTrue(gte20(20));
            Assert.IsTrue(gte20(25));
        }
    }
}
