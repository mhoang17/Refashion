using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refashion.Database
{
    public class SellerDML
    {
        private MySqlCommand command;
        private string connectionString = @"server=localhost;userid=devUser;password=devpass;database=refashion;Allow User Variables=True";
        public SellerDML()
        {
            MySqlConnection con = new MySqlConnection(connectionString);
            con.Open();

            string stm = "SELECT VERSION()";
            MySqlCommand cmd = new MySqlCommand(stm, con);

            string version = cmd.ExecuteScalar().ToString();
            Console.WriteLine($"MySQL version: {version}");
            con.Close();
        }

        // TODO: create abstraction for try-catch 
        public List<Seller> GetAll()
        {
            List<Seller> sellers = new List<Seller>();
            MySqlConnection con = new MySqlConnection(connectionString);
            try
            {
                con.Open();

                string query = "SELECT * " + 
                            "FROM sellers";
                command = new MySqlCommand(query, con);

                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    sellers.Add(MapToSeller(reader));
                }
                reader.Close();

                Console.WriteLine("Got " + sellers.Count().ToString() + " sellers");
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
            Seller seller = new Seller("","","","",0,"");
            MySqlConnection con = new MySqlConnection(connectionString);
            try
            {
                string query = "SELECT * FROM sellers WHERE";
                StringBuilder queryBuilder = new StringBuilder(query);

                Dictionary<string, string> conditionDictionary = ParseConditionsToDictionary(conditions);
                queryBuilder.Append(CreateEqualsParameters(conditionDictionary));

                // Return only a single row
                queryBuilder.Append(" LIMIT 1");

                command = new MySqlCommand(queryBuilder.ToString(), con);
                conditionDictionary = ParseConditionsToDictionary(conditions);
                AddEqualsParameters(conditionDictionary);

                Console.WriteLine(command.CommandText);

                con.Open();

                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    seller = MapToSeller(reader);
                }
                reader.Close();

                Console.WriteLine("Got seller with id: " + seller.Tag);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
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
            MySqlConnection con = new MySqlConnection(connectionString);
            try
            {
                string query = "SELECT * FROM sellers WHERE";
                StringBuilder queryBuilder = new StringBuilder(query);

                Dictionary<string, string> conditionDictionary = ParseConditionsToDictionary(conditions);
                queryBuilder.Append(CreateLikeParameters(conditionDictionary));

                command = new MySqlCommand(queryBuilder.ToString(), con);

                conditionDictionary = ParseConditionsToDictionary(conditions);
                AddLikeParameters(conditionDictionary);

                Console.WriteLine(command.CommandText);

                con.Open();

                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    sellers.Add(MapToSeller(reader));
                }
                reader.Close();

                Console.WriteLine("Got " + sellers.Count().ToString() + " sellers");
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
                }

                string rowName = items[0].Trim();
                string rowValue = items[1].Trim();

                conditionDictionary.Add(rowName, rowValue);
            }

            return conditionDictionary;
        }

        private string CreateEqualsParameters(Dictionary<string, string> conditions)
        {
            StringBuilder queryBuilder = new StringBuilder();

            if (conditions.Count > 0)
            {
                KeyValuePair<string, string> firstCondition = conditions.First();

                queryBuilder.Append(string.Format(" {0}=@{0}", firstCondition.Key));

                // Remove the condition just added so it is not included in following loop
                conditions.Remove(firstCondition.Key);
            }
            if(conditions.Count > 0)
            {
                foreach (KeyValuePair<string, string> conditionPair in conditions)
                {
                    queryBuilder.Append(string.Format(" OR {0} = '@{0}'", conditionPair.Key));
                }
            }

            return queryBuilder.ToString();
        }

        private string CreateLikeParameters(Dictionary<string, string> conditions)
        {
            StringBuilder queryBuilder = new StringBuilder();

            if (conditions.Count > 0)
            {
                KeyValuePair<string, string> firstCondition = conditions.First();

                queryBuilder.Append(string.Format(" {0} Like @{0}", firstCondition.Key));

                // Remove the condition just added so it is not included in following loop
                conditions.Remove(firstCondition.Key);
            }
            if (conditions.Count > 0)
            {
                foreach (KeyValuePair<string, string> conditionPair in conditions)
                {
                    queryBuilder.Append(string.Format(" OR {0} LIKE @{0}", conditionPair.Key));
                }
            }

            return queryBuilder.ToString();
        }

        private void AddEqualsParameters(Dictionary<string, string> conditions)
        {
            foreach (KeyValuePair<string, string> conditionPair in conditions)
            {
                Console.WriteLine(conditionPair.Key + " : " + conditionPair.Value);
                command.Parameters.AddWithValue(conditionPair.Key, conditionPair.Value);
                //command.Parameters["@" + conditionPair.Key].Value = conditionPair.Value;
            }
        }

        private void AddLikeParameters(Dictionary<string, string> conditions)
        {
            foreach (KeyValuePair<string, string> conditionPair in conditions)
            {
                Console.WriteLine(conditionPair.Key + " : " + conditionPair.Value);
                command.Parameters.AddWithValue(conditionPair.Key, "%" + conditionPair.Value + "%");
                //command.Parameters["@" + conditionPair.Key].Value = conditionPair.Value;
            }
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

            return new Seller(id, name, email, address, city, postnumber, phonenumber);
        }

        public void Insert_Single(Seller seller)
        {
            var con = new MySqlConnection(connectionString);
            try
            {
                con.Open();

                var query = "INSERT INTO sellers (name, email, address, postnumber, city, phonenumber) " +
                                         "VALUES (@name, @email, @address, @postnumber, @city, @phonenumber)";
                command = new MySqlCommand(query, con);

                addParameter("name", MySqlDbType.String, seller.Name);
                addParameter("email", MySqlDbType.String, seller.Email);
                addParameter("address", MySqlDbType.String, seller.Address);
                addParameter("postnumber", MySqlDbType.Int32, seller.ZIP.ToString());
                addParameter("city", MySqlDbType.String, seller.City);
                addParameter("phonenumber", MySqlDbType.String, seller.PhoneNumber.ToString());

                bool querySuccess = command.ExecuteNonQuery() > 0;
                long id = command.LastInsertedId;

                Console.WriteLine("Success: " + querySuccess.ToString());
                Console.WriteLine("Inserted seller with id: " + id.ToString());

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

        // Take max_allowed_packet into account
        public void Insert_Multiple(List<Seller> sellers)
        {
            var con = new MySqlConnection(connectionString);
            try
            {
                string query = "INSERT INTO sellers (name, email, address, postnumber, city, phonenumber) VALUES ";
                StringBuilder queryBuilder = new StringBuilder(query);

                List<string> sellerRows = new List<string>();
                foreach (Seller seller in sellers)
                {   
                    sellerRows.Add(string.Format("('{0}','{1}','{2}','{3}','{4}','{5}')",
                        MySqlHelper.EscapeString(seller.Name),
                        MySqlHelper.EscapeString(seller.Email),
                        MySqlHelper.EscapeString(seller.Address),
                        seller.ZIP,
                        MySqlHelper.EscapeString(seller.City),
                        MySqlHelper.EscapeString(seller.PhoneNumber.ToString())
                        ));
                }

                queryBuilder.Append(string.Join(",", sellerRows));
                queryBuilder.Append(";");

                con.Open();

                command = new MySqlCommand(queryBuilder.ToString(), con);
                command.CommandType = CommandType.Text;

                bool querySuccess = command.ExecuteNonQuery() > 0;

                Console.WriteLine("Success: " + querySuccess.ToString());
                Console.WriteLine("Inserted " + sellerRows.Count + " sellers ");
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

        public void Update_Single(Seller seller)
        {
            var con = new MySqlConnection(connectionString);
            try
            {
                con.Open();

                var query = "UPDATE sellers SET name=@name, email=@email, address=@email, postnumber=@postnumber, city=@city, phonenumber=@phonenumber " +
                            "WHERE id=@id";
                command = new MySqlCommand(query, con);

                addParameter("name", MySqlDbType.String, seller.Name);
                addParameter("email", MySqlDbType.String, seller.Email);
                addParameter("address", MySqlDbType.String, seller.Address);
                addParameter("postnumber", MySqlDbType.Int32, seller.ZIP.ToString());
                addParameter("city", MySqlDbType.String, seller.City);
                addParameter("phonenumber", MySqlDbType.String, seller.PhoneNumber.ToString());
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
            var con = new MySqlConnection(connectionString);
            try
            {
                string query = "INSERT INTO sellers (id, name, email, address, postnumber, city, phonenumber) VALUES ";
                StringBuilder queryBuilder = new StringBuilder(query);

                List<string> sellerRows = new List<string>();
                foreach (Seller seller in sellers)
                {
                    sellerRows.Add(string.Format("('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                        seller.Tag,
                        MySqlHelper.EscapeString(seller.Name),
                        MySqlHelper.EscapeString(seller.Email),
                        MySqlHelper.EscapeString(seller.Address),
                        seller.ZIP,
                        MySqlHelper.EscapeString(seller.City),
                        MySqlHelper.EscapeString(seller.PhoneNumber.ToString())
                        ));
                }

                queryBuilder.Append(string.Join(",", sellerRows));
                queryBuilder.Append(" ON DUPLICATE KEY UPDATE" +
                                    "name=VALUEs(name)," +
                                    "email=VALUES(email)," +
                                    "address=VALUES(address)," +
                                    "postnumber=VALUES(postnumber)," +
                                    "city=VALUES(city)," +
                                    "phonenumber=VALUES(phonenumber);");

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
            var con = new MySqlConnection(connectionString);
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
            var con = new MySqlConnection(connectionString);
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
