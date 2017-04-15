using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Dynamic;
using System.Collections.Generic;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class ForEach
    {
        private readonly object[] list = new object[] { new { X = 1, Y = 2 }, new { X = 100, Y = 200 }, new { X = 300, Y = 400 }, new { X = 234, Y = 345 } };

        [TestMethod]
        public void ForEach_Performs_The_Passed_In_Function_On_Each_Element_Of_The_List() {
            var sideEffect = new Dictionary<int, int>();
            var expected = new Dictionary<int, int> {
                [1] = 2,
                [100] = 200,
                [300] = 400,
                [234] = 345
            };

            R.ForEach<dynamic>(elem => sideEffect[elem.X] = elem.Y, list);

            DynamicAssert.AreEqual(sideEffect, expected);
        }
    }
}
