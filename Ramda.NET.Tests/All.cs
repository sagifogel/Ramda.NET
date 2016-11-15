using System;
using System.Linq;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class All : AbstractAnyOrAll
    {
        [TestMethod]
        public void All_Returns_True_If_All_Elements_Satisfy_The_Predicate() {
            Assert.AreEqual(R.All(even, new[] { 2, 4, 6, 8, 10, 12 }), true);
            Assert.AreEqual(R.All(isFalse, new[] { false, false, false }), true);
        }

        [TestMethod]
        public void All_Returns_False_If_Any_Element_Fails_To_Satisfy_The_Predicate() {
            Assert.AreEqual(R.All(even, new[] { 22, 4, 6, 8, 9, 10 }), false);
        }

        [TestMethod]
        public void All_Returns_True_For_An_Empty_List() {
            Assert.AreEqual(R.All(T, new int[0]), true);
        }


        [TestMethod]
        public void All_Returns_True_Into_Array_If_All_Elements_Satisfy_The_Predicate() {
            CollectionAssert.AreEqual((ICollection)intoArray(R.All(even), new[] { 2, 4, 6, 8, 10, 12 }), new[] { true });
            CollectionAssert.AreEqual((ICollection)intoArray(R.All(isFalse), new[] { false, false, false }), new[] { true });
        }

        [TestMethod]
        public void All_Returns_False_Into_Array_If_Any_Element_Fails_To_Satisfy_The_Predicate() {
            CollectionAssert.AreEqual((ICollection)intoArray(R.All(even), new[] { 2, 4, 6, 8, 9, 10 }), new[] { false });
        }

        [TestMethod]
        public void All_Returns_True_Into_Array_For_An_Empty_List() {
            CollectionAssert.AreEqual(intoArray(R.All(T), new int[0]), new [] { true });
        }

        [TestMethod]
        public void All_Works_With_More_Complex_Objects() {
            Func<object, bool> len3 = o => ((dynamic)o).x.Length == 3;
            Func<object, bool> hasA = o => ((dynamic)o).x.IndexOf("a") > -1;
            var xs = new object[] { new { x = "abc" }, new { x = "ade" }, new { x = "fghiajk" } };

            Assert.AreEqual(R.All(len3, xs), false);
            Assert.AreEqual(R.All(hasA, xs), true);
        }

        [TestMethod]
        public void All_Dispatches_When_Given_A_Transformer_In_List_Position() {
            Assert.IsInstanceOfType(R.All(even, new ListXf()), typeof(XAll));
        }

        [TestMethod]
        public void All_Is_Curried() {
            var count = 0;
            Func<int, bool> test = n => {
                count += 1;
                return even(n);
            };

            Assert.AreEqual(R.All(test)(new[] { 2, 4, 6, 7, 8, 10 }), false);
        }
    }
}
