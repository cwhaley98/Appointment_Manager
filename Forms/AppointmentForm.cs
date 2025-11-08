using Appointment_Manager.Controller;
using Appointment_Manager.Controller.Utils;
using Appointment_Manager.Model.Database;
using Appointment_Manager.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Appointment_Manager.Forms
{
    public partial class AppointmentForm : Form
    {
        private AppointmentController appointmentController = new AppointmentController();

        private CustomerController customerController = new CustomerController();

        private UserController userController = new UserController();

        private string appointmentId = string.Empty;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsUpdate { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Action<bool> RefreshHandler { get; set; }

        private struct TimeSlot
        {
            public string DisplayText { get; set; }
            public TimeSpan Value { get; set; }
        }
        public AppointmentForm()
        {
            InitializeComponent();
            LoadFormControls();
        }

        #region EventHandlers

        private void save_btn_Click(object sender, EventArgs e)
        {
            // --- Special check for weekend ---
            DayOfWeek selectedDay = AppointmentDatePicker.Value.DayOfWeek;
            if (selectedDay == DayOfWeek.Saturday || selectedDay == DayOfWeek.Sunday)
            {
                // Show the specific error message IN the popup
                MessageBox.Show("Appointments cannot be scheduled on Saturday or Sunday.",
                                "Invalid Date",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                // Set the error provider on the control
                errorProvider.SetError(AppointmentDatePicker, "Appointments cannot be scheduled on Saturday or Sunday.");
                return; // Stop the save
            }
            // --- END of special check ---

            //Time Conversion
            DateTime datePart = AppointmentDatePicker.Value.Date;
            TimeSpan timePart = (TimeSpan)AppointmentTimeComboBox.SelectedValue;
            DateTime startTimeLocal = datePart + timePart;
            DateTime endTimeLocal = startTimeLocal.AddMinutes(30);

            // Convert to EST *only* for validation
            DateTime startTimeEST = TimeUtil.ConvertToEST(startTimeLocal);
            DateTime endTimeEST = TimeUtil.ConvertToEST(endTimeLocal);

            // Perform the 9-5 EST Business Hours Check
            TimeSpan businessStart = new TimeSpan(9, 0, 0);  // 9:00 AM
            TimeSpan businessEnd = new TimeSpan(17, 0, 0); // 5:00 PM

            if (startTimeEST.TimeOfDay < businessStart || endTimeEST.TimeOfDay > businessEnd)
            {
                MessageBox.Show("The selected time is outside of business hours (9:00 AM - 5:00 PM EST).",
                                "Invalid Time", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                errorProvider.SetError(AppointmentTimeComboBox, "Outside business hours (9:00 AM - 5:00 PM EST).");
                return;
            }

            // Convert to UTC *for database storage*
            DateTime startTimeUTC = TimeUtil.ConvertToUTC(startTimeLocal);
            DateTime endTimeUTC = TimeUtil.ConvertToUTC(endTimeLocal);

            int consultantId = (int)ConsultantComboBox.SelectedValue;

            // Overlap check must now also use UTC
            if (appointmentController.CheckForOverlappingAppointments(consultantId, startTimeUTC, endTimeUTC, appointmentId))
            {
                MessageBox.Show("This appointment overlaps with an existing appointment for this consultant. Please choose a different time.", "Overlap Detected.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Update the data payload to use UTC values
            var appointmentData = new Dictionary<string, object>
            {
                { "AppointmentId", this.appointmentId },
                { "CustomerId", CustomerNameComboBox.SelectedValue },
                { "UserId", ConsultantComboBox.SelectedValue },
                { "Description", DescriptionTextBox.Text.Trim() },
                { "Location", LocationComboBox.SelectedItem.ToString() },
                { "Type", VisitTypeComboBox.SelectedItem.ToString() },
                { "Start", startTimeUTC },
                { "End", endTimeUTC }, 
                { "Title", titleTextBox.Text.Trim() },
                { "Contact", contactTextBox.Text.Trim() },
                { "URL", urlTextBox.Text.Trim() },
            };

            bool success = appointmentController.SaveAppointment(appointmentData, IsUpdate);

            if (success)
            {
                RefreshHandler?.Invoke(IsUpdate);
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to save appointment.", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            RefreshHandler?.Invoke(false);
            this.Close();
        }

        private void AppointmentTimeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Clear error provider
            if (sender is ComboBox comboBox && comboBox.SelectedIndex != -1)
            {
                errorProvider.SetError(comboBox, "");
            }
        }

        private void AppointmentDatePicker_ValueChanged(object sender, EventArgs e)
        {

        }

        private void LocationComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Clear error provider
            if (sender is ComboBox comboBox && comboBox.SelectedIndex != -1)
            {
                errorProvider.SetError(comboBox, "");
            }
        }

        private void VisitTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Clear error provider
            if (sender is ComboBox comboBox && comboBox.SelectedIndex != -1)
            {
                errorProvider.SetError(comboBox, "");
            }
        }

        private void DescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(DescriptionTextBox.Text))
            {
                errorProvider.SetError(DescriptionTextBox, "");
            }
        }

        private void ConsultantComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Clear error provider
            if (sender is ComboBox comboBox && comboBox.SelectedIndex != -1)
            {
                errorProvider.SetError(comboBox, "");
            }
        }

        private void CustomerNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if the sender is a ComboBox and has a valid selection
            if (sender is ComboBox comboBox && comboBox.SelectedIndex != -1)
            {
                // Clear any old validation errors
                errorProvider.SetError(comboBox, "");

                // Get the selected customer's ID
                var selectedCustomer = (KeyValuePair<int, string>)comboBox.SelectedItem;

                int customerId = selectedCustomer.Key;

                // Use the controller to fetch the phone number
                string phone = customerController.GetCustomerPhone(customerId);
                phoneTextBox.Text = phone;
            }
            else
            {
                // Clear the phone box if no customer is selected
                phoneTextBox.Text = "";
            }
        }
        #endregion

        #region HelperMethods

        private void LoadFormControls()
        {

            phoneTextBox.ReadOnly = true;
            AppointmentDatePicker.MinDate = DateTime.Today;

            PopulateTimeComboBox();


            // Populate Location ComboBox from Locations.cs Enum
            var locations = Enum.GetNames(typeof(Locations))
                                .Select(l => l.Replace("_", " "))
                                .ToList();
            LocationComboBox.DataSource = locations;

            // Populate Visit Type ComboBox from VisitTypes.cs Enum
            var visitTypes = Enum.GetNames(typeof(VisitTypes))
                                 .Select(v => v.Replace("_", " "))
                                 .ToList();
            VisitTypeComboBox.DataSource = visitTypes;

            // Populate Customer ComboBox from Database
            var customers = customerController.GetCustomersAsList(DBQueries.GetCustomersListQuery);
            CustomerNameComboBox.DataSource = new BindingSource(customers, null);
            CustomerNameComboBox.DisplayMember = "Value"; // The customerName
            CustomerNameComboBox.ValueMember = "Key";   // The customerId

            // Populate Consultant ComboBox from Database
            var users = userController.GetUsersAsList(DBQueries.GetUsersListQuery);
            ConsultantComboBox.DataSource = new BindingSource(users, null);
            ConsultantComboBox.DisplayMember = "Value"; // The userName
            ConsultantComboBox.ValueMember = "Key";   // The userId


            //Set default (unselected) state
            CustomerNameComboBox.SelectedIndex = -1;
            ConsultantComboBox.SelectedIndex = -1;
            LocationComboBox.SelectedIndex = -1;
            VisitTypeComboBox.SelectedIndex = -1;

            // Wire up validation event handlers
            CustomerNameComboBox.SelectedIndexChanged += CustomerNameComboBox_SelectedIndexChanged;
            ConsultantComboBox.SelectedIndexChanged += ConsultantComboBox_SelectedIndexChanged;
            LocationComboBox.SelectedIndexChanged += LocationComboBox_SelectedIndexChanged;
            VisitTypeComboBox.SelectedIndexChanged += VisitTypeComboBox_SelectedIndexChanged;
            AppointmentTimeComboBox.SelectedIndexChanged += AppointmentTimeComboBox_SelectedIndexChanged;
        }

        /// <summary>
        /// Populates the time ComboBox with 30-min intervals during business hours (9am-5pm).
        /// </summary>
        public void PopulateTimeComboBox()
        {
            var timeSlots = new List<TimeSlot>();
            TimeSpan startTime = new TimeSpan(9, 0, 0);  // 9:00 AM
            TimeSpan endTime = new TimeSpan(17, 0, 0); // 5:00 PM

            while (startTime < endTime)
            {
                TimeSpan slotEnd = startTime.Add(new TimeSpan(0, 30, 0));
                timeSlots.Add(new TimeSlot
                {
                    // Format: "09:00 - 09:30"
                    DisplayText = $"{startTime:hh\\:mm} - {slotEnd:hh\\:mm}",
                    Value = startTime
                });
                startTime = slotEnd;
            }

            AppointmentTimeComboBox.DataSource = timeSlots;
            AppointmentTimeComboBox.DisplayMember = "DisplayText";
            AppointmentTimeComboBox.ValueMember = "Value";
            AppointmentTimeComboBox.SelectedIndex = -1;
        }

        public void UpdateAppointmentTitle(bool isUpdate)
        {
            this.IsUpdate = isUpdate;
            AppointmentFormTitle.Text = isUpdate ? "Update Appointment" : "Add Appointment";
        }

        public void PopulateAppointmentFields(DataGridViewRow row)
        {
            // Get LOCAL time
            DateTime startTimeLocal = (DateTime)row.Cells["start"].Value;

            // Set simple fields
            DescriptionTextBox.Text = row.Cells["description"].Value.ToString();
            LocationComboBox.SelectedItem = row.Cells["location"].Value.ToString();
            VisitTypeComboBox.SelectedItem = row.Cells["yype"].Value.ToString();
            CustomerNameComboBox.SelectedValue = (int)row.Cells["customerId"].Value;
            ConsultantComboBox.SelectedValue = (int)row.Cells["userId"].Value;

            // --- SPLIT DATETIME FOR UI CONTROLS ---
            AppointmentDatePicker.MinDate = new DateTime(1753, 1, 1);
            AppointmentDatePicker.Value = startTimeLocal.Date;

            // Find the matching TimeSpan in the ComboBox items
            // This is safer than using SelectedItem directly
            TimeSpan timeValue = startTimeLocal.TimeOfDay;
            foreach (TimeSlot slot in AppointmentTimeComboBox.Items)
            {
                if (slot.Value == timeValue)
                {
                    AppointmentTimeComboBox.SelectedItem = slot;
                    break;
                }
            }

            appointmentId = row.Cells["appointmentId"].Value.ToString();

        }

        /// <summary>
        /// Validates that all required controls have a selection.
        /// </summary>
        private bool FieldsAreValid()
        {
            bool valid = true;

            // Validate ComboBoxes
            if (CustomerNameComboBox.SelectedIndex == -1)
            {
                errorProvider.SetError(CustomerNameComboBox, "Please select a customer.");
                valid = false;
            }
            if (ConsultantComboBox.SelectedIndex == -1)
            {
                errorProvider.SetError(ConsultantComboBox, "Please select a consultant.");
                valid = false;
            }
            if (LocationComboBox.SelectedIndex == -1)
            {
                errorProvider.SetError(LocationComboBox, "Please select a location.");
                valid = false;
            }
            if (VisitTypeComboBox.SelectedIndex == -1)
            {
                errorProvider.SetError(VisitTypeComboBox, "Please select a visit type.");
                valid = false;
            }
            if (AppointmentTimeComboBox.SelectedIndex == -1)
            {
                errorProvider.SetError(AppointmentTimeComboBox, "Please select a time.");
                valid = false;
            }

            // === OPTIONAL TextBoxes ===
            // Clear any errors from them since they are not required.
            errorProvider.SetError(titleTextBox, "");
            errorProvider.SetError(DescriptionTextBox, "");
            errorProvider.SetError(contactTextBox, "");
            errorProvider.SetError(urlTextBox, "");

            

            return valid;
        }
        #endregion


    }
}
