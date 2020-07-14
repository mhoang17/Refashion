using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refashion.Database
{
    public class SellerDML : RefashionDML<Seller>
    {
        private MySqlCommand command;
        private DatabaseConnection database;
        public SellerDML()
        {
            database = new DatabaseConnection();
        }

        // TODO: create abstraction for try-catch 
        public List<Seller> GetAll()
        {
            List<Seller> sellers = new List<Seller>();
            MySqlConnection con = database.GetConnection();
            try
            {
                con.Open();

                string query = "SELECT * FROM sellers";

                command = new MySqlCommand(query, con);

                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    sellers.Add(MapToSeller(reader));
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

            return sellers;
        }

        public Seller Select_Single(string conditions)
        {
            // empty seller in case query returns nothing
            Seller seller = new Seller("","","","",0,"",0);
            MySqlConnection con = database.GetConnection();
            try
            {
                string query = "SELECT * FROM sellers WHERE";
                CommandBuilder commandBuilder = new CommandBuilder(query);

                StringBuilder queryBuilder = new StringBuilder(query);
                Dictionary<string, string> conditionDictionary = ParseConditionsToDictionary(conditions);
                commandBuilder.AddEqualsParameters(conditionDictionary);
                // Should only return a single result
                commandBuilder.AddLimit(1);

                commandBuilder.CreateCommand(con);

                con.Open();

                MySqlDataReader reader = commandBuilder.Command.ExecuteReader();
                if (reader.Read())
                {
                    seller = MapToSeller(reader);
                }
                reader.Close();

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
            return seller;
        }

        // TODO: Refactor as this contains same logic as Select_Single but without limit
        public List<Seller> Select_Multiple(string conditions)
        {
            List<Seller> sellers = new List<Seller>();
            MySqlConnection con = database.GetConnection();
            try
            {
                string query = "SELECT * FROM sellers WHERE";
                CommandBuilder commandBuilder = new CommandBuilder(query);

                Dictionary<string, string> conditionDictionary = ParseConditionsToDictionary(conditions);
                commandBuilder.AddLikeParameters(conditionDictionary);

                commandBuilder.CreateCommand(con);

                con.Open();

                MySqlDataReader reader = commandBuilder.Command.ExecuteReader();
                while (reader.Read())
                {
                    sellers.Add(MapToSeller(reader));
                }
                reader.Close();
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
            return sellers;
        }

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

        public void Insert_Single(Seller seller)
        {
            var con = database.GetConnection();
            try
            {
                con.Open();

                var query = "INSERT INTO sellers (name, email, address, postnumber, city, phonenumber, woocommerceId) " +
                                         "VALUES (@name, @email, @address, @postnumber, @city, @phonenumber, @woocommerceId)";
                command = new MySqlCommand(query, con);

                addParameter("name", MySqlDbType.String, seller.Name);
                addParameter("email", MySqlDbType.String, seller.Email);
                addParameter("address", MySqlDbType.String, seller.Address);
                addParameter("postnumber", MySqlDbType.Int32, seller.ZIP.ToString());
                addParameter("city", MySqlDbType.String, seller.City);
                addParameter("phonenumber", MySqlDbType.String, seller.PhoneNumber.ToString());
                addParameter("woocommerceId", MySqlDbType.Int32, seller.WooCommerceId.ToString());

                bool querySuccess = command.ExecuteNonQuery() > 0;
                long id = command.LastInsertedId;

                Console.WriteLine("Success: " + querySuccess.ToString());
                Console.WriteLine("Inserted seller with id: " + id.ToString());

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

        // TODO: Take max_allowed_packet into account
        public void Insert_Multiple(List<Seller> sellers)
        {
            var con = database.GetConnection();
            try
            {
                // (name, email, address, postnumber, city, phonenumber, woocommerceId)
                //VALUES

                List<string> sellerParameters = new List<string>()
                {
                    "name",
                    "email",
                    "address",
                    "postnumber",
                    "city",
                    "phonenumber",
                    "woocommerceId"
                };

                CommandBuilder commandBuilder = new CommandBuilder("INSERT INTO sellers ");
                commandBuilder.AddInsertParameters(sellerParameters);

                List<List<string>> rows = new List<List<string>>();
                foreach (Seller seller in sellers)
                {
                    rows.Add(mapSellerToStrings(seller));
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

        private List<string> mapSellerToStrings(Seller seller)
        {
            List<string> row = new List<string>()
            {
                seller.Name,
                seller.Email,
                seller.Address,
                seller.ZIP.ToString(),
                seller.City,
                seller.PhoneNumber,
                seller.WooCommerceId.ToString()
            };

            return row;
        }

        public void Update_Single(Seller seller)
        {
            var con = database.GetConnection();
            if(seller.Tag == 0)
            {
                throw new ArgumentException("Seller must have a valid Tag to be updated");
            }
            try
            {
                con.Open();

                var query = "UPDATE sellers SET name=@name, email=@email, address=@address, postnumber=@postnumber, city=@city, phonenumber=@phonenumber, woocommerceId=@woocommerceId " +
                            "WHERE id=@id";
                command = new MySqlCommand(query, con);

                addParameter("name", MySqlDbType.String, seller.Name);
                addParameter("email", MySqlDbType.String, seller.Email);
                addParameter("address", MySqlDbType.String, seller.Address);
                addParameter("postnumber", MySqlDbType.Int32, seller.ZIP.ToString());
                addParameter("city", MySqlDbType.String, seller.City);
                addParameter("phonenumber", MySqlDbType.String, seller.PhoneNumber.ToString());
                addParameter("woocommerceId", MySqlDbType.Int32, seller.WooCommerceId.ToString());
                addParameter("id", MySqlDbType.Int32, seller.Tag.ToString());

                bool querySuccess = command.ExecuteNonQuery() > 0;
                long id = command.LastInsertedId;

                Console.WriteLine("Success: " + querySuccess.ToString());
                Console.WriteLine("Updated seller with id: " + seller.Tag.ToString());

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

        public void Update_Multiple(List<Seller> sellers)
        {
            if (sellers.FindAll(seller => seller.Tag == 0).Count > 0)
            {
                throw new ArgumentException("All sellers must have a valid tag");
            }

            var con = database.GetConnection();
            try
            {
                string query = "INSERT INTO sellers (id, name, email, address, postnumber, city, phonenumber, woocommerceId) VALUES ";
                StringBuilder queryBuilder = new StringBuilder(query);

                List<string> sellerRows = new List<string>();
                foreach (Seller seller in sellers)
                {
                    sellerRows.Add(string.Format("('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                        seller.Tag,
                        MySqlHelper.EscapeString(seller.Name),
                        MySqlHelper.EscapeString(seller.Email),
                        MySqlHelper.EscapeString(seller.Address),
                        seller.ZIP,
                        MySqlHelper.EscapeString(seller.City),
                        MySqlHelper.EscapeString(seller.PhoneNumber.ToString()),
                        seller.WooCommerceId
                        ));
                }

                queryBuilder.Append(string.Join(",", sellerRows));
                queryBuilder.Append(" ON DUPLICATE KEY UPDATE " +
                                    "name=VALUEs(name)," +
                                    "email=VALUES(email)," +
                                    "address=VALUES(address)," +
                                    "postnumber=VALUES(postnumber)," +
                                    "city=VALUES(city)," +
                                    "phonenumber=VALUES(phonenumber)," +
                                    "woocommerceId=VALUES(woocommerceId);");

                con.Open();

                command = new MySqlCommand(queryBuilder.ToString(), con);
                command.CommandType = CommandType.Text;

                bool querySuccess = command.ExecuteNonQuery() > 0;

                Console.WriteLine("Success: " + querySuccess.ToString());
                Console.WriteLine("Updated " + sellerRows.Count + " sellers ");
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

        // TODO: Consider implementing soft delete
        public void Delete_Single(Seller seller)
        {
            if(seller.Tag == 0)
            {
                throw new ArgumentException("Seller must have a valid Tag");
            }

            var con = database.GetConnection();
            try
            {
                con.Open();

                var query = "DELETE FROM sellers WHERE id=@id";
                command = new MySqlCommand(query, con);

                addParameter("id", MySqlDbType.Int32, seller.Tag.ToString());

                bool querySuccess = command.ExecuteNonQuery() > 0;
                long id = command.LastInsertedId;

                Console.WriteLine("Success: " + querySuccess.ToString());
                Console.WriteLine("Deleted seller with id: " + seller.Tag.ToString());

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

        public void Delete_Multiple(List<Seller> sellers)
        {
            if(sellers.Any(seller => seller.Tag == 0))
            {
                throw new ArgumentException("All sellers must have a valid Tag");
            }

            var con = database.GetConnection();
            try
            {
                string query = "DELETE FROM sellers WHERE (id) IN " + "(";
                StringBuilder queryBuilder = new StringBuilder(query);

                List<string> sellerRows = new List<string>();
                foreach (Seller seller in sellers)
                {
                    sellerRows.Add(string.Format("('{0}')", seller.Tag));
                }

                queryBuilder.Append(string.Join(",", sellerRows));
                queryBuilder.Append(");");

                con.Open();

                command = new MySqlCommand(queryBuilder.ToString(), con);
                command.CommandType = CommandType.Text;

                bool querySuccess = command.ExecuteNonQuery() > 0;

                Console.WriteLine("Success: " + querySuccess.ToString());
                Console.WriteLine("Deleted " + sellerRows.Count + " sellers ");

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

        // Value must be string
        // Numbers are parsed
        private void addParameter(string parameter, MySqlDbType parameterType, string value)
        {
            string parameterString = "@" + parameter;
            command.Parameters.Add(parameterString, parameterType);

            // add value of correct type
            if(parameterType == MySqlDbType.String)
            {
                command.Parameters[parameterString].Value = value;
            }
            else if(parameterType == MySqlDbType.Int32)
            {
                command.Parameters[parameterString].Value = int.Parse(value);
            }
        }
    }
}
