using Appointment_Manager.Controller.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using Appointment_Manager.Model;
using Appointment_Manager.Model.Database;
using Appointment_Manager.Forms;

namespace Appointment_Manager.Controller
{
    public class LoginController
    {
        public bool ValidateLogin(string username, string password)
        {
            using (MySqlConnection connection = DBConnection.GetNewConnection())
            {
                try
                {
                    connection.Open();
                    MySqlDataReader reader;
                    using (var loginCMD = new MySqlCommand(DBQueries.ValidateUserQuery, connection))
                    {
                        // The query only needs the username, as it's just fetching the user's data
                        loginCMD.Parameters.AddWithValue("@username", username);

                        reader = loginCMD.ExecuteReader();

                        // Use reader.Read() to check if a user was found AND move to that row
                        if (reader.Read())
                        {
                            // A user was found! Check the password.
                            string dbPassword = reader.GetString("password"); // Get password from DB

                            // Compare the user-supplied password with the database password
                            if (password == dbPassword)
                            {
                                // --- SUCCESS! ---
                                // Passwords match. Log them in.
                                LoginSuccessful();
                                UserActivity.LogUserActivity(username);
                                UserSessions.CurrentUserName = username;

                                int userId = reader.GetInt32("userId");
                                UserSessions.CurrentUserId = userId;

                                return true;
                            }
                        }

                        // If we get here, it means one of two things:
                        // 1. The reader.Read() failed (no user found with that name)
                        // 2. The passwords did not match
                        // In either case, the login fails.

                        LoginFail();
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error during login validation: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        // Displays login success and fail messages based on current culture settings
        private void LoginSuccessful()
        {
            string uiLanguage = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            string formatLanguage = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

            if (uiLanguage == "de" || formatLanguage == "de")
            {
                MessageBox.Show("Anmeldung erfolgreich!", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void LoginFail()
        {
            string uiLanguage = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            string formatLanguage = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

            if (uiLanguage == "de" || formatLanguage == "de")
            {
                MessageBox.Show("Anmeldung fehlgeschlagen! Bitte überprüfen Sie Ihren Benutzernamen und Ihr Passwort.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Login Failed! Please check your username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
