using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class TakeWhile {
        [TestMethod]
        [Description("TakeWhile_Continues_Taking_Elements_While_The_Function_Reports_\"true\"")]
        public void TakeWhile_Continues_Taking_Elements_While_The_Function_Reports_True() {
            CollectionAssert.AreEqual(R.TakeWhile(x => x != 5, new[] { 1, 3, 5, 7, 9 }), new[] { 1, 3 });
        }

        [TestMethod]
        [Description("TakeWhile_Starts_At_The_Right_Arg_And_Acknowledges_Undefined")]
        public void TakeWhile_Starts_At_The_Right_Arg_And_Acknowledges_R_Null() {
            CollectionAssert.AreEqual(R.TakeWhile(x => x != R.@null, new object[] { 1, 3, R.@null, 5, 7 }), new[] { 1, 3 });
        }

        [TestMethod]
        public void TakeWhile_Is_Curried() {
            var takeUntil7 = R.TakeWhile<int>(x => x != 7);

            CollectionAssert.AreEqual(takeUntil7(new[] { 1, 3, 5, 7, 9 }), new[] { 1, 3, 5 });
            CollectionAssert.AreEqual(takeUntil7(new[] { 2, 4, 6, 8, 10}), new[] { 2, 4, 6, 8, 10});
        }        
    }
}
