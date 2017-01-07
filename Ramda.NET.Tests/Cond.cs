using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Cond
    {
        [TestMethod]
        public void Cond_Returns_A_Function() {
            Assert.IsInstanceOfType(R.Cond(new object[0]), typeof(DynamicDelegate));
        }

        [TestMethod]
        public void Cond_Returns_A_Conditional_Function() {
            var fn = R.Cond(new[] {
                new [] { R.Equals(0), R.Always("water freezes at 0°C") },
                new [] { R.Equals(100), R.Always("water boils at 100°C") },
                new [] { R.T, new Func<int, string>(temp => $"nothing special happens at {temp}°C") }
            });

            Assert.AreEqual(fn(0), "water freezes at 0°C");
            Assert.AreEqual(fn(50), "nothing special happens at 50°C");
            Assert.AreEqual(fn(100), "water boils at 100°C");
        }

        [TestMethod]
        public void Cond_Returns_A_Function_Which_Returns_Null_If_None_Of_The_Predicates_Matches() {
            var fn = R.Cond(new[] {
                new [] { R.Equals("foo"), R.Always(1) },
                new [] { R.Equals("bar"), R.Always(2) }
            });

            Assert.IsNull(fn("quux"));
        }

        [TestMethod]
        public void Cond_Predicates_Are_Tested_In_Order() {
            var fn = R.Cond(new[] {
                new [] { R.T, R.Always("foo") },
                new [] { R.T, R.Always("bar") },
                new [] { R.T, R.Always("baz") }
            });

            Assert.AreEqual(fn(), "foo");
        }

        [TestMethod]
        public void Cond_Forwards_All_Arguments_To_Predicates_And_To_Transformers() {
            var fn = R.Cond(new object[] {
                new object[] { Currying.Delegate((object[] arguments) => (int)arguments[1] == 42), Currying.Delegate((object[] arguments) => arguments.Length) }
            });

            Assert.AreEqual(fn(new[] { 21, 42, 84 }), 3);
        }

        [TestMethod]
        public void Cond_Retains_Highest_Predicate_Arity() {
            var fn = R.Cond(new[] {
                new [] { R.NAry(2, R.T), R.T },
                new [] { R.NAry(3, R.T), R.T },
                new [] { R.NAry(1, R.T), R.T }
            });

            Assert.AreEqual(fn.Length, 3);
        }
    }
}
