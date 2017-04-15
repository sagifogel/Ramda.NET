using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class DifferenceWith
    {
        public class Container : IEquatable<Container>
        {
            public int A { get; set; }

            public override bool Equals(object obj) {
                return Equals(obj as Container);
            }

            public bool Equals(Container other) {
                if (other == null) {
                    return false;
                }

                return other.A == A;
            }

            public override int GetHashCode() {
                return A.GetHashCode();
            }
        }

        Func<int, int, bool> identical = (a, b) => a == b;
        Func<Container, Container, bool> eqA = (r, s) => r.A == s.A;
        Container[] Ro = new[] { new Container { A = 1 }, new Container { A = 2 }, new Container { A = 3 }, new Container { A = 4 } };
        Container[] So = new[] { new Container { A = 3 }, new Container { A = 4 }, new Container { A = 5 }, new Container { A = 6 } };
        Container[] Ro2 = new[] { new Container { A = 1 }, new Container { A = 2 }, new Container { A = 3 }, new Container { A = 4 }, new Container { A = 1 }, new Container { A = 2 }, new Container { A = 3 }, new Container { A = 4 } };
        Container[] So2 = new[] { new Container { A = 3 }, new Container { A = 4 }, new Container { A = 5 }, new Container { A = 6 }, new Container { A = 3 }, new Container { A = 4 }, new Container { A = 5 }, new Container { A = 6 } };

        [TestMethod]
        [Description("DifferenceWith_Combines_Two_Lists_Into_The_Set_Of_All_Their_Elements_Based_On_The_Passed-In_Equality_Predicate")]
        public void DifferenceWith_Combines_Two_Lists_Into_The_Set_Of_All_Their_Elements_Based_On_The_Passed_In_Equality_Predicate() {
            CollectionAssert.AreEqual(R.DifferenceWith(eqA, Ro, So), new[] { new Container { A = 1 }, new Container { A = 2 } });
        }

        [TestMethod]
        public void DifferenceWith_Does_Not_Allow_Duplicates_In_The_Output_Even_If_The_Input_Lists_Had_Duplicates() {
            CollectionAssert.AreEqual(R.DifferenceWith(eqA, Ro2, So2), new[] { new Container { A = 1 }, new Container { A = 2 } });
        }

        [TestMethod]
        [Description("DifferenceWith_Does_Not_Return_A_\"Sparse\"_Array")]
        public void DifferenceWith_Does_Not_Return_A_Sparse_Array() {
            Assert.AreEqual(R.DifferenceWith(identical, new[] { 1, 3, 2, 1, 3, 1, 2, 3 }, new[] { 3 }).Length, 2);
        }
    }
}
