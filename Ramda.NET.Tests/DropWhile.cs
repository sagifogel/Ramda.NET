using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class DropWhile
    {
        [TestMethod]
        [Description("DropWhile_Skips_Elements_While_The_Function_Reports_`true`")]
        public void DropWhile_Skips_Elements_While_The_Function_Reports_True() {
            CollectionAssert.AreEqual(R.DropWhile(x => x < 5, new[] { 1, 3, 5, 7, 9 }), new[] { 5, 7, 9 });
        }

        [TestMethod]
        public void DropWhile_Returns_An_Empty_List_For_An_Empty_List() {
            int[] emptyArray = new int[0];

            CollectionAssert.AreEqual(R.DropWhile(x => false, emptyArray), emptyArray);
            CollectionAssert.AreEqual(R.DropWhile(x => true, emptyArray), emptyArray);
        }

        [TestMethod]
        [Description("DropWhile_Starts_At_The_Right_Arg_And_Acknowledges_Undefined")]
        public void DropWhile_Starts_At_The_Right_Arg_And_Acknowledges_Null() {
            var sublist = R.DropWhile(x => x != null, new object[] { 1, 3, null, 5, 7 });

            Assert.AreEqual(sublist.Length, 3);
            Assert.AreEqual(sublist[0], null);
            Assert.AreEqual(sublist[1], 5);
            Assert.AreEqual(sublist[2], 7);
        }

        [TestMethod]
        public void DropWhile_Is_Curried() {
            var dropLt7 = R.DropWhile<int>(x => x < 7);

            CollectionAssert.AreEqual(dropLt7(new[] { 1, 3, 5, 7, 9 }), new[] { 7, 9 });
            CollectionAssert.AreEqual(dropLt7(new[] { 2, 4, 6, 8, 10 }), new[] { 8, 10 });
        }
    }
}
