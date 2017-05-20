using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class PathSatisfies
    {
        private Func<dynamic, bool> isPositive = n => !n.Equals(R.@null) && n > 0;

        [TestMethod]
        public void PathSatisfies_Returns_True_If_The_Specified_Object_Path_Satisfies_The_Given_Predicate() {
            Assert.IsTrue(R.PathSatisfies(isPositive, new[] { "X", "1", "Y" }, new { X = new object[] { new { Y = -1 }, new { Y = 1 } } }));
        }

        [TestMethod]
        public void PathSatisfies_Returns_False_If_The_Specified_Path_Does_Not_Exist() {
            Assert.IsFalse(R.PathSatisfies(isPositive, new[] { "X", "Y" }, new { X = new { Z = 42 } }));
        }

        [TestMethod]
        public void PathSatisfies_Returns_False_If_The_Path_Is_Empty() {
            Assert.IsFalse(R.PathSatisfies(isPositive, new string[0], new { X = new { Z = 42 } }));
        }

        [TestMethod]
        public void PathSatisfies_Returns_False_Otherwise() {
            Assert.IsFalse(R.PathSatisfies(isPositive, new[] { "X", "Y" }, new { X = new { y = 0 } }));
        }
    }
}
