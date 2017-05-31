using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Reduced
    {
        [TestMethod]
        public void Reduced_Wraps_A_Value() {
            var v = new { };

            Assert.AreEqual(R.Reduced(v).Value, v);
        }

        [TestMethod]
        public void Reduced_Flags_Value_As_Reduced() {
            Assert.IsTrue(R.Reduced(new { }).Reduced);
        }

        [TestMethod]
        [Description("Reduced_Short-Circuits_Reduce")]
        public void Reduced_Short_Circuits_Reduce() {
            Assert.AreEqual(R.Reduce((acc, v) => {
                dynamic result = acc + v;

                if (result >= 10) {
                    result = R.Reduced(result);
                }

                return result;
            }, 0, new[] { 1, 2, 3, 4, 5 }), 10);
        }
    }
}
