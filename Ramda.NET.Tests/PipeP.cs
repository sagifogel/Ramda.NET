using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class PipeP
    {
        private readonly Func<dynamic, Task<dynamic>> f = a => Task.Run<dynamic>(() => new[] { a });
        private readonly Func<dynamic, dynamic, Task<dynamic>> g = (a, b) => Task.Run<dynamic>(() => new[] { a, b });

        [TestMethod]
        public void PipeP_Is_A_Variadic_Function() {
            var pipeMethod = typeof(R).GetMethod("PipeP", new [] { typeof(Func<dynamic, Task<dynamic>>[]) });

            Assert.IsInstanceOfType(R.PipeP(R.__), typeof(DynamicDelegate));
            Assert.IsTrue(pipeMethod.GetParameters()[0].IsDefined(typeof(ParamArrayAttribute), true));
        }
        
        [TestMethod]
        [Description("PipeP_Performs_Left-to-Right_Composition_Of_Promise-Returning_Functions")]
        public async Task PipeP_Performs_Left_To_Right_Composition_Of_Promise_Returning_Functions() {
            Assert.AreEqual(R.PipeP(f).Length, 1);
            Assert.AreEqual(R.PipeP(g).Length, 2);
            Assert.AreEqual(R.PipeP(f, f).Length, 1);
            Assert.AreEqual(R.PipeP(f, g).Length, 1);
            Assert.AreEqual(R.PipeP(g, f).Length, 2);
            Assert.AreEqual(R.PipeP(g, g).Length, 2);

            await Task.Run(() => {
                ((PromiseLikeDynamicDelegate)R.PipeP(f, g)(1)).Then(result1 => {
                    NestedCollectionAssert.AreEqual(result1, new object[] { new[] { 1 }, null });

                    ((PromiseLikeDynamicDelegate)R.PipeP(g, f)(1)).Then(result2 => {
                        NestedCollectionAssert.AreEqual(result2, new[] { new object[] { 1, null } });

                        ((PromiseLikeDynamicDelegate)R.PipeP(f, g)(1, 2)).Then(result3 => {
                            NestedCollectionAssert.AreEqual(result3, new object[] { new[] { 1 }, null });

                            ((PromiseLikeDynamicDelegate)R.PipeP(g, f)(1, 2)).Then(result4 => {
                                NestedCollectionAssert.AreEqual(result4, new object[] { new[] { 1, 2 } });
                                return result4;
                            });

                            return result3;
                        });

                        return result2;
                    });

                    return result1;
                });
            });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PipeP_Throws_If_Given_No_Arguments() {
            R.PipeP(new Func<dynamic, Task<dynamic>>[0]);
        }
    }
}
