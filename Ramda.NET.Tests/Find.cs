using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Find
    {
        private static readonly object obj1 = new { X = 100 };
        private static readonly object obj2 = new { X = 200 };
        private static readonly dynamic isXNotNull = R.Compose(new dynamic[] { R.Not(R.__), R.IsNil(R.__), R.Prop("X") });
        private static object[] a = new[] { 11, 10, 9, "cow", obj1, 8, 7, 100, 200, 300, obj2, 4, 3, 2, 1, 0 };
        private static Func<object, Func<int, bool>, bool> parseAndExec = (n, f) => {
            int result;
            var str = n.ToString();

            if (int.TryParse(str, out result)) {
                return f(result);
            }

            return false;
        };

        private static readonly Func<object, bool> even = n => parseAndExec(n, result => result % 2 == 0);
        private static readonly Func<object, bool> gt100 = n => parseAndExec(n, result => result > 100);
        private static readonly dynamic intoArray = R.Into(new object[0]);
        private static readonly Func<object, bool> isStr = x => x.GetType().Equals(typeof(string));
        private static readonly Func<dynamic, bool> xGt100 = o => {
            if (isXNotNull(o)) {
                return (int)(o.X) > 100;
            }

            return false;
        };

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
