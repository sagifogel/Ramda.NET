using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Map
    {
        private readonly Func<int, int> dec = x => x - 1;
        private readonly Func<int, int> add1 = x => x + 1;
        private readonly Func<int, int> times2 = x => x * 2;
        private readonly dynamic intoArray = R.Into(new object[0]);

        class _ListXF : ITransformer
        {
            public object Init() => new object[0];

            public object Result(object result) => result;

            public object Step(object result, object input) => ((IList)result).Concat(new[] { input });
        }

        class Test
        {
            public object X { get; private set; }

            public Test(object value) {
                X = value;
            }

            public object Map(dynamic f) => f(X);
        }

        [TestMethod]
        public void Map_Maps_Simple_Functions_Over_Arrays() {
            CollectionAssert.AreEqual(R.Map(times2, new[] { 1, 2, 3, 4 }), new[] { 2, 4, 6, 8 });
        }

        [TestMethod]
        public void Map_Maps_Simple_Functions_Into_Arrays() {
            CollectionAssert.AreEqual(intoArray(R.Map(times2), new[] { 1, 2, 3, 4 }), new[] { 2, 4, 6, 8 });
        }

        [TestMethod]
        public void Map_Maps_Over_Objects() {
            DynamicAssert.AreEqual(R.Map(dec, new { }), new { });
            DynamicAssert.AreEqual(R.Map(dec, new { X = 4, Y = 5, Z = 6 }), new { X = 3, Y = 4, Z = 5 });
        }

        [TestMethod]
        [Description("Map_Interprets_((->)_R)_As_A_Functor")]
        public void Map_Interprets_LiftedFunction_As_A_Functor() {
            Func<int, int> f = a => a - 1;
            Func<int, int> g = b => b * 2;
            var h = R.Map(f, g);

            Assert.AreEqual(h(10), (10 * 2) - 1);
        }

        [TestMethod]
        [Description("Map_Dispatches_To_Objects_That_Implement_\"Map\"")]
        public void Map_Dispatches_To_Objects_That_Implement_Map() {
            var obj = new Test(100);

            Assert.AreEqual(R.Map(add1, obj), 101);
        }

        [TestMethod]
        public void Map_Dispatches_To_Transformer_Objects() {
            var listXf = new _ListXF();
            object res = R.Map(add1, listXf);
            DynamicDelegate f = (DynamicDelegate)res.Member("f", @private: true);
            object xf = res.Member("xf", @private: true);

            Assert.IsInstanceOfType(res, typeof(XMap));
            Assert.AreEqual(xf, listXf);
            Assert.AreEqual(f.Unwrap(), add1);
        }

        [TestMethod]
        public void Map_Composes() {
            var mdec = R.Map(dec);
            var mdouble = R.Map(times2);

            CollectionAssert.AreEqual(mdec(mdouble(new[] { 10, 20, 30 })), new[] { 19, 39, 59 });
        }

        [TestMethod]
        [Description("Map_Can_Compose_Transducer-Style")]
        public void Map_Can_Compose_Transducer_Style() {
            var listXf = new _ListXF();
            var mdouble = R.Map(times2);
            var mdec = R.Map(dec);
            var xcomp = mdec(mdouble(listXf));
            object objxcomp = xcomp;
            var xmap = objxcomp.Member("xf", @private: true);
            DynamicDelegate f = (DynamicDelegate)objxcomp.Member("f", @private: true);
            DynamicDelegate xmapf = (DynamicDelegate)xmap.Member("f", @private: true);
            object xmapXf = xmap.Member("xf", @private: true);

            Assert.IsInstanceOfType(xcomp, typeof(XMap));
            Assert.IsInstanceOfType(xmap, typeof(XMap));
            Assert.AreEqual(xmapXf, listXf);
            Assert.AreEqual(xmapf.Unwrap(), times2);
            Assert.AreEqual(f.Unwrap(), dec);
        }

        [TestMethod]
        public void Map_Is_Curried() {
            var inc = R.Map(add1);

            CollectionAssert.AreEqual(inc(new[] { 1, 2, 3 }), new[] { 2, 3, 4 });
        }

        [TestMethod]
        public void Map_Correctly_Reports_The_Arity_Of_Curried_Versions() {
            Assert.AreEqual(R.Map(add1).Length, 1);
        }
    }
}
