using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Values
    {
        private readonly object obj = new { A = 100, B = new[] { 1, 2, 3 }, C = new { X = 200, Y = 300 }, D = "D", E = R.@null, F = R.@null };

        class Base
        {
            public string X() => "X";
            public string Y => "Y";
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
        [Description("Values_Returns_An_Array_Of_The_Given_Object\"s_Values")]
        public void Values_Returns_An_Array_Of_The_Given_Objects_Values() {
            Func<object, string> getStr = o => o.IsArray() ? string.Join(",", ((int[])o).Select(i => i.ToString())) : o.IsAnonymousType() ? "[object object]" : R.@null.Equals(o) ? "null" : o.ToString();
            var vs = ((Array)R.Values(obj)).Sort((object a, object b) => string.Compare(getStr(a), getStr(b), StringComparison.Ordinal));
            var ts = new object[] { new[] { 1, 2, 3 }, 100, "D", new { X = 200, Y = 300 }, R.@null, R.@null };

            Assert.AreEqual(vs.Length, ts.Length);
            CollectionAssert.AreEqual((IList)vs[0], (IList)ts[0]);
            Assert.AreEqual(vs[1], ts[1]);
            Assert.AreEqual(vs[2], ts[2]);
            Assert.AreEqual(vs[3], ts[3]);
            Assert.AreEqual(vs[4], ts[4]);
            Assert.AreEqual(vs[5], ts[5]);
        }

        [TestMethod]
        [Description("Values_Does_Not_Include_The_Given_Object\"s_Prototype_Properties")]
        public void Values_Does_Not_Include_The_Given_Objects_Prototype_Properties() {
            CollectionAssert.AreEqual(R.Values(cobj), new[] { 100, 200 });
        }

        [TestMethod]
        public void Values_Returns_An_Empty_Object_For_Primitives() {
            var result = R.Map(val => R.Values(val), new object[] { R.@null, 55, string.Empty, true, false, new object[0] });

            NestedCollectionAssert.AreEqual(result, R.Repeat(new object[0], 6));
        }
    }
}
