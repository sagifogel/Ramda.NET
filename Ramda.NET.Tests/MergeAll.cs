using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class MergeAll : BaseMerge
    {
        [TestMethod]
        public void MergeAll_Merges_A_List_Of_Objects_Together_Into_One_Object() {
            DynamicAssert.AreEqual(R.MergeAll(new object[] { new { FOO = 1 }, new { BAR = 2 }, new { BAZ = 3 } }), new { FOO = 1, BAR = 2, BAZ = 3 });
        }

        [TestMethod]
        public void MergeAll_Gives_Precedence_To_Later_Objects_In_The_List() {
            DynamicAssert.AreEqual(R.MergeAll(new object[] { new { FOO = 1 }, new { FOO = 2 }, new { BAR = 2 } }), new { FOO = 2, BAR = 2 });
        }

        [TestMethod]
        public void MergeAll_Ignores_Inherited_Properties() {
            var foo = new Foo();
            var res = R.MergeAll(new object[] { foo, new { Fizz = "buzz" } });

            DynamicAssert.AreEqual(res, new { FIZZ = "buzz" });
        }
    }
}
