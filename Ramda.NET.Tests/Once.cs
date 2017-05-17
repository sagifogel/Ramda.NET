using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Once
    {
        [TestMethod]
        public void Once_Returns_A_Function_That_Calls_The_Supplied_Function_Only_The_First_Time_Called() {
            var ctr = 0;
            var fn = R.Once(new Func<int>(() => ctr += 1));

            fn();
            Assert.AreEqual(ctr, 1);
            fn();
            Assert.AreEqual(ctr, 1);
            fn();
            Assert.AreEqual(ctr, 1);
        }

        [TestMethod]
        public void Once_Passes_Along_Arguments_Supplied() {
            var fn = R.Once(new Func<int, int, int>((a, b) => a + b));
            var result = fn(5, 10);

            Assert.AreEqual(result, 15);
        }

        [TestMethod]
        [Description("Once_Retains_And_Returns_The_First_Value_Calculated,_Even_If_Different_Arguments_Are_Passed_Later")]
        public void Once_Retains_And_Returns_The_First_Value_Calculated_Even_If_Different_Arguments_Are_Passed_Later() {
            var ctr = 0;
            var fn = R.Once(new Func<int, int, int>((a, b) => {
                ctr += 1;
                return a + b;
            }));

            var result = fn(5, 10);
            Assert.AreEqual(result, 15);
            Assert.AreEqual(ctr, 1);
            result = fn(20, 30);
            Assert.AreEqual(result, 15);
            Assert.AreEqual(ctr, 1);
        }

        [TestMethod]
        public void Once_Retains_Arity() {
            var f = R.Once(new Func<int, int, int>((a, b) => a + b));

            Assert.AreEqual(f.Length, 2);
        }
    }
}
