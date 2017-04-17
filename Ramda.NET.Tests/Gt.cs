using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Gt
    {
        [TestMethod]
        public void Gt_Reports_Whether_One_Item_Is_Less_Than_Another() {
            Assert.IsFalse(R.Gt(3, 5));
            Assert.IsTrue(R.Gt(6, 4));
            Assert.IsFalse(R.Gt(7.0, 7.0));
            Assert.IsFalse(R.Gt("abc", "xyz"));
            Assert.IsTrue(R.Gt("abcd", "abc"));
        }

        [TestMethod]
        public void Gt_Is_Curried() {
            var lt20 = R.Gt(20);

            Assert.IsTrue(lt20(10));
            Assert.IsFalse(lt20(20));
            Assert.IsFalse(lt20(25));
        }

        [TestMethod]
        [Description("Gt_Behaves_Right_Curried_When_Passed_\"R.__\"_For_Its_First_Argument")]
        public void Gt_Behaves_Right_Curried_When_Passed_R_PlaceHolder_For_Its_First_Argument() {
            var gt20 = R.Gt(R.__, 20);

            Assert.IsFalse(gt20(10));
            Assert.IsFalse(gt20(20));
            Assert.IsTrue(gt20(25));
        }
    }
}
