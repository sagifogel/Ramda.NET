using System;
using Ramda.NET;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Clamp
    {
        [TestMethod]
        public void Clamp_Clamps_To_The_Lower_Bound() {
            Assert.AreEqual(R.Clamp(1, 10, 0), 1);
            Assert.AreEqual(R.Clamp(3, 12, 1), 3);
            Assert.AreEqual(R.Clamp(-15, 3, -100), -15);
        }

        [TestMethod]
        public void Clamp_Clamps_To_The_Upper_Bound() {
            Assert.AreEqual(R.Clamp(1, 10, 20), 10);
            Assert.AreEqual(R.Clamp(3, 12, 23), 12);
            Assert.AreEqual(R.Clamp(-15, 3, 16), 3);
        }

        [TestMethod]
        public void Clamp_Leaves_It_Alone_When_Within_The_Bound() {
            Assert.AreEqual(R.Clamp(1, 10, 4), 4);
            Assert.AreEqual(R.Clamp(3, 12, 6), 6);
            Assert.AreEqual(R.Clamp(-15, 3, 0), 0);
        }


        [TestMethod]
        public void Clamp_Works_With_Letters_As_Well() {
            Assert.AreEqual(R.Clamp('d', 'n', 'f'), 'f');
            Assert.AreEqual(R.Clamp('d', 'n', 'a'), 'd');
            Assert.AreEqual(R.Clamp('d', 'n', 'q'), 'n');
        }
    }
}
