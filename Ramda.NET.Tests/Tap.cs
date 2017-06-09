using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Tap
    {
        [TestMethod]
        public void Tap_Returns_A_Function_That_Always_Returns_Its_Argument() {
            var f = R.Tap(R.Identity(R.__));

            Assert.IsInstanceOfType(f, typeof(DynamicDelegate));
            Assert.AreEqual(f(100), 100);
        }

        [TestMethod]
        [Description("Tap_May_Take_A_Function_As_The_First_Argument_That_Executes_With_Tap's_Argument")]
        public void Tap_May_Take_A_Function_As_The_First_Argument_That_Executes_With_Taps_Argument() {
            var sideEffect = string.Empty;

            Assert.AreEqual(sideEffect, string.Empty);

            var rv = R.Tap(x => {
                sideEffect = string.Format("string {0}", x);
            }, 200);

            Assert.AreEqual(rv, 200);
            Assert.AreEqual(sideEffect, "string 200");
        }
    }
}
