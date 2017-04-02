using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class DropRepeats
    {
        private int[] objs = new[] { 1, 2, 3, 4, 5, 3, 2 };
        private int[] objs2 = new[] { 1, 2, 2, 2, 3, 4, 4, 5, 5, 3, 2, 2 };

        [TestMethod]
        public void DropRepeats_Removes_Repeated_Elements() {
            CollectionAssert.AreEqual(R.DropRepeats(objs2), objs);
            CollectionAssert.AreEqual(R.DropRepeats(objs), objs);
        }

        [TestMethod]
        public void DropRepeats_Returns_An_Empty_Array_For_An_Empty_Array() {
            CollectionAssert.AreEqual(R.DropRepeats(new int[0]), new int[0]);
        }

        [TestMethod]
        public void DropRepeats_Can_Act_As_A_Transducer() {
            CollectionAssert.AreEqual(R.Into(new int[0], R.DropRepeats(R.__), objs2), objs);
        }

        [TestMethod]
        [Description("DropRepeats_Has_R.equals_Semantics")]
        public void DropRepeats_Has_R_Equals_Semantics() {
            Assert.AreEqual(R.DropRepeats(new[] { R.Null, R.Null }).Length, 1);
            Assert.AreEqual(R.DropRepeats(new[] { new Just(new[] { 42 }), new Just(new[] { 42 }) }).Length, 1);
        }
    }
}
