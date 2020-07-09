using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using Refashion;
using Refashion.Database;

namespace RefashionTest.DatabaseTests.IntegrationTests
{
    // These tests should NEVER be run on a production database
    // TODO: Move different test project?
    [TestClass]
    public class SellerDMLIntegrationTests
    {
        [TestInitialize]
        public void ClearDatabase()
        {
            DatabaseConnection db = new DatabaseConnection();
            MySqlConnection con = db.GetConnection();

            string query = "TRUNCATE TABLE sellers";

            con.Open();

            MySqlCommand command = new MySqlCommand(query, con);

            command.ExecuteNonQuery();

            con.Close();
        }

        // Insert_Single Tests
        [TestMethod, TestCategory("IntegrationTest")]
        public void Insert_Single_Runs_Without_Errors()
        {
            SellerDML sellerdml = new SellerDML();
            Seller seller = new Seller("TestSeller", "Test", "Test", "Test", 0, "Test");

            try
            {
                sellerdml.Insert_Single(seller);
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Assert.Fail();
            }
        }


        // Select_Single Tests
        [TestMethod, TestCategory("IntegrationTest")]
        public void Select_Single_With_No_Conditions_Throws_ArgumentException()
        {
            SellerDML sellerdml = new SellerDML();
            ArgumentException expected = null;

            try
            {
                sellerdml.Select_Single("");
            }
            catch (ArgumentException e)
            {
                expected = e;
            }

            Assert.IsNotNull(expected);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void Select_Single_Returns_Single_Element_Matching_Name()
        {
            SellerDML sellerdml = new SellerDML();
            InsertTestData();
            Seller expected = new Seller("TestSeller1", "TestMail1", "Test", "Test", 0123, "Test");

            Seller actual = sellerdml.Select_Single("name:TestSeller1");

            Assert.AreEqual(expected, actual);
        }

        // Select_Multiple Tests
        [TestMethod, TestCategory("IntegrationTest")]
        public void Select_Multiple_With_No_Conditions_Throws_ArgumentException()
        {
            SellerDML sellerdml = new SellerDML();
            ArgumentException expected = null;

            try
            {
                sellerdml.Select_Multiple("");
            }
            catch (ArgumentException e)
            {
                expected = e;
            }

            Assert.IsNotNull(expected);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void Select_Multiple_Returns_Multiple_Elements_Matching_Name()
        {
            ClearDatabase();
            SellerDML sellerdml = new SellerDML();
            InsertTestData();
            List<Seller> expected = new List<Seller> {
                new Seller("TestSeller1", "TestMail1", "Test", "Test", 0123, "Test"),
                new Seller("TestSeller2", "TestMail2", "Test", "Test", 0123, "Test"),
                new Seller("TestSeller3", "TestMail3", "Test", "Test", 0123, "Test")
            };

            List<Seller> actual = sellerdml.Select_Multiple("name:TestSeller");

            // Do not know another way to compare list items
            for (int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        private void InsertTestData()
        {
            DatabaseConnection db = new DatabaseConnection();
            MySqlConnection con = db.GetConnection();

            string query = "INSERT INTO sellers(name, email, address, postnumber, city, phonenumber) VALUES " +  
                                         "('TestSeller1', 'TestMail1', 'Test', '0123', 'Test', 'Test')," +
                                         "('TestSeller2', 'TestMail2', 'Test', '0123', 'Test', 'Test')," +
                                         "('TestSeller3', 'TestMail3', 'Test', '0123', 'Test', 'Test')";

            con.Open();

            MySqlCommand command = new MySqlCommand(query, con);

            command.ExecuteNonQuery();

            con.Close();
        }
    }
}
