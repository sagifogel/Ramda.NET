using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class AllPass
    {
        private Func<int, bool> gt5 = n => n > 5;
        private Func<int, bool> lt20 = n => n < 20;
        private Func<int, bool> odd = n => n % 2 != 0;
        private Func<int, int, int, int, bool> plusEq = (w, x, y, z) => w + x == y + z;

        [TestMethod]
        public void AllPass_Reports_Whether_All_Predicates_Are_Satisfied_By_A_Given_Value() {
            var ok = R.AllPass(new Delegate[] { odd, lt20, gt5 });

            Assert.AreEqual(ok(7), true);
            Assert.AreEqual(ok(9), true);
            Assert.AreEqual(ok(10), false);
            Assert.AreEqual(ok(3), false);
            Assert.AreEqual(ok(21), false);
        }

        [TestMethod]
        public void AllPass_Returns_True_On_Empty_Predicate_List() {
            Assert.AreEqual((bool)R.AllPass(new object[0])(3), true);
        }

        [TestMethod]
        public void AllPass_Returns_A_Curried_Function_Whose_Arity_Matches_That_Of_The_Highest() {
            Assert.AreEqual((int)R.AllPass(new Delegate[] { odd, gt5, plusEq }).Length, 4);
            Assert.AreEqual((bool)R.AllPass(new Delegate[] { odd, gt5, plusEq })(9, 9, 9, 9), true);
            //Assert.AreEqual((bool)R.AllPass(new Delegate[] { odd, gt5, plusEq })(9)(9)(9)(9), true);
        }
    }
}
