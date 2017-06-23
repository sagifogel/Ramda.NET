using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Until
    {
        [TestMethod]
        public void Until_Applies_Fn_Until_Pred_Is_Satisfied() {
            Assert.AreEqual(R.Until(R.Gt(R.__, 100), R.Multiply(2), 1), 128);
        }

        [TestMethod]
        public void Until_Works_With_Lists() {
            CollectionAssert.AreEqual(R.Until(R.None(R.IsArrayLike(R.__)), R.Unnest(R.__))(new object[] { 1, new[] { 2 }, new object[] { new[] { 3 } } }), new[] { 1, 2, 3 });
        }

        [TestMethod]
        public void Until_Ignores_Fn_If_Predicate_Is_Always_True() {
            Assert.IsFalse(R.Until(R.T, R.T, false));
        }
    }
}
