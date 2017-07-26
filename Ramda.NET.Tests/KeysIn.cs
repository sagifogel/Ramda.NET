using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class KeysIn : AbstrcatKeys
    {
        [TestMethod]
        [Description("KeysIn_Returns_An_Array_Of_The_Given_Object's_Own_KeysIn")]
        public void KeysIn_Returns_An_Array_Of_The_Given_Objects_Own_KeysIn() {
            string[] KeysIn = R.KeysIn(obj);

            Array.Sort(KeysIn);
            CollectionAssert.AreEqual(KeysIn, new[] { "A", "B", "C", "D", "E", "F" });
        }

        [TestMethod]
        [Description("KeysIn_Includes_The_Given_Object's_Prototype_Properties")]
        public void KeysIn_Does_Not_Include_The_Given_Objects_Prototype_Properties() {
            var KeysIn = R.KeysIn(cobj);

            Array.Sort(KeysIn);
            CollectionAssert.AreEqual(KeysIn, new[] { "A", "B", "X", "Y"});
        }

        [TestMethod]
        public void KeysIn_Works_For_Primitives() {
            var result = R.Map(val => R.KeysIn(val), new object[] { R.@null, 55, string.Empty, true, false, new object[0] });

            NestedCollectionAssert.AreEqual(result, R.Repeat(new string[0], 6));
        }
    }
}
