using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Converge
    {
        private static Func<int, int> identity = a => a;
        private static Func<int, int, int> mult = (a, b) => a * b;
        private static dynamic f1 = R.Converge(mult, (IList<Delegate>)new Delegate[] { identity, identity });
        private static dynamic f2 = R.Converge(mult, (IList<Delegate>)new Delegate[] { identity, new Func<int, int, int>((a, b) => b) });
        private static dynamic f3 = R.Converge(mult, (IList<Delegate>)new Delegate[] { identity, new Func<int, int, int, int>((a, b, c) => c) });

        [TestMethod]
        public void Converge_Passes_The_Results_Of_Applying_The_Arguments_Individually_To_Two_Separate_Functions_Into_A_Single_One() {
            Assert.AreEqual(R.Converge(mult, new[] { R.Add(1), R.Add(3) })(2), 15);
        }

        [TestMethod]
        [Description("Converge_Returns_A_Function_With_The_Length_Of_The_\"longest\"_Argument")]
        public void Converge_Returns_A_Function_With_The_Length_Of_The_longest_Argument() {
            Assert.AreEqual(f1.Length, 1);
            Assert.AreEqual(f2.Length, 2);
            Assert.AreEqual(f3.Length, 3);
        }

        [TestMethod]
        public void Converge_Returns_A_Curried_Function() {
            Assert.AreEqual(f2(6)(7), 42);
            Assert.AreEqual(f3(R.__).Length, 3);
        }

        [TestMethod]
        public void Converge_Works_With_Empty_Functions_List() {
            dynamic @delegate = Currying.Delegate(arguments => arguments.Length);
            var fn = R.Converge(@delegate, new Delegate[0]);

            Assert.AreEqual(fn.Length, 0);
            Assert.AreEqual(fn(), 0);
        }
    }
}
