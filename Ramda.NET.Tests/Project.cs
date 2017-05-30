using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Project
    {
        private readonly object[] kids = new object[] {
            new { Name = "Abby", Age = 7, Hair = "blond"},
            new { Name = "Fred", Age = 12, Hair = "brown"},
            new { Name = "Rusty", Age = 10, Hair = "brown"},
            new { Name = "Alois", Age = 15, Disposition = "surly"}
        };

        [TestMethod]
        public void Product_Selects_The_Chosen_Properties_From_Each_Element_In_A_List() {
            NestedCollectionAssert.AreEqual(R.Project(new[] { "Name", "Age" }, kids), new[] {
                new { Name = "Abby", Age = 7 },
                new { Name = "Fred", Age = 12 },
                new { Name = "Rusty", Age = 10 },
                new { Name = "Alois", Age = 15 }
            });
        }

        [TestMethod]
        [Description("Product_Has_An_Undefined_Property_On_The_Output_Tuple_For_Any_Input_Tuple_That_Does_Not_Have_The_Property")]
        public void Product_Has_An_Undefined_Property_On_The_Output_Tuple_For_Any_Input_Tuple_That_Does_Not_Have_The_Property() {
            NestedCollectionAssert.AreEqual(R.Project(new[] { "Name", "Hair" }, kids), new object[] {
                new { Name = "Abby", Hair = "blond" },
                new { Name = "Fred", Hair = "brown" },
                new { Name = "Rusty", Hair = "brown" },
                new { Name = "Alois", Hair = R.@null }
            });
        }

        [TestMethod]
        public void Product_Is_Curried() {
            var myFields = R.Project(new[] { "Name", "Age" });

            NestedCollectionAssert.AreEqual(myFields(kids), new[] {
                new { Name = "Abby", Age = 7 },
                new { Name = "Fred", Age = 12 },
                new { Name = "Rusty", Age = 10 },
                new { Name = "Alois", Age = 15 }
            });
        }
    }
}
