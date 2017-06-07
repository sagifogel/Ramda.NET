using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class SymmetricDifferenceWith
    {
        private readonly Func<dynamic, dynamic, bool> eqA = (r, s) => r.A == s.A;
        private readonly Func<dynamic, dynamic, bool> identical = (a, b) => a == b;
        private readonly object[] Ro = new[] { new { A = 1 }, new { A = 2 }, new { A = 3 }, new { A = 4 } };
        private readonly object[] So = new[] { new { A = 3 }, new { A = 4 }, new { A = 5 }, new { A = 6 } };
        private readonly object[] Ro2 = new[] { new { A = 1 }, new { A = 2 }, new { A = 3 }, new { A = 4 }, new { A = 1 }, new { A = 2 }, new { A = 3 }, new { A = 4 } };
        private readonly object[] So2 = new[] { new { A = 3 }, new { A = 4 }, new { A = 5 }, new { A = 6 }, new { A = 3 }, new { A = 4 }, new { A = 5 }, new { A = 6 } };

        [TestMethod]
        public void SymmetricDifferenceWith_Combines_Two_Lists_Into_The_Set_Of_All_Elements_Unique_To_Either_List_Based_On_The_Passed() {
            DynamicAssert.AreEqual(R.SymmetricDifferenceWith(eqA, Ro, So), new[] { new { A = 1 }, new { A = 2 }, new { A = 5 }, new { A = 6 } });
        }

        [TestMethod]
        public void SymmetricDifferenceWith_Does_Not_Allow_Duplicates_In_The_Output_Even_If_The_Input_Lists_Had_Duplicates() {
            DynamicAssert.AreEqual(R.SymmetricDifferenceWith(eqA, Ro2, So2), new[] { new { A = 1 }, new { A = 2 }, new { A = 5 }, new { A = 6 } });
        }

        [TestMethod]
        [Description("SymmetricDifferenceWith_Does_Not_Return_A_\"Sparse\"_Array")]
        public void SymmetricDifferenceWith_Does_Not_Return_A_Sparse_Array() {
            Assert.AreEqual(R.SymmetricDifferenceWith(identical, new[] { 1, 3, 2, 1, 3, 1, 2, 3 }, new[] { 3 }).Length, 2);
        }
    }
}
