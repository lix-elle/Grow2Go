using System;
using MySql.Data.MySqlClient;

namespace Grow2Go.Helpers 
{
    public class DBConnection
    {
        private static readonly string connectionString = "Server=localhost;Port=3306;Database=grow2go;Uid=root;Pwd=grow2go123";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        public static bool TestConnection() 
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection Failed: " + ex.Message);
            }
        }
    }
}