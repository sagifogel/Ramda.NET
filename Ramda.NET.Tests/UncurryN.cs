using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class UncurryN
    {
        delegate int Params(params int?[] arguments);
        Func<int, Func<int, int>> a2 = a => b => a + b;
        Func<int, Func<int, Func<int, int>>> a3 = a => b =>  c => a + b + c;
        Func<int, Func<int, Params>> a3b = a => b => new Params((int?[] arguments) => {
            var c = (int)arguments[0];

            return a + b + c + (arguments.Length > 1 ? arguments[1] ?? 0 : 0) + (arguments.Length > 2 ? arguments[2] ?? 0 : 0);
        });

        Func<int, Func<int, Func<int, Func<int, int>>>> a4 = a => b => c => d => a + b + c + d;

        [TestMethod]
        public void UncurryN_Accepts_An_Arity() {
            var uncurried = R.UncurryN(3, a3);

            Assert.AreEqual(uncurried(1, 2, 3), 6);
        }

        [TestMethod]
        public void UncurryN_Returns_A_Function_Of_The_Specified_Arity() {
            Assert.AreEqual(R.UncurryN(2, a2).Length, 2);
            Assert.AreEqual(R.UncurryN(3, a3).Length, 3);
            Assert.AreEqual(R.UncurryN(4, a4).Length, 4);
        }

        [TestMethod]
        public void UncurryN_Forwards_Extra_Arguments() {
            var g = R.UncurryN(3, a3b);

            Assert.AreEqual(g(1, 2, 3), 6);
            Assert.AreEqual(g(1, 2, 3, 4), 10);
            Assert.AreEqual(g(1, 2, 3, 4, 5), 15);
        }

        [TestMethod]
        public void UncurryN_Works_With_Ordinary_Uncurried_Functions() {
            Assert.AreEqual(R.UncurryN(2, new Func<int, int, int>((a, b) => a + b))(10, 20), 30);
            Assert.AreEqual(R.UncurryN(3, new Func<int, int, int, int>((a, b, c) => a + b + c))(10, 20, 30), 60);
        }

        [TestMethod]
        [Description("UncurryN_Works_With_Ramda-Curried_Functions")]
        public void UncurryN_Works_With_Ramda_Curried_Functions() {
            Assert.AreEqual(R.UncurryN(2, R.Add(R.__))(10, 20), 30);
        }

        [TestMethod]
        [Description("UncurryN_Returns_A_Function_That_Supports_R.___Placeholder")]
        public void UncurryN_Returns_A_Function_That_Supports_R_Placeholder() {
            var g = R.UncurryN(3, a3);
            var _ = R.__;

            Assert.AreEqual(g(1)(2)(3), 6);
            Assert.AreEqual(g(1)(2, 3), 6);
            Assert.AreEqual(g(1, 2)(3), 6);
            Assert.AreEqual(g(1, 2, 3), 6);
            Assert.AreEqual(g(_, 2, 3)(1), 6);
            Assert.AreEqual(g(1, _, 3)(2), 6);
            Assert.AreEqual(g(1, 2, _)(3), 6);
            Assert.AreEqual(g(1, _, _)(2)(3), 6);
            Assert.AreEqual(g(_, 2, _)(1)(3), 6);
            Assert.AreEqual(g(_, _, 3)(1)(2), 6);
            Assert.AreEqual(g(1, _, _)(2, 3), 6);
            Assert.AreEqual(g(_, 2, _)(1, 3), 6);
            Assert.AreEqual(g(_, _, 3)(1, 2), 6);
            Assert.AreEqual(g(1, _, _)(_, 3)(2), 6);
            Assert.AreEqual(g(_, 2, _)(_, 3)(1), 6);
            Assert.AreEqual(g(_, _, 3)(_, 2)(1), 6);
            Assert.AreEqual(g(_, _, _)(_, _)(_)(1, 2, 3), 6);
            Assert.AreEqual(g(_, _, _)(1, _, _)(_, _)(2, _)(_)(3), 6);
        }
    }
}
