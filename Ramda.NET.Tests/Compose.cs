using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Reflection;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Compose
    {
        private readonly Func<bool, bool> f = s => false;
        private readonly Func<bool, bool> g = s => true;
        private readonly Func<string, bool> h = s => false;

        [TestMethod]
        public void Compose_Is_A_Variadic_Function() {
            var compose = R.Compose((Func<bool>)(() => true));

            Assert.IsInstanceOfType(compose, typeof(DynamicDelegate));
            Assert.AreEqual(compose.Length, 0);
        }

        [TestMethod]
        [Description("Compose_Performs_Right-to-left_Function_Composition")]
        public void Compose_Performs_Right_To_Left_Function_Composition() {
            var f = R.Compose(R.Map(R.__), R.Multiply(R.__), (Func<string, int>)int.Parse);

            Assert.AreEqual(f.Length, 1);
            CollectionAssert.AreEqual((ICollection)f("10")(new[] { 1, 2, 3 }), new[] { 10, 20, 30 });
            CollectionAssert.AreEqual((ICollection)f("10", 2)(new[] { 1, 2, 3 }), new[] { 2, 4, 6 });
        }


        [TestMethod]
        public void Compose_Throws_If_Given_No_Arguments() {
            try {
                var f = R.Compose();
            }
            catch (TargetInvocationException ex) {
                Assert.IsInstanceOfType(ex.InnerException, typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void Compose_Compose_Properties_Composes_Two_Functions() {
            Assert.IsTrue(R.Equals(R.Compose(f, g)(false), f(g(false))));
        }

        [TestMethod]
        public void Compose_Compose_Properties_Associative() {
            var result = f(g(h(string.Empty)));
            var res = R.All(R.Equals(result), new[] {
                  R.Compose(f, g, h)(string.Empty),
                  R.Compose(f, R.Compose(g, h))(string.Empty),
                  R.Compose(R.Compose(f, g), h)(string.Empty)
            });

            Assert.IsTrue(res);
        }
    }
}
