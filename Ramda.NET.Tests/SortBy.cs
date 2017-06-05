using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class SortBy
    {
        class Album
        {
            public string Title { get; set; }
            public string Genre { get; set; }
            public string Artist { get; set; }
        }

        private object[] Argumnets(params object[] args) => args;

        private readonly Album[] albums = new[] {
            new Album { Title = "Art of the Fugue", Artist = "Glenn Gould", Genre = "Baroque"},
            new Album { Title = "A Farewell to Kings", Artist = "Rush", Genre = "Rock"},
            new Album { Title = "Timeout", Artist = "Dave Brubeck Quartet", Genre = "Jazz"},
            new Album { Title = "Fly By Night", Artist = "Rush", Genre = "Rock"},
            new Album { Title = "Goldberg Variations", Artist = "Daniel Barenboim", Genre = "Baroque"},
            new Album { Title = "New World Symphony", Artist = "Leonard Bernstein", Genre = "Romantic"},
            new Album { Title = "Romance with the Unseen", Artist = "Don Byron", Genre = "Jazz"},
            new Album { Title = "Somewhere In Time", Artist = "Iron Maiden", Genre = "Metal"},
            new Album { Title = "In Times of Desparation", Artist = "Danny Holt", Genre = "Modern"},
            new Album { Title = "Evita", Artist = "Various", Genre = "Broadway"},
            new Album { Title = "Five Leaves Left", Artist = "Nick Drake",  Genre = "Folk"},
            new Album { Title = "The Magic Flute", Artist = "John Eliot Gardiner", Genre = "Classical"}
        };

        [TestMethod]
        public void SortBy_Sorts_By_A_Simple_Property_Of_The_Objects() {
            var sortedAlbums = R.SortBy(R.Prop("Title"), albums);

            Assert.AreEqual(sortedAlbums.Length, albums.Length);
            Assert.AreEqual(sortedAlbums[0].Title, "A Farewell to Kings");
            Assert.AreEqual(sortedAlbums[11].Title, "Timeout");
        }

        [TestMethod]
        public void SortBy_Is_Curried() {
            var sorter = R.SortBy(R.Prop("Title"));
            var sortedAlbums = sorter(albums);

            Assert.AreEqual(sortedAlbums.Length, albums.Length);
            Assert.AreEqual(sortedAlbums[0].Title, "A Farewell to Kings");
            Assert.AreEqual(sortedAlbums[11].Title, "Timeout");
        }

        [TestMethod]
        public void SortBy_Preserves_Object_Identity() {
            var a = new { Value = "A" };
            var b = new { Value = "B" };
            var result = R.SortBy(R.Prop("Value"), new[] { b, a });

            Assert.AreEqual(result[0], a);
            Assert.AreEqual(result[1], b);
        }

        [TestMethod]
        [Description("SortBy_Sorts_Array-Like_Object")]
        public void SortBy_Sorts_Array_Like_Object() {
            var args = Argumnets("C", "A", "B");
            var result = R.SortBy(R.Identity(R.__), args);

            Assert.AreEqual(result[0], "A");
            Assert.AreEqual(result[1], "B");
            Assert.AreEqual(result[2], "C");
        }
    }
}
