using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Pluck
    {
        private readonly object[] people = new[] {
            new { Name = "Fred", Age = 23 },
            new { Name = "Wilma", Age = 21 },
            new { Name = "Pebbles", Age = 2 }
        };

        [TestMethod]
        public void Pluck_Returns_A_Function_That_Maps_The_Appropriate_Property_Over_An_Array() {
            var nm = R.Pluck("Name");

            Assert.IsInstanceOfType(nm, typeof(DynamicDelegate));
            CollectionAssert.AreEqual(nm(people), new[] { "Fred", "Wilma", "Pebbles" });
        }

        [TestMethod]
        public void Pluck_Behaves_As_A_Transducer_When_Given_A_Transducer_In_List_Position() {
            var numbers = new[] { new { A = 1 }, new { A = 2 }, new { A = 3 }, new { A = 4 } };
            var transducer = R.Compose(R.Pluck("A"), R.Map(R.Add(1)), R.Take(2));

            CollectionAssert.AreEqual(R.Transduce(transducer, R.Flip(R.Append(R.__)), new object[0], numbers), new[] { 2, 3 });
        }
    }
}
