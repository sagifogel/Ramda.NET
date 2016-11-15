using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class AnyPass : AbstractAnyOrAllPass
    {
        [TestMethod]
        public void AnyPass_Reports_Whether_All_Predicates_Are_Satisfied_By_A_Given_Value() {
            var ok = R.AnyPass(new Delegate[] { odd, gt20, lt5 });

            Assert.AreEqual(ok(7), true);
            Assert.AreEqual(ok(9), true);
            Assert.AreEqual(ok(10), false);
            Assert.AreEqual(ok(18), false);
            Assert.AreEqual(ok(3), true);
            Assert.AreEqual(ok(22), true);
        }

        [TestMethod]
        public void AnyPass_Returns_False_On_Empty_Predicate_List() {
            Assert.AreEqual((bool)R.AnyPass(new object[0])(3), false);
        }

        [TestMethod]
        public void AnyPass_Returns_A_Curried_Function_Whose_Arity_Matches_That_Of_The_Highest_Arity_Predicate() {
            Assert.AreEqual((int)R.AnyPass(new Delegate[] { odd, lt5, plusEq }).Length, 4);
            Assert.AreEqual((bool)R.AnyPass(new Delegate[] { odd, lt5, plusEq })(6, 7, 8, 9), false);
            Assert.AreEqual((bool)R.AnyPass(new Delegate[] { odd, lt5, plusEq })(6)(7)(8)(9), false);
        }
    }
}
