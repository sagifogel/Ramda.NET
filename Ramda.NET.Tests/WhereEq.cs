using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class WhereEq
    {
        private readonly Parent parent = new Parent { X = 5 };
        private readonly object spec = new { X = 1, Y = 2 };
        private readonly object test1 = new { X = 0, Y = 200 };
        private readonly object test2 = new { X = 0, Y = 10 };
        private readonly object test3 = new { X = 1, Y = 101 };
        private readonly object test4 = new { X = 1, Y = 2 };

        class Base
        {
            public object X { get; set; }
            public int A { get; set; }
        }

        class Parent : Base
        {
            public int Y { get; set; }

            public Parent() {
                Y = 6;
            }
        }

        class Spec2 : Base
        {
            public dynamic Y { get; set; }

            public Spec2() {
                Y = R.Equals(6);
                X = R.Equals(5);
            }
        }

        [TestMethod]
        public void WhereEq_Returns_True_If_The_Test_Object_Satisfies_The_Spec() {
            Assert.IsFalse(R.WhereEq(spec, test1));
            Assert.IsFalse(R.WhereEq(spec, test2));
            Assert.IsFalse(R.WhereEq(spec, test3));
            Assert.IsTrue(R.WhereEq(spec, test4));
        }

        [TestMethod]
        [Description("WhereEq_Does_Not_Need_The_Spec_And_The_Test_Object_To_Have_The_Same_Interface_(The_Test_Object_Will_Have_A_Superset_Of_The_Specs_Properties)")]
        public void WhereEq_Does_Not_Need_The_Spec_And_The_Test_Object_To_Have_The_Same_Interface() {
            var spec = new { X = 100 };
            var test1 = new { X = 20, Y = 100, Z = 100 };
            var test2 = new { W = 1, X = 100, Y = 100, Z = 100 };

            Assert.IsFalse(R.WhereEq(spec, test1));
            Assert.IsTrue(R.WhereEq(spec, test2));
        }

        [TestMethod]
        [Description("WhereEq_Matches_Specs_That_Have_Undefined_Properties(The_Test_Object_Will_Have_A_Superset_Of_The_Specs_Properties)")]
        public void WhereEq_Matches_Specs_That_Have_R_Null_Properties() {
            var spec = new { X = R.@null };
            var test1 = new { };
            var test2 = new { X = (object)null };
            var test3 = new { X = 1 };

            Assert.IsTrue(R.WhereEq(spec, test1));
            Assert.IsTrue(R.WhereEq(spec, test2));
            Assert.IsFalse(R.WhereEq(spec, test3));
        }

        [TestMethod]
        public void WhereEq_Is_Curried() {
            var predicate = R.WhereEq(new { X = 1, Y = 2 });

            Assert.IsTrue(predicate(new { X = 1, Y = 2, Z = 3 }));
            Assert.IsFalse(predicate(new { X = 3, Y = 2, Z = 1 }));
        }

        [TestMethod]
        public void WhereEq_Is_True_For_An_Empty_Spec() {
            Assert.IsTrue(R.WhereEq(new { }, new { A = 1 }));
        }

        [TestMethod]
        public void WhenEq_Reports_True_When_The_Object_Equals_The_Spec() {
            var areEqual = new Action<bool>(Assert.IsTrue);

            Assert.IsTrue(R.WhereEq(areEqual, areEqual));
        }

        [TestMethod]
        public void WhereEq_Matches_Inherited_Properties() {
            Assert.IsTrue(R.WhereEq(new { Y = 6 }, parent));
            Assert.IsTrue(R.WhereEq(new { X = 5 }, parent));
            Assert.IsTrue(R.WhereEq(new { X = 5, Y = 6 }, parent));
            Assert.IsFalse(R.WhereEq(new { X = 4, Y = 6 }, parent));
        }

        [TestMethod]
        public void WhereEq_Does_Not_Match_Inherited_Spec() {
            Assert.IsTrue(R.WhereEq(parent, new { Y = 6 }));
            Assert.IsFalse(R.WhereEq(parent, new { X = 5 }));
        }
    }
}
