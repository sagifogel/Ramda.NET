using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public abstract class AbstractSort
    {
        protected class Album
        {
            public int Score { get; set; }
            public string Title { get; set; }
            public string Genre { get; set; }
            public string Artist { get; set; }
        }

        protected object[] Argumnets(params object[] args) => args;
    }
}
