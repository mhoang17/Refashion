using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refashion.Database
{
    public class BaseDML<T>
    {
        private string table;
        private List<string> columns;
        private DatabaseConnection database;
        public BaseDML(string table, List<string> columns)
        {
            // TODO: Check strings for SQL injection
            // Either by checking for actual SQL code
            // Or by getting all tables from database
            // and check if name matches any,
            // then do the same for the columns
            this.table = table;
            this.columns = columns;
            // TODO: Consider injecting the database connection
            database = new DatabaseConnection();
        }

        public delegate T MapToElement(MySqlDataReader reader);
        public List<T> GetAll(MapToElement mapFunction)
        {
            List<T> result = new List<T>();
            MySqlConnection con = database.GetConnection();
            try
            {
                con.Open();

                CommandBuilder commandBuilder = new CommandBuilder("SELECT * FROM " + table);

                commandBuilder.CreateCommand(con);

                MySqlDataReader reader = commandBuilder.Command.ExecuteReader();
                result = readAllResults(reader, mapFunction);
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

            return result;
        }
        public bool Delete_Multiple(List<int> ids)
        {
            MySqlConnection con = database.GetConnection();
            try
            {
                CommandBuilder commandBuilder = new CommandBuilder("DELETE FROM " + table + " WHERE (id) IN ");

                commandBuilder.AddValuesToInsert(new List<List<string>>
                {
                    ids.Select(id => id.ToString()).ToList()
                });

                con.Open();

                commandBuilder.CreateCommand(con);
                commandBuilder.Command.CommandType = CommandType.Text;

                bool querySuccess = commandBuilder.Command.ExecuteNonQuery() > 0;

                return querySuccess;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                con.Close();
            }
            return false;
        }

        public delegate List<string> MapToStrings(T element);
        public List<int> Insert_Multiple(List<T> elements, MapToStrings mapFunction)
        {
            MySqlConnection con = database.GetConnection();
            try
            {
                CommandBuilder commandBuilder = new CommandBuilder("INSERT INTO " + table);
                commandBuilder.AddInsertParameters(columns);

                List<List<string>> rows = new List<List<string>>();
                foreach (T element in elements)
                {
                    rows.Add(mapFunction(element));
                }

                commandBuilder.AddValuesToInsert(rows);
                con.Open();

                commandBuilder.CreateCommand(con);
                commandBuilder.Command.CommandType = CommandType.Text;

                bool querySuccess = commandBuilder.Command.ExecuteNonQuery() > 0;

                // Get the id of the first element inserted using bulk insert
                int lastInsertedId = (int)commandBuilder.Command.LastInsertedId;

                // Calculate the others based in auto increment
                List<int> ids = new List<int>();
                for (int i = lastInsertedId; i < lastInsertedId + elements.Count; i++)
                {
                    ids.Add(i);
                }

                Console.WriteLine("Success: " + querySuccess.ToString());
                return ids;
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

        public List<T> Select_Multiple(string conditions, MapToElement mapFunction, uint limit = 100)
        {
            List<T> result = new List<T>();
            MySqlConnection con = database.GetConnection();
            try
            {
                string query = "SELECT * FROM " + table + " WHERE";
                CommandBuilder commandBuilder = new CommandBuilder(query);

                Dictionary<string, string> conditionDictionary = ParseConditionsToDictionary(conditions);
                commandBuilder.AddLikeParameters(conditionDictionary);
                commandBuilder.AddLimit(limit);
                commandBuilder.CreateCommand(con);

                con.Open();

                MySqlDataReader reader = commandBuilder.Command.ExecuteReader();
                result = readAllResults(reader, mapFunction);
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
            return result;
        }

        private List<T> readAllResults(MySqlDataReader reader, MapToElement map)
        {
            List<T> result = new List<T>();
            while (reader.Read())
            {
                result.Add(map(reader));
            }

            return result;
        }

        public void Update_Multiple(List<T> elements, MapToStrings mapFunction)
        {
            MySqlConnection con = database.GetConnection();
            try
            {
                List<string> bagParameters = new List<string>() { "id" };
                bagParameters.AddRange(columns);

                CommandBuilder commandBuilder = new CommandBuilder("INSERT INTO " + table);
                commandBuilder.AddInsertParameters(bagParameters);

                List<List<string>> rows = new List<List<string>>();
                foreach (T element in elements)
                {
                    rows.Add(mapFunction(element));
                }

                commandBuilder.AddValuesToInsert(rows);
                commandBuilder.UpdateDuplicateKeys();

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
                    throw new ArgumentException("Conditions must be of the form 'ConditionName:Value'");
                }

                string rowName = items[0].Trim();
                string rowValue = items[1].Trim();

                conditionDictionary.Add(rowName, rowValue);
            }

            return conditionDictionary;
        }
    }
}
