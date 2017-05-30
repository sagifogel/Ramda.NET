using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class PropEq
    {
        private readonly object obj1 = new { Name = "Abby", Age = 7, Hair = "blond" };
        private readonly object obj2 = new { Name = "Fred", Age = 12, Hair = "brown" };
        private readonly object obj3 = new { Name = "Rusty", Age = 10, Hair = "brown" };
        private readonly object obj4 = new { Name = "Alois", Age = 15, Disposition = "surly" };

        [TestMethod]
        public void PropEq_Determines_Whether_A_Particular_Property_Matches_A_Given_Value_For_A_Specific_Object() {
            Assert.IsTrue(R.PropEq("Name", "Abby", obj1));
            Assert.IsTrue(R.PropEq("Hair", "brown", obj2));
            Assert.IsFalse(R.PropEq("Hair", "blond", obj2));
        }

        [TestMethod]
        [Description("PropEq_Has_R.Equals_Semantics")]
        public void PropEq_Has_R_equals_Semantics() {
            object nullObject = R.@null;

            Assert.IsTrue(R.PropEq("Value", R.@null, new { Value = nullObject }));
            Assert.IsTrue(R.PropEq("Value", new Just(new[] { 42 }), new { Value = new Just(new[] { 42 }) }));
        }

        [TestMethod]
        public void PropEq_Is_Curried() {
            var hairMatch = R.PropEq("Hair");
            var brunette = hairMatch("brown");
            var kids = new[] { obj1, obj2, obj3, obj4 };

            Assert.IsInstanceOfType(hairMatch, typeof(DynamicDelegate));
            NestedCollectionAssert.AreEqual(R.Filter(brunette, kids), new[] { obj2, obj3 });
            NestedCollectionAssert.AreEqual(R.Filter(R.PropEq("Hair", "brown"), kids), new[] { obj2, obj3 });
        }
    }
}
