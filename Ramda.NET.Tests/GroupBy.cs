using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Dynamic;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class GroupBy
    {
        public class Tuple : IEquatable<Tuple>
        {
            public string Type { get; set; }
            public int Value { get; set; }

            public override bool Equals(object other) {
                return Equals(other as Tuple);
            }

            public bool Equals(Tuple other) {
                if (other == null) {
                    return false;
                }

                return Value == other.Value &&
                       Type.Equals(other.Type);
            }

            public override int GetHashCode() {
                return Value.GetHashCode() ^ Type.GetHashCode();
            }
        }

        public class Student : IEquatable<Student>
        {
            public int Score { get; set; }
            public string Name { get; set; }

            public override bool Equals(object other) {
                return Equals(other as Student);
            }

            public bool Equals(Student other) {
                if (other == null) {
                    return false;
                }

                return Score == other.Score &&
                       Name.Equals(other.Name);
            }

            public override int GetHashCode() {
                return Score.GetHashCode() ^ Name.GetHashCode();
            }
        }

        public class Transformer : ITransformer
        {
            public object Init() {
                return new ExpandoObject();
            }

            public object Result(object result) {
                return result;
            }

            public object Step(object result, object input) {
                return R.Merge(R.__);
            }
        }

        [TestMethod]
        public void GroupBy_Splits_The_List_Into_Groups_According_To_The_Grouping_Function() {
            Func<int, string> grade = score => (score < 65) ? "F" : (score < 70) ? "D" : (score < 80) ? "C" : (score < 90) ? "B" : "A";
            Func<Student, string> byGrade = student => grade(student.Score);

            var students = new[] {
                new Student { Name = "Jack", Score = 69 },
                new Student { Name = "Eddy", Score = 58 },
                new Student { Name = "Fred", Score = 67 },
                new Student { Name = "Abby", Score = 84 },
                new Student { Name = "Brad", Score = 73 },
                new Student { Name = "Chris", Score = 89 },
                new Student { Name = "Irene", Score = 85 },
                new Student { Name = "Dianne", Score = 99 },
                new Student { Name = "Hannah", Score = 78 },
                new Student { Name = "Gillian", Score = 91 }
            };

            var expected = new {
                A = new[] { new Student { Name = "Dianne", Score = 99 }, new Student { Name = "Gillian", Score = 91 } },
                B = new[] { new Student { Name = "Abby", Score = 84 }, new Student { Name = "Chris", Score = 89 }, new Student { Name = "Irene", Score = 85 } },
                F = new[] { new Student { Name = "Eddy", Score = 58 } },
                C = new[] { new Student { Name = "Brad", Score = 73 }, new Student { Name = "Hannah", Score = 78 } },
                D = new[] { new Student { Name = "Fred", Score = 67 }, new Student { Name = "Jack", Score = 69 } }
            };

            DynamicAssert.AreEqual(R.GroupBy(byGrade, students), expected);
        }

        [TestMethod]
        public void GroupBy_Is_Curried() {
            var splitByType = R.GroupBy(R.Prop("Type"));

            var tuples = new[] {
                new Tuple { Type = "A", Value = 10 },
                new Tuple { Type = "B", Value = 20 },
                new Tuple { Type = "A", Value = 30 },
                new Tuple { Type = "A", Value = 40 },
                new Tuple { Type = "C", Value = 50 },
                new Tuple { Type = "B", Value = 60 }
            };

            var expected = new {
                A = new[] { new Tuple { Type = "A", Value = 10 }, new Tuple { Type = "A", Value = 30 }, new Tuple { Type = "A", Value = 40 } },
                B = new[] { new Tuple { Type = "B", Value = 20 }, new Tuple { Type = "B", Value = 60 } },
                C = new[] { new Tuple { Type = "C", Value = 50 } }
            };

            DynamicAssert.AreEqual(splitByType(tuples), expected);
        }

        [TestMethod]
        public void GroupBy_Returns_An_Empty_Object_If_Given_An_Empty_Array() {
            DynamicAssert.AreEqual(R.GroupBy(R.Prop("x"), new object[0]), new ExpandoObject());
        }

        [TestMethod]
        public void GroupBy_Dispatches_On_Transformer_Objects_In_List_Position() {
            var byType = R.Prop("Type");

            Assert.IsTrue(typeof(ITransformer).IsAssignableFrom(R.GroupBy(byType, new Transformer()).GetType()));
        }
    }
}
