using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refashion.Database
{
    public class BagDML : RefashionDML<Bag>
    {
        private DatabaseConnection database;

        public BagDML()
        {
            database = new DatabaseConnection();
        }
        public void Delete_Multiple(List<Bag> bags)
        {
            if(bags.Any(bag => bag.Id == 0))
            {
                throw new ArgumentException("All Bags must have a valid Id");
            }
            MySqlConnection con = database.GetConnection();
            try
            {
                CommandBuilder commandBuilder = new CommandBuilder("DELETE FROM bags WHERE (id) IN ");

                commandBuilder.AddValuesToInsert(new List<List<string>>
                {
                    bags.Select(bag => bag.Id.ToString()).ToList()
                });

                con.Open();

                commandBuilder.CreateCommand(con);
                commandBuilder.Command.CommandType = CommandType.Text;

                bool querySuccess = commandBuilder.Command.ExecuteNonQuery() > 0;

                Console.WriteLine("Success: " + querySuccess.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        public void Delete_Single(Bag bag)
        {
            if(bag.Id == 0)
            {
                throw new ArgumentException("Bag must have a valid Id");
            }

            var con = database.GetConnection();
            try
            {
                con.Open();

                CommandBuilder commandBuilder = new CommandBuilder("DELETE FROM bags WHERE ");

                commandBuilder.AddEqualsParameter("id", bag.Id.ToString());

                commandBuilder.CreateCommand(con);

                Console.WriteLine(commandBuilder.Command.CommandText);

                bool querySuccess = commandBuilder.Command.ExecuteNonQuery() > 0;

                Console.WriteLine("Success: " + querySuccess.ToString());
                Console.WriteLine("Deleted bag with id: " + bag.Id.ToString());

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        public List<Bag> GetAll()
        {
            List<Bag> bags = new List<Bag>();
            MySqlConnection con = database.GetConnection();
            try
            {
                con.Open();

                CommandBuilder commandBuilder = new CommandBuilder("SELECT * FROM bags");

                commandBuilder.CreateCommand(con);

                MySqlDataReader reader = commandBuilder.Command.ExecuteReader();
                while (reader.Read())
                {
                    bags.Add(mapToBag(reader));
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                con.Close();
            }

            return bags;
        }

        public void Insert_Multiple(List<Bag> bags)
        {
            var con = database.GetConnection();
            try
            {
                List<string> bagParameters = new List<string>()
                {
                    "sellerId",
                    "added_at"
                };

                CommandBuilder commandBuilder = new CommandBuilder("INSERT INTO bags ");
                commandBuilder.AddInsertParameters(bagParameters);

                List<List<string>> rows = new List<List<string>>();
                foreach (Bag bag in bags)
                {
                    rows.Add(mapBagToStrings(bag));
                }

                commandBuilder.AddValuesToInsert(rows);
                con.Open();

                commandBuilder.CreateCommand(con);
                commandBuilder.Command.CommandType = CommandType.Text;

                bool querySuccess = commandBuilder.Command.ExecuteNonQuery() > 0;

                Console.WriteLine("Success: " + querySuccess.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw (e);
            }
            finally
            {
                con.Close();
            }
        }

        private List<string> mapBagToStrings(Bag bag)
        {
            List<string> row = new List<string>()
            {
                bag.SellerId.ToString(),
                bag.AddedDate.ToString("yyyy-MM-dd H:mm:ss")
            };

            return row;
        }

        public void Insert_Single(Bag bag)
        {
            Insert_Multiple(new List<Bag> { bag });
        }

        // TODO: Refactor as this is literally the same logic as Sellers with a different type
        // Maybe a generic super class
        public List<Bag> Select_Multiple(string conditions)
        {
            List<Bag> bags = new List<Bag>();
            MySqlConnection con = database.GetConnection();
            try
            {
                string query = "SELECT * FROM bags WHERE";
                CommandBuilder commandBuilder = new CommandBuilder(query);

                Dictionary<string, string> conditionDictionary = ParseConditionsToDictionary(conditions);
                commandBuilder.AddLikeParameters(conditionDictionary);

                commandBuilder.CreateCommand(con);

                con.Open();

                MySqlDataReader reader = commandBuilder.Command.ExecuteReader();
                while (reader.Read())
                {
                    bags.Add(mapToBag(reader));
                }
                reader.Close();
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {
                con.Close();
            }
            return bags;
        }

        // TODO: Almost same logic as Select_Multiple, but without list and with limit
        public Bag Select_Single(string conditions)
        {
            Bag bag = new Bag(0);
            MySqlConnection con = database.GetConnection();
            try
            {
                string query = "SELECT * FROM bags WHERE";
                CommandBuilder commandBuilder = new CommandBuilder(query);

                Dictionary<string, string> conditionDictionary = ParseConditionsToDictionary(conditions);
                commandBuilder.AddLikeParameters(conditionDictionary);
                commandBuilder.AddLimit(1);

                commandBuilder.CreateCommand(con);

                con.Open();

                MySqlDataReader reader = commandBuilder.Command.ExecuteReader();
                if(reader.Read())
                {
                    bag = mapToBag(reader);
                }
                reader.Close();
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {
                con.Close();
            }
            return bag;
        }

        // TODO: Move to another class as this is also in SellerDML
        private Dictionary<string, string> ParseConditionsToDictionary(string conditions)
        {
            Dictionary<string, string> conditionDictionary = new Dictionary<string, string>();

            List<string> conditionStrings = new List<string>(conditions.Split(','));
            foreach (string condition in conditionStrings)
            {
                List<string> items = new List<string>(condition.Split(':'));
                if (items.Count > 2)
                {
                    // incorrect format
                    // TODO: Throw correct error type
                    throw new ArgumentException("Conditions must be of the form 'ConditionName:Value'");
                }

                string rowName = items[0].Trim();
                string rowValue = items[1].Trim();

                conditionDictionary.Add(rowName, rowValue);
            }

            return conditionDictionary;
        }

        public void Update_Multiple(List<Bag> bags)
        {
            if (bags.FindAll(bag => bag.Id == 0).Count > 0)
            {
                throw new ArgumentException("All bags must have a valid Id");
            }

            MySqlConnection con = database.GetConnection();
            try
            {
                List<string> bagParameters = new List<string>()
                {
                    "id",
                    "sellerId",
                    "added_at"
                };

                CommandBuilder commandBuilder = new CommandBuilder("INSERT INTO bags ");
                commandBuilder.AddInsertParameters(bagParameters);

                List<List<string>> rows = new List<List<string>>();
                foreach (Bag bag in bags)
                {
                    rows.Add(mapBagWithIdToStrings(bag));
                }

                commandBuilder.AddValuesToInsert(rows);

                commandBuilder.Query.Append(" ON DUPLICATE KEY UPDATE " +
                                    "id=VALUES(id)," +
                                    "sellerId=VALUES(sellerId)," +
                                    "added_at=VALUES(added_at)");
                con.Open();

                commandBuilder.CreateCommand(con);
                commandBuilder.Command.CommandType = CommandType.Text;

                bool querySuccess = commandBuilder.Command.ExecuteNonQuery() > 0;

                Console.WriteLine("Success: " + querySuccess.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        // Might not be the most efficient for updating a single element
        public void Update_Single(Bag bag)
        {
            Update_Multiple(new List<Bag> { bag });
        }

        private List<string> mapBagWithIdToStrings(Bag bag)
        {
            List<string> row = new List<string>()
            {
                bag.Id.ToString(),
                bag.SellerId.ToString(),
                bag.AddedDate.ToString("yyyy-MM-dd H:mm:ss")
            };

            return row;
        }

        private Bag mapToBag(MySqlDataReader reader)
        {
            int id = reader.GetInt32("id");
            int sellerId = reader.GetInt32("sellerId");
            DateTime addedDate = reader.GetDateTime("added_at");

            return new Bag(id, sellerId, addedDate);
        }
    }
}
