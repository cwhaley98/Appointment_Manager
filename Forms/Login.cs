using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Globalization;
using System.Threading;
using System.Drawing;
using Appointment_Manager.Controller;
using Appointment_Manager.Model.Database;
using Appointment_Manager.Controller.Utils;

namespace Appointment_Manager
{
    public partial class Login : Form
    {
        LoginController loginController = new LoginController();
        AppointmentController appointmentController = new AppointmentController();
        public Login()
        {
            InitializeComponent();

            CheckLabelLanguage();

            CheckDBConnection();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void login_btn_Click(object sender, EventArgs e)
        {
            try
            {
                DBConnection.InitializeDatabase();
                string username = username_textbox.Text;
                string password = password_textbox.Text;
                var loginAttempt = loginController.ValidateLogin(username, password);
                if (loginAttempt)
                {
                    //Validate appointments within 15 mins
                    appointmentController.ValidateUpcomingAppointments();
                    this.Hide();
                }
            }
            catch (MySqlException)
            {
                MessageBox.Show("Database connection error.");
            }
            finally
            {
                CheckDBConnection();
            }
        }
        private void exit_btn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void CheckDBConnection()
        {
            bool success = DBConnection.InitializeDatabase();
            if (success)
            {
                dbErrorProvider.Clear();
                dbSuccessProvider.SetError(dbLabel, "!");
                dbLabel.Text = "Database connection verified and initialized.";
            }
            else
            {
                dbSuccessProvider.Clear();
                dbErrorProvider.SetError(dbLabel, "!");
                dbLabel.Text = "Database connection failed. See error message for details.";
            }
        }
        private void CheckLabelLanguage()
        {
            if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "en")
            {
                Username.Text = "Username";
                Password.Text = "Password";
                login_btn.Text = "Login";
                exit_btn.Text = "Exit";
                AMS_Title.Text = "Appointment Management System";
            }

            else if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "de")
            {
                Username.Text = "Benutzername";
                Password.Text = "Passwort";
                login_btn.Text = "Login";
                exit_btn.Text = "Beenden";
                AMS_Title.Text = "Terminverwaltungssystem";
            }
        }

    }
}
