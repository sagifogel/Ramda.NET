using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class TryCatch
    {
        int HeadX(dynamic ls) => ls[0].X;

        int Catcher() => 10101;

        void Thrower(string a, string b, string c) {
            throw new Exception("throwerError");
        }

        string Catch3(Exception e, string a, string b, string c) {
            return string.Join(" ", new[] { e.Message, a, b, c });
        }

        void Throw10(object i) {
            throw new Exception("10");
        }

        int ECatcher(Exception e, object arg) => int.Parse(e.Message);

        [TestMethod]
        public void TryCatch_Takes_Two_Functions_And_Return_A_Function() {
            var mayThrow = R.TryCatch(new Func<dynamic, int>(HeadX), new Func<int>(Catcher));

            Assert.IsInstanceOfType(mayThrow, typeof(DynamicDelegate));
        }

        [TestMethod]
        [Description("TryCatch_Returns_A_Function_With_The_Same_Arity_As_The_\"Tryer\"_Function")]
        public void TryCatch_Returns_A_Function_With_The_Same_Arity_As_The_Tryer_Function() {
            Func<dynamic, dynamic> a1 = a => a;
            var catcher = new Func<int>(Catcher);
            Func<dynamic, dynamic, dynamic> a2 = (a, b) => b;
            Func<dynamic, dynamic, dynamic, dynamic> a3 = (a, b, c) => c;
            Func<dynamic, dynamic, dynamic, dynamic, dynamic> a4 = (a, b, c, d) => d;

            Assert.AreEqual(R.TryCatch(a1, catcher).Length, 1);
            Assert.AreEqual(R.TryCatch(a2, catcher).Length, 2);
            Assert.AreEqual(R.TryCatch(a3, catcher).Length, 3);
            Assert.AreEqual(R.TryCatch(a4, catcher).Length, 4);
        }

        [TestMethod]
        public void TryCatch_Returns_The_Value_Of_The_First_Function_If_It_Does_Not_Throw() {
            var mayThrow = R.TryCatch(new Func<dynamic, int>(HeadX), new Func<int>(Catcher));

            Assert.AreEqual(mayThrow(new[] { new { X = 10 }, new { X = 20 }, new { X = 30 } }), 10);
        }

        [TestMethod]
        public void TryCatch_Returns_The_Value_Of_The_Second_Function_If_The_First_Function_Throws() {
            var mayThrow = R.TryCatch(new Func<dynamic, int>(HeadX), new Func<int>(Catcher));
            var willThrow = R.TryCatch(new Action<object>(Throw10), new Func<Exception, object, int>(ECatcher));

            Assert.AreEqual(willThrow(new object[0]), 10);
            Assert.AreEqual(mayThrow(new object[0]), 10101);
            Assert.AreEqual(willThrow(new[] { new { }, new { }, new { } }), 10);
        }

        [TestMethod]
        public void TryCatch_The_Second_Function_Gets_Passed_The_Error_Object_And_The_Same_Arguments_As_The_First_Function() {
            var mayThrow = R.TryCatch(new Action<string, string, string>(Thrower), new Func<Exception, string, string, string, string>(Catch3));

            Assert.AreEqual(mayThrow("A", "B", "C"), "throwerError A B C");
        }
    }
}
