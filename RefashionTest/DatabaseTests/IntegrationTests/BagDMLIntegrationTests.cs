using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using Refashion;
using Refashion.Database;

namespace RefashionTest.DatabaseTests.IntegrationTests
{
    [TestClass]
    public class BagDMLIntegrationTests
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
                string dropSellerConstraint = "ALTER TABLE bags DROP FOREIGN KEY seller";
                MySqlCommand dropSellerCommand = new MySqlCommand(dropSellerConstraint, con);
                dropSellerCommand.ExecuteNonQuery();

                string dropBagConstraint = "ALTER TABLE products DROP FOREIGN KEY bag";
                MySqlCommand dropBagCommand = new MySqlCommand(dropBagConstraint, con);
                dropBagCommand.ExecuteNonQuery();

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
            ClearDatabase();
            DatabaseConnection db = new DatabaseConnection();
            MySqlConnection con = db.GetConnection();

            con.Open();
            try
            {
                MySqlTransaction transaction = con.BeginTransaction();

                // Put it back as to not break things
                string addSellerConstraint = "ALTER TABLE bags ADD CONSTRAINT seller FOREIGN KEY (sellerId) REFERENCES sellers(id)";
                MySqlCommand addSellerCommand = new MySqlCommand(addSellerConstraint, con);
                addSellerCommand.ExecuteNonQuery();

                string addBagConstraint = "ALTER TABLE products ADD CONSTRAINT bag FOREIGN KEY (bagId) REFERENCES bags(id)";
                MySqlCommand addBagCommand = new MySqlCommand(addBagConstraint, con);
                addBagCommand.ExecuteNonQuery();

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

                string truncateBags = "TRUNCATE TABLE bags";
                MySqlCommand command = new MySqlCommand(truncateBags, con);
                command.ExecuteNonQuery();

                transaction.Commit();

                string truncateSellers = "TRUNCATE TABLE sellers";
                MySqlCommand secondCommand = new MySqlCommand(truncateSellers, con);
                secondCommand.ExecuteNonQuery();
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

        // Delete_Multiple tests
        [TestMethod, TestCategory("IntegrationTest")]
        public void Delete_Multiple_Given_Bags_Without_Id_Throws_Execption()
        {
            BagDML bagdml = new BagDML();
            List<Bag> bags = new List<Bag>
            {
                new Bag(0),
                new Bag(0)
            };

            ArgumentException expected = null;

            try
            {
                bagdml.Delete_Multiple(bags);
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
            BagDML bagdml = new BagDML();
            ClearDatabase();
            InsertTestData();

            int expected = Count() - 2;

            List<Bag> bags = new List<Bag>
            {
                new Bag(1, 1, DateTime.Now),
                new Bag(2, 1, DateTime.Now),
            };

            bagdml.Delete_Multiple(bags);

            int actual = Count();
            Assert.AreEqual(expected, actual);

        }

        // Delete_Single tests
        [TestMethod, TestCategory("IntegrationTest")]
        public void Delete_Single_Given_Bag_Without_Id_Throws_Exception()
        {
            BagDML bagdml = new BagDML();
            Bag bag = new Bag(0);
            ArgumentException expected = null;

            try
            {
                bagdml.Delete_Single(bag);
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
            BagDML bagDML = new BagDML();
            ClearDatabase();
            InsertTestData();

            int expected = Count() - 1;

            Bag bag = new Bag(1, 1, DateTime.Now);

            bagDML.Delete_Single(bag);

            int actual = Count();
            Assert.AreEqual(expected, actual);
        }

        // Insert_Multiple tests
        [TestMethod, TestCategory("IntegrationTest")]
        public void Insert_Multiple_Runs_Without_Errors()
        {
            ClearDatabase();
            BagDML bagdml = new BagDML();
            List<Bag> bags = new List<Bag>(){
                new Bag(0),
                new Bag(0)
            };

            try
            {
                bagdml.Insert_Multiple(bags);
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
        public void Insert_Multiple_Inserts_Bags()
        {
            ClearDatabase();
            BagDML bagdml = new BagDML();
            List<Bag> expected = new List<Bag>(){
                new Bag(0),
                new Bag(0)
            };

            bagdml.Insert_Multiple(expected);
            List<Bag> actual = getMultipleBags();

            for (int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        // Insert_Single Tests
        [TestMethod, TestCategory("IntegrationTest")]
        public void Insert_Single_Runs_Without_Errors()
        {
            ClearDatabase();
            BagDML bagdml = new BagDML();
            Bag bag = new Bag(0);

            try
            {
                bagdml.Insert_Single(bag);
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Assert.Fail();
            }
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void Insert_Single_Inserts_Bag()
        {
            ClearDatabase();
            BagDML bagdml = new BagDML();
            Bag expected = new Bag(0);

            bagdml.Insert_Single(expected);
            Bag actual = getMultipleBags()[0];
            bool eql = expected.Equals(actual);
            Assert.AreEqual(expected, actual);
        }

        // Select_Multiple Tests
        [TestMethod, TestCategory("IntegrationTest")]
        public void Select_Multiple_With_No_Conditions_Throws_ArgumentException()
        {
            BagDML bagdml = new BagDML();
            ArgumentException expected = null;

            try
            {
                bagdml.Select_Multiple("");
            }
            catch (ArgumentException e)
            {
                expected = e;
            }

            Assert.IsNotNull(expected);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void Select_Multiple_Returns_Multiple_Elements_Matching_SellerId()
        {
            ClearDatabase();
            BagDML bagdml = new BagDML();
            InsertTestData();
            List<Bag> expected = new List<Bag> {
                new Bag(1),
                new Bag(1)
            };

            List<Bag> actual = bagdml.Select_Multiple("sellerId:1");

            // Do not know another way to compare list items
            for (int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        // Select_Single Tests
        [TestMethod, TestCategory("IntegrationTest")]
        public void Select_Single_With_No_Conditions_Throws_ArgumentException()
        {
            BagDML bagdml = new BagDML();
            ArgumentException expected = null;

            try
            {
                bagdml.Select_Single("");
            }
            catch (ArgumentException e)
            {
                expected = e;
            }

            Assert.IsNotNull(expected);
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void Select_Single_Returns_Single_Element_Matching_SellerId()
        {
            BagDML bagdml = new BagDML();
            ClearDatabase();
            InsertTestData();
            Bag expected = new Bag(1);

            Bag actual = bagdml.Select_Single("sellerId:1");

            Assert.AreEqual(expected, actual);
        }

        // Update_Multiple tests
        [TestMethod, TestCategory("IntegrationTest")]
        public void Update_Multiple_Given_Bag_Without_Id_Throws_ArgumentException()
        {
            BagDML bagdml = new BagDML();
            ArgumentException expected = null;
            List<Bag> bags = new List<Bag> {
                new Bag(0),
                new Bag(0)
            };
            try
            {
                bagdml.Update_Multiple(bags);
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
            BagDML bagdml = new BagDML();
            ClearDatabase();
            InsertTestData();
            List<Bag> expected = new List<Bag> {
                new Bag(1, 2, DateTime.Now),
                new Bag(2, 2, DateTime.Now),
                new Bag(3, 1, DateTime.Now)
            };

            bagdml.Update_Multiple(expected);

            List<Bag> actual = getMultipleBags();

            for (int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        // Update_Single tests
        [TestMethod, TestCategory("IntegrationTest")]
        public void Update_Single_Given_Bag_Without_Id_Throws_ArgumentException()
        {
            BagDML bagdml = new BagDML();
            ArgumentException expected = null;
            Bag bag = new Bag(0);
            try
            {
                bagdml.Update_Single(bag);
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
            InsertTestData();
            BagDML bagdml = new BagDML();
            Bag expected = new Bag(1, 2, DateTime.Now);

            bagdml.Update_Single(expected);

            Bag actual = getBagById(1);
            Assert.AreEqual(expected, actual);
        }

        private int Count()
        {
            DatabaseConnection db = new DatabaseConnection();
            MySqlConnection con = db.GetConnection();

            string sql = "SELECT * FROM bags";
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

        private List<Bag> getMultipleBags()
        {
            List<Bag> bags = new List<Bag>();
            DatabaseConnection db = new DatabaseConnection();
            MySqlConnection con = db.GetConnection();

            string query = "SELECT * FROM bags";

            con.Open();

            MySqlCommand command = new MySqlCommand(query, con);

            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                bags.Add(mapToBag(reader));
            }
            reader.Close();

            con.Close();
            return bags;
        }

        private Bag mapToBag(MySqlDataReader reader)
        {
            int id = reader.GetInt32("id");
            int sellerId = reader.GetInt32("sellerId");
            DateTime addedDate = reader.GetDateTime("added_at");

            return new Bag(id, sellerId, addedDate);
        }

        private Bag getBagById(int id)
        {
            Bag bag;
            DatabaseConnection db = new DatabaseConnection();
            MySqlConnection con = db.GetConnection();

            string query = "SELECT * FROM bags WHERE id=" + id;

            con.Open();

            MySqlCommand command = new MySqlCommand(query, con);

            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            bag = mapToBag(reader);
            reader.Close();

            con.Close();
            return bag;
        }

        private void InsertTestData()
        {
            DatabaseConnection db = new DatabaseConnection();
            MySqlConnection con = db.GetConnection();
            

            string query = "INSERT INTO sellers(name, email, address, postnumber, city, phonenumber, woocommerceId) VALUES " +
                                         "('TestSeller1', 'TestMail1', 'Test', '0123', 'Test', 'Test', '0123')," +
                                         "('TestSeller2', 'TestMail2', 'Test', '0123', 'Test', 'Test', '0123')";

            con.Open();

            MySqlCommand command = new MySqlCommand(query, con);

            command.ExecuteNonQuery();

            con.Close();
            

            DateTime now = DateTime.Now;
            string bagQuery = "INSERT INTO bags(sellerId, added_at) VALUES " +
                                         "('1', '" + now.ToString("yyyy-MM-dd H:mm:ss") + "')," +
                                         "('1', '" + now.ToString("yyyy-MM-dd H:mm:ss") + "')," +
                                         "('2', '" + now.ToString("yyyy-MM-dd H:mm:ss") + "')";

            con.Open();

            MySqlCommand bagCommand = new MySqlCommand(bagQuery, con);

            bagCommand.ExecuteNonQuery();

            con.Close();
        }
    }
}
