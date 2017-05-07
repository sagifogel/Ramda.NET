using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Lte
    {
        [TestMethod]
        public void Lt_Reports_Whether_One_Item_Is_Less_Than_Another() {
            Assert.IsTrue(R.Lte(3, 5));
            Assert.IsFalse(R.Lte(6, 4));
            Assert.IsTrue(R.Lte(7.0, 7.0));
            Assert.IsTrue(R.Lte("abc", "xyz"));
            Assert.IsFalse(R.Lte("abcd", "abc"));
        }

        [TestMethod]
        public void Lte_Is_Curried() {
            var gte20 = R.Lte(20);

            Assert.IsFalse(gte20(10));
            Assert.IsTrue(gte20(20));
            Assert.IsTrue(gte20(25));
        }

        [TestMethod]
        [Description("Lt_Behaves_Right_Curried_When_Passed_\"R.__\"_For_Its_First_Argument")]
        public void Lt_Behaves_Right_Curried_When_Passed_R_Placeholder_For_Its_First_Argument() {
            var upTo20 = R.Lte(R.__, 20);

            Assert.IsTrue(upTo20(10));
            Assert.IsTrue(upTo20(20));
            Assert.IsFalse(upTo20(25));
        }
    }
}
