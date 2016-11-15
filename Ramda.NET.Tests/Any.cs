using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Collections;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Any : AbstractAnyOrAll
    {
        [TestMethod]
        public void Any_Returns_True_If_Any_Element_Satisfy_The_Predicate() {
            Assert.AreEqual(R.Any(odd, new[] { 2, 4, 6, 8, 10, 11, 12 }), true);
        }

        [TestMethod]
        public void Any_Returns_False_If_Any_Element_Fails_To_Satisfy_The_Predicate() {
            Assert.AreEqual(R.Any(odd, new[] { 2, 4, 6, 8, 10, 12 }), false);
        }

        [TestMethod]
        public void Any_Returns_True_Into_Array_If_Any_Element_Satisfies_The_Predicate() {
            CollectionAssert.AreEqual((ICollection)intoArray(R.Any(odd), new[] { 2, 4, 6, 8, 10, 11, 12 }), new[] { true });
        }

        [TestMethod]
        public void Any_Returns_False_If_All_Elements_Fails_To_Satisfy_The_Predicate() {
            CollectionAssert.AreEqual((ICollection)intoArray(R.Any(odd), new[] { 2, 4, 6, 8, 10, 12 }), new[] { false });
        }

        [TestMethod]
        public void Any_Works_With_More_Complex_Objects() {
            var people = new List<dynamic> { new { first = "Tosin", last = "Abasi" }, new { first = "Matt", last = "Garstka" }, new { first = "Javier", last = "Reyes" } };
            Func<dynamic, bool> alliterative = person => person.first[0] == person.last[0];
            Assert.AreEqual(R.Any(alliterative, people), false);

            people.Add(new { first = "Donald", last = "Duck" });

            Assert.AreEqual(R.Any(alliterative, people), true);
        }

        [TestMethod]
        public void Any_Can_Use_A_Configurable_Function() {
            var teens = new[] { new { name = "Alice", age = 14 }, new { name = "Betty", age = 18 }, new { name = "Cindy", age = 17 } };
            Func<int, Func<dynamic, bool>> atLeast = age => person => person.age >= age;

            Assert.AreEqual(R.Any(atLeast(16), teens), true);
            Assert.AreEqual(R.Any(atLeast(21), teens), false);
        }


        [TestMethod]
        public void Any_Returns_False_For_An_Empty_List() {
            Assert.AreEqual(R.Any(T, new int[0]), false);
        }

        [TestMethod]
        public void Any_Returns_False_Into_Array_For_An_Empty_List() {
            CollectionAssert.AreEqual((ICollection)intoArray(R.Any(T), new int[0]), new[] { false });
        }

        [TestMethod]
        public void Any_Dispatches_When_Given_A_Transformer_In_List_Position() {
            Assert.IsInstanceOfType(R.All(even, new ListXf()), typeof(XAll));
        }

        [TestMethod]
        public void Any_Is_Curried() {
            var count = 0;
            Func<int, bool> test = n => {
                count += 1;
                return odd(n);
            };

            Assert.AreEqual(R.Any(test)(new[] { 2, 4, 6, 7, 8, 10 }), true);
        }
    }
}
