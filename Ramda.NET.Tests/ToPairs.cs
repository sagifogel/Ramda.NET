using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class ToPairs
    {
        protected class Base
        {
            public string ProtoProp => "you can\'t see me";
        }

        class F
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        [TestMethod]
        [Description("ToPairs_Converts_An_Object_Into_An_Array_Of_Two-Element_[key,_Value]_Arrays")]
        public void ToPairs_Converts_An_Object_Into_An_Array_Of_Two_Element_ey_Value_Arrays() {
            NestedCollectionAssert.AreEqual(R.ToPairs(new { A = 1, B = 2, C = 3 }), new object[] { new object[] { "A", 1 }, new object[] { "B", 2 }, new object[] { "C", 3 } });
        }

        [TestMethod]
        [Description("ToPairs_Only_Iterates_The_Object's_Own_Properties")]
        public void ToPairs_Only_Iterates_The_Objects_Own_Properties() {
            var f = new F { X = 1, Y = 2 };

            NestedCollectionAssert.AreEqual(R.ToPairs(f), new object[] { new object[] { "X", 1 }, new object[] { "Y", 2 } });
        }
    }
}
