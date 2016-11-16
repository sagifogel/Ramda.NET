using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ramda.NET.Tests
{
    public static class NestedCollectionAssert
    {
        public static void AreEqual(IList result, IList expected) {
            result.ForEach((item, i) => {
                var innerList = expected[i];

                if (item.IsList()) {
                    CollectionAssert.AreEqual((ICollection)item, (ICollection)innerList);
                }
                else {
                    Assert.AreEqual(item, innerList); 
                }
            });
        }
    }
}
