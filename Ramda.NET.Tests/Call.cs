using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Call
    {
        delegate int ParamsDelegate(params object[] argumnets);

        class Test
        {
        }

        [TestMethod]
        public void Call_Returns_The_Result_Of_Calling_Its_First_Argument_With_The_Remaining_Arguments() {
            Assert.AreEqual(R.Call((Func<int, int, int>)Math.Max, -99, 42), 42);
        }


        [TestMethod]
        public void Call_Accepts_One_Or_More_Arguments() {
            var fn = new ParamsDelegate((object[] arguments) => arguments.Length);

            Assert.AreEqual(R.Call(fn), 0);
            Assert.AreEqual(R.Call(fn, "x"), 1);
            Assert.AreEqual(R.Call(fn, "x", "y"), 2);
            Assert.AreEqual(R.Call(fn, "x", "y", "z"), 3);
        }

        [TestMethod]
        public void Call_Provides_No_Way_To_Specify_Context() {
            var obj1 = new Test();
            var obj2 = new Test();

            Assert.IsFalse(R.Call((Func<object, bool>)obj2.Equals, obj1));
            Assert.IsTrue(R.Call(R.Bind((Func<object, bool>)obj2.Equals, obj1), obj1));
        }
    }
}
