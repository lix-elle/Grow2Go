using System;
using Grow2Go.Models;
using Grow2Go.Helpers;
using MySql.Data.MySqlClient;

namespace Grow2Go.Repositories
{
    public class UserRepository
    {
        public bool EmailExists(string email)
        {
            try
            {
                using (var conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    const string query = "SELECT COUNT(*) FROM users WHERE email = @email";
                    var cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@email", email);

                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

        public bool CreateUser(User user)
        {
            try
            {
                using (var conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    const string query = @"
                        INSERT INTO users (name, email, password, role)
                        VALUES (@name, @email, @password, @role)";

                    var cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", user.Name);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    cmd.Parameters.AddWithValue("@role", user.Role);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

        public User GetUserByEmailAndPassword(string email, string password)
        {
            try
            {
                using (var conn = DBConnection.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM users WHERE email = @email AND password = @password";
                    var cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password", password);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                Id = reader.GetInt32("id"),
                                Name = reader.GetString("name"),
                                Email = reader.GetString("email"),
                                Password = reader.GetString("password"),
                                Role = reader.GetString("role"),
                                CreatedAt = reader.GetDateTime("created_at")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return null;
        }
    }
}
