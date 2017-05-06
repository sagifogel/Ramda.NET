using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Lift
    {

        private static readonly dynamic Add3 = R.Curry(new Func<int, int, int, int>((a, b, c) => a + b + c));
        private static readonly dynamic Add4 = R.Curry(new Func<int, int, int, int, int>((a, b, c, d) => a + b + c + d));
        private static readonly dynamic Add5 = R.Curry(new Func<int, int, int, int, int, int>((a, b, c, d, e) => a + b + c + d + e));
        private static readonly dynamic madd3 = R.Lift(Add3);
        private static readonly dynamic madd4 = R.Lift(Add4);
        private static readonly dynamic madd5 = R.Lift(Add5);

        [TestMethod]
        public void Lift_Returns_A_Function_If_Called_With_Just_A_Function() {
            Assert.IsInstanceOfType(R.Lift(R.Add(R.__)), typeof(DynamicDelegate));
        }

        [TestMethod]
        [Description("Lift_Produces_A_Cross-Product_Of_Array_Values")]
        public void Lift_Produces_A_Cross_Product_Of_Array_Values() {
            CollectionAssert.AreEqual(madd3(new[] { 1, 2, 3 }, new[] { 1, 2 }, new[] { 1, 2, 3 }), new[] { 3, 4, 5, 4, 5, 6, 4, 5, 6, 5, 6, 7, 5, 6, 7, 6, 7, 8 });
            CollectionAssert.AreEqual(madd3(new[] { 1 }, new[] { 2 }, new[] { 3 }), new[] { 6 });
            CollectionAssert.AreEqual(madd3(new[] { 1, 2 }, new[] { 3, 4 }, new[] { 5, 6 }), new[] { 9, 10, 10, 11, 10, 11, 11, 12 });
        }

        [TestMethod]
        public void Lift_Can_Lift_Functions_Of_Any_Arity() {
            CollectionAssert.AreEqual(madd3(new[] { 1, 10 }, new[] { 2 }, new[] { 3 }), new[] { 6, 15 });
            CollectionAssert.AreEqual(madd4(new[] { 1, 10 }, new[] { 2 }, new[] { 3 }, new[] { 40 }), new[] { 46, 55 });
            CollectionAssert.AreEqual(madd5(new[] { 1, 10 }, new[] { 2 }, new[] { 3 }, new[] { 40 }, new[] { 500, 1000 }), new[] { 546, 1046, 555, 1055 });
        }

        [TestMethod]
        [Description("Lift_Works_With_Other_Functors_Such_As_\"Maybe\"")]
        public void Lift_Works_With_Other_Functors_Such_As_Maybe() {
            var addM = R.Lift(R.Add(R.__));

            Assert.AreEqual(addM(_Maybe.Of(3), _Maybe.Of(5)), _Maybe.Of(8));
        }
    }
}
