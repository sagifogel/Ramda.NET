using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    public class AbstractHas
    {
        protected class Person
        {
            public int Age { get; set; }
            public string Name { get; set; }
        }

        protected class Bob : Person
        {
        }

        protected readonly object anon = new { Age = 99 };
        protected readonly object fred = new { Name = "Fred", Age = 23 };
    }
}
