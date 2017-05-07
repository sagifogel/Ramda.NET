using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class LiftN
    {
        private static readonly dynamic AddN = new Func<object[], object>(arguments => {
            return R.Reduce(new Func<int, int, int>((a, b) => a + b), 0, arguments);
        });

        private static readonly dynamic Add3 = R.Curry(new Func<int, int, int, int>((a, b, c) => a + b + c));
        private static readonly dynamic AddN3 = R.LiftN(3, AddN);
        private static readonly dynamic AddN4 = R.LiftN(4, AddN);
        private static readonly dynamic AddN5 = R.LiftN(5, AddN);

        [TestMethod]
        public void LiftN_Returns_A_Function() {
            Assert.IsInstanceOfType(R.LiftN(3, Add3), typeof(DynamicDelegate));
        }

        [TestMethod]
        public void LiftN_Limits_A_Variadic_Function_To_The_Specified_Arity() {
            CollectionAssert.AreEqual(AddN3(new[] { 1, 10 }, new[] { 2 }, new[] { 3 }), new[] { 6, 15 });
        }

        [TestMethod]
        public void Lift_Can_Lift_Functions_Of_Any_Arity() {
            CollectionAssert.AreEqual(AddN3(new[] { 1, 10 }, new[] { 2 }, new[] { 3 }), new[] { 6, 15 });
            CollectionAssert.AreEqual(AddN4(new[] { 1, 10 }, new[] { 2 }, new[] { 3 }, new[] { 40 }), new[] { 46, 55 });
            CollectionAssert.AreEqual(AddN5(new[] { 1, 10 }, new[] { 2 }, new[] { 3 }, new[] { 40 }, new[] { 500, 1000 }), new[] { 546, 1046, 555, 1055 });
        }

        [TestMethod]
        public void LiftN_Is_Curried() {
            var f4 = R.LiftN(4);

            Assert.IsInstanceOfType(f4, typeof(DynamicDelegate));
            CollectionAssert.AreEqual(f4(AddN)(new[] { 1 }, new[] { 2 }, new[] { 3 }, new[] { 4, 5 }), new[] { 10, 11 });
        }

        [TestMethod]
        [Description("LiftN_Works_With_Other_Functors_Such_As_\"Maybe\"")]
        public void LiftN_Works_With_Other_Functors_Such_As_Maybe() {
            var addM = R.LiftN(2, R.Add(R.__));

            Assert.AreEqual(addM(_Maybe.Maybe(3), _Maybe.Maybe(5)), _Maybe.Maybe(8));
        }

        [TestMethod]
        [Description("LiftN_Interprets_[a]_As_A_Functor")]
        public void LiftN_Interprets_Array_As_A_Functor() {
            CollectionAssert.AreEqual(AddN3(new[] { 1, 2, 3 }, new[] { 10, 20 }, new[] { 100, 200, 300 }), new[] { 111, 211, 311, 121, 221, 321, 112, 212, 312, 122, 222, 322, 113, 213, 313, 123, 223, 323 });
            CollectionAssert.AreEqual(AddN3(new[] { 1 }, new[] { 2 }, new[] { 3 }), new[] { 6 });
            CollectionAssert.AreEqual(AddN3(new[] { 1, 2 }, new[] { 10, 20 }, new[] { 100, 200 }), new[] { 111, 211, 121, 221, 112, 212, 122, 222 });
        }

        [TestMethod]
        [Description("LiftN_Interprets_((->)_R)_As_A_Functor")]
        public void LiftN_Interprets_LiftedFunction_As_A_Functor() {
            var convergedOnInt = AddN3(R.Add(2), R.Multiply(3), R.Subtract(4));
            var convergedOnBool = R.LiftN(2, R.And(R.__))(R.Gt(R.__, 0), R.Lt(R.__, 3));

            Assert.IsInstanceOfType(convergedOnInt, typeof(DynamicDelegate));
            Assert.IsInstanceOfType(convergedOnBool, typeof(DynamicDelegate));
            Assert.AreEqual(convergedOnInt(10), (10 + 2) + (10 * 3) + (4 - 10));
            Assert.AreEqual(convergedOnBool(0), (0 > 0) && (0 < 3));
            Assert.AreEqual(convergedOnBool(1), (1 > 0) && (1 < 3));
            Assert.AreEqual(convergedOnBool(2), (2 > 0) && (2 < 3));
            Assert.AreEqual(convergedOnBool(3), (3 > 0) && (3 < 3));
        }
    }
}
