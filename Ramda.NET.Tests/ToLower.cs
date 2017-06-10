using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class ToLower
    {
        [TestMethod]
        [Description("ToLower_Returns_The_Lower-Case_Equivalent_Of_The_Input_String")]
        public void ToLower_Returns_The_Lower_Case_Equivalent_Of_The_Input_String() {
            Assert.AreEqual(R.ToLower("XYZ"), "xyz");
        }
    }
}
