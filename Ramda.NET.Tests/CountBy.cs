using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Dynamic;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class CountBy
    {
        public class Album
        {
            public string Title { get; set; }
            public string Artist { get; set; }
            public string Genre { get; set; }
        }

        public Func<Album, string> DerivedGenre = DerivedGenreFactory();

        private static Func<Album, string> DerivedGenreFactory() {
            var remap = new Dictionary<string, string> {
                ["Metal"] = "Rock",
                ["Modern"] = "Classical",
                ["Baroque"] = "Classical",
                ["Romantic"] = "Classical"
            };

            return (album) => {
                var genre = R.Prop("Genre", album);

                return remap.ContainsKey(genre) ? remap[genre] : genre;
            };
        }

        private Album[] albums = new[] {
              new Album{Title ="Fly By Night", Artist = "Rush", Genre = "Rock"},
              new Album{Title ="Evita", Artist = "Various", Genre = "Broadway"},
              new Album{Title ="A Farewell to Kings", Artist = "Rush", Genre = "Rock"},
              new Album{Title ="Five Leaves Left", Artist = "Nick Drake", Genre = "Folk"},
              new Album{Title ="Timeout", Artist = "Dave Brubeck Quartet", Genre = "Jazz"},
              new Album{Title ="Somewhere In Time", Artist = "Iron Maiden", Genre = "Metal"},
              new Album{Title ="Art of the Fugue", Artist = "Glenn Gould", Genre = "Baroque"},
              new Album{Title ="Romance with the Unseen", Artist = "Don Byron", Genre = "Jazz"},
              new Album{Title ="In Times of Desparation", Artist = "Danny Holt", Genre = "Modern"},
              new Album{Title ="Goldberg Variations", Artist = "Daniel Barenboim", Genre = "Baroque"},
              new Album{Title ="New World Symphony", Artist = "Leonard Bernstein", Genre = "Romantic"},
              new Album{Title ="The Magic Flute", Artist = "John Eliot Gardiner", Genre = "Classical"}
        };

        [TestMethod]
        public void CountBy_Counts_By_A_Simple_Property_Of_The_Objects() {
            ExpandoObject countedBy = R.CountBy(R.Prop("Genre"), albums);
            var result = new {
                Baroque = 2,
                Rock = 2,
                Jazz = 2,
                Romantic = 1,
                Metal = 1,
                Modern = 1,
                Broadway = 1,
                Folk = 1,
                Classical = 1
            }.ToExpando();

            Assert.IsTrue(countedBy.ContentEquals(result));
        }

        [TestMethod]
        public void CountBy_Counts_By_A_More_Complex_Function_On_The_Objects() {
            ExpandoObject countedBy = R.CountBy(DerivedGenre, albums);
            var result = new {
                Classical = 5,
                Rock = 3,
                Jazz = 2,
                Broadway = 1,
                Folk = 1
            }.ToExpando();

            Assert.IsTrue(countedBy.ContentEquals(result));
        }

        [TestMethod]
        public void CountBy_Is_Curried() {
            var counter = R.CountBy(R.Prop("Genre"));
            var result = new {
                Baroque = 2,
                Rock = 2,
                Jazz = 2,
                Romantic = 1,
                Metal = 1,
                Modern = 1,
                Broadway = 1,
                Folk = 1,
                Classical = 1
            }.ToExpando();

            Assert.IsTrue(((ExpandoObject)counter(albums)).ContentEquals(result));
        }

        [TestMethod]
        public void CountBy_Ignores_Inherited_Properties() {
            dynamic result = R.CountBy(R.Identity(R.__), new[] { "Abc", "ToString" });

            Assert.AreEqual(result.Abc, 1);
            Assert.AreEqual(result.ToString, 1);
        }

        [TestMethod]
        public void CountBy_Can_Act_As_A_Transducer() {
            var transducer = R.Compose(R.CountBy(R.Prop("Genre")), R.Map(R.Adjust(R.ToString(R.__), 1)));
            ExpandoObject into = R.Into<Album>(new ExpandoObject(), transducer, albums);

            Assert.IsTrue(into.ContentEquals(new {
                Baroque = "2",
                Rock = "2",
                Jazz = "2",
                Romantic = "1",
                Metal = "1",
                Modern = "1",
                Broadway = "1",
                Folk = "1",
                Classical = "1"
            }.ToExpando()));
        }
    }
}
