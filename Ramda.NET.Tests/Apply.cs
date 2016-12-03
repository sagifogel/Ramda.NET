using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Apply
    {

        class Test
        {
        }

        [TestMethod]
        public void Apply_Applies_Function_To_Argument_List() {
            Assert.AreEqual(R.Apply((Func<int, int, int>)Math.Max, new object[] { -99, 42 }), 42);
        }

        [TestMethod]
        public void Apply_Is_Cuuried() {
            Assert.AreEqual(R.Apply((Func<int, int, int>)Math.Max)(new object[] { -99, 42 }), 42);
        }

        [TestMethod]
        public void Apply_Provides_No_Way_To_Specify_Context() {
            var obj = new Test();

            Assert.IsTrue(R.Apply(R.Bind((Func<object, bool>)obj.Equals, obj), new[] { obj }));
        }
    }
}
