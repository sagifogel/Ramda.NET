using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Keys
    {
        class C
        {
            public string X() => "X";
            public string Y() => "Y";
            public int A { get; set; }
            public int B { get; set; }

            public C() {
                A = 100;
                B = 200;
            }
        }

        private readonly object obj = new { A = 100, B = new[] { 1, 2, 3 }, C = new { X = 200, Y = 300 }, D = "D", E = R.Null, F = (object)null };
        private readonly C cobj = new C();

        [TestMethod]
        [Description("Keys_Returns_An_Array_Of_The_Given_Object's_Own_Keys")]
        public void Keys_Returns_An_Array_Of_The_Given_Objects_Own_Keys() {
            string[] keys = R.Keys(obj);

            Array.Sort(keys);
            CollectionAssert.AreEqual(keys, new[] { "A", "B", "C", "D", "E", "F" });
        }

        [TestMethod]
        public void Keys_Works_For_Primitives() {
            var result = R.Map(val => R.Keys(val), new object[] { R.Null, 55, string.Empty, true, false, new object[0] });

            NestedCollectionAssert.AreEqual(result, R.Repeat(new string[0], 6));
        }

        [TestMethod]
        [Description("Keys_Does_Not_Include_The_Given_Object's_Prototype_Properties")]
        public void Keys_Does_Not_Include_The_Given_Objects_Prototype_Properties() {
            var keys = R.Keys(cobj);

            Array.Sort(keys);
            CollectionAssert.AreEqual(keys, new[] { "A", "B"});
        }
    }
}
