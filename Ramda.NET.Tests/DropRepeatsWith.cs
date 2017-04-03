using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class DropRepeatsWith
    {
        private dynamic eqI = R.EqProps("I");
        private object[] objs = new object[] { new { I = 1 }, new { I = 2 }, new { I = 3 }, new { I = 4 }, new { I = 5 }, new { I = 3 } };
        private object[] objs2 = new object[] {
            new { I = 1 }, new { I = 1 }, new { I = 1 }, new { I = 2 }, new { I = 3 },
            new { I = 3 }, new { I = 4 }, new { I = 4 }, new { I = 5 }, new { I = 3 }
        };

        [TestMethod]
        public void DropRepeatsWith_Removes_Repeated_Elements_Based_On_Predicate() {
            CollectionAssert.AreEqual(R.DropRepeatsWith(eqI, objs2), objs);
            CollectionAssert.AreEqual(R.DropRepeatsWith(eqI, objs), objs);
        }

        [TestMethod]
        public void DropRepeatsWith_Keeps_Elements_From_The_Left() {
            CollectionAssert.AreEqual(
                R.DropRepeatsWith(eqI, new object[] { new { I = 1, N = 1 }, new { I = 1, N = 2 },
                new { I = 1, N = 3 }, new { I = 4, N = 1 }, new { I = 4, N = 2 } }), new object[] { new { I = 1, N = 1 }, new { I = 4, N = 1 } }
            );
        }

        [TestMethod]
        public void DropRepeatsWith_Returns_An_Empty_Array_For_An_Empty_Array() {
            CollectionAssert.AreEqual(R.DropRepeatsWith(eqI, new object[0]), new object[0]);
        }

        [TestMethod]
        public void DropRepeatsWith_Is_Curried() {
            Assert.IsInstanceOfType(R.DropRepeatsWith(eqI), typeof(DynamicDelegate));
            CollectionAssert.AreEqual(R.DropRepeatsWith(eqI)(objs), objs);
            CollectionAssert.AreEqual(R.DropRepeatsWith(eqI)(objs2), objs);
        }

        [TestMethod]
        public void DropRepeatsWith_Can_Act_As_A_Transducer() {
            CollectionAssert.AreEqual(R.Into(new object[0], R.DropRepeatsWith(eqI), objs2), objs);
        }
    }
}
