using MySql.Data.MySqlClient;

namespace TaskWebService
{
    public class DBConnection
    {
        public static MySqlConnection CreateConnection()
        {
            return new MySqlConnection("server=localhost;user=root;database=emlployedapper;password=root");
        }
    }
}
