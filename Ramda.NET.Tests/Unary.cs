using System;
using static Ramda.NET.ReflectionExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Unary
    {
        [TestMethod]
        [Description("Unary_Turns_Multiple-Argument_Function_Into_Unary_One")]
        public void Unary_Turns_MultipleArgument_Function_Into_Unary_One() {
            R.Unary(new Func<int, int?, int?, int>((x, y, z) => {
                Assert.AreEqual(Arity(x, y, z).Count, 1);
                Assert.IsNull(y);
                Assert.IsNull(z);

                return x;
            }))(10, 20, 30);
        }

        [TestMethod]
        public void Unary_Initial_Argument_Is_Passed_Through_Normally() {
            R.Unary(new Func<int, int, int?, int>((x, y, z) => {
                Assert.AreEqual(x, 10);
                return x;
            }))(10, 20, 30);
        }
    }
}
