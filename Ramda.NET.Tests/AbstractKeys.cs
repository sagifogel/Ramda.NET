using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class AbstrcatKeys
    {
        protected class Base
        {
            public string X => "X";
            public string Y => "Y";
        }

        protected class C : Base
        {
            public int A { get; set; }
            public int B { get; set; }

            public C() {
                A = 100;
                B = 200;
            }
        }

        protected readonly C cobj = new C();
        protected readonly object obj = new { A = 100, B = new[] { 1, 2, 3 }, C = new { X = 200, Y = 300 }, D = "D", E = R.@null, F = (object)null };
    }
}
