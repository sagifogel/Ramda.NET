using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Pipe
    {
        public class Context
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Z { get; set; }
            public dynamic A { get; set; }

            public int InvokeX(int val) {
                return X * val;
            }

            public int InvokeY(int val) {
                return Y * val;
            }

            public int InvokeZ(int val) {
                return Z * val;
            }
        }

        [TestMethod]
        public void Pipe_Is_A_Variadic_Function() {
            var pipeMethod = typeof(R).GetMethod("Pipe", new Type[] { typeof(Delegate[]) });

            Assert.IsInstanceOfType(R.Pipe(R.__), typeof(DynamicDelegate));
            Assert.IsTrue(pipeMethod.GetParameters()[0].IsDefined(typeof(ParamArrayAttribute), true));
        }

        [TestMethod]
        [Description("Pipe_Performs_Left-To-Right_Function_Composition")]
        public void Pipe_Performs_Left_To_Right_Function_Composition() {
            var f = R.Pipe((Func<string, int>)int.Parse, R.Multiply(R.__), R.Map(R.__));

            Assert.AreEqual(f.Length, 1);
            CollectionAssert.AreEqual(f("10")(new[] { 1, 2, 3 }), new[] { 10, 20, 30 });
        }

        [TestMethod]
        public void Pipe_Passes_Context_To_Functions() {
            var context = new Context {
                X = 4,
                Y = 2,
                Z = 1
            };

            context.A = R.Pipe((Func<int, int>)context.InvokeX, (Func<int, int>)context.InvokeY, (Func<int, int>)context.InvokeZ);

            Assert.AreEqual(context.A(5), 40);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Pipe_Throws_If_Given_No_Arguments() {
            R.Pipe(new Delegate[0]);
        }

        [TestMethod]
        public void Pipe_Can_Be_Applied_To_One_Argument() {
            Func<int, int, int, int[]> f = (a, b, c) => new[] { a, b, c };
            var g = R.Pipe(f);

            Assert.AreEqual(g.Length, 3);
            CollectionAssert.AreEqual(g(1, 2, 3), new[] { 1, 2, 3 });
        }
    }
}
