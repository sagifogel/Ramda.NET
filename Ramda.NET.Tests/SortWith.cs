using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class SortWith : AbstractSort
    {
        protected readonly Album[] albums = new[] {
            new Album { Title = "A Farewell to Kings", Artist = "Rush", Genre = "Rock", Score = 3 },
            new Album { Title = "Timeout", Artist = "Dave Brubeck Quartet", Genre = "Jazz", Score = 3 },
            new Album { Title = "Fly By Night", Artist = "Rush", Genre = "Rock", Score = 5 },
            new Album { Title = "Goldberg Variations", Artist = "Daniel Barenboim", Genre = "Baroque", Score = 3 },
            new Album { Title = "Art of the Fugue", Artist = "Glenn Gould", Genre = "Baroque", Score = 3 },
            new Album { Title = "New World Symphony", Artist = "Leonard Bernstein", Genre = "Romantic", Score = 4 },
            new Album { Title = "Romance with the Unseen", Artist = "Don Byron", Genre = "Jazz", Score = 5 },
            new Album { Title = "Somewhere In Time", Artist = "Iron Maiden", Genre = "Metal", Score = 2 },
            new Album { Title = "In Times of Desparation", Artist = "Danny Holt", Genre = "Modern", Score = 1 },
            new Album { Title = "Evita", Artist = "Various", Genre = "Broadway", Score = 3 },
            new Album { Title = "Five Leaves Left", Artist = "Nick Drake",  Genre = "Folk", Score = 1 },
            new Album { Title = "The Magic Flute", Artist = "John Eliot Gardiner", Genre = "Classical", Score = 4 }
        };

        [TestMethod]
        public void SortWith_Sorts_By_A_Simple_Property_Of_The_Objects() {
            var sortedAlbums = R.SortWith(new[] {
                R.Ascend(R.Prop("Title"))
            }, albums);

            Assert.AreEqual(sortedAlbums.Length, albums.Length);
            Assert.AreEqual(sortedAlbums[0].Title, "A Farewell to Kings");
            Assert.AreEqual(sortedAlbums[11].Title, "Timeout");
        }

        [TestMethod]
        public void SortWith_Sorts_By_Multiple_Properties_Of_The_Objects() {
            var sortedAlbums = R.SortWith(new[] {
                R.Ascend(R.Prop("Score")),
                R.Ascend(R.Prop("Title"))
            }, albums);

            Assert.AreEqual(sortedAlbums.Length, albums.Length);
            Assert.AreEqual(sortedAlbums[0].Title, "Five Leaves Left");
            Assert.AreEqual(sortedAlbums[1].Title, "In Times of Desparation");
            Assert.AreEqual(sortedAlbums[11].Title, "Romance with the Unseen");
        }

        [TestMethod]
        public void SortWith_Sorts_By_3_Properties_Of_The_Objects() {
            var sortedAlbums = R.SortWith(new[] {
                R.Ascend(R.Prop("Genre")),
                R.Ascend(R.Prop("Score")),
                R.Ascend(R.Prop("Title"))
            }, albums);

            Assert.AreEqual(sortedAlbums.Length, albums.Length);
            Assert.AreEqual(sortedAlbums[0].Title, "Art of the Fugue");
            Assert.AreEqual(sortedAlbums[1].Title, "Goldberg Variations");
            Assert.AreEqual(sortedAlbums[11].Title, "New World Symphony");
        }

        [TestMethod]
        public void SortWith_Sorts_By_Multiple_Properties_Using_Ascend_And_Descend() {
            var sortedAlbums = R.SortWith(new[] {
                R.Descend(R.Prop("Score")),
                R.Ascend(R.Prop("Title"))
            }, albums);

            Assert.AreEqual(sortedAlbums.Length, albums.Length);
            Assert.AreEqual(sortedAlbums[0].Title, "Fly By Night");
            Assert.AreEqual(sortedAlbums[1].Title, "Romance with the Unseen");
            Assert.AreEqual(sortedAlbums[11].Title, "In Times of Desparation");
        }

        [TestMethod]
        public void SortWith_Is_Curried() {
            var sorter = R.SortWith<Album>(new[] { R.Ascend(R.Prop("Title")) });
            var sortedAlbums = sorter(albums);

            Assert.AreEqual(sortedAlbums.Length, albums.Length);
            Assert.AreEqual(sortedAlbums[0].Title, "A Farewell to Kings");
            Assert.AreEqual(sortedAlbums[11].Title, "Timeout");
        }

        [TestMethod]
        public void SortWith_Preserves_Object_Identity() {
            var a = new { Value = "A" };
            var b = new { Value = "B" };
            var result = R.SortWith(new[] { R.Ascend(R.Prop("Value")) }, new[] { b, a });

            Assert.AreEqual(result[0], a);
            Assert.AreEqual(result[1], b);
        }

        [TestMethod]
        [Description("SortWith_Sorts_Array-Like_Object")]
        public void SortWith_Sorts_Array_Like_Object() {
            var args = Argumnets("C", "A", "B");
            var result = R.SortWith(new[] { R.Ascend(R.Identity(R.__)) }, args);

            Assert.AreEqual(result[0], "A");
            Assert.AreEqual(result[1], "B");
            Assert.AreEqual(result[2], "C");
        }
    }
}
