using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class IndexBy
    {
        private readonly object[] list = new[] { new { id = "xyz", title = "A" }, new { id = "abc", title = "B" } };

        [TestMethod]
        public void IndexBy_Indexes_List_By_The_Given_Property() {
            var indexed = R.IndexBy(R.Prop("id"), list);

            DynamicAssert.AreEqual(indexed, new { abc = new { id = "abc", title = "B" }, xyz = new { id = "xyz", title = "A" } });
        }

        [TestMethod]
        public void IndexBy_Indexes_List_By_The_Given_Property_Upper_Case() {
            var indexed = R.IndexBy(R.Compose(R.ToUpper(R.__), R.Prop("id")), list);

            DynamicAssert.AreEqual(indexed, new { ABC = new { id = "abc", title = "B" }, XYZ = new { id = "xyz", title = "A" } });
        }

        [TestMethod]
        public void IndexBy_Is_Curried() {
            var indexed = R.IndexBy(R.Prop("id"))(list);

            DynamicAssert.AreEqual(indexed, new { abc = new { id = "abc", title = "B" }, xyz = new { id = "xyz", title = "A" } });
        }

        [TestMethod]
        public void IndexBy_Can_Act_As_A_Transducer() {
            var transducer = R.Compose(
              R.IndexBy(R.Prop("id")),
              R.Map(R.Pipe(
                R.Adjust(R.ToUpper(R.__), 0),
                R.Adjust(R.Omit("id"), 1)
              )));

            var result = R.Into(new { }, transducer, list);

            DynamicAssert.AreEqual(result, new { ABC = new { title = "B" }, XYZ = new { title = "A" } });
        }
    }
}
