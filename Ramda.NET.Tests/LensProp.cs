using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class LensProp
    {
        private readonly object testObject = new { A = 1, B = 2, C = 3 };

        [TestMethod]
        public void LensProp_View_Focuses_Object_The_Specified_Object_Property() {
            Assert.AreEqual(R.View(R.LensProp("A"), testObject), 1);
        }

        [TestMethod]
        [Description("LensProp_Returns_Undefined_If_The_Specified_Property_Does_Not_Exist")]
        public void LensProp_Returns_Null_If_The_Specified_Property_Does_Not_Exist() {
            Assert.AreEqual(R.View(R.LensProp("X"), testObject), R.@null);
        }

        [TestMethod]
        public void LensProp_Set_Sets_The_Value_Of_The_Object_Property_Specified() {
            DynamicAssert.AreEqual(R.Set(R.LensProp("A"), 0, testObject), new { A = 0, B = 2, C = 3 });
        }

        [TestMethod]
        [Description("LensProp_Set_Adds_The_Property_To_The_Object_If_It_Doesn't_Exist")]
        public void LensProp_Set_Adds_The_Property_To_The_Object_If_It_Doesnt_Exist() {
            DynamicAssert.AreEqual(R.Set(R.LensProp("D"), 4, testObject), new { A = 1, B = 2, C = 3, D = 4 });
        }

        [TestMethod]
        public void LensProp_Over_Applies_Function_To_The_Value_Of_The_Specified_Object_Property() {
            DynamicAssert.AreEqual(R.Over(R.LensProp("A"), R.Inc(R.__), testObject), new { A = 2, B = 2, C = 3 });
        }

        [TestMethod]
        [Description("LensProp_Over_Applies_Function_To_Undefined_And_Adds_The_Property_If_It_Doesn't_Exist")]
        public void LensProp_Over_Applies_Function_To_Nulld_And_Adds_The_Property_If_It_Doesnt_Exist() {
            DynamicAssert.AreEqual(R.Over(R.LensProp("X"), R.Identity(R.__), testObject), new { A = 1, B = 2, C = 3, X = R.@null });
        }

        [TestMethod]
        public void Lens_Composability_Can_Be_Composed() {
            var nestedObj = new { A = new { B = 1 }, C = 2 };
            var composedLens = R.Compose(R.LensProp("A"), R.LensProp("B"));

            Assert.AreEqual(R.View(composedLens, nestedObj), 1);
        }

        [TestMethod]
        [Description("LensProp_Well_Behaved_Lens_Set_S_(Get_S)_===_S")]
        public void LensProp_Well_Behaved_Lens_Set_S_Get_S_Equals_S() {
            DynamicAssert.AreEqual(R.Set(R.LensProp("A"), R.View(R.LensProp("A"), testObject), testObject), testObject);
        }

        [TestMethod]
        [Description("LensProp_Well_Behaved_Lens_Set_S_Get_S_Equals_S_Well_Behaved_Get_(Set_S_V)_===_V")]
        public void LensProp_Well_Behaved_Lens_Set_S_Get_S_Equals_S_Well_Behaved_Get_Set_S_V_Equals_V() {
            Assert.AreEqual(R.View(R.LensProp("A"), R.Set(R.LensProp("A"), 0, testObject)), 0);
        }

        [TestMethod]
        [Description("LensProp_Well_Behaved_Lens_Set_S_Get_S_Equals_S_Well_Behaved_Get_(Set(Set_S_V1)_V2)_===_V2")]
        public void LensProp_Well_Behaved_Lens_Set_S_Get_S_Equals_S_Well_Behaved_Get_Set_Set_S_V1_V2_Equals_V2() {
            Assert.AreEqual(R.View(R.LensProp("A"), R.Set(R.LensProp("A"), 11, R.Set(R.LensProp("A"), 10, testObject))), 11);
        }
    }
}
