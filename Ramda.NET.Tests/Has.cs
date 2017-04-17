using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Has
    {
        private readonly object anon = new { Age = 99 };
        private readonly object fred = new { Name = "Fred", Age = 23 };

        [TestMethod]
        public void Has_Returns_True_If_The_Specified_Property_Is_Present() {
            Assert.IsTrue(R.Has("Name", fred));
        }

        [TestMethod]
        public void Has_Returns_False_If_The_Specified_Property_Is_Absent() {
            Assert.IsFalse(R.Has("Name", anon));
        }

        [TestMethod]
        [Description("Has_Is_Curried,_Op-Style")]
        public void Has_Is_Curried_Op_Style() {
            var point = new { X = 0, Y = 0 };
            var hasName = R.Has("Name");
            var pointHas = R.Has(R.__, point);

            Assert.IsTrue(hasName(fred));
            Assert.IsFalse(hasName(anon));
            Assert.IsTrue(pointHas("X"));
            Assert.IsTrue(pointHas("Y"));
            Assert.IsFalse(pointHas("Z"));
        }
    }
}
