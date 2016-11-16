using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Aperture
    {
        private int[] sevenLs = new[] { 1, 2, 3, 4, 5, 6, 7 };

        [TestMethod]
        public void Aperture_Creates_A_List_Of_N_tuples_From_A_List() {
            NestedCollectionAssert.AreEqual((Array)R.Aperture(1, sevenLs), new[] { new[] { 1 }, new[] { 2 }, new[] { 3 }, new[] { 4 }, new[] { 5 }, new[] { 6 }, new[] { 7 } });
            NestedCollectionAssert.AreEqual((Array)R.Aperture(2, sevenLs), new[] { new[] { 1, 2 }, new[] { 2, 3 }, new[] { 3, 4 }, new[] { 4, 5 }, new[] { 5, 6 }, new[] { 6, 7 } });
            NestedCollectionAssert.AreEqual((Array)R.Aperture(3, sevenLs), new[] { new[] { 1, 2, 3 }, new[] { 2, 3, 4 }, new[] { 3, 4, 5 }, new[] { 4, 5, 6 }, new[] { 5, 6, 7 } });
            NestedCollectionAssert.AreEqual((Array)R.Aperture(4, new[] { 1, 2, 3, 4 }), new[] { new[] { 1, 2, 3, 4 } });
        }

        [TestMethod]
        [Description("Aperture_Returns_An_Empty_List_When_`n`_>_`list.length")]
        public void Aperture_Returns_An_Empty_List_When_N_Greater_Then_List_Length() {
            NestedCollectionAssert.AreEqual((Array)R.Aperture(1, sevenLs), new[] { new[] { 1 }, new[] { 2 }, new[] { 3 }, new[] { 4 }, new[] { 5 }, new[] { 6 }, new[] { 7 } });
            NestedCollectionAssert.AreEqual((Array)R.Aperture(2, sevenLs), new[] { new[] { 1, 2 }, new[] { 2, 3 }, new[] { 3, 4 }, new[] { 4, 5 }, new[] { 5, 6 }, new[] { 6, 7 } });
            NestedCollectionAssert.AreEqual((Array)R.Aperture(3, sevenLs), new[] { new[] { 1, 2, 3 }, new[] { 2, 3, 4 }, new[] { 3, 4, 5 }, new[] { 4, 5, 6 }, new[] { 5, 6, 7 } });
            NestedCollectionAssert.AreEqual((Array)R.Aperture(4, new[] { 1, 2, 3, 4 }), new[] { new[] { 1, 2, 3, 4 } });
        }


        [TestMethod]
        public void Aperture_Is_Curried() {
            var pairwise = R.Aperture(2);
            var res = (Array)pairwise(sevenLs);
            var expected = new[] { new[] { 1, 2 }, new[] { 2, 3 }, new[] { 3, 4 }, new[] { 4, 5 }, new[] { 5, 6 }, new[] { 6, 7 } };

            NestedCollectionAssert.AreEqual(res, expected);
        }

        [TestMethod]
        public void Aperture_Can_Act_As_A_Transducer() {
            var expected = new[] { new[] { 1, 2 }, new[] { 2, 3 }, new[] { 3, 4 }, new[] { 4, 5 }, new[] { 5, 6 }, new[] { 6, 7 } };
            var res = (Array)R.Into(new object[0], R.Aperture(2), sevenLs);

            NestedCollectionAssert.AreEqual(res, expected);
        }
    }
}
