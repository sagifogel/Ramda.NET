using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Unapply
    {
        private int ArgumentsLength(object[] arr) => arr.Length;
        private string ArgumentsToString(double[] arr) => $"[{string.Join(",", arr)}]";

        [TestMethod]
        public void Unapply_Forwards_Arguments_To_Decorated_Function_As_An_Array() {
            var fn = R.Unapply(new Func<double[], string>(ArgumentsToString));

            Assert.AreEqual(fn(), "[]");
            Assert.AreEqual(fn(2), "[2]");
            Assert.AreEqual(fn(2, 4), "[2,4]");
            Assert.AreEqual(fn(2, 4, 6), "[2,4,6]");
        }

        [TestMethod]
        public void Unapply_Returns_A_Function_With_Length_0() {
            var fn = R.Unapply(R.Identity(R.__));

            Assert.AreEqual(fn.Length, 0);
        }

        [TestMethod]
        [Description("Unapply_Is_The_Inverse_Of_R.Apply")]
        public void Unapply_Is_The_Inverse_Of_R_Apply() {
            dynamic g;
            double a, b, c, d, e, n;
            Func<double> rand = () => {
                return Math.Floor(200d * new Random().Next()) - 100;
            };

            g = R.Unapply(R.Apply(new Func<double[], double>(ds => ds.Max())));
            n = 1;

            while (n <= 100) {
                a = rand(); b = rand(); c = rand(); d = rand(); e = rand();
                Assert.AreEqual(Enumerable.Max(new[] { a, b, c, d, e }), g(a, b, c, d, e));
                n += 1;
            }

            g = R.Apply(R.Unapply(new Func<double[], string>(ArgumentsToString)));
            n = 1;

            while (n <= 100) {
                a = rand(); b = rand(); c = rand(); d = rand(); e = rand();
                Assert.AreEqual(ArgumentsToString(new [] { a, b, c, d, e }), g(new[] { a, b, c, d, e }));
                n += 1;
            }
        }
    }
}
