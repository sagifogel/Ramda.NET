using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class PickBy
    {
        private dynamic obj = new { A = 1, B = 2, C = 3, D = 4, E = 5, F = 6 };

        class Base
        {
            public int Y { get; set; } = 40;
            public int Z { get; set; } = 50;
        }

        class F : Base
        {
            public int X { get; set; }
            public int V { get; set; }
            public int W { get; set; }

            public F(int x) {
                X = x;
            }
        }

        [TestMethod]
        public void PickBy_Creates_A_Copy_Of_The_Object() {
            DynamicAssert.AreEqual(R.PickBy(R.Always(true), obj), obj);
        }

        [TestMethod]
        [Description("PickBy_When_Returning_Truthy,_Keeps_The_Key")]
        public void PickBy_When_Returning_Truthy_Keeps_The_Key() {
            DynamicAssert.AreEqual(R.PickBy(R.T, obj), obj);
        }

        [TestMethod]
        [Description("PickBy_When_Returning_Falsy,_Keeps_The_Key")]
        public void PickBy_When_Returning_Falsy_Keeps_The_Key() {
            DynamicAssert.AreEqual(R.PickBy(R.Always(false), obj), new { });
            DynamicAssert.AreEqual(R.PickBy(R.Always(R.@null), obj), new { });
        }

        [TestMethod]
        [Description("PickBy_Is_Called_With_(val,key,obj)")]
        public void PickBy_Is_Called_With_Val_Key_Obj() {
            Func<object, string, object, bool> pickBy = (val, key, _obj) => {
                Assert.AreEqual(_obj, obj);

                return key.Equals("D") && val.Equals(4);
            };

            DynamicAssert.AreEqual(R.PickBy<object>(pickBy, obj), new { D = 4 });
        }
        [TestMethod]
        public void PickBy_Retrieves_Prototype_Properties() {
            var obj = new F(30) { V = 10, W = 20 };

            DynamicAssert.AreEqual(R.PickBy((val, key, _) => (int)val < 45, obj), new { V = 10, W = 20, X = 30, Y = 40 });
        }
    }
}
