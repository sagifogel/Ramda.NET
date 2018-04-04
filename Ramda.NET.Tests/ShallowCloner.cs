using System.Dynamic;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class ShallowClonerTest
    {
        class Test
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        private readonly object anonymous = new { X = 10, Y = 20 };

        [TestMethod]
        public void ShallowCloner_Clone_And_Assign_Value_To_Exisiting_Member_With_Assignable_Type_Returns_Same_Type_As_The_Input() {
            dynamic cloned = ShallowCloner.CloneAndAssignValue("X", 20, new Test { X = 10, Y = 20 });

            Assert.AreEqual(cloned.X, 20);
            Assert.AreEqual(cloned.Y, 20);
            Assert.IsInstanceOfType(cloned, typeof(Test));
        }

        [TestMethod]
        public void ShallowCloner_Clone_And_Assign_Value_To_Non_Exisiting_Member_Returns_ExpandoObject() {
            dynamic cloned = ShallowCloner.CloneAndAssignValue("Z", 20, new Test { X = 10, Y = 20 });

            Assert.AreEqual(cloned.X, 10);
            Assert.AreEqual(cloned.Y, 20);
            Assert.AreEqual(cloned.Z, 20);
            Assert.IsInstanceOfType(cloned, typeof(ExpandoObject));
        }

        [TestMethod]
        public void ShallowCloner_Clone_And_Assign_Value_To_Exisiting_Member_With_Incompatible_Assignable_Type_Returns_ExpandoObject() {
            dynamic cloned = ShallowCloner.CloneAndAssignValue("X", "20", new Test { X = 10, Y = 20 });

            Assert.AreEqual(cloned.X, "20");
            Assert.AreEqual(cloned.Y, 20);
            Assert.IsInstanceOfType(cloned, typeof(ExpandoObject));
        }

        [TestMethod]
        public void ShallowCloner_Clone_And_Assign_Value_To_Exisiting_Member_Of_AnonymousType_With_Assignable_Type_Returns_Same_AnonymousType() {
            dynamic cloned = ShallowCloner.CloneAndAssignValue("X", 20, anonymous);

            Assert.AreEqual(cloned.X, 20);
            Assert.AreEqual(cloned.Y, 20);
            Assert.AreEqual(cloned.GetType(), anonymous.GetType());
        }

        [TestMethod]
        public void ShallowCloner_Clone_And_Assign_Value_To_Non_Exisiting_Member_Of_AnonymousType_Returns_ExpandoObject() {
            dynamic cloned = ShallowCloner.CloneAndAssignValue("Z", 20, anonymous);

            Assert.AreEqual(cloned.X, 10);
            Assert.AreEqual(cloned.Y, 20);
            Assert.AreEqual(cloned.Z, 20);
            Assert.AreNotEqual(cloned.GetType(), anonymous.GetType());
        }

        [TestMethod]
        public void ShallowCloner_Clone_And_Assign_Value_To_Exisiting_Member_Of_AnonymousType_With_Incompatible_Assignable_Type_Returns_ExpandoObject() {
            dynamic cloned = ShallowCloner.CloneAndAssignValue("X", "20", anonymous);

            Assert.AreEqual(cloned.X, "20");
            Assert.AreEqual(cloned.Y, 20);
            Assert.AreNotEqual(cloned.GetType(), anonymous.GetType());
        }

        [TestMethod]
        public void ShallowCloner_Clone_And_Omit_Prop_Returns_ExpandoObject() {
            dynamic cloned = ShallowCloner.CloneAndOmitValue("X", new { X = 10, Y = 20 });

            Assert.IsFalse(((IDictionary<string, object>)cloned).ContainsKey("X"));
            Assert.AreEqual(cloned.Y, 20);
            Assert.IsInstanceOfType(cloned, typeof(ExpandoObject));
        }
    }
}
