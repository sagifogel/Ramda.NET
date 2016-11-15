using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Ap
    {
        private Func<int, int> mult2 = x => x * 2;
        private Func<int, int> plus3 = x => x + 3;
        private Func<int, Func<int, int>> f = r => a => r + a;

        [TestMethod]
        [Description("Ap_Interprets_[a]_As_An_Applicative")]
        public void Ap_Interprets_Array_As_An_Applicative() {
            CollectionAssert.AreEqual((ICollection)R.Ap((IList<Func<int, int>>)new[] { mult2, plus3 }, new[] { 1, 2, 3 }), new[] { 2, 4, 6, 4, 5, 6 });
        }

        [TestMethod]
        [Description("Ap_Interprets_((->)_R)_As_An_Applicative")]
        public void Ap_Interprets_Function_As_An_Applicative() {
            var h = R.Ap(f, mult2);
            dynamic add = R.Add();

            Assert.AreEqual((int)h(10), 10 + (10 * 2));
            Assert.AreEqual((int)R.Ap(add)(mult2)(10), 10 + (10 * 2));
        }

        [TestMethod]
        [Description("Ap_Dispatches_To_The_Passed_Object's_Ap_Method_When_Values_Is_A_Non-Array_Object")]
        public void Ap_Dispatches_To_The_Passed_Objects_Ap_Method_When_Values_Is_A_Non_Array_Object() {
            var obj = new { Ap = new Func<int, string>(n => $"called ap with {n}") };

            Assert.AreEqual(R.Ap(obj, 10), obj.Ap(10));
        }

        [TestMethod]
        public void Ap_Is_Curried() {
            var val = R.Ap<int>(new[] { mult2, plus3 });

            Assert.IsTrue(typeof(DynamicDelegate).IsAssignableFrom(val.GetType()));
            CollectionAssert.AreEqual((ICollection)val(new[] { 1, 2, 3 }), new[] { 2, 4, 6, 4, 5, 6 });
        }
    }
}
