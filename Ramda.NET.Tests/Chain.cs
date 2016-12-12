using System;
using Ramda.NET;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Chain
    {
        private dynamic intoArray = R.Into(new object[0]);
        private Func<int, int[]> dec = x => new[] { x - 1 };
        private Func<int, int[]> add1 = x => new[] { x + 1 };
        private Func<int, int[]> times2 => x => new[] { x * 2 };

        public class Chainable
        {
            public int X { get; private set; }

            public Chainable(int x) {
                X = x;
            }

            public object Chain(dynamic f) {
                return f(X);
            }
        }

        [TestMethod]
        [Description("Chain_Maps_A_Function_Over_A_Nested_List_And_Returns_The_(shallow)_Flattened_Result")]
        public void Chain_Maps_A_Function_Over_A_Nested_List_And_Returns_The_Shallow_Flattened_Result() {
            CollectionAssert.AreEqual((ICollection)R.Chain(times2, new[] { 1, 2, 3, 1, 0, 10, -3, 5, 7 }), new[] { 2, 4, 6, 2, 0, 20, -6, 10, 14 });
            CollectionAssert.AreEqual((ICollection)R.Chain(times2, new[] { 1, 2, 3 }), new[] { 2, 4, 6 });
        }

        [TestMethod]
        public void Chain_Does_Not_Flatten_Recursively() {
            Func<object[], object[]> f = xs => xs.Length > 0 ? new object[] { xs[0] } : new object[0];
            Assert.IsTrue(((IEnumerable)R.Chain(f, new object[] { new[] { 1 }, new object[] { new[] { 2 }, 100 }, new object[0], new object[] { 3, new[] { 4 } } })).SequenceEqual(new object[] { 1, new[] { 2 }, 3 }));
        }

        [TestMethod]
        [Description("Chain_Maps_A_Function_(a_->_[b])_Into_A_(shallow)_Flat_Result")]
        public void Chain_Maps_A_Function__Into_A_Shallow_Flat_Result() {
            CollectionAssert.AreEqual((ICollection)intoArray(R.Chain(times2), new[] { 1, 2, 3, 4 }), new[] { 2, 4, 6, 8 });
        }

        [TestMethod]
        [Description("Chain_Interprets_((->)_R)_As_A_Monad")]
        public void Chain_Interprets_As_A_Monad() {
            Func<int, int> h = r => r * 2;
            Func<int, Func<int, int>> f = a => {
                return r => {
                    return r + a;
                };
            };

            var bound = R.Chain(h, (Delegate)f);

            Assert.AreEqual(bound(10), 10 * 2 + 10);
        }

        [TestMethod]
        [Description("Chain_Dispatches_To_Objects_That_Implement_`chain`")]
        public void Chain_Dispatches_To_Objects_That_Implement_Chain() {
            var obj = new Chainable(100);

            CollectionAssert.AreEqual((ICollection)R.Chain(add1, obj), new[] { 101 });
        }

        [TestMethod]
        public void Chain_Dispatches_To_Transformer_Objects() {
            Assert.IsInstanceOfType(R.Chain(add1, new ListXf()), typeof(ITransformer));
        }

        [TestMethod]
        public void Chain_Composes() {
            var mdouble = R.Chain(times2);
            var mdec = R.Chain(dec);

            CollectionAssert.AreEqual((ICollection)mdec(mdouble(new[] { 10, 20, 30 })), new[] { 19, 39, 59 });
        }

        [TestMethod]
        [Description("Chain_Can_Compose_Transducer-Style")]
        public void Chain_Can_Compose_Transducer_Style() {
            var mdouble = R.Chain(times2);
            var mdec = R.Chain(dec);
            var xcomp = R.Compose(mdec, mdouble);

            CollectionAssert.AreEqual((ICollection)intoArray(xcomp, new[] { 10, 20, 30 }), new[] { 18, 38, 58 });
        }

        [TestMethod]
        public void Chain_Is_Curried() {
            var flatInc = R.Chain(add1);

            CollectionAssert.AreEqual((ICollection)flatInc(new[] { 1, 2, 3, 4, 5, 6}), new[] { 2, 3, 4, 5, 6, 7 });
        }

        [TestMethod]
        public void Chain_Correctly_Reports_The_Arity_Of_Curried_Versions() {
            var inc = R.Chain(add1);

            Assert.AreEqual(inc.Length, 1);
        }
    }
}
