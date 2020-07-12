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
        public void RemoveForeignKey()
        {
            DatabaseConnection db = new DatabaseConnection();
            MySqlConnection con = db.GetConnection();

            con.Open();
            try
            {
                MySqlTransaction transaction = con.BeginTransaction();

                // Drop foreign key, because they make things complicated
                string dropConstraint = "ALTER TABLE bags DROP FOREIGN KEY seller";
                MySqlCommand dropCommand = new MySqlCommand(dropConstraint, con);
                dropCommand.ExecuteNonQuery();
                
                transaction.Commit();
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
            finally
            {
                con.Close();
            }
        }

        [TestCleanup]
        public void RestoreForeignKey()
        {
            DatabaseConnection db = new DatabaseConnection();
            MySqlConnection con = db.GetConnection();

            con.Open();
            try
            {
                MySqlTransaction transaction = con.BeginTransaction();

                // Put it back as to not break things
                string addConstraint = "ALTER TABLE bags ADD CONSTRAINT seller FOREIGN KEY (sellerId) REFERENCES sellers(id)";
                MySqlCommand addCommand = new MySqlCommand(addConstraint, con);
                addCommand.ExecuteNonQuery();
                
                transaction.Commit();
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
            finally
            {
                con.Close();
            }
        }

        public void ClearDatabase()
        {
            DatabaseConnection db = new DatabaseConnection();
            MySqlConnection con = db.GetConnection();

            con.Open();
            try
            {
                MySqlTransaction transaction = con.BeginTransaction();

                string query = "TRUNCATE TABLE sellers";
                MySqlCommand command = new MySqlCommand(query, con);
                command.ExecuteNonQuery();

                transaction.Commit();
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
            finally
            {
                con.Close();
            }
        }

        // Insert_Single Tests
        [TestMethod, TestCategory("IntegrationTest")]
        public void Insert_Single_Runs_Without_Errors()
        {
            SellerDML sellerdml = new SellerDML();
            Seller seller = new Seller("TestSeller", "Test", "Test", "Test", 0, "Test", 0);

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

        [TestMethod, TestCategory("IntegrationTest")]
        public void Insert_Single_Inserts_Seller()
        {
            ClearDatabase();
            SellerDML sellerdml = new SellerDML();
            Seller expected = new Seller("TestSeller", "Test", "Test", "Test", 0, "Test", 0);

            sellerdml.Insert_Single(expected);
            Seller actual = GetSeller();

            Assert.AreEqual(expected, actual);
        }

        private Seller GetSeller()
        {
            Seller seller;
            DatabaseConnection db = new DatabaseConnection();
            MySqlConnection con = db.GetConnection();

            string query = "SELECT * FROM sellers";

            con.Open();

            MySqlCommand command = new MySqlCommand(query, con);

            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            seller = MapToSeller(reader);
            reader.Close();

            con.Close();
            return seller;
        }

        // Insert_Single Tests
        [TestMethod, TestCategory("IntegrationTest")]
        public void Insert_Multiple_Runs_Without_Errors()
        {
            ClearDatabase();
            SellerDML sellerdml = new SellerDML();
            List<Seller> sellers = new List<Seller>(){
                new Seller("TestSeller", "Test", "Test", "Test", 0, "Test", 0),
                new Seller("TestSeller1", "Test1", "Test", "Test", 0, "Test", 0),
            };

            try
            {
                sellerdml.Insert_Multiple(sellers);
                // Pass test if it runs without error
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Assert.Fail();
            }
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void Insert_Multiple_Inserts_Sellers()
        {
            ClearDatabase();
            SellerDML sellerdml = new SellerDML();
            List<Seller> expected = new List<Seller>(){
                new Seller("TestSeller", "Test", "Test", "Test", 0, "Test", 0),
                new Seller("TestSeller1", "Test1", "Test", "Test", 0, "Test", 0),
            };

            sellerdml.Insert_Multiple(expected);
            List<Seller> actual = GetMultipleSellers();

            for (int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        private List<Seller> GetMultipleSellers()
        {
            List<Seller> sellers = new List<Seller>();
            DatabaseConnection db = new DatabaseConnection();
            MySqlConnection con = db.GetConnection();

            string query = "SELECT * FROM sellers";

            con.Open();

            MySqlCommand command = new MySqlCommand(query, con);

            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                sellers.Add(MapToSeller(reader));
            }
            reader.Close();

            con.Close();
            return sellers;
        }

        private Seller MapToSeller(MySqlDataReader reader)
        {
            int id = reader.GetInt32("id");
            string name = reader.GetString("name");
            string email = reader.GetString("email");
            string address = reader.GetString("address");
            int postnumber = reader.GetInt32("postnumber");
            string city = reader.GetString("city");
            string phonenumber = reader.GetString("phonenumber");
            int wooCommerceId = reader.GetInt32("woocommerceId");

            return new Seller(id, name, email, address, city, postnumber, phonenumber, wooCommerceId);
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
            ClearDatabase();
            InsertTestData();
            Seller expected = new Seller("TestSeller1", "TestMail1", "Test", "Test", 0123, "Test", 0123);

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
                new Seller("TestSeller1", "TestMail1", "Test", "Test", 0123, "Test", 0123),
                new Seller("TestSeller2", "TestMail2", "Test", "Test", 0123, "Test", 0123),
                new Seller("TestSeller3", "TestMail3", "Test", "Test", 0123, "Test", 0123)
            };

            List<Seller> actual = sellerdml.Select_Multiple("name:TestSeller");

            // Do not know another way to compare list items
            for (int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        // Update_Single tests
        [TestMethod, TestCategory("IntegrationTest")]
        public void Update_Single_Given_User_Without_Id_Throws_ArgumentException()
        {
            SellerDML sellerdml = new SellerDML();
            ArgumentException expected = null;
            Seller seller = new Seller("", "", "", "", 0, "", 0);
            try
            {
                sellerdml.Update_Single(seller);
            }
            catch (ArgumentException e)
            {
                expected = e;
            }

            Assert.IsNotNull(expected);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void Update_Single_Updates_Row_Matching_Id()
        {
            ClearDatabase();
            SellerDML sellerdml = new SellerDML();
            InsertTestData();
            Seller expected = new Seller(1, "UpdatedSeller", "TestMail1", "Test", "Test", 0123, "Test", 0123);

            sellerdml.Update_Single(expected);

            Seller actual = GetSellerById(1);
            Assert.AreEqual(expected, actual);
        }

        // Update_Single tests
        [TestMethod, TestCategory("IntegrationTest")]
        public void Update_Multiple_Given_User_Without_Id_Throws_ArgumentException()
        {
            SellerDML sellerdml = new SellerDML();
            ArgumentException expected = null;
            List<Seller> sellers = new List<Seller> {
                new Seller("TestSeller1", "TestMail1", "Test", "Test", 0123, "Test", 0123),
                new Seller("TestSeller2", "TestMail2", "Test", "Test", 0123, "Test", 0123),
                new Seller("TestSeller3", "TestMail3", "Test", "Test", 0123, "Test", 0123)
            };
            try
            {
                sellerdml.Update_Multiple(sellers);
            }
            catch (ArgumentException e)
            {
                expected = e;
            }

            Assert.IsNotNull(expected);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void Update_Multiple_Updates_Row_Matching_Id()
        {
            ClearDatabase();
            SellerDML sellerdml = new SellerDML();
            InsertTestData();
            List<Seller> expected = new List<Seller> {
                new Seller(1, "Updated1", "TestMail1", "Test", "Test", 0123, "Test", 0123),
                new Seller(2, "updated2", "TestMail2", "Test", "Test", 0123, "Test", 0123),
                new Seller(3, "updated3", "TestMail3", "Test", "Test", 0123, "Test", 0123)
            };

            sellerdml.Update_Multiple(expected);

            List<Seller> actual = GetMultipleSellers(new List<int>() { 1, 2, 3 });

            for (int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        private Seller GetSellerById(int id)
        {
            Seller seller;
            DatabaseConnection db = new DatabaseConnection();
            MySqlConnection con = db.GetConnection();

            string query = "SELECT * FROM sellers WHERE id=" + id;

            con.Open();

            MySqlCommand command = new MySqlCommand(query, con);

            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            seller = MapToSeller(reader);
            reader.Close();

            con.Close();
            return seller;
        }

        private List<Seller> GetMultipleSellers(List<int> ids)
        {
            List<Seller> sellers = new List<Seller>();
            DatabaseConnection db = new DatabaseConnection();
            MySqlConnection con = db.GetConnection();

            string query = "SELECT * FROM sellers";

            con.Open();

            MySqlCommand command = new MySqlCommand(query, con);

            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                sellers.Add(MapToSeller(reader));
            }
            reader.Close();

            con.Close();
            return sellers;
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void Delete_Single_Given_Seller_Without_Id_Throws_Error()
        {
            SellerDML sellerdml = new SellerDML();
            Seller seller = new Seller("", "", "", "", 0, "", 0);
            ArgumentException expected = null;

            try
            {
                sellerdml.Delete_Single(seller);
            }
            catch (ArgumentException e)
            {
                expected = e;
            }

            Assert.IsNotNull(expected);
        }


        [TestMethod, TestCategory("IntegrationTest")]
        public void Delete_Single_Deletes_Single_Entry()
        {
            SellerDML sellerdml = new SellerDML();
            ClearDatabase();
            InsertTestData();

            int expected = Count() - 1;

            Seller seller = new Seller(1, "TestSeller", "TestMail1", "Test", "Test", 0123, "Test", 0123);

            sellerdml.Delete_Single(seller);

            int actual = Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void Delete_Multiple_Given_Sellers_Without_Id_Throws_Error()
        {
            SellerDML sellerdml = new SellerDML();
            List<Seller> sellers = new List<Seller>
            {
                new Seller("", "", "", "", 0, "", 0),
                new Seller("", "", "", "", 0, "", 0),
            };
            ArgumentException expected = null;

            try
            {
                sellerdml.Delete_Multiple(sellers);
            }
            catch (ArgumentException e)
            {
                expected = e;
            }

            Assert.IsNotNull(expected);
        }


        [TestMethod, TestCategory("IntegrationTest")]
        public void Delete_Multiple_Deletes_Multiple_Entries()
        {
            SellerDML sellerdml = new SellerDML();
            ClearDatabase();
            InsertTestData();

            int expected = Count() - 2;

            List<Seller> sellers = new List<Seller>
            {
                new Seller(1, "TestSeller", "TestMail1", "Test", "Test", 0123, "Test", 0123),
                new Seller(2, "TestSeller", "TestMail1", "Test", "Test", 0123, "Test", 0123),
            };

            sellerdml.Delete_Multiple(sellers);

            int actual = Count();
            Assert.AreEqual(expected, actual);
        }

        private int Count()
        {
            DatabaseConnection db = new DatabaseConnection();
            MySqlConnection con = db.GetConnection();

            string sql = "SELECT * FROM sellers";
            MySqlCommand cmd = new MySqlCommand(sql, con);

            con.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            int count = 0;
            while (reader.Read())
                count++;

            reader.Close();
            con.Close();

            return count;
        }

        private void InsertTestData()
        {
            DatabaseConnection db = new DatabaseConnection();
            MySqlConnection con = db.GetConnection();

            string query = "INSERT INTO sellers(name, email, address, postnumber, city, phonenumber, woocommerceId) VALUES " +  
                                         "('TestSeller1', 'TestMail1', 'Test', '0123', 'Test', 'Test', '0123')," +
                                         "('TestSeller2', 'TestMail2', 'Test', '0123', 'Test', 'Test', '0123')," +
                                         "('TestSeller3', 'TestMail3', 'Test', '0123', 'Test', 'Test', '0123')";

            con.Open();

            MySqlCommand command = new MySqlCommand(query, con);

            command.ExecuteNonQuery();

            con.Close();
        }
    }
}
