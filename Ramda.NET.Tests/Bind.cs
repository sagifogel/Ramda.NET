using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Bind
    {
        public class Foo
        {
            public virtual dynamic X { get; protected set; }

            public Foo(dynamic x) {
                if (x != null) {
                    X = x;
                }
            }

            public dynamic Add(dynamic x) => X + x;

            public bool IsFoo() => GetType().Equals(typeof(Bar));
        }

        public class Bar : Foo
        {
            public dynamic Y { get; private set; }
            public override dynamic X { get; protected set; } = "bx";

            public Bar(dynamic x, dynamic y) : base((object)x) {
                Y = y;
            }
        }

        [TestMethod]
        public void Bind_Returns_A_Function() {
            var foo = new Foo(10);
            Assert.IsInstanceOfType(R.Bind((Func<dynamic, dynamic>)foo.Add, foo), typeof(DynamicDelegate));
        }

        [TestMethod]
        public void Bind_Returns_A_Function_Bound_To_The_Specified_Context_Object() {
            var f = new Foo(12);
            var isFooBound = R.Bind((Func<bool>)f.IsFoo, new Bar(10, 12));

            Assert.IsFalse(f.IsFoo());
            Assert.IsTrue(isFooBound());
        }

        [TestMethod]
        [Description("Bind_Works_With_User-Defined_Types")]
        public void Bind_Works_With_User_Defined_Types() {
            var f = new Foo(12);
            var getGetMethod = f.GetType().GetProperty("X").GetGetMethod();
            var @delegate = getGetMethod.CreateDelegate(new Foo(10));

            var getXFooBound = R.Bind(@delegate, f);
            Assert.AreEqual(getXFooBound(), 12);
        }

        [TestMethod]
        public void Bind_Does_Not_Interfere_With_Existing_Object_Methods() {
            var b = new Bar(null, "b");
            var getGetMethod = typeof(Foo).GetProperty("X").GetGetMethod();
            var @delegate = getGetMethod.CreateDelegate(b);
            var getXBarBound = R.Bind(@delegate, new Bar("a", "b"));

            Assert.AreEqual(b.X, "bx");
            Assert.AreEqual(getXBarBound(), "a");
        }

        [TestMethod]
        public void Bind_Is_Curried() {
            var f = new Foo(1);
            Func<dynamic, dynamic> add = new Foo(10).Add;

            Assert.AreEqual(R.Bind(add)(f)(10), 11);
        }
    }
}
