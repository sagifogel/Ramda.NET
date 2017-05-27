using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class PipeP
    {
        [TestMethod]
        public void PipeP_Is_A_Variadic_Function() {
            var pipeMethod = typeof(R).GetMethod("PipeP", new Type[] { typeof(Func<dynamic, Task<dynamic>>[]) });

            Assert.IsInstanceOfType(R.PipeP(R.__), typeof(DynamicDelegate));
            Assert.IsTrue(pipeMethod.GetParameters()[0].IsDefined(typeof(ParamArrayAttribute), true));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PipeP_Throws_If_Given_No_Arguments() {
            R.PipeP(new Func<dynamic, Task<dynamic>>[0]);
        }

        [TestMethod]
        [Description("PipeP_Performs_Left-to-Right_Composition_Of_Promise-Returning_Functions")]
        public void PipeP_Performs_Left_To_Right_Composition_Of_Promise_Returning_Functions() {
            //var res = R.PipeP((Func<int, Task<int[]>>)F, (Func<int, int, Task<int[]>>)G);
        }

        private Task<int[]> F(int a) {
            return new Task<int[]>(() => new[] { a });
        }

        private Task<int[]> G(int a, int b) {
            return new Task<int[]>(() => new[] { a, b });
        }
    }
}
