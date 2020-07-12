using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using Refashion;
using Refashion.Database;

namespace RefashionTest.DatabaseTests.IntegrationTests
{
    [TestClass]
    public class BagDMLIntegrationTests
    {
        [TestInitialize]
        public void ClearDatabase()
        {
            DatabaseConnection db = new DatabaseConnection();
            MySqlConnection con = db.GetConnection();

            con.Open();
            try
            {
                MySqlTransaction transaction = con.BeginTransaction();

                string dropConstraint = "ALTER TABLE bags DROP FOREIGN KEY seller";
                MySqlCommand dropCommand = new MySqlCommand(dropConstraint, con);
                dropCommand.ExecuteNonQuery();

                string deleteSellers = "TRUNCATE TABLE sellers";
                MySqlCommand deleteSellerCommand = new MySqlCommand(deleteSellers, con);
                deleteSellerCommand.ExecuteNonQuery();

                string deleteBags = "TRUNCATE TABLE bags";
                MySqlCommand deleteBagsCommand = new MySqlCommand(deleteBags, con);
                deleteBagsCommand.ExecuteNonQuery();

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

        [TestMethod, TestCategory("IntegrationTest")]
        public void Delete_Multiple_Runs_Without_Errors()
        {
        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void Delete_Multiple_Deletes_Multiple_Entries()
        {

        }

        [TestMethod, TestCategory("IntegrationTest")]
        public void Delete_Single_Given_Bag_Without_Id_Throws_Error()
        {
            BagDML bagdml = new BagDML();
            Bag bag = new Bag(0);
            ArgumentException expected = null;

            try
            {
                bagdml.Delete_Single(bag);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
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

        private Bag MapToBag(MySqlDataReader reader)
        {
            int id = reader.GetInt32("id");
            int sellerId = reader.GetInt32("sellerId");
            DateTime addedDate = reader.GetDateTime("added_at");

            return new Bag(id, sellerId, addedDate);
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
                                         "('1', '" + now.ToString() + "')," +
                                         "('1', '" + now.ToString() + "')," +
                                         "('2', '" + now.ToString() + "')";

            con.Open();

            MySqlCommand bagCommand = new MySqlCommand(bagQuery, con);

            bagCommand.ExecuteNonQuery();

            con.Close();
        }
    }
}
