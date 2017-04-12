using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Contains
    {
        public class Just : IEquatable<Just>
        {
            public object Value { get; private set; }

            public Just(object value) {
                Value = value;
            }
            public override bool Equals(object obj) {
                return base.Equals((Just)obj);
            }

            public bool Equals(Just other) {
                if (other == null) {
                    return false;
                }

                return other.Value == Value;
            }

            public override int GetHashCode() {
                return Value.GetHashCode();
            }
        }

        [TestMethod]
        public void Contains_Returns_True_If_An_Element_Is_In_A_List() {
            Assert.IsTrue(R.Contains(7, new[] { 1, 2, 3, 9, 8, 7, 100, 200, 300 }));
        }

        [TestMethod]
        public void Contains_Returns_False_If_An_Element_Is_Not_In_A_List() {
            Assert.IsFalse(R.Contains(99, new[] { 1, 2, 3, 9, 8, 7, 100, 200, 300 }));
        }

        [TestMethod]
        public void Contains_Returns_False_For_The_Empty_List() {
            Assert.IsFalse(R.Contains(1, new int[0]));
        }

        [TestMethod]
        [Description("Contains_Has_R.Equals_Semantics")]
        public void Contains_Has_R_Equals_Semantics() {
            Assert.IsFalse(R.Contains(-0, new[] { new object() }));
            Assert.IsFalse(R.Contains(new Just(new[] { 42 }), new[] { new Just(new[] { 42 }) }));
        }

        [TestMethod]
        public void Contains_Is_Curried() {
            Assert.IsInstanceOfType(R.Contains(7), typeof(DynamicDelegate));
            Assert.IsFalse(R.Contains(7)(new[] { 1, 2, 3 }));
            Assert.IsTrue(R.Contains(7)(new[] { 1, 2, 7, 3 }));
        }

        [TestMethod]
        [Description("Contains_Is_Curried_Like_A_Binary_Operator, _That_Accepts_An_Initial_Placeholder")]
        public void Contains_Is_Curried_Like_A_Binary_Operator_That_Accepts_An_Initial_Placeholder() {
            var isDigit = R.Contains(R.__, new[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" });

            Assert.IsInstanceOfType(isDigit, typeof (DynamicDelegate));
            Assert.IsTrue(isDigit("0"));
            Assert.IsTrue(isDigit("1"));
            Assert.IsFalse(isDigit("x"));
        }
    }
}
