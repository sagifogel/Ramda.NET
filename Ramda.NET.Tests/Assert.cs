using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ramda.NET.Tests;

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
}
