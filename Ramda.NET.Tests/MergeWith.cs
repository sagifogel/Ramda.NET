using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class MergeWith : BaseMerge
    {
        private readonly Func<object, object, object> last = (x, y) => y;

        [TestMethod]
        [Description("MergeWith_Takes_Two_Objects,_Merges_Their_Own_Properties_And_Returns_A_New_Object")]
        public void MergeWith_Takes_Two_Objects_Merges_Their_Own_Properties_And_Returns_A_New_Object() {
            var a = new { W = 1, X = 2 };
            var b = new { Y = 3, Z = 4 };

            DynamicAssert.AreEqual(R.MergeWith(last, a, b), new { W = 1, X = 2, Y = 3, Z = 4 });
        }

        [TestMethod]
        public void MergeWith_Applies_The_Provided_Function_To_The_Value_From_The_First_Object_And_The_Value_From_The_Second_Object_To_Determine_The_Value_For_Keys_That_Exist_In_Both_Objects() {
            var a = new { X = "A", Y = "C" };
            var b = new { X = "B", Z = "D" };
            var c = R.MergeWith((string _a, string _b) => _a + _b, a, b);

            DynamicAssert.AreEqual(c, new { X = "AB", Y = "C", Z = "D" });
        }

        [TestMethod]
        public void MergeWith_Is_Not_Destructive() {
            var a = new { W = 1, X = 2 };
            var res = R.MergeWith(last, a, new { X = 5 });

            Assert.AreNotEqual(a, res);
            DynamicAssert.AreEqual(res, new { W = 1, X = 5 });
        }

        [TestMethod]
        public void MergeWith_Reports_Only_Own_Properties() {
            var a = new { W = 1, X = 2 };
            var cla = new Cla { X = 5 };

            DynamicAssert.AreEqual(R.MergeWith(last, new Cla(), a), new { W = 1, X = 2 });
            DynamicAssert.AreEqual(R.MergeWith(last, a, cla), new { W = 1, X = 2 });
            DynamicAssert.AreEqual(R.MergeWith(last, cla, new { W = 1 }), new { W = 1 });
        }

        [TestMethod]
        public void MergeWith_Is_Curried() {
            DynamicAssert.AreEqual(R.MergeWith(last)(new { W = 1, X = 2 })(new { Y = 3, Z = 4 }), new { W = 1, X = 2, Y = 3, Z = 4 });
        }
    }
}
