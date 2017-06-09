using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class TakeLastWhile
    {
        [TestMethod]
        [Description("TakeLastWhile_Continues_Taking_Elements_While_The_Function_Reports_\"true\"")]
        public void TakeLastWhile_Continues_Taking_Elements_While_The_Function_Reports_True() {
            CollectionAssert.AreEqual(R.TakeLastWhile(x => x != 5, new[] { 1, 3, 5, 7, 9 }), new[] { 7, 9 });
        }

        [TestMethod]
        [Description("TakeLastWhile_Starts_At_The_Right_Arg_And_Acknowledges_Undefined")]
        public void TakeLastWhile_Starts_At_The_Right_Arg_And_Acknowledges_R_Null() {
            CollectionAssert.AreEqual(R.TakeLastWhile(x => x != R.@null, new object[] { 1, 3, R.@null, 5, 7 }), new[] { 5, 7 });
        }

        [TestMethod]
        public void TakeLastWhile_Is_Curried() {
            var takeLastUntil7 = R.TakeLastWhile<int>(x => x != 7);

            CollectionAssert.AreEqual(takeLastUntil7(new[] { 1, 3, 5, 7, 9 }), new[] { 9 });
            CollectionAssert.AreEqual(takeLastUntil7(new[] { 2, 4, 6, 8, 10 }), new[] { 2, 4, 6, 8, 10 });
        }
    }
}
