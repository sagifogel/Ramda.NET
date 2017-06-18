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
            Assert.AreEqual(R.Apply(new Func<int, int, int>(Math.Max), new [] { -99, 42 }), 42);
        }

        [TestMethod]
        public void Apply_Is_Cuuried() {
            Assert.AreEqual(R.Apply((Func<int, int, int>)Math.Max)(new [] { -99, 42 }), 42);
        }

        [TestMethod]
        public void Apply_Provides_No_Way_To_Specify_Context() {
            var obj1 = new Test();
            var obj2 = new Test();

            Assert.IsFalse(R.Apply((Func<object, bool>)obj2.Equals, new[] { obj1 }));
            Assert.IsTrue(R.Apply(R.Bind((Func<object, bool>)obj2.Equals, obj1), new[] { obj1 }));
        }
    }
}
