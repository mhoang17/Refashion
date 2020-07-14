using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        //private List<string> parameters = new List<string>();
        private Dictionary<string, string> parameters = new Dictionary<string, string>();

        public CommandBuilder(string baseCommand)
        {
            Query = new StringBuilder(baseCommand);
        }

        public void CreateCommand(MySqlConnection connection)
        {
            Query.Append(";");
            Command = new MySqlCommand(Query.ToString(), connection);
            addParameterValues();
        }

        public void AddEqualsParameters(Dictionary<string, string> parameters)
        {
            foreach (KeyValuePair<string, string> parameter in parameters)
            {
                AddEqualsParameter(parameter.Key, parameter.Value);
            }
        }

        // Adds an equal parameter to query
        // Might be vulnerable to SQL injection
        public void AddEqualsParameter(string parameterName, string parameterValue)
        {
            // Add parameter to query
            AddParameter("=", parameterName);
            // Save parameter name and value
            parameters.Add(parameterName, parameterValue);
        }

        private void addParameterValues()
        {
            foreach (KeyValuePair<string, string> parameter in parameters)
            {
                Command.Parameters.AddWithValue(parameter.Key, parameter.Value);
            }
        }

        // Almost same logic as equal parameters
        public void AddLikeParameters(Dictionary<string, string> parameters)
        {
            foreach (KeyValuePair<string, string> parameter in parameters)
            {
                AddLikeParameter(parameter.Key, parameter.Value);
            }
        }

        public void AddLikeParameter(string parameterName, string parameterValue)
        {
            // Add parameter to query
            AddParameter("LIKE", parameterName);
            // Save parameter name and value
            parameters.Add(parameterName, "%" + parameterValue + "%");
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
        }

        public void AddInsertParameters(List<string> parameters)
        {
            if(parameters.Count < 1)
            {
                return;
            }
            Query.Append(" (");

            Query.Append(string.Join(",", parameters));

            Query.Append(") VALUES ");
        }

        // Needs to be renamed as i can also be used in DELETE operations
        public void AddValuesToInsert(List<List<string>> rowValues)
        {
            if(rowValues.Count < 1)
            {
                return;
            }

            string formatString = createFormatString(rowValues[0].Count);

            List<string> rows = new List<string>();
            foreach (List<string> row in rowValues)
            {
                rows.Add(string.Format(formatString, row.ToArray()));
            }

            Query.Append(string.Join(",", rows));
        }

        private string createFormatString(int numberOfParameters)
        {
            StringBuilder formatString = new StringBuilder("(");
            List<string> formatParameters = new List<string>();
            for (int i = 0; i < numberOfParameters; i++)
            {
                formatParameters.Add("'{" + i + "}'");
            }
            formatString.Append(string.Join(",", formatParameters));
            formatString.Append(")");

            return formatString.ToString();
        }

        public void UpdateDuplicateKeys()
        {

        }

        public void AddLimit(uint limit)
        {
            Query.Append(" LIMIT " + limit.ToString());
        }
    }
}
