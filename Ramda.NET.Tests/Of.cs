using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Of
    {
        [TestMethod]
        public void Of_Returns_Its_Argument_As_An_Array() {
            NestedCollectionAssert.AreEqual(R.Of(100), new[] { 100 });
            NestedCollectionAssert.AreEqual(R.Of(new[] { 100 }), new object[] { new[] { 100 } });
            NestedCollectionAssert.AreEqual(R.Of(R.@null), new[] { R.@null });
            NestedCollectionAssert.AreEqual(R.Of(new object[0]), new object[] { new object[0] });
        }
    }
}
