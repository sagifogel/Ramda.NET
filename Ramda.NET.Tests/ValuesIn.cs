using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class ValuesIn
    {
        private readonly dynamic obj = new { A = 100, B = new[] { 1, 2, 3 }, C = new { X = 200, Y = 300 }, D = "D", E = R.@null, F = R.@null };

        class Base
        {
            public string X => "X";
            public string Y = "Y";
        }

        class C : Base
        {
            public int A { get; set; }
            public int B { get; set; }

            public C() {
                A = 100;
                B = 200;
            }
        }

        private readonly C cobj = new C();

        [TestMethod]
        [Description("ValuesIn_Returns_An_Array_Of_The_Given_Object\"s_Values")]
        public void ValuesIn_Returns_An_Array_Of_The_Given_Objects_Values() {
            var vs = R.ValuesIn(obj);

            Assert.AreEqual(vs.Length, 6);
            Assert.IsTrue(R.IndexOf(100, vs) >= 0);
            Assert.IsTrue(R.IndexOf("D", vs) >= 0);
            Assert.IsTrue(R.IndexOf(R.@null, vs) >= 0);
            Assert.IsTrue(R.IndexOf(obj.B, vs) >= 0);
            Assert.IsTrue(R.IndexOf(obj.C, vs) >= 0);
        }

        [TestMethod]
        [Description("ValuesIn_Includes_The_Given_Object\"s_Prototype_Properties")]
        public void ValuesIn_Includes_The_Given_Objects_Prototype_Properties() {
            var vs = R.ValuesIn(cobj);

            Assert.AreEqual(vs.Length, 4);
            Assert.IsTrue(R.IndexOf(100, vs) >= 0);
            Assert.IsTrue(R.IndexOf(200, vs) >= 0);
            Assert.IsTrue(R.IndexOf(cobj.X, vs) >= 0);
            Assert.IsTrue(R.IndexOf("Y", vs) >= 0);
        }

        [TestMethod]
        public void ValuesIn_Returns_An_Empty_Object_For_Primitives() {
            var result = R.Map(val => R.ValuesIn(val), new object[] { R.@null, 55, string.Empty, true, false, new object[0] });

            NestedCollectionAssert.AreEqual(result, R.Repeat(new string[0], 6));
        }
    }
}
