using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class Product
    {
        [TestMethod]
        public void Product_Multiplies_Together_The_Array_Of_Numbers_Supplied() {
            Assert.AreEqual(R.Product(new[] { 1, 2, 3, 4 }), 24);
        }
    }
}
