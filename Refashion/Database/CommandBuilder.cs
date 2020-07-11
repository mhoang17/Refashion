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
        //private List<string> parameters = new List<string>();
        private Dictionary<string, string> parameters = new Dictionary<string, string>();

        public CommandBuilder(string baseCommand)
        {
            Query = new StringBuilder(baseCommand);
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

        public void AddLimit(uint limit)
        {
            Query.Append(" LIMIT " + limit.ToString());
        }

        public void CreateCommand(MySqlConnection connection)
        {
            Command = new MySqlCommand(Query.ToString(), connection);
            addParameterValues();
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
            Query.Append(" (");

            // Add all parameters, with correctly placed commas
            Query.Append(parameters[0]);
            for (int i = 1; i < parameters.Count; i++)
            {
                Query.Append("," + parameters[i]);
            }
            Query.Append(") VALUES ");

        }

        public void AddValuesToInsert(Dictionary<string, List<string>> parameters)
        {
            // Create format string
            StringBuilder formatString = new StringBuilder("(");
            List<string> formatParameters = new List<string>();
            for (int i = 0; i < parameters.Keys.Count; i++)
            {
                formatParameters.Append("{" + i + "}");
            }
            // Remove the last comma
            formatString.Append(string.Join(",", formatParameters));
            formatString.Append(")");

            // Transform parameterValues into nested list of strings and transpose
            List<List<string>> parameterLists = parameters.Values.ToList();

            // Each resulting list contains the values of all colums of a single row
            List<List<string>> result = parameterLists
                .SelectMany(inner => inner.Select((item, index) => new { item, index }))
                .GroupBy(i => i.index, i => i.item)
                .Select(g => g.ToList())
                .ToList();


            List<string> rows = new List<string>();
            foreach (List<string> row in result)
            {
                rows.Add(string.Format("('{0}','{1}','{2}','{3}','{4}','{5}',{6})", row.ToArray()));
            }

            Query.Append(string.Join(",", rows));
            Query.Append(";");
            /**/
        }

        public void AddValuesToInsert(List<List<string>> rowValues)
        {
            if(rowValues.Count < 1)
            {
                return;
            }

            // Create format string
            StringBuilder formatString = new StringBuilder("(");
            List<string> formatParameters = new List<string>();
            for (int i = 0; i < rowValues[0].Count; i++)
            {
                formatParameters.Add("'{" + i + "}'");
            }
            // Remove the last comma
            formatString.Append(string.Join(",", formatParameters));
            formatString.Append(")");

            List<string> rows = new List<string>();
            foreach (List<string> row in rowValues)
            {
                rows.Add(string.Format(formatString.ToString(), row.ToArray()));
            }

            Query.Append(string.Join(",", rows));
            Query.Append(";");
            /**/
        }


        public void UpdateDuplicateKeys()
        {

        }
    }
}
