using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Chain
    {
        private Func<int, int[]> times2 => x => new[] { x * 2 };

        [TestMethod]
        [Description("Chain_Maps_A_Function_Over_A_Nested_List_And_Returns_The_(shallow)_Flattened_Result")]
        public void Chain_Maps_A_Function_Over_A_Nested_List_And_Returns_The_Shallow_Flattened_Result() {
            CollectionAssert.AreEqual((ICollection)R.Chain(times2, new[] { 1, 2, 3, 1, 0, 10, -3, 5, 7 }), new[] { 2, 4, 6, 2, 0, 20, -6, 10, 14 });
            CollectionAssert.AreEqual((ICollection)R.Chain(times2, new[] { 1, 2, 3 }), new[] { 2, 4, 6 });
        }
    }
}
