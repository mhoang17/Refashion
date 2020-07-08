using MySql.Data.MySqlClient;
using System.Configuration;

namespace Refashion.Database
{
    public class DatabaseConnection
    {
        private string connectionString;
        
        public DatabaseConnection()
        {
            // Get "Test" connectionString from app.config or use fallback
            connectionString = ConfigurationManager.ConnectionStrings["Test"] != null ? 
                ConfigurationManager.ConnectionStrings["Test"].ConnectionString :
                "server=localhost;userid=devUser;password=devpass;database=refashion;Allow User Variables=True";
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
