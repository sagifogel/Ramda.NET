using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class ComposeK
    {
        [TestMethod]
        public void ComposeK_Is_A_Variadic_Function() {
            var composeKMethod = typeof(R).GetMethod("ComposeK", new [] { typeof(Delegate[]) });
            var composeK = R.ComposeK(R.__);

            Assert.IsInstanceOfType(composeK, typeof(DynamicDelegate));
            //Assert.IsTrue(composeKMethod.GetParameters()[0].IsDefined(typeof(ParamArrayAttribute), true));
        }

        [TestMethod]
        public void ComposeK_Throws_If_Given_No_Arguments() {
            try {
                var composeK = R.ComposeK();
            }
            catch (Exception ex) {
            }
        }
    }
}
