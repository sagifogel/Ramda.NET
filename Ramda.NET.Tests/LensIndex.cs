using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class LensIndex
    {
        private readonly object[] testList = new object[] { new { A = 1 }, new { B = 2 }, new { C = 3 } };

        [TestMethod]
        public void LensIndex_View_focuses_List_Element_At_The_Specified_Index() {
            DynamicAssert.AreEqual(R.View(R.LensIndex(0), testList), new { A = 1 });
        }

        [TestMethod]
        [Description("LensIndex_Returns_Undefined_If_The_Specified_Index_Does_Not_Exist")]
        public void LensIndex_Returns_Null_If_The_Specified_Index_Does_Not_Exist() {
            DynamicAssert.AreEqual(R.View(R.LensIndex(10), testList), R.@null);
        }

        [TestMethod]
        public void LensIndex_Set_Sets_The_List_Value_At_The_Specified_Index() {
            CollectionAssert.AreEqual(R.Set(R.LensIndex(0), 0, testList), new object[] { 0, new { B = 2 }, new { C = 3 } });
        }

        [TestMethod]
        public void LensIndex_Over_Applies_Function_To_The_Value_At_The_Specified_List_Index() {
            NestedCollectionAssert.AreEqual(R.Over(R.LensIndex(2), R.Keys(R.__), testList), new object[] { new { A = 1 }, new { B = 2 }, new[] { "C" } });
        }

        [TestMethod]
        public void LensIndex_Composability_Can_Be_Composed() {
            var nestedList = new object[] { 0, new[] { 10, 11, 12 }, 1, 2 };
            var composedLens = R.Compose(R.LensIndex(1), R.LensIndex(0));

            Assert.AreEqual(R.View(composedLens, nestedList), 10);
        }

        [TestMethod]
        [Description("LensIndex_Well_Behaved_Lens_Set_S_(Get_S)_===_S")]
        public void LensIndex_Well_Behaved_Lens_Set_S_Get_S_Equals_S() {
            CollectionAssert.AreEqual(R.Set(R.LensIndex(0), R.View(R.LensIndex(0), testList), testList), testList);
        }

        [TestMethod]
        [Description("LensIndex_Well_Behaved_Get_(Set_S_V)_===_V")]
        public void LensIndex_Well_Behaved_Get_Set_S_V_Equals_V() {
            Assert.AreEqual(R.View(R.LensIndex(0), R.Set(R.LensIndex(0), 0, testList)), 0);
        }

        [TestMethod]
        [Description("LensIndex_Well_Behaved_Get_(Set(Set_S_V1)_V2)_===_V2")]
        public void LensIndex_Well_Behaved_Get_Set_Set_S_V1_V2_Equals_V2() {
            Assert.AreEqual(R.View(R.LensIndex(0), R.Set(R.LensIndex(0), 11, R.Set(R.LensIndex(0), 10, testList))), 11);
        }
    }
}
