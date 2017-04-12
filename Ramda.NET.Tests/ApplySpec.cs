using System.Dynamic;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class ApplySpec
    {
        [TestMethod]
        public void ApplySpec_Works_With_Empty_Spec() {
            DynamicAssert.AreEqual(R.ApplySpec(new { })(), new ExpandoObject());
        }

        [TestMethod]
        public void ApplySpec_Works_With_Unary_Functions() {
            dynamic expando = new ExpandoObject();
            var applied = R.ApplySpec(new { v = R.Inc(R.__), u = R.Dec(R.__) })(1);

            expando.v = 2;
            expando.u = 0;

            DynamicAssert.AreEqual(applied, expando);
        }

        [TestMethod]
        public void ApplySpec_Works_With_Binary_Functions() {
            dynamic expando = new ExpandoObject();
            var applied = R.ApplySpec(new { sum = R.Add(R.__) })(1, 2);

            expando.sum = 3;

            DynamicAssert.AreEqual(applied, expando);
        }

        [TestMethod]
        public void ApplySpec_Works_With_Nested_Specs() {
            dynamic expando = new ExpandoObject();
            var anonymous = new {
                unnested = R.Always(0),
                nested = new {
                    sum = R.Add(R.__)
                }
            };

           var applied = R.ApplySpec(anonymous)(1, 2);

            expando.unnested = 0;
            expando.nested = new ExpandoObject();
            expando.nested.sum = 3;

            DynamicAssert.AreEqual(applied, expando);
        }

        [TestMethod]
        public void ApplySpec_Retains_The_Highest_Arity() {
            var f = R.ApplySpec(new { f1 = R.NAry(2, R.T), f2 = R.NAry(5, R.T) });

            Assert.AreEqual(f.Length, 5);
        }

        [TestMethod]
        public void ApplySpec_Returns_A_Curried_Function() {
            dynamic expando = new ExpandoObject();
            var f = R.ApplySpec(new { sum = R.Add(R.__) })(1)(2);

            expando.sum = 3;

            DynamicAssert.AreEqual(f, expando);
        }
    }
}
