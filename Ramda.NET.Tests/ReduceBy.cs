using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Dynamic;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class ReduceBy
    {
        class Test
        {
            public int Val { get; set; }
            public string Type { get; set; }
        }

        class Student
        {
            public int Score { get; set; }
            public string Name { get; set; }
        }

        private readonly dynamic byType = R.Prop("Type");
        private readonly Func<int, Test, int> sumValues = (acc, obj) => acc + obj.Val;
        private readonly Test[] sumInput = new[] {
            new Test { Type = "A", Val = 10 },
            new Test { Type = "B", Val = 20 },
            new Test { Type = "A", Val = 30 },
            new Test { Type = "A", Val = 40 },
            new Test { Type = "C", Val = 50 },
            new Test { Type = "B", Val = 60 }
        };

        [TestMethod]
        public void ReduceBy_Splits_The_List_Into_Groups_According_To_The_Grouping_Function() {
            Func<int, string> grade = score => {
                return (score < 65) ? "F" : (score < 70) ? "D" : (score < 80) ? "C" : (score < 90) ? "B" : "A";
            };

            Func<Student, string> byGrade = student => grade(student.Score);
            Func<string[], Student, string[]> collectNames = (acc, student) => acc.Concat<string>(new[] { student.Name }).ToArray();

            var students = new Student[] {
                new Student { Name = "Abby", Score = 84 },
                new Student { Name = "Brad", Score = 73 },
                new Student { Name = "Chris", Score = 89 },
                new Student { Name = "Dianne", Score = 99 },
                new Student { Name = "Eddy", Score = 58 },
                new Student { Name = "Fred", Score = 67 },
                new Student { Name = "Gillian", Score = 91 },
                new Student { Name = "Hannah", Score = 78 },
                new Student { Name = "Irene", Score = 85 },
                new Student { Name = "Jack", Score = 69 }
            };

            DynamicAssert.AreEqual(R.ReduceBy(collectNames, new string[0], byGrade, students), new {
                A = new[] { "Dianne", "Gillian" },
                B = new[] { "Abby", "Chris", "Irene" },
                C = new[] { "Brad", "Hannah" },
                D = new[] { "Fred", "Jack" },
                F = new[] { "Eddy" }
            });
        }

        [TestMethod]
        public void ReduceBy_Returns_An_Empty_Object_If_Given_An_Empty_Array() {
            DynamicAssert.AreEqual(R.ReduceBy(sumValues, 0, byType, new Test[0]), new ExpandoObject());
        }

        [TestMethod]
        public void ReduceBy_Is_Curried() {
            var reduceToSumsBy = R.ReduceBy(sumValues, 0);
            var sumByType = reduceToSumsBy(byType);

            DynamicAssert.AreEqual(sumByType(sumInput), new { A = 80, B = 80, C = 50 });
        }

        [TestMethod]
        public void ReduceBy_Correctly_Reports_The_Arity_Of_Curried_Versions() {
            var inc = R.ReduceBy(sumValues, 0)(byType);

            Assert.AreEqual(inc.Length, 1);
        }

        [TestMethod]
        public void ReduceBy_Can_Act_As_A_Transducer() {
            var reduceToSumsBy = R.ReduceBy(sumValues, 0);
            var sumByType = reduceToSumsBy(byType);

            DynamicAssert.AreEqual(R.Into(new { }, R.Compose(sumByType, R.Map(R.Adjust(R.Multiply(10), 1))), sumInput), new { A = 800, B = 800, C = 500 });
        }
    }
}
