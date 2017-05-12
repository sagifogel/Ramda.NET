using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Memoize
    {
        private int optionalCount = 0;

        public object Arguments(params object[] arguments) {
            return arguments[0];
        }

        public string OptionalArguments(params object[] arguments) {
            var length = arguments.Length;
            var a = length > 0 ? arguments[0] : null;
            var b = length > 1 ? arguments[1] : null;

            optionalCount += 1;

            switch (length) {
                case 0:
                    a = "foo";
                    b = "bar";
                    break;
                case 1:
                    b = "bar";
                    break;
            }

            return a.ToString() + b.ToString();
        }

        [TestMethod]
        public void Memoize_Calculates_The_Value_For_A_Given_Input_Only_Once() {
            var ctr = 0;
            dynamic fib = null;

            fib = R.Memoize(new Func<int, int>(n => {
                ctr += 1;

                return n < 2 ? n : fib(n - 2) + fib(n - 1);
            }));

            Assert.AreEqual(fib(10), 55);
            Assert.AreEqual(ctr, 11);
        }

        [TestMethod]
        public void Memoize_Handles_Multiple_Parameters() {
            var f = R.Memoize(new Func<string, string, string, string>((a, b, c) => $"{a}, {b}{c}"));

            Assert.AreEqual(f("Hello", "World", "!"), "Hello, World!");
            Assert.AreEqual(f("Goodbye", "Cruel World", "!!!"), "Goodbye, Cruel World!!!");
            Assert.AreEqual(f("Hello", "how are you", "?"), "Hello, how are you?");
            Assert.AreEqual(f("Hello", "World", "!"), "Hello, World!");
        }

        [TestMethod]
        public void Memoize_Does_Not_Rely_On_Reported_Arity() {
            var identity = R.Memoize(new Func<object[], object>(Arguments));

            Assert.AreEqual(identity("x"), "x");
            Assert.AreEqual(identity("y"), "y");
        }

        [TestMethod]
        [Description("Memoize_Memoizes_\"false\"_Return_Values")]
        public void Memoize_Memoizes_False_Return_Values() {
            var count = 0;
            var inc = R.Memoize(new Func<int, int>(n => {
                count += 1;
                return n + 1;
            }));

            Assert.AreEqual(inc(-1), 0);
            Assert.AreEqual(inc(-1), 0);
            Assert.AreEqual(inc(-1), 0);
            Assert.AreEqual(count, 1);
        }

        [TestMethod]
        public void Memoize_Can_Be_Applied_To_Nullary_Function() {
            var count = 0;
            var f = R.Memoize(new Func<int, int>(n => {
                count += 1;
                return 42;
            }));

            Assert.AreEqual(f(), 42);
            Assert.AreEqual(f(), 42);
            Assert.AreEqual(f(), 42);
            Assert.AreEqual(count, 1);
        }

        [TestMethod]
        public void Memoize_Can_Be_Applied_To_Function_With_Optional_Arguments() {
            var f = R.Memoize(new Func<object[], string>(OptionalArguments));

            Assert.AreEqual(f(), "foobar");
            Assert.AreEqual(f(), "foobar");
            Assert.AreEqual(f(), "foobar");
            Assert.AreEqual(optionalCount, 1);
        }

        [TestMethod]
        public void Memoize_Differentiates_Values_With_Same_String_Representation() {
            var f = R.Memoize(R.ToString(R.__));

            Assert.AreEqual(f(42), "42");
            Assert.AreEqual(f("42"), "\"42\"");
            Assert.AreEqual(f(new[] { new[] { 42 } }), "[[42]]");
            Assert.AreEqual(f(new[] { new[] { "42" } }), "[[\"42\"]]");
        }

        [TestMethod]
        public void Memoize_Respects_Object_Equivalence() {
            var count = 0;
            var f = R.Memoize(new Func<object, string>(x => {
                count += 1;
                return R.ToString(x);
            }));

            Assert.AreEqual(f(new { X = 1, Y = 2 }), "{\"X\": 1, \"Y\": 2}");
            Assert.AreEqual(f(new { X = 1, Y = 2 }), "{\"X\": 1, \"Y\": 2}");
            Assert.AreEqual(f(new { Y = 2, X = 1 }), "{\"X\": 1, \"Y\": 2}");
            Assert.AreEqual(count, 1);
        }

        [TestMethod]
        public void Memoize_Retains_Arity() {
            var f = R.Memoize(new Func<int, int, int>((int a, int b) => a + b));

            Assert.AreEqual(f.Length, 2);
        }
    }
}
