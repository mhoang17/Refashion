using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using Refashion.Database;

namespace RefashionTest.DatabaseTests
{
    [TestClass]
    public class DatabaseConnectionTests
    {
        [TestMethod]
        public void GetConnection_Returns_MySqlConnection()
        {
            DatabaseConnection connection = new DatabaseConnection();

            Assert.IsInstanceOfType(connection.GetConnection(), typeof(MySqlConnection));
        }
    }
}
