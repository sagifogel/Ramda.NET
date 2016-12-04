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
            IDictionary<string, object> expando = new ExpandoObject();

            Assert.IsTrue(expando.ContentEquals((IDictionary<string, object>)R.ApplySpec(new { })()));
        }

        [TestMethod]
        public void ApplySpec_Works_With_Unary_Functions() {
            dynamic expando = new ExpandoObject();
            IDictionary<string, object> applied = R.ApplySpec(new { v = R.Inc(R.__), u = R.Dec(R.__) })(1);

            expando.v = (double)2;
            expando.u = (double)0;

            Assert.IsTrue(applied.ContentEquals((ExpandoObject)expando));
        }

        [TestMethod]
        public void ApplySpec_Works_With_Binary_Functions() {
            dynamic expando = new ExpandoObject();
            IDictionary<string, object> applied = R.ApplySpec(new { sum = R.Add(R.__) })(1, 2);

            expando.sum = (double)3;

            Assert.IsTrue(applied.ContentEquals((ExpandoObject)expando));
        }

        [TestMethod]
        public void ApplySpec_Works_With_Nested_Specs() {
            dynamic expando = new ExpandoObject();
            var anonymous = new {
                unnested = R.Always((double)0),
                nested = new {
                    sum = R.Add(R.__)
                }
            };

            IDictionary<string, object> applied = R.ApplySpec(anonymous)(1, 2);

            expando.unnested = (double)0;
            expando.nested = new ExpandoObject();
            expando.nested.sum = (double)3;

            Assert.IsTrue(applied.ContentEquals((ExpandoObject)expando));
        }

        [TestMethod]
        public void ApplySpec_Retains_The_Highest_Arity() {
            var f = R.ApplySpec(new { f1 = R.NAry(2, R.T), f2 = R.NAry(5, R.T) });

            Assert.AreEqual(f.Length, 5);
        }

        [TestMethod]
        public void ApplySpec_Returns_A_Curried_Function() {
            dynamic expando = new ExpandoObject();
            IDictionary<string, object> f = R.ApplySpec(new { sum = R.Add(R.__) })(1)(2);

            expando.sum = (double)3;
            Assert.IsTrue(f.ContentEquals((ExpandoObject)expando));
        }
    }
}
