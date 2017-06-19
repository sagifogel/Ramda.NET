using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Unfold
    {
        [TestMethod]
        public void Unfold_Unfolds_Simple_Functions_With_A_Starting_Point_To_Create_A_List() {
            CollectionAssert.AreEqual(R.Unfold(n => {
                if (n > 0) {
                    return new[] { n, n - 1 };
                }

                return null;
            }, 10), new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 });
        }

        [TestMethod]
        [Description("Unfold_Is_Cool!")]
        public void Unfold_Is_Cool() {
            Func<int, dynamic> fib = n => {
                var count = 0;

                return new Func<int[], object[]>(pair => {
                    count += 1;

                    if (count <= n) {
                        return new object[] { pair[0], new[] { pair[1], pair[0] + pair[1] } };
                    }

                    return null;
                });
            };

            var res = R.Unfold(fib(10), new[] { 0, 1 });
            CollectionAssert.AreEqual(R.Unfold(fib(10), new[] { 0, 1 }), new[] { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34 });
        }
    }
}
