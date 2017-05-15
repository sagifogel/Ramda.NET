using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class LensPath
    {
        private readonly string[] p = new[] { "D" };
        private readonly object[] q = new object[] { "A", 0, "B" };
        private readonly object testObject = new {
            A = new object[] {
                new { B = 1 },
                new { B = 2 }
            },
            D = 3
        };

        [TestMethod]
        public void LensPath_View_Focuses_The_Specified_Object_Property() {
            Assert.AreEqual(R.View(R.LensPath(p), testObject), 3);
            Assert.AreEqual(R.View(R.LensPath(new object[] { "A", 1, "B" }), testObject), 2);
            DynamicAssert.AreEqual(R.View(R.LensPath(new string[0]), testObject), testObject);
        }

        [TestMethod]
        public void LensPath_Set_Sets_The_Value_Of_The_Object_Property_Specified() {
            DynamicAssert.AreEqual(R.Set(R.LensPath(p), 0, testObject), new { A = new[] { new { B = 1 }, new { B = 2 } }, D = 0 });
            DynamicAssert.AreEqual(R.Set(R.LensPath(q), 0, testObject), new { A = new[] { new { B = 0 }, new { B = 2 } }, D = 3 });
            Assert.AreEqual(R.Set(R.LensPath(new object[0]), 0, testObject), 0);
        }

        [TestMethod]
        [Description("LensPath_Set_Adds_The_Property_To_The_Object_If_It_Doesn't_Exist")]
        public void LensPath_Set_Adds_The_Property_To_The_Object_If_It_Does_Not_Exist() {
            DynamicAssert.AreEqual(R.Set(R.LensPath(new[] { "X" }), 0, testObject), new { A = new[] { new { B = 1 }, new { B = 2 } }, X = 0, D = 3 });
            DynamicAssert.AreEqual(R.Set(R.LensPath(new object[] { "A", 0, "X" }), 0, testObject), new { A = new object[] { new { B = 1, X = 0 }, new { B = 2 } }, D = 3 });
        }

        [TestMethod]
        public void LensPath_Over_Applies_Function_To_The_Value_Of_The_Specified_Object_Property() {
            DynamicAssert.AreEqual(R.Over(R.LensPath(p), R.Inc(R.__), testObject), new { A = new[] { new { B = 1 }, new { B = 2 } }, D = 4 });
            DynamicAssert.AreEqual(R.Over(R.LensPath(new object[] { "A", 1, "B" }), R.Inc(R.__), testObject), new { A = new[] { new { B = 1 }, new { B = 3 } }, D = 3 });
            NestedCollectionAssert.AreEqual(R.Over(R.LensPath(new object[0]), R.ToPairs(R.__), testObject), new object[] { new object[] { "A", new[] { new { B = 1 }, new { B = 2 } } }, new object[] { "D", 3 } });
        }

        [TestMethod]
        [Description("Lens_over_Applies_Function_To_Undefined_And_Adds_The_Property_If_It_Doesn't_Exist")]
        public void Lens_over_Applies_Function_To_Undefined_And_Adds_The_Property_If_It_Doesnt_Exist() {
            DynamicAssert.AreEqual(R.Over(R.LensPath(new[] { "X" }), R.Identity(R.__), testObject), new { A = new object[] { new { B = 1 }, new { B = 2 } }, D = 3, X = R.@null });
            DynamicAssert.AreEqual(R.Over(R.LensPath(new object[] { "A", 0, "X" }), R.Identity(R.__), testObject), new { A = new object[] { new { B = 1, X = R.@null }, new { B = 2 } }, D = 3 });
        }

        [TestMethod]
        public void Lens_Composability_Can_Be_Composed() {
            var composedLens = R.Compose(R.LensPath(new[] { "A" }), R.LensPath(new object[] { 1, "B" }));

            Assert.AreEqual(R.View(composedLens, testObject), 2);
        }

        [TestMethod]
        [Description("LensPath_Well_Behaved_Lens_Set_S_(Get_S)_===_S")]
        public void LensPath_Well_Behaved_Lens_Set_S_Get_S_Equals_S() {
            DynamicAssert.AreEqual(R.Set(R.LensPath(p), R.View(R.LensPath(p), testObject), testObject), testObject);
            DynamicAssert.AreEqual(R.Set(R.LensPath(q), R.View(R.LensPath(q), testObject), testObject), testObject);
        }
        
        [TestMethod]
        [Description("LensPath_Well_Behaved_Lens_Set_S_Get_S_Equals_S_Well_Behaved_Get_(Set_S_V)_===_V")]
        public void LensPath_Well_Behaved_Lens_Set_S_Get_S_Equals_S_Well_Behaved_Get_Set_S_V_Equals_V() {
            Assert.AreEqual(R.View(R.LensPath(p), R.Set(R.LensPath(p), 0, testObject)), 0);
            Assert.AreEqual(R.View(R.LensPath(q), R.Set(R.LensPath(q), 0, testObject)), 0);
        }

        [TestMethod]
        [Description("LensPath_Well_Behaved_Lens_Set_S_Get_S_Equals_S_Well_Behaved_Get_(Set(Set_S_V1)_V2)_===_V2")]
        public void LensPath_Well_Behaved_Lens_Set_S_Get_S_Equals_S_Well_Behaved_Get_Set_Set_S_V1_V2_Equals_V2() {
            Assert.AreEqual(R.View(R.LensPath(p), R.Set(R.LensPath(p), 11, R.Set(R.LensPath(p), 10, testObject))), 11);
            Assert.AreEqual(R.View(R.LensPath(q), R.Set(R.LensPath(q), 11, R.Set(R.LensPath(q), 10, testObject))), 11);
        }
    }
}
