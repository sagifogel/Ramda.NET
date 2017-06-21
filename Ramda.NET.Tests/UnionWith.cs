using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class UnionWith
    {
        private readonly Func<dynamic, dynamic, bool> eqA = (dynamic r, dynamic s) => r.A == s.A;
        private readonly object[] Ro = new[] { new { A = 1 }, new { A = 2 }, new { A = 3 }, new { A = 4 } };
        private readonly object[] So = new[] { new { A = 3 }, new { A = 4 }, new { A = 5 }, new { A = 6 } };

        [TestMethod]
        [Description("UnionWith_Combines_Two_Lists_Into_The_Set_Of_All_Their_Elements_Based_On_The_Passed-In_Equality_Predicate")]
        public void UnionWith_Combines_Two_Lists_Into_The_Set_Of_All_Their_Elements_Based_On_The_Passed_In_Equality_Predicate() {
            CollectionAssert.AreEqual(R.UnionWith(eqA, Ro, So), new[] { new { A = 1 }, new { A = 2 }, new { A = 3 }, new { A = 4 }, new { A = 5 }, new { A = 6 } });
        }
    }
}
