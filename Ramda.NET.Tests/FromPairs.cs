using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class FromPairs
    {
        [TestMethod]
        [Description("FromPairs_Combines_An_Array_Of_Two-Element_Arrays_Into_An_Object")]
        public void FromPairs_Combines_An_Array_Of_Two_Element_Arrays_Into_An_Object() {
            DynamicAssert.AreEqual(R.FromPairs(new object[][] { new object[] { "A", 1 }, new object[] { "B", 2 }, new object[] { "C", 3 } }), new { A = 1, B = 2, C = 3 });
        }

        [TestMethod]
        public void FromPairs_Gives_Later_Entries_Precedence_Over_Earlier_Ones() {
            DynamicAssert.AreEqual(R.FromPairs(new object[][] { new object[] { "X", 1 }, new object[] { "X", 2 } }), new { X = 2 });
        }
    }
}
