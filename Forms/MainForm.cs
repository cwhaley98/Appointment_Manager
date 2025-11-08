using Appointment_Manager.Controller;
using Appointment_Manager.Controller.Utils;
using Appointment_Manager.Model.Database;
using Appointment_Manager.Model.Enums;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Appointment_Manager.Forms
{
    public partial class MainForm : Form
    {
        private AppointmentController appointmentController = new AppointmentController();

        private UserController userController = new UserController();

        private CustomerController customerController = new CustomerController();

        private DataGridViewRow selectedRow = null;

        private FormState formState = FormState.Customers; // Defaults to Customers tab

        private int selectedMonth = DateTime.Now.Month; // Defaults to current month

        private int selectedYear = DateTime.Now.Year; // and current year

        private string selectedUserId = UserSessions.CurrentUserId.ToString();

        private string selectedUserName = UserSessions.CurrentUserName;

        private bool isUpdate = false;

        private Form _loginForm;
        public MainForm(Form loginForm)
        {
            InitializeComponent();
            UpdateButtons();
            RefreshTable();
            SetupCustomerDGV();
            RefreshTableSettings();
            _loginForm = loginForm;

            this.FormClosed += new FormClosedEventHandler(MainForm_FormClosed);

            // --- Lambda Event Subscriptions ---

            // Lambda 1: Appointment Types by Month
            appointmentTypesByMonthToolStripMenuItem.Click += (sender, e) =>
            {
                comboBoxMonths.SelectedIndex = selectedMonth - 1;

                HandleReportClick(
                    FormState.TypesByMonth,
                    // The data-fetching logic is now encapsulated in (Func<DataTable>)
                    () => appointmentController.GetAppointmentTypesByMonth(selectedMonth, selectedYear),
                    false
                );
            };

            // Lambda 2: Consultant Schedules
            consultantSchedulesToolStripMenuItem.Click += (sender, e) =>
            {
                HandleReportClick(
                    FormState.Constultants,
                    () => appointmentController.GetConsultantSchedules(selectedUserName, selectedUserId)
                );
            };

            // Lambda 3: Appointment Per Location
            appointmentPerLocationToolStripMenuItem.Click += (sender, e) =>
            {
                HandleReportClick(
                    FormState.CountPerLocation,
                    () => appointmentController.GetAppointmentCountPerLocation(),
                    // Pass false to skip SetupAppointmentDGV since it's not needed here
                    false
                );
            };


            // --- End Lambda Event Subscriptions ---
        }

        #region EventHandlers

        private void add_btn_Click(object sender, EventArgs e)
        {
            isUpdate = false;
            if (formState == FormState.Customers)
            {
                var addCustomerForm = new CustomerForm();
                addCustomerForm.UpdateCustomerTitle(isUpdate);
                addCustomerForm.RefreshHandler = (didUpdate) =>
                {
                    RefreshTable();
                    RefreshTableSettings();
                    GiveUserFeedback(didUpdate);
                };
                addCustomerForm.Show();

            }
            else if (formState == FormState.Appointments)
            {
                var addAppointmentForm = new AppointmentForm();
                addAppointmentForm.UpdateAppointmentTitle(isUpdate);
                addAppointmentForm.Show();
            }
        }

        private void update_btn_Click(object sender, EventArgs e)
        {
            if (selectedRow == null)
            {
                MessageBox.Show("Please select a row to update.");
                return;
            }
            isUpdate = true;
            if (formState == FormState.Customers)
            {
                var updateCustomerForm = new CustomerForm();
                updateCustomerForm.UpdateCustomerTitle(isUpdate);
                updateCustomerForm.RefreshHandler = (didUpdate) =>
                {
                    RefreshTable();
                    RefreshTableSettings();
                    GiveUserFeedback(didUpdate);
                };
                updateCustomerForm.PopulateCustomerFields(selectedRow);
                updateCustomerForm.Show();
            }
            else if (formState == FormState.Appointments)
            {
                var updateAppointmentForm = new AppointmentForm();
                updateAppointmentForm.UpdateAppointmentTitle(isUpdate);
                updateAppointmentForm.PopulateAppointmentFields(selectedRow);
                updateAppointmentForm.Show();
            }
        }

        private void delete_btn_Click(object sender, EventArgs e)
        {
            if (selectedRow != null)
            {
                if (!ConfirmDelettion())
                {
                    return;
                }
                if (formState == FormState.Customers)
                {
                    string customerId = selectedRow.Cells["CustomerId"].Value.ToString();
                    customerController.DeleteCustomer(customerId);
                }
                else if (formState == FormState.Appointments)
                {
                    string appointmentId = selectedRow.Cells["AppointmentId"].Value.ToString();
                    appointmentController.DeleteAppointment(appointmentId);
                }

                RefreshTable();
                RefreshTableSettings();

                errorProvider.Clear();
                successProvider.SetError(feedbackLabel, "!");
                feedbackLabel.Text = "Deletion successful.";
            }
            else
            {
                successProvider.Clear();
                errorProvider.SetError(feedbackLabel, "!");
                feedbackLabel.Text = "Please select a row to delete.";
            }
            selectedRow = null;
        }

        private void CustomersTab_Click(object sender, EventArgs e)
        {
            formState = FormState.Customers;
            UpdateButtons();
            RefreshTable();
            RefreshTableSettings();
            SetupCustomerDGV();
        }

        private void AppointmentsTab_Click(object sender, EventArgs e)
        {
            formState = FormState.Appointments;
            UpdateButtons();
            RefreshTable();
            RefreshTableSettings();
            SetupAppointmentDGV();

            //Explicitly show calendarView when navigating to Appointments tab and load current days appointments by default
            calendarView.Visible = true;
        }

        private void calendarView_DateSelected(object sender, DateRangeEventArgs e)
        {
            DateTime selectedDate = e.Start;
            LoadDailyAppointments(selectedDate);
        }

        private void mainDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int indexSelected = e.RowIndex;
            if (indexSelected < 0) { return; } // Error handlder for header clicks
            selectedRow = mainDGV.Rows[indexSelected];
        }

        private void mainDGV_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            mainDGV.ClearSelection(); //Clear any selected rows after data binding
        }

        private void comboBoxMonths_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedMonth = comboBoxMonths.SelectedIndex + 1; // Adjust for zero-based index
        }

        private void comboBoxConsultants_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedUserId = ((KeyValuePair<int, string>)comboBoxConsultants.SelectedItem).Key.ToString();
            selectedUserName = ((KeyValuePair<int, string>)comboBoxConsultants.SelectedItem).Value;
        }

        private void viewReport_btn_Click(object sender, EventArgs e)
        {
            // Define the data-fetching function based on the current form state
            Func<DataTable> getDataFunction = null;
            bool requiresSetupDGV = true;

            switch (formState)
            {
                case FormState.TypesByMonth:
                    // This is the specific logic for the TypesByMonth report
                    getDataFunction = () => appointmentController.GetAppointmentTypesByMonth(selectedMonth, selectedYear);
                    requiresSetupDGV = false;
                    break;

                case FormState.Constultants:
                    // This is the specific logic for the Consultant Schedules report
                    getDataFunction = () => appointmentController.GetConsultantSchedules(selectedUserName, selectedUserId);
                    requiresSetupDGV = true;
                    break;

                case FormState.CountPerLocation:
                    // This is the specific logic for the Count Per Location report
                    getDataFunction = () => appointmentController.GetAppointmentCountPerLocation();
                    requiresSetupDGV = false;
                    break;

                default:
                    // If the button is clicked when not on a report state (shouldn't happen if buttons are hidden correctly)
                    return;
            }

            // Common Logic for all reports (Execute the delegated function)
            if (getDataFunction != null)
            {
                mainDGV.DataSource = getDataFunction.Invoke();
                if (requiresSetupDGV)
                {
                    SetupAppointmentDGV();
                }
            }
        }

        #endregion

        #region HelperMethods

        private void UpdateCalendarBoldedDates()
        {
            // 1. Clear all previous bolded dates
            calendarView.RemoveAllBoldedDates();

            // 2. Get the new list of dates from the controller
            List<DateTime> appointmentDates = appointmentController.GetDatesWithAppointments();

            // 3. Add them to the calendar as an array
            if (appointmentDates.Any())
            {
                calendarView.BoldedDates = appointmentDates.ToArray();
            }

            // 4. Refresh the calendar control to show the changes
            calendarView.UpdateBoldedDates();
        }

        private void LoadDailyAppointments(DateTime date)
        {
            mainDGV.DataSource = appointmentController.GetAppointmentsByDay(date);
            SetupAppointmentDGV();
        }

        private void HandleReportClick(FormState newState, Func<DataTable> getDataFunction, bool callSetupDGV = true)
        {
            // 1. Logic common to all reports
            formState = newState;
            UpdateButtons();
            RefreshTableSettings();

            // 2. Report-specific logic (Data retrieval)
            mainDGV.DataSource = getDataFunction.Invoke();

            // 3. Optional DGV Setup (only needed for Appointments/Consultants)
            if (callSetupDGV)
            {
                SetupAppointmentDGV();
            }
        }

        public void RefreshTable()
        {
            if (formState == FormState.Appointments)
            {
                mainDGV.DataSource = appointmentController.GetAppointments();
                SetupAppointmentDGV();
                UpdateCalendarBoldedDates();
            }
            else if (formState == FormState.Customers)
            {
                mainDGV.DataSource = customerController.GetAllCustomers(DBQueries.GetCustomerTableQuery);
                SetupCustomerDGV();
            }
        }

        public void RefreshTableSettings()
        {
            if (formState == FormState.Customers)
            {
                CustomersTab.ForeColor = Color.Blue;
                AppointmentsTab.ForeColor = Color.Black;
                ReportsMenuItem.ForeColor = Color.Black;
            }
            else if (formState == FormState.Appointments)
            {
                AppointmentsTab.ForeColor = Color.Blue;
                CustomersTab.ForeColor = Color.Black;
                ReportsMenuItem.ForeColor = Color.Black;
            }
            else
            {
                ReportsMenuItem.ForeColor = Color.Blue;
                CustomersTab.ForeColor = Color.Black;
                AppointmentsTab.ForeColor = Color.Black;
            }

            //Resize columns to fit current view
            mainDGV.ReadOnly = true;
            mainDGV.MultiSelect = false;
            mainDGV.AllowUserToAddRows = false;
            mainDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            mainDGV.AutoResizeColumns();
            mainDGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            mainDGV.ClearSelection();
            successProvider.Clear();
            errorProvider.Clear();
            feedbackLabel.Text = string.Empty;
        }

        private bool ConfirmDelettion()
        {
            var confirmResult = MessageBox.Show("Are you sure you want to delete the selected record?", "Confirm Deletion", MessageBoxButtons.YesNo);
            return confirmResult == DialogResult.Yes ? true : false;
        }

        public void GiveUserFeedback(bool isUpdate)
        {
            successProvider.Clear();
            errorProvider.Clear();
            feedbackLabel.Text = string.Empty;
            if (isUpdate)
            {
                successProvider.SetError(feedbackLabel, "!");
                feedbackLabel.Text = "Update successful.";
            }
            else
            {
                successProvider.SetError(feedbackLabel, "!");
                feedbackLabel.Text = "Addition successful.";
            }
        }

        private void UpdateButtons()
        {
            add_btn.Show();
            update_btn.Show();
            delete_btn.Show();
            viewReport_btn.Hide();

            monthsLabel.Visible = false;
            consultantsLabel.Visible = false;

            comboBoxMonths.Visible = false;
            comboBoxConsultants.Visible = false;

            calendarView.Visible = false;
            showAll_btn.Hide();

            switch (formState)
            {
                case FormState.Customers:
                    add_btn.Text = "Add Customer";
                    update_btn.Text = "Update Customer";
                    delete_btn.Text = "Delete Customer";

                    break;

                case FormState.Appointments:
                    add_btn.Text = "Add Appointment";
                    update_btn.Text = "Update Appointment";
                    delete_btn.Text = "Delete Appointment";

                    calendarView.Visible = true;
                    showAll_btn.Show();

                    break;

                default:
                    add_btn.Hide();
                    update_btn.Hide();
                    delete_btn.Hide();
                    viewReport_btn.Show();
                    mainDGV.DataSource = null;

                    if (formState == FormState.TypesByMonth)
                    {
                        comboBoxMonths.Visible = true;
                        monthsLabel.Visible = true;
                    }
                    else if (formState == FormState.Constultants)
                    {
                        comboBoxConsultants.Visible = true;
                        consultantsLabel.Visible = true;
                        var users = userController.GetUsersAsList(DBQueries.GetUsersListQuery);
                        comboBoxConsultants.DataSource = new BindingSource(users, null);
                    }
                    else
                    {
                        viewReport_btn.Visible = false;
                        consultantsLabel.Visible = false;
                        monthsLabel.Visible = false;
                    }
                    break;
            }
        }

        private void SetupCustomerDGV()
        {
            //Setting Customer DGV specific properties and titles
            mainDGV.Columns["customerId"].HeaderText = "Customer ID";
            mainDGV.Columns["customerName"].HeaderText = "Customer Name";
            mainDGV.Columns["address"].HeaderText = "Address";
            mainDGV.Columns["phone"].HeaderText = "Phone";
            mainDGV.Columns["city"].HeaderText = "City";
            mainDGV.Columns["postalCode"].HeaderText = "Postal Code";
            mainDGV.Columns["country"].HeaderText = "Country";

            mainDGV.Columns["customerId"].Visible = false;
            mainDGV.Columns["addressId"].Visible = false;
            mainDGV.Columns["cityId"].Visible = false;
            mainDGV.Columns["countryId"].Visible = false;
        }

        private void SetupAppointmentDGV()
        {
            mainDGV.Columns["userName"].HeaderText = "Consultant";
            mainDGV.Columns["customerName"].HeaderText = "Customer Name";
            mainDGV.Columns["description"].HeaderText = "description";
            mainDGV.Columns["type"].HeaderText = "Visit Type";
            mainDGV.Columns["location"].HeaderText = "Location";
            mainDGV.Columns["start"].HeaderText = "Start";
            mainDGV.Columns["end"].HeaderText = "End";
            mainDGV.Columns["phone"].HeaderText = "Phone";
            mainDGV.Columns["url"].HeaderText = "url";

            mainDGV.Columns["customerId"].Visible = false;
            mainDGV.Columns["addressId"].Visible = false;
            mainDGV.Columns["cityId"].Visible = false;
            mainDGV.Columns["countryId"].Visible = false;

            mainDGV.Columns["appointmentId"].Visible = false;
            mainDGV.Columns["userId"].Visible = false;

            mainDGV.Columns["customerName"].DisplayIndex = 0;
            mainDGV.Columns["userName"].DisplayIndex = 1;
        }
        #endregion

        private void LogOutMenuItem_Click(object sender, EventArgs e)
        {
            _loginForm.Show(); // Show the original, hidden login form
            this.Close();     // Close this MainForm
        }

        // This handles the user clicking the "X" button
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // If the login form is NOT visible, it means the user clicked "X"
            // and didn't log out. We should exit the whole app.
            if (!_loginForm.Visible)
            {
                // This closes the hidden Login form, which exits the application
                _loginForm.Close();
            }
        }

        private void showAll_btn_Click(object sender, EventArgs e)
        {
            RefreshTable();
        }
    }
}