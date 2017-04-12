using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Dynamic;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class DissocPath
    {
        public class F
        {
            public F() {
                A = 1;
            }

            public object B { get; set; }

            public int A { get; private set; }
        }

        [TestMethod]
        [Description("DissocPath_Makes_A_Shallow_Clone_Of_An_Object,_Omitting_Only_What_Is_Necessary_For_The_Path")]
        public void DissocPath_Makes_A_Shallow_Clone_Of_An_Object_Omitting_Only_What_Is_Necessary_For_The_Path() {
            var obj1 = new { A = new { B = 1, C = 2, D = new { E = 3 } }, F = new { G = new { H = 4, I = 5, J = new { K = 6, L = 7 } } }, M = 8 };
            dynamic obj2 = R.DissocPath(new[] { "F", "G", "I" }, obj1);

            DynamicAssert.AreEqual(obj2, new { A = new { B = 1, C = 2, D = new { E = 3 } }, F = new { G = new { H = 4, J = new { K = 6, L = 7 } } }, M = 8 });
            Assert.AreEqual(obj2.A, obj1.A);
            Assert.AreEqual(obj2.M, obj1.M);
            Assert.AreEqual(obj2.F.G.H, obj1.F.G.H);
            Assert.AreEqual(obj2.F.G.J, obj1.F.G.J);
        }

        [TestMethod]
        public void DissocPath_Does_Not_Try_To_Omit_Inner_Properties_That_Do_Not_Exist() {
            var obj1 = new { A = 1, B = new { C = 2, D = 3 }, E = 4, F = 5 };
            var obj2 = R.DissocPath(new[] { "X", "Y", "Z" }, obj1);

            DynamicAssert.AreEqual(obj2, new { A = 1, B = new { C = 2, D = 3 }, E = 4, F = 5 });
            Assert.AreEqual(obj2.A, obj1.A);
            Assert.AreEqual(obj2.B, obj1.B);
            Assert.AreEqual(obj2.F, obj1.F);
        }

        [TestMethod]
        public void DissocPath_Leaves_An_Empty_Object_When_All_Properties_Omitted() {
            var obj1 = new { A = 1, B = new { C = 2 }, D = 3 };
            var obj2 = R.DissocPath(new[] { "B", "C" }, obj1);

            DynamicAssert.AreEqual(obj2, new { A = 1, B = new { }, D = 3 });
        }

        [TestMethod]
        [Description("DissocPath_Flattens_Properties_From_Prototype")]
        public void DissocPath_Includes_Well_Typed_Properties() {
            var obj1 = new F { B = new { C = 2, D = 3 } };
            var obj2 = R.DissocPath(new[] { "B", "C" }, obj1);

            DynamicAssert.AreEqual(obj2, new { A = 1, B = new { D = 3 } });
        }

        [TestMethod]
        public void DissocPath_Is_Curried() {
            var obj1 = new { A = new { B = 1, C = 2, D = new { E = 3 } }, F = new { G = new { H = 4, I = 5, J = new { K = 6, L = 7 } } }, M = 8 };
            var expected = new { A = new { B = 1, C = 2, D = new { E = 3 } }, F = new { G = new { H = 4, J = new { K = 6, L = 7 } } }, M = 8 };
            var f = R.DissocPath(new[] { "F", "G", "I" });
            var res = f(obj1);

            DynamicAssert.AreEqual(res, expected);
        }

        [TestMethod]
        public void DissocPath_Accepts_Empty_Path() {
            object res = R.DissocPath(new string[0], new { a = 1, b = 2 });

            DynamicAssert.AreEqual(res, new { a = 1, b = 2 });
        }
    }
}