using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class PipeK
    {
        [TestMethod]
        public void PipeK_Is_A_Variadic_Function() {
            var pipeMethod = typeof(R).GetMethod("PipeK", new Type[] { typeof(Delegate[]) });

            Assert.IsInstanceOfType(R.Pipe(R.__), typeof(DynamicDelegate));
            Assert.IsTrue(pipeMethod.GetParameters()[0].IsDefined(typeof(ParamArrayAttribute), true));
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PipeK_Throws_If_Given_No_Arguments() {
            R.PipeK(new Delegate[0]);
        }
    }
}
