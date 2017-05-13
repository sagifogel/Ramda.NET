using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    public class BaseMerge
    {   
        public class Base
        {
            public int X { get; set; }

            public int Bar { get; protected set; }
        }

        public class Foo : Base
        {

            public Foo() {
                Bar = 42;
            }
        }

        public class Cla : Base
        {
        }
    }
}
