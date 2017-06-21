using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class UniqWith
    {
        private readonly object[] objs = new object[] {
            new { X = R.T, I = 0}, new { X = R.F, I = 1 }, new { X = R.T, I = 2 }, new { X = R.T, I = 3 },
            new { X = R.F, I = 4}, new { X = R.F, I = 5 }, new { X = R.T, I = 6 }, new { X = R.F, I = 7 }
        };

        private readonly object[] objs2 = new object[]  {
            new { X = R.T, I = 0 }, new { X = R.F, I = 1 }, new { X = R.T, I= 2 }, new { X = R.T, I = 3 },
            new { X = R.F, I = 0 }, new { X = R.T, I = 1 }, new { X = R.F, I= 2 }, new { X = R.F, I = 3 }
        };

        private readonly Func<dynamic, dynamic, bool> eqI = (x, accX) => x.I == accX.I;

        [TestMethod]
        [Description("UniqWith_Returns_A_Set_From_Any_Array_(i.e._Purges_Duplicate_Elements)_Based_On_Predicate")]
        public void UniqWith_Returns_A_Set_From_Any_Array_Based_On_Predicate() {
            NestedCollectionAssert.AreEqual(R.UniqWith(eqI, objs), objs);
            NestedCollectionAssert.AreEqual(R.UniqWith(eqI, objs2), new [] { new { X = R.T, I = 0 }, new { X = R.F, I = 1 }, new { X = R.T, I = 2 }, new { X = R.T, I = 3 } });
        }

        [TestMethod]
        public void UniqWith_Keeps_Elements_From_The_Left() {
            NestedCollectionAssert.AreEqual(R.UniqWith(eqI, new[] { new { I = 1 }, new { I = 2 }, new { I = 3 }, new { I = 4 }, new { I = 1 } }), new[] { new { I = 1 }, new { I = 2 }, new { I = 3 }, new { I = 4 } });
        }

        [TestMethod]
        public void UniqWith_Returns_An_Empty_Array_For_An_Empty_Array() {
            CollectionAssert.AreEqual(R.UniqWith(eqI, new int[0]), new int[0]);
        }

        [TestMethod]
        public void UniqWith_Is_Curried() {
            Assert.IsInstanceOfType(R.UniqWith(eqI), typeof(DynamicDelegate));
            NestedCollectionAssert.AreEqual(R.UniqWith(eqI)(objs), objs);
            NestedCollectionAssert.AreEqual(R.UniqWith(eqI)(objs2), new [] { new { X = R.T, I = 0 }, new { X = R.F, I = 1 }, new { X = R.T, I = 2 }, new { X = R.T, I = 3 } });
        }
    }
}
