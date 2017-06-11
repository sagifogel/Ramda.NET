using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Ramda.NET.R;
using System.Dynamic;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Transduce
    {
        private readonly Func<int, int, int> mult = (a, b) => a * b;
        private readonly Func<int, bool> isOdd = b => b % 2 == 1;

        class Reducible : IReducible
        {
            public int[] X { get; set; }
            public object Reduce(Func<object, object, object> step, object acc) => "override";
        }

        private readonly ITransformer addxf = new Transformer(new {
            Init = new Func<int>(() => 0),
            Result = new Func<object, object>(x => x),
            Step = new Func<dynamic, dynamic, dynamic>((acc, x) => acc + x)
        });

        private readonly ITransformer listxf = new Transformer(new {
            Init = new Func<object>(() => new int[0]),
            Result = new Func<object, object>(x => x),
            Step = new Func<object, object, object>((acc, x) => ((int[])acc).Concat<int>(new[] { (int)x }).ToArray())
        });

        private readonly ITransformer multxf = new Transformer(new {
            Init = new Func<object>(() => 1),
            Result = new Func<object, object>(x => x),
            Step = new Func<object, object, object>((acc, x) => (int)acc * (int)x)
        });

        private readonly Func<dynamic, Func<ITransformer, ITransformer>> toxf = fn => {
            return xf => {
                return new Transformer(new {
                    f = fn,
                    Step = new Func<object, object, object>(xf.Step),
                    Result = new Func<object, object>(xf.Result),
                    xf = xf
                });
            };
        };

        public class Transformer : ITransformer
        {
            private readonly dynamic xf;

            public Transformer(dynamic xf) {
                this.xf = xf;
            }

            public object Init() {
                return xf.Init();
            }

            public object Result(object result) {
                return xf.Result(result);
            }

            public object Step(object result, object input) {
                return xf.Step(result, input);
            }
        }

        [TestMethod]
        public void Transduce_Transduces_Into_Arrays() {
            CollectionAssert.AreEqual(R.Transduce(R.Map(R.Add(1)), R.Flip(R.Append(R.__)), new int[0], new[] { 1, 2, 3, 4 }), new[] { 2, 3, 4, 5 });
            CollectionAssert.AreEqual(R.Transduce(R.Filter(isOdd), R.Flip(R.Append(R.__)), new int[0], new[] { 1, 2, 3, 4 }), new[] { 1, 3 });
            CollectionAssert.AreEqual(R.Transduce(R.Compose(R.Map(R.Add(1)), R.Take(2)), R.Flip(R.Append(R.__)), new int[0], new[] { 1, 2, 3, 4 }), new[] { 2, 3 });
        }

        [TestMethod]
        public void Transduce_Transduces_Into_Strings() {
            Func<dynamic, dynamic, dynamic> add = (x, y) => x + y;

            Assert.AreEqual(R.Transduce(R.Map(R.Inc(R.__)), add, string.Empty, new[] { 1, 2, 3, 4 }), "2345");
            Assert.AreEqual(R.Transduce(R.Filter(isOdd), add, string.Empty, new[] { 1, 2, 3, 4 }), "13");
            Assert.AreEqual(R.Transduce(R.Compose(R.Map(R.Add(1)), R.Take(2)), add, string.Empty, new[] { 1, 2, 3, 4 }), "23");
        }

        [TestMethod]
        public void Transduce_Transduces_Into_Objects() {
            Func<dynamic, dynamic, dynamic> add = (x, y) => x + y;

            DynamicAssert.AreEqual(R.Transduce(R.Map(R.Identity(R.__)), R.Merge(R.__), new ExpandoObject(), new object[] { new { A = 1 }, new { B = 2, C = 3 } }), new { A = 1, B = 2, C = 3 });
        }

        [TestMethod]
        public void Transduce_Folds_Transformer_Objects_Over_A_Collection_With_The_Supplied_Accumulator() {
            Assert.AreEqual(R.Transduce(toxf(R.Add(R.__)), addxf, 0, new[] { 1, 2, 3, 4 }), 10);
            Assert.AreEqual(R.Transduce(toxf(mult), multxf, 1, new[] { 1, 2, 3, 4 }), 24);
            CollectionAssert.AreEqual(R.Transduce(toxf(R.Concat(R.__)), listxf, new[] { 0 }, new[] { 1, 2, 3, 4 }), new[] { 0, 1, 2, 3, 4 });
            Assert.AreEqual(R.Transduce(toxf(R.Add(R.__)), R.Add(R.__), 0, new[] { 1, 2, 3, 4 }), 10);
            Assert.AreEqual(R.Transduce(toxf(mult), mult, 1, new[] { 1, 2, 3, 4 }), 24);
        }

        [TestMethod]
        [Description("Transduce_Dispatches_To_Objects_That_Implement_\"Reduce\"")]
        public void Transduce_Dispatches_To_Objects_That_Implement_Reduce() {
            var obj = new Reducible { X = new[] { 1, 2, 3 } };

            Assert.AreEqual(R.Transduce(R.Map(R.Add(1)), R.Add(R.__), 0, obj), "override");
            Assert.AreEqual(R.Transduce(R.Map(R.Add(1)), R.Add(R.__), 10, obj), "override");
        }

        [TestMethod]
        public void Transduce_Returns_The_Accumulator_For_An_Empty_Collection() {
            Assert.AreEqual(R.Transduce(toxf(R.Add(R.__)), addxf, 0, new int[0]), 0);
            Assert.AreEqual(R.Transduce(toxf(mult), multxf, 1, new int[0]), 1);
            CollectionAssert.AreEqual(R.Transduce(toxf(R.Concat(R.__)), listxf, new int[0], new int[0]), new int[0]);
        }

        [TestMethod]
        public void Transduce_Is_Curried() {
            var addOrCat1 = R.Transduce(toxf(R.Add(R.__)));
            var addOrCat2 = addOrCat1(addxf);
            var sum = addOrCat2(0);
            var cat = addOrCat2(string.Empty);

            Assert.AreEqual(sum(new[] { 1, 2, 3, 4 }), 10);
            Assert.AreEqual(cat(new[] { "1", "2", "3", "4" }), "1234");
        }

        [TestMethod]
        public void Transduce_Correctly_Reports_The_Arity_Of_Curried_Versions() {
            var sum = R.Transduce(toxf(R.Add(R.__)), addxf, 0);

            Assert.AreEqual(sum.Length, 1);
        }
    }
}
