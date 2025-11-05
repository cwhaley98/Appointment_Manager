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
        /// <summary>
        /// Retrieves a list of all users (consultants) to populate ComboBoxes.
        /// </summary>
        /// <returns>A Dictionary where Key is UserId and Value is UserName.</returns>
        public Dictionary<int, string> GetUsersAsList()
        {
            var users = new Dictionary<int, string>();

            using (MySqlConnection connection = DBConnection.GetNewConnection())
            {
                // --- UPDATED ---
                using (MySqlCommand command = new MySqlCommand(DBQueries.GetUsersListQuery, connection))
                {
                    try
                    {
                        connection.Open();
                        MySqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            users.Add(reader.GetInt32("userId"), reader.GetString("userName"));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error retrieving user list: {ex.Message}");
                    }
                }
            }
            return users;
        }

        //Consultant Schedules
        public DataTable GetConsultantSchedule(string userName, string userId)
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection connection = DBConnection.GetNewConnection())
                {

                    using (MySqlCommand consultantCMD = new MySqlCommand(DBQueries.GetUserSchedule, connection))
                    {
                        consultantCMD.Parameters.AddWithValue("@userName", userName);
                        consultantCMD.Parameters.AddWithValue("@userId", userId);
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(consultantCMD))
                        {
                            adapter.Fill(dt);
                        }
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
