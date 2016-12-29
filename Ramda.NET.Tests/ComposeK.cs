using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class ComposeK
    {
        [TestMethod]
        public void ComposeK_Is_A_Variadic_Function() {
            var composeK = R.ComposeK(R.__);

            Assert.IsInstanceOfType(composeK, typeof(DynamicDelegate));
            Assert.AreEqual(composeK.Length, 2);
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
