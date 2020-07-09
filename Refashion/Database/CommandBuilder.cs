using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refashion.Database
{
    public class CommandBuilder
    {
        public StringBuilder Query { get; private set; }
        public MySqlCommand Command { get; private set; }
        private bool hasFirstParameter = false;
        private List<string> parameters = new List<string>();

        public CommandBuilder(string baseCommand)
        {
            Query = new StringBuilder(baseCommand);
        }

        public void AddEqualsParameters(List<string> parameters)
        {
            foreach (string parameter in parameters)
            {
                AddEqualParameter(parameter);
            }
        }

        // Adds an equal parameter to query
        // Might be vulnerable to SQL injection
        public void AddEqualParameter(string parameterName)
        {
            AddParameter("=", parameterName);
        }

        public void AddLimit(uint limit)
        {
            Query.Append(" LIMIT " + limit.ToString());
        }

        public void CreateCommand(MySqlConnection connection)
        {
            Command = new MySqlCommand(Query.ToString(), connection);
        }

        public void AddEqualsParameterValues(Dictionary<string, string> parameters)
        {
            foreach (KeyValuePair<string, string> parameter in parameters)
            {
                AddEqualsParameterValue(parameter.Key, parameter.Value);
            }
        }
        public void AddEqualsParameterValue(string parameterName, string value)
        {
            Command.Parameters.AddWithValue(parameterName, value);
        }

        // Almost same logic as equal parameters
        public void AddLikeParameters(List<string> parameters)
        {
            foreach (string parameter in parameters)
            {
                AddLikeParameter(parameter);
            }
        }

        public void AddLikeParameter(string parameterName)
        {
            AddParameter("LIKE", parameterName);
        }

        public void AddParameter(string parameterType, string parameterName)
        {
            if (hasFirstParameter)
            {
                // TODO: Add support for AND parameters
                Query.Append(" OR");
            }
            else
            {
                hasFirstParameter = true;
            }

            Query.Append(string.Format(" {0} " + parameterType + " @{0}", parameterName));

            parameters.Add(parameterName);
        }

        public void AddLikeParameterValues(Dictionary<string, string> parameters)
        {
            foreach (KeyValuePair<string, string> parameter in parameters)
            {
                AddLikeParameterValue(parameter.Key, parameter.Value);
            }
        }

        public void AddLikeParameterValue(string parameterName, string value)
        {
            Command.Parameters.AddWithValue(parameterName, "%" + value + "%");
        }
    }
}
