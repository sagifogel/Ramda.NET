using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class LensPath
    {
        private readonly object testObject = new {
            A = new object[] {
                new { B = 1 },
                new { B = 2 }
            },
            D = 3
        };

        [TestMethod]
        public void LensPath_View_Focuses_The_Specified_Object_Property() {
            Assert.AreEqual(R.View(R.LensPath(new[] { "D" }), testObject), 3);
            Assert.AreEqual(R.View(R.LensPath(new object[] { "A", 1, "B" }), testObject), 2);
            DynamicAssert.AreEqual(R.View(R.LensPath(new string[0]), testObject), testObject);
        }

        [TestMethod]
        public void LensPath_Set_Sets_The_Value_Of_The_Object_Property_Specified() {
            DynamicAssert.AreEqual(R.Set(R.LensPath(new[] { "D" }), 0, testObject), new { A = new[] { new { B = 1 }, new { B = 2 } }, D = 0 });
            DynamicAssert.AreEqual(R.Set(R.LensPath(new object[] { "A", 0, "B" }), 0, testObject), new { A = new[] { new { B = 0 }, new { B = 2 } }, D = 3 });
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
            DynamicAssert.AreEqual(R.Over(R.LensPath(new[] { "D" }), R.Inc(R.__), testObject), new { A = new[] { new { B = 1 }, new { B = 2 } }, D = 4 });
            DynamicAssert.AreEqual(R.Over(R.LensPath(new object[] { "A", 1, "B" }), R.Inc(R.__), testObject), new { A = new[] { new { B = 1 }, new { B = 3 } }, D = 3 });
            NestedCollectionAssert.AreEqual(R.Over(R.LensPath(new object[0]), R.ToPairs(R.__), testObject), new object[] { new object[] { "A", new[] { new { B = 1 }, new { B = 2 } } }, new object[] { "D", 3 } });
        }
    }
}
