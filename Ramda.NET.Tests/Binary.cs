using System;
using static Ramda.NET.ReflectionExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Binary
    {
        [TestMethod]
        [Description("Binary_Turns_Multiple-argument_Function_Into_Binary_One")]
        public void Binary_Turns_Multiple_argument_Function_Into_Binary_One() {
            R.Binary(new Func<int, int, int?, int>((x, y, z) => {
                Assert.AreEqual(Arity(x, x, z).Length, 2);
                Assert.IsNull(z);
                return x + y;
            }))(10, 20, 30);
        }

        [TestMethod]
        public void Binary_Initial_Arguments_Are_Passed_Through_Normally() {
            R.Binary(new Func<int, int, int?, int>((x, y, z) => {
                Assert.AreEqual(10, x);
                Assert.AreEqual(y, 20);
                return x + y;
            }))(10, 20, 30);
        }
    }
}
