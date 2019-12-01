using Npgsql;
using System.Configuration;
using System.Data.SqlClient;

namespace Booleans.Tools.Managers
{
    public class ConnectionManager
    {
        private static readonly ConnectionManager instance = new ConnectionManager();

        public string ConnectionString { get; private set; }
        public NpgsqlConnection Connection => Generate();

        private ConnectionManager()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["Booleans"].ConnectionString;
        }

        public static ConnectionManager GetInstance()
        {
            return instance;
        }

        private NpgsqlConnection Generate()
        {
            var conn = new NpgsqlConnection(ConnectionString);
            try
            {
                conn.Open();
                return conn;
            }
            catch (SqlException e)
            {
                System.Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}