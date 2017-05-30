using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class PropSatisfies
    {
        private readonly Func<int, bool> isPositive = n => n > 0;

        [TestMethod]
        public void PropSatisfies_Returns_True_If_The_Specified_Object_Property_Satisfies_The_Given_Predicate() {
            Assert.IsTrue(R.PropSatisfies(isPositive, "X", new { X = 1, Y = 0 }));
        }

        [TestMethod]
        public void PropSatisfies_Returns_False_Otherwise() {
            Assert.IsFalse(R.PropSatisfies(isPositive, "Y", new { X = 1, Y = 0 }));
        }
    }
}
