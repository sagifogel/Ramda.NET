using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class MergeWithKey : BaseMerge
    {
        private readonly Func<object, object, object, object> last = (k, x, y) => y;
        
        [TestMethod]
        [Description("MergeWithKey_Takes_Two_Objects,_Merges_Their_Own_Properties_And_Returns_A_New_Object")]
        public void MergeWithKey_Takes_Two_Objects_Merges_Their_Own_Properties_And_Returns_A_New_Object() {
            var a = new { W = 1, X = 2 };
            var b = new { Y = 3, Z = 4 };

            DynamicAssert.AreEqual(R.MergeWithKey(last, a, b), new { W = 1, X = 2, Y = 3, Z = 4 });
        }

        [TestMethod]
        public void MergeWithKey_Applies_The_Provided_Function_To_The_Value_From_The_First_Object_And_The_Value_From_The_Second_Object_To_Determine_The_Value_For_Keys_That_Exist_In_Both_Objects() {
            var a = new { A = "B", X = "D" };
            var b = new { A = "C", Y = "E" };
            var c = R.MergeWithKey((string k, string _a, string _b) =>  k + _a + _b, a, b);

            DynamicAssert.AreEqual(c, new { A = "ABC", X = "D", Y = "E" });
        }

        [TestMethod]
        public void MergeWithKey_Is_Not_Destructive() {
            var a = new { W = 1, X = 2 };
            var res = R.MergeWithKey(last, a, new { X = 5 });

            Assert.AreNotEqual(a, res);
            DynamicAssert.AreEqual(res, new { W = 1, X = 5 });
        }

        [TestMethod]
        public void MergeWithKey_Reports_Only_Own_Properties() {
            var a = new { W = 1, X = 2 };
            var cla = new Cla { X = 5 };

            DynamicAssert.AreEqual(R.MergeWithKey(last, new Cla(), a), new { W = 1, X = 2 });
            DynamicAssert.AreEqual(R.MergeWithKey(last, a, cla), new { W = 1, X = 2 });
            DynamicAssert.AreEqual(R.MergeWithKey(last, cla, new { W = 1 }), new { W = 1 });
        }

        [TestMethod]
        public void MergeWithKey_Is_Curried() {
            DynamicAssert.AreEqual(R.MergeWithKey(last)(new { W = 1, X = 2 })(new { Y = 3, Z = 4 }), new { W = 1, X = 2, Y = 3, Z = 4 });
        }
    }
}
