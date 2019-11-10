using Npgsql;
using System.Configuration;
using System.Data.SqlClient;

namespace Booleans.Tools.Managers
{
    public class ConnectionManager
    {
        private static readonly ConnectionManager instance = new ConnectionManager();

        public string ConnectionString { get; private set; }
        public NpgsqlConnection Connection { get; private set; }

        private ConnectionManager()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["Booleans"].ConnectionString;
            Connection = new NpgsqlConnection(ConnectionString);
            try
            {
                Connection.Open();
            }
            catch (SqlException e)
            {
                System.Console.WriteLine(e.Message);
            }
        }

        public static ConnectionManager GetInstance()
        {
            return instance;
        }
    }
}
