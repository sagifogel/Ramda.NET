using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Lenses
    {
        class Person
        {
            public string Name { get; set; }
            public string[] Address { get; set; }
            public Dictionary<string, string> Pets { get; set; }
        }

        private readonly Person Alice = new Person() {
            Name = "Alice Jones",
            Address = new[] { "22 Walnut St", "San Francisco", "CA" },
            Pets = new Dictionary<string, string> {
                ["Dog"] = "Joker",
                ["Cat"] = "Batman"
            }
        };

        private readonly dynamic HeadLens = R.LensIndex(0);
        private readonly dynamic AddressLens = R.LensProp("Address");
        private readonly dynamic DogLens = R.LensPath(new[] { "Pets", "Dog" });
        private readonly dynamic NameLens = R.Lens(R.Prop("Name"), R.Assoc("Name"));

        [TestMethod]
        [Description("Lenses_view,_Over,_And_Set_May_Be_Applied_To_A_Lens_Created_By_\"LensPath\"")]
        public void Lenses_view_Over_And_Set_May_Be_Applied_To_A_Lens_Created_By_LensPath() {
            var res = R.View(DogLens, Alice);

            Assert.AreEqual(res, "Joker");
        }

        [TestMethod]
        [Description("Lenses_view,_Over,_And_Set_May_Be_Applied_To_A_Lens_Created_By_\"LensProp\"")]
        public void Lenses_view_Over_And_Set_May_Be_Applied_To_A_Lens_Created_By_LensProp() {
            Assert.AreEqual(R.View(NameLens, Alice), "Alice Jones");
            DynamicAssert.AreEqual(R.Over(NameLens, R.ToUpper(R.__), Alice), new Person() {
                Name = "ALICE JONES",
                Address = new[] { "22 Walnut St", "San Francisco", "CA" },
                Pets = new Dictionary<string, string> {
                    ["Dog"] = "Joker",
                    ["Cat"] = "Batman"
                }
            });

            DynamicAssert.AreEqual(R.Set(NameLens, "Alice Smith", Alice), new Person() {
                Name = "Alice Smith",
                Address = new[] { "22 Walnut St", "San Francisco", "CA" },
                Pets = new Dictionary<string, string> {
                    ["Dog"] = "Joker",
                    ["Cat"] = "Batman"
                }
            });
        }

        [TestMethod]
        [Description("Lenses_view,_Over,_And_Set_May_Be_Applied_To_A_Lens_Created_By_\"LensIndex\"")]
        public void Lenses_view_Over_And_Set_May_Be_Applied_To_A_Lens_Created_By_LensIndex() {
            Assert.AreEqual(R.View(HeadLens, Alice.Address), "22 Walnut St");
            CollectionAssert.AreEqual(R.Over(HeadLens, R.ToUpper(R.__), Alice.Address), new[] { "22 WALNUT ST", "San Francisco", "CA" });
            CollectionAssert.AreEqual(R.Set(HeadLens, "52 Crane Ave", Alice.Address), new[] { "52 Crane Ave", "San Francisco", "CA" });
        }

        [TestMethod]
        [Description("Lenses_view,_Over,_And_Set_May_Be_Applied_To_Composed_Lenses")]
        public void Lenses_view_Over_And_Set_May_Be_Applied_To_Composed_Lenses() {
            var streetLens = R.Compose(AddressLens, HeadLens);
            var dogLens = R.Compose(R.LensPath(new[] { "Pets" }), R.LensPath(new[] { "Dog" }));
            
            Assert.AreEqual(R.View(dogLens, Alice), R.View(R.LensPath(new[] { "Pets", "Dog" }), Alice));
            Assert.AreEqual(R.View(streetLens, Alice), "22 Walnut St");
            DynamicAssert.AreEqual(R.Over(streetLens, R.ToUpper(R.__), Alice), new Person() {
                Name = "Alice Jones",
                Address = new[] { "22 WALNUT ST", "San Francisco", "CA" },
                Pets = new Dictionary<string, string> {
                    ["Dog"] = "Joker",
                    ["Cat"] = "Batman"
                }
            });

            DynamicAssert.AreEqual(R.Set(streetLens, "52 Crane Ave", Alice), new Person() {
                Name = "Alice Jones",
                Address = new[] { "52 Crane Ave", "San Francisco", "CA" },
                Pets = new Dictionary<string, string> {
                    ["Dog"] = "Joker",
                    ["Cat"] = "Batman"
                }
            });
        }
    }
}
