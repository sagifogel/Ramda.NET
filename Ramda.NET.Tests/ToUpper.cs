using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class ToUpper
    {
        [TestMethod]
        [Description("ToLower_Returns_The_Upper-Case_Equivalent_Of_The_Input_String")]
        public void ToLower_Returns_The_Upper_Case_Equivalent_Of_The_Input_String() {
            Assert.AreEqual(R.ToUpper("abc"), "ABC");
        }
    }
}
