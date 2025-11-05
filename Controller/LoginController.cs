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
                MySqlDataReader reader;
                using (var loginCMD = new MySqlCommand(DBQueries.ValidateUserQuery, connection))
                {
                    loginCMD.Parameters.AddWithValue("@username", username);
                    loginCMD.Parameters.AddWithValue("@password", password);
                    reader = loginCMD.ExecuteReader();
                    if (reader.HasRows)
                    {
                        LoginSuccessful();
                        UserActivity.LogUserActivity(username); //Logs user information
                        UserSessions.CurrentUserName = username; //Sets current user for data logging
                        while (reader.Read())
                        {
                            int userId = reader.GetInt32("UserID");
                            UserSessions.CurrentUserId = userId;
                        }
                        MainForm mainForm = new MainForm();
                        mainForm.Show();
                        return true;
                    }
                    else
                    {
                        LoginFail();
                        return false;
                    }
                }
            }
        }

        // Displays login success and fail messages based on current culture settings
        private void LoginSuccessful()
        {
            if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "en")
            {
                MessageBox.Show("Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "de")
            {
                MessageBox.Show("Anmeldung erfolgreich!", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void LoginFail()
        {
            if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "en")
            {
                MessageBox.Show("Login Failed! Please check your username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "de")
            {
                MessageBox.Show("Anmeldung fehlgeschlagen! Bitte überprüfen Sie Ihren Benutzernamen und Ihr Passwort.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
