using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class NAry
    {
        private object[] ToArray(params object[] args) => args;
        private object[] ToNotNullablesArray(params object[] args) => args.Where(arg => arg != null).ToArray();

        [TestMethod]
        [Description("NAry_Turns_Multiple-Argument_Function_Into_A_Nullary_One")]
        public void NAry_Turns_Multiple_Argument_Function_Into_A_Nullary_One() {
            var fn = R.NAry(0, new Func<object, object, object, object[]>((x, y, z) => ToNotNullablesArray(x, y, z)));

            Assert.AreEqual(fn.Length, 0);
            CollectionAssert.AreEqual(fn(1, 2, 3), new object[0]);
        }

        [TestMethod]
        [Description("NAry_Turns_Multiple-argument_Function_Into_A_Ternary_One")]
        public void NAry_Turns_Multiple_Argument_Function_Into_A_Ternary_One() {
            var fn = R.NAry(3, new Func<object[], object[]>(ToArray));

            Assert.AreEqual(fn.Length, 3);
            CollectionAssert.AreEqual(fn(1, 2, 3, 4), new[] { 1, 2, 3 });
            CollectionAssert.AreEqual(fn(1), new object[] { 1, R.@null, R.@null });
        }

        [TestMethod]
        public void NAry_Creates_Functions_Of_Arity_Less_Than_Or_Equal_To_Ten() {
            var fn = R.NAry(10, new Func<object[], object[]>(ToNotNullablesArray));
            var undefs = fn();
            var ns = R.Repeat(R.@null, 10);
            var idx = undefs.Length - 1;

            while (idx >= 0) {
                Assert.AreEqual(undefs[idx], ns[idx]);
                idx -= 1;
            }

            Assert.AreEqual(fn.Length, 10);
            Assert.AreEqual(undefs.Length, ns.Length);
            CollectionAssert.AreEqual(fn(R.Range(0, 25)), R.Range(0, 10));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void NAry_Throws_If_N_Is_Greater_Than_Ten() {
            R.NAry(11, new Func<object>(() => new object()));
        }
    }
}
