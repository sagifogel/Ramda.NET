using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class DropLastWhile
    {
        [TestMethod]
        [Description("DropLastWhile_Skips_Elements_While_The_Function_Reports_`true`")]
        public void DropLastWhile_Skips_Elements_While_The_Function_Reports_True() {
            CollectionAssert.AreEqual(R.DropLastWhile(x => x >= 5, new[] { 1, 3, 5, 7, 9 }), new[] { 1, 3 });
        }

        [TestMethod]
        public void DropLastWhile_Returns_An_Empty_List_For_An_Empty_List() {
            int[] emptyArray = new int[0];

            CollectionAssert.AreEqual(R.DropLastWhile(x => false, emptyArray), emptyArray);
            CollectionAssert.AreEqual(R.DropLastWhile(x => true, emptyArray), emptyArray);
        }

        [TestMethod]
        public void DropLastWhile_Starts_At_The_Right_Arg_And_Acknowledges_Null() {
            var sublist = R.DropLastWhile(x => x != null, new object[] { 1, 3, null, 5, 7 });

            Assert.AreEqual(sublist.Length, 3);
            Assert.AreEqual(sublist[0], 1);
            Assert.AreEqual(sublist[1], 3);
            Assert.AreEqual(sublist[2], null);
        }

        [TestMethod]
        public void DropLastWhile_Is_Curried() {
            var dropGt7 = R.DropLastWhile<int>(x => x > 7);

            CollectionAssert.AreEqual(dropGt7(new[] { 1, 3, 5, 7, 9 }), new[] { 1, 3, 5, 7 });
            CollectionAssert.AreEqual(dropGt7(new[] { 1, 3, 5 }), new[] { 1, 3, 5 });
        }

        [TestMethod]
        public void DropLast_Can_Act_As_A_Transducer() {
            var dropLt7 = R.DropLastWhile<int>(x => x < 7);

            CollectionAssert.AreEqual(R.Into(new int[0], dropLt7, new[] { 1, 3, 5, 7, 9, 1, 2 }), new[] { 1, 3, 5, 7, 9 });
            CollectionAssert.AreEqual(R.Into(new int[0], dropLt7, new[] { 1, 3, 5 }), new int[0]);
        }
    }
}
