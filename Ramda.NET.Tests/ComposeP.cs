using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class ComposeP
    {
        private readonly Func<dynamic, Task<dynamic>> f = a => Task.Run<dynamic>(() => new[] { a });
        private readonly Func<dynamic, dynamic, Task<dynamic>> g = (a, b) => Task.Run<dynamic>(() => new[] { a, b });

        [TestMethod]
        public void ComposeP_Is_A_Variadic_Function() {
            var pipeMethod = typeof(R).GetMethod("ComposeP", new Type[] { typeof(Func<dynamic, Task<dynamic>>[]) });

            Assert.IsInstanceOfType(R.ComposeP(R.__), typeof(DynamicDelegate));
            Assert.IsTrue(pipeMethod.GetParameters()[0].IsDefined(typeof(ParamArrayAttribute), true));
        }

        [TestMethod]
        [Description("ComposeP_Performs_Left-to-Right_Composition_Of_Promise-Returning_Functions")]
        public async Task ComposeP_Performs_Left_To_Right_Composition_Of_Promise_Returning_Functions() {
            Assert.AreEqual(R.ComposeP(f).Length, 1);
            Assert.AreEqual(R.ComposeP(g).Length, 2);
            Assert.AreEqual(R.ComposeP(f, f).Length, 1);
            Assert.AreEqual(R.ComposeP(f, g).Length, 2);
            Assert.AreEqual(R.ComposeP(g, f).Length, 1);
            Assert.AreEqual(R.ComposeP(g, g).Length, 2);

            await Task.Run(() => {
                ((PromiseLikeDynamicDelegate)R.ComposeP(f, g)(1)).Then(result1 => {
                    NestedCollectionAssert.AreEqual(result1, new[] { new object[] { 1, null } });

                    ((PromiseLikeDynamicDelegate)R.ComposeP(g, f)(1)).Then(result2 => {
                        NestedCollectionAssert.AreEqual(result2, new object[] { new[] { 1 }, null });

                        ((PromiseLikeDynamicDelegate)R.ComposeP(f, g)(1, 2)).Then(result3 => {
                            NestedCollectionAssert.AreEqual(result3, new object[] { new[] { 1, 2 } });

                            ((PromiseLikeDynamicDelegate)R.ComposeP(g, f)(1, 2)).Then(result4 => {
                                NestedCollectionAssert.AreEqual(result4, new object[] { new[] { 1 }, null });
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
        public void ComposeP_Throws_If_Given_No_Arguments() {
            R.ComposeP(new Func<dynamic, Task<dynamic>>[0]);
        }
    }
}
