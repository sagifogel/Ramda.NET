using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class UseWith
    {
        private static Func<int, int> add1 = x => x + 1;
        private static Func<int, int> div3 = x => x / 3;
        private static Func<int, int> mult2 = x => x * 2;
        private static Func<int[], int> max = arguments => arguments.Max();
        private dynamic f = R.UseWith(max, new[] { add1, mult2, div3 });

        class Context
        {
            private dynamic F1 = R.Add(1);
            private dynamic F2 = R.Add(2);
            private dynamic F3 = R.Add(R.__);

            public object A(dynamic x) => F1(x);
            public object B(dynamic x) => F2(x);
            public object C(dynamic x, dynamic y) => F3(x, y);
            public dynamic D;

            public Context() {
                D = R.UseWith(new Func<dynamic, dynamic, object>(C), new Func<dynamic, object>[] { A, B });
            }
        }

        [TestMethod]
        public void UseWith_Takes_A_List_Of_Function_And_Returns_A_Function() {
            Assert.IsInstanceOfType(R.UseWith(max, new Func<int>[0]), typeof(DynamicDelegate));
            Assert.IsInstanceOfType(R.UseWith(max, new[] { add1 }), typeof(DynamicDelegate));
            Assert.IsInstanceOfType(R.UseWith(max, new[] { add1, mult2, div3 }), typeof(DynamicDelegate));
        }

        [TestMethod]
        public void UseWith_Passes_The_Arguments_Received_To_Their_Respective_Functions() {
            Assert.AreEqual(f(7, 8, 9), 16);
        }

        [TestMethod]
        public void UseWith_Passes_Additional_Arguments_To_The_Main_Function() {
            Assert.AreEqual(f(7, 8, 9, 10), 16);
            Assert.AreEqual(f(7, 8, 9, 20), 20);
        }

        [TestMethod]
        public void UseWith_Has_The_Correct_Arity() {
            Assert.AreEqual(f.Length, 3);
        }

        [TestMethod]
        public void UseWith_Passes_Context_To_Its_Functions() {
            var context = new Context();

            Assert.AreEqual(context.A(1), 2);
            Assert.AreEqual(context.B(1), 3);
            Assert.AreEqual(context.D(1, 1), 5);
            //Assert.AreEqual(context.D(2, 3), 8);
        }
    }
}
