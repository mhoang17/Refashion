using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refashion;

namespace RefashionTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Seller seller = new Seller("Tom", "mail", "addresse", "city", 1234, "123456789");

            Assert.AreEqual("addresse", seller.Address);
        }
    }
}
