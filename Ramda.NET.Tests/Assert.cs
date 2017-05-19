using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ramda.NET.Tests;
using System;
using System.Collections;

namespace Ramda.NET.Tests
{
    public static class DynamicAssert
    {
        public static void AreEqual(dynamic expected, dynamic actual, string message = null) {
            if (!Extension.ContentEquals((object)expected, (object)actual)) {
                Assert.Fail(message);
            }
        }
    }

    public static class NestedCollectionAssert
    {
        public static void AreEqual(IList expected, IList actual, string message = null) {
            if (expected.Equals(actual)) {
                return;
            }

            if (expected.Count != actual.Count || !actual.SequenceEqual(expected, ItemsEqual)) {
                Assert.Fail(message);
            }
        }

        private static bool ItemsEqual(object a,  object b) {
            bool aIsList = a.IsList();
            bool bIsList = b.IsList();
            bool both = (Convert.ToInt32(aIsList) + Convert.ToInt32(bIsList)) % 2 == 0;

            if (aIsList && bIsList) {
                return ((IList)a).SequenceEqual((IList)b, ItemsEqual);
            }

            if (!both) {
                return false;
            }

            return a.EqualsInternal(b);
        }
    }
}
