using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Find : BaseFind
    {
        [TestMethod]
        public void Find_Returns_The_First_Element_That_Satisfies_The_Predicate() {
            Assert.AreEqual(R.Find(even, a), 10);
            Assert.AreEqual(R.Find(gt100, a), 200);
            Assert.AreEqual(R.Find(isStr, a), "cow");
            Assert.AreEqual(R.Find(xGt100, a), obj2);
        }

        [TestMethod]
        public void Find_Transduces_The_First_Element_That_Satisfies_The_Predicate_Into_An_Array() {
            CollectionAssert.AreEqual(intoArray(R.Find(even), a), new[] { 10 });
            CollectionAssert.AreEqual(intoArray(R.Find(gt100), a), new[] { 200 });
            CollectionAssert.AreEqual(intoArray(R.Find(isStr), a), new[] { "cow" });
            CollectionAssert.AreEqual(intoArray(R.Find(xGt100), a), new[] { obj2 });
        }

        [TestMethod]
        [Description("Find_Returns_\"Undefined\"_When_No_Element_Satisfies_The_Predicate")]
        public void Find_Returns_Null_When_No_Element_Satisfies_The_Predicate() {
            Assert.IsNull(R.Find(even, new[] { "zing" }));
        }

        [TestMethod]
        [Description("Find_Returns_\"Undefined\"_In_Array_When_No_Element_Satisfies_The_Predicate_Into_An_Array")]
        public void Find_Returns_Null_In_Array_When_No_Element_Satisfies_The_Predicate_Into_An_Array() {
            CollectionAssert.AreEqual(intoArray(R.Find(even), new[] { "zing" }), new object[] { null });
        }

        [TestMethod]
        [Description("Find_Returns_\"Undefined\"_When_Given_An_Empty_List")]
        public void Find_Returns_Null_When_Given_An_Empty_List() {
            Assert.IsNull(R.Find(even, new object[0]));
        }

        [TestMethod]
        [Description("Find_Returns_\"Undefined\"_Into_An_Array_When_Given_An_Empty_List")]
        public void Find_Returns_Null_Into_An_Array_When_Given_An_Empty_List() {
            CollectionAssert.AreEqual(intoArray(R.Find(even), new object[0]), new object[] { null });
        }

        [TestMethod]
        public void Find_Is_Curried() {
            Assert.IsInstanceOfType(R.Find(even), typeof(DynamicDelegate));
            Assert.AreEqual(R.Find(even)(a), 10);
        }
    }
}
