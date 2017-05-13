using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Merge
    {
        class Base
        {
            public int X { get; set; }
        }

        class Cla : Base
        {
        }

        [TestMethod]
        [Description("Merge_Takes_Two_Objects,_Merges_Their_Own_Properties_And_Returns_A_New_Object")]
        public void Merge_Takes_Two_Objects_Merges_Their_Own_Properties_And_Returns_A_New_Object() {
            var a = new { W = 1, X = 2 };
            var b = new { Y = 3, Z = 4 };

            DynamicAssert.AreEqual(R.Merge(a, b), new { W = 1, X = 2, Y = 3, Z = 4 });
        }

        [TestMethod]
        public void Merge_Overrides_Properties_In_The_First_Object_With_Properties_In_The_Second_Object() {
            var a = new { W = 1, X = 2 };
            var b = new { W = 100, Y = 3, Z = 4 };

            DynamicAssert.AreEqual(R.Merge(a, b), new { W = 100, X = 2, Y = 3, Z = 4 });
        }

        [TestMethod]
        public void Merge_Is_Not_Destructive() {
            var a = new { W = 1, X = 2 };
            var res = R.Merge(a, new { X = 5 });

            DynamicAssert.AreEqual(res, new { W = 1, X = 5 });
        }

        [TestMethod]
        public void Merge_Reports_Only_Own_Properties() {
            var a = new { W = 1, X = 2 };
            var cla = new Cla { X = 5 };

            DynamicAssert.AreEqual(R.Merge(cla, a), new { W = 1, X = 2 });
            DynamicAssert.AreEqual(R.Merge(a, cla), new { W = 1, X = 2 });
        }

        [TestMethod]
        public void Merge_Is_Curried() {
            var a = new { W = 1, X = 2 };
            var curried = R.Merge(a);
            var curried2 = R.Merge(R.__, a);
            var b = new { Y = 3, Z = 4 };

            DynamicAssert.AreEqual(curried(b), new { W = 1, X = 2, Y = 3, Z = 4 });
            DynamicAssert.AreEqual(curried2(new { X = 3, Y = 4 }), new { W = 1, X = 2, Y = 4 });
        }
    }
}
