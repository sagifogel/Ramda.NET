using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Without
    {
        [TestMethod]
        public void Without_Returns_An_Array_Not_Containing_Values_In_The_First_Argument() {
            CollectionAssert.AreEqual(R.Without(new[] { 1, 2 }, new[] { 1, 2, 1, 4, 5 }), new[] { 4, 5 });
        }

        [TestMethod]
        public void Without_Is_Curried() {
            var withoutOnes = R.Without(new[] { 1 });

            CollectionAssert.AreEqual(withoutOnes(new[] { 1, 2, 3, 5, 1 }), new[] { 2, 3, 5 });
        }

        [TestMethod]
        public void Without_Can_Act_As_A_Transducer() {
            CollectionAssert.AreEqual(R.Into(new int[0], R.Without(new[] { 1 }), new[] { 1 }), new int[0]);
        }

        [TestMethod]
        [Description("Without_Has_R.Equals_Semantics")]
        public void Without_Has_R_Equals_Semantics() {
            Func<_Nothing> Nothing = _Nothing.Nothing;
            Func<object, Just> Just = _Maybe.Just;

            Assert.AreEqual(R.Without(new[] { 0 }, new[] { -0 }).Length, 0);
            Assert.AreEqual(R.Without(new[] { 1 }, new[] { 1 }).Length, 0);
            Assert.AreEqual(R.Without(new[] { Nothing() }, new[] { Nothing() }).Length, 0);
            Assert.AreEqual(R.Without(new[] { new Just(new[] { 42 }) }, new[] { new Just(new[] { 42 }) }).Length, 0);
        }
    }
}
