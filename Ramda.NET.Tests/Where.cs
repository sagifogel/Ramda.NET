using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Where
    {
        private readonly object spec = new { X = R.Equals(1), Y = R.Equals(2) };
        private readonly object test1 = new { X = 0, Y = 200 };
        private readonly object test2 = new { X = 0, Y = 10 };
        private readonly object test3 = new { X = 1, Y = 101 };
        private readonly object test4 = new { X = 1, Y = 2 };

        class Base
        {
            public dynamic X { get; set; }
        }

        class Spec : Base
        {
            public new dynamic ToString = null;

            public Spec(object obj) {
                ToString = R.Equals(new Func<string>(obj.ToString));
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
        public void Where_Returns_True_If_The_Test_Object_Satisfies_The_Spec() {
            Assert.IsFalse(R.Where(spec, test1));
            Assert.IsFalse(R.Where(spec, test2));
            Assert.IsFalse(R.Where(spec, test3));
            Assert.IsTrue(R.Where(spec, test4));
        }

        [TestMethod]
        [Description("Where_Does_Not_Need_The_Spec_And_The_Test_Object_To_Have_The_Same_Interface_(The_Test_Object_Will_Have_A_Superset_Of_The_Specs_Properties)")]
        public void Where_Does_Not_Need_The_Spec_And_The_Test_Object_To_Have_The_Same_Interface() {
            var spec = new { X = R.Equals(100) };
            var test1 = new { X = 20, Y = 100, Z = 100 };
            var test2 = new { W = 1, X = 100, Y = 100, Z = 100 };

            Assert.IsFalse(R.Where(spec, test1));
            Assert.IsTrue(R.Where(spec, test2));
        }

        [TestMethod]
        [Description("Where_Matches_Specs_That_Have_Undefined_Properties(The_Test_Object_Will_Have_A_Superset_Of_The_Specs_Properties)")]
        public void Where_Matches_Specs_That_Have_R_Null_Properties() {
            var spec = new { X = R.Equals(R.@null) };
            var test1 = new { };
            var test2 = new { X = R.@null };

            Assert.IsTrue(R.Where(spec, test1));
            Assert.IsTrue(R.Where(spec, test2));
            Assert.IsFalse(R.Where(spec, test3));
        }

        [TestMethod]
        public void Where_Is_Curried() {
            var predicate = R.Where(new { X = R.Equals(1), Y = R.Equals(2) });

            Assert.IsTrue(predicate(new { X = 1, Y = 2, Z = 3 }));
            Assert.IsFalse(predicate(new { X = 3, Y = 2, Z = 1 }));
        }

        [TestMethod]
        public void Where_Is_True_For_An_Empty_Spec() {
            Assert.IsTrue(R.Where(new { }, new { A = 1 }));
        }

        [TestMethod]
        public void Where_Matches_Inherited_Properties() {
            var obj = new { };

            Assert.IsTrue(R.Where(new Spec(obj), obj));
        }

        [TestMethod]
        public void Where_Does_Not_Match_Inherited_Spec() {
            var spec = new Spec2();

            Assert.IsTrue(R.Where(spec, new { Y = 6 }));
            Assert.IsFalse(R.Where(spec, new { X = 5 }));
        }
    }
}
