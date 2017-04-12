using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Dynamic;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Evolve
    {
        [TestMethod]
        [Description("Evolve_Creates_A_New_Object_By_Evolving_The_\"Object\"_According_To_The_\"Transformation\"_Functions")]
        public void Evolve_Creates_A_New_Object_By_Evolving_The_Object_According_To_The_Transformation_Functions() {
            var transf = new { Elapsed = R.Add(1), Remaining = R.Add(-1) };
            var obj = new { Name = "Tomato", Elapsed = 100, Remaining = 1400 };
            var expected = new { Name = "Tomato", Elapsed = 101, Remaining = 1399 };

            DynamicAssert.AreEqual(expected, R.Evolve(transf, obj));
        }

        [TestMethod]
        public void Evolve_Does_Not_Invoke_Function_If_Object_Does_Not_Contain_The_Key() {
            var transf = new { N = R.Add(1), M = R.Add(1) };
            var obj = new { M = 3 };
            var expected = new { M = 4 };

            DynamicAssert.AreEqual(R.Evolve(transf, obj), expected);
        }

        [TestMethod]
        public void Evolve_Is_Not_Destructive() {
            var transf = new { Elapsed = R.Add(1), Remaining = R.Add(-1) };
            var obj = new { Name = "Tomato", Elapsed = 100, Remaining = 1400 };
            var expected = new { Name = "Tomato", Elapsed = 100, Remaining = 1400 };

            R.Evolve(transf, obj);

            DynamicAssert.AreEqual(obj, expected);
        }

        [TestMethod]
        public void Evolve_Is_Recursive() {
            var transf = new { Nested = new { Second = R.Add(-1), Third = R.Add(1) } };
            var obj = new { First = 1, Nested = new { Second = 2, Third = 3 } };
            var expected = new { First = 1, Nested = new { Second = 1, Third = 4 } };

            DynamicAssert.AreEqual(R.Evolve(transf, obj), expected);
        }

        [TestMethod]
        public void Evolve_Is_Curried() {
            var tick = R.Evolve(new { Elapsed = R.Add(1), Remaining = R.Add(-1) });
            var obj = new { Name = "Tomato", Elapsed = 100, Remaining = 1400 };
            var expected = new { Name = "Tomato", Elapsed = 101, Remaining = 1399 };

            DynamicAssert.AreEqual(tick(obj), expected);
        }

        [TestMethod]
        public void Evolve_Ignores_Primitive_Value_Transformations() {
            var transf = new { N = 2, M = "foo" };
            var obj = new { N = 0, M = 1 };
            var expected = new { N = 0, M = 1 };

            DynamicAssert.AreEqual(R.Evolve(transf, obj), expected);
        }

        [TestMethod]
        public void Evolve_Ignores_Null_Transformations() {
            var transf = new { N = (object)null };
            var obj = new { N = 0 };
            var expected = new { N = 0 };

            DynamicAssert.AreEqual(R.Evolve(transf, obj), expected);
        }
    }
}
