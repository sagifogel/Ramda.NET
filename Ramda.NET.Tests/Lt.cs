using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Lt
    {
        [TestMethod]
        public void Lt_Reports_Whether_One_Item_Is_Less_Than_Another() {
            Assert.IsTrue(R.Lt(3, 5));
            Assert.IsFalse(R.Lt(6, 4));
            Assert.IsFalse(R.Lt(7.0, 7.0));
            Assert.IsTrue(R.Lt("abc", "xyz"));
            Assert.IsFalse(R.Lt("abcd", "abc"));
        }

        [TestMethod]
        public void Lt_Is_Curried() {
            var gt5 = R.Lt(5);

            Assert.IsTrue(gt5(10));
            Assert.IsFalse(gt5(5));
            Assert.IsFalse(gt5(3));
        }

        [TestMethod]
        [Description("Lt_Behaves_Right_Curried_When_Passed_\"R.__\"_For_Its_First_Argument")]
        public void Lt_Behaves_Right_Curried_When_Passed_R_Placeholder_For_Its_First_Argument() {
            var lt5 = R.Lt(R.__, 5);

            Assert.IsFalse(lt5(10));
            Assert.IsFalse(lt5(5));
            Assert.IsTrue(lt5(3));
        }
    }
}
