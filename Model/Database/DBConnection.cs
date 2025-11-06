using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_Manager.Model.Database
{
    public class DBConnection
    {
        // Helper method to retrieve the connection string securely
        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["MySQLClientSchedule"].ConnectionString;
        }

        // InitializeDatabase now ensures the connection is brand new and automatically closed/disposed.
        public static bool InitializeDatabase()
        {
            bool success = false;
            string connStr = GetConnectionString();

            // This ensures connection.Close() and connection.Dispose() are called, even on exception.
            using (MySqlConnection connection = new MySqlConnection(connStr))
            {
                try
                {
                    connection.Open(); // Open the new connection

                    // 2. Wrap all MySqlCommand objects in 'using' statements for disposal.
                    using (MySqlCommand tableCMD = new MySqlCommand(DBQueries.CheckTablesExistQuery, connection))
                    {
                        int tableCount = Convert.ToInt32(tableCMD.ExecuteScalar());

                        using (MySqlCommand userCMD = new MySqlCommand(DBQueries.GetUserCount, connection))
                        {
                            int userCount = Convert.ToInt32(userCMD.ExecuteScalar());

                            if (tableCount < 6 || userCount == 0)
                            {
                                string[] initQueries = DBQueries.InitializeDatabaseQuery.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                                // Loop and execute each command one by one
                                foreach (string query in initQueries)
                                {
                                    // Trim() removes extra whitespace/newlines.
                                    // The SQL comments (--) will be ignored by the server.
                                    string trimmedQuery = query.Trim();

                                    if (!string.IsNullOrEmpty(trimmedQuery))
                                    {
                                        using (MySqlCommand initCMD = new MySqlCommand(trimmedQuery, connection))
                                        {
                                            initCMD.ExecuteNonQuery();
                                        }
                                    }
                                }
                            }
                            success = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Catch any database or configuration errors
                    MessageBox.Show("Error initializing database: " + ex.Message);
                    success = false;
                }
            }

            return success;
        }

        // A public method to get a new connection for external users.
        public static MySqlConnection GetNewConnection()
        {
            return new MySqlConnection(GetConnectionString());
        }
    }  
}
