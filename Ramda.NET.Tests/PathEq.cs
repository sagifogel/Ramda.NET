using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class PathEq
    {
        private dynamic obj = new { A = 1, B = new[] { new { BA = 2 }, new { BA = 3 } } };

        [TestMethod]
        public void PathEq_Returns_True_If_The_Path_Matches_The_Value() {
            Assert.IsTrue(R.PathEq(new[] { "A" }, 1, obj));
            Assert.IsTrue(R.PathEq(new object[] { "B", 1, "BA" }, 3, obj));
        }

        [TestMethod]
        public void PathEq_Returns_False_For_Non_Matches() {
            Assert.IsFalse(R.PathEq(new[] { "A" }, "1", obj));
            Assert.IsFalse(R.PathEq(new object[] { "B", 0, "BA" }, 3, obj));
        }

        [TestMethod]
        public void PathEq_Returns_False_For_Non_Existing_Values() {
            Assert.IsFalse(R.PathEq(new[] { "C" }, "Foo", obj));
            Assert.IsFalse(R.PathEq(new object[] { "C", "D" }, "Foo", obj));
        }

        [TestMethod]
        public void PathEq_Accepts_Empty_Path() {
            Assert.IsFalse(R.PathEq(new object[0], 42, new { A = 1, B = 2 }));
            Assert.IsTrue(R.PathEq(new object[0], obj, obj));
        }

        [TestMethod]
        [Description("PathEq_Has_R.Equals_Semantics")]
        public void PathEq_Has_R_Equals_Semantics() {
            Assert.IsTrue(R.PathEq(new[] { "Value" }, R.@null, new { Value = R.@null }));
            Assert.IsTrue(R.PathEq(new[] { "Value" }, new Just(new[] { 42 }), new { Value = new Just(new[] { 42 }) }));
        }
    }
}
