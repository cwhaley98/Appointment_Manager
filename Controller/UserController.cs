using Appointment_Manager.Model.Database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_Manager.Controller
{
    public class UserController
    {
        public Dictionary<int, string> GetUserNames()
        {
            // Retrieve user IDs and names from the database
            Dictionary<int, string> userNames = new Dictionary<int, string>();

            try
            {
                DBConnection.InitializeDatabase();

                using (var cmd = new MySqlCommand(DBQueries.GetUsersQuery, DBConnection.dbconnection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int userId = reader.GetInt32("User_ID");
                        string userName = reader.GetString("User_Name");
                        userNames.Add(userId, userName);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error)
                Console.WriteLine($"Error retrieving user names: {ex.Message}");
            }
            return userNames;
        }

        //Consultant Schedules
        public DataTable GetConsultantSchedule(string userName, string userId)
        {
            DataTable dt = new DataTable();
            try
            {
                DBConnection.InitializeDatabase();
                using (MySqlCommand consultantCMD = new MySqlCommand(DBQueries.GetUserSchedule, DBConnection.dbconnection))
                {
                    consultantCMD.Parameters.AddWithValue("@userName", userName);
                    consultantCMD.Parameters.AddWithValue("@userId", userId);
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(consultantCMD))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " - Error retrieving consultant schedule.");
            }
            foreach (DataRow row in dt.Rows)
            {
                if (row["start"] is DateTime startUtc)
                {
                    row["start"] = TimeZoneInfo.ConvertTimeFromUtc(startUtc, TimeZoneInfo.Local);
                }
                if (row["end"] is DateTime endUtc)
                {
                    row["end"] = TimeZoneInfo.ConvertTimeFromUtc(endUtc, TimeZoneInfo.Local);
                }
            }
            return dt;
        }
    }
}
