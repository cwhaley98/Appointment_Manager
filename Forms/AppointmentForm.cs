using Appointment_Manager.Controller;
using Appointment_Manager.Controller.Utils;
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
            //Field Validation
            if (!FieldsAreValid())
            {
                MessageBox.Show("Please select a value for all fields marked with an error.", "Invalid input.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Time Conversion
            DateTime datePart = AppointmentDatePicker.Value.Date;
            TimeSpan timePart = (TimeSpan)AppointmentTimeComboBox.SelectedValue;

            DateTime startTimeLocal = datePart + timePart;
            DateTime endTimeLocal = startTimeLocal.AddMinutes(30); // Makes appointments 30 minutes long

            DateTime startTimeEST = TimeUtil.ConvertToEST(startTimeLocal);
            DateTime endTimeEST = TimeUtil.ConvertToEST(endTimeLocal);

            int consultantId = (int)ConsultantComboBox.SelectedValue;

            if (appointmentController.CheckForOverlappingAppointments(consultantId, startTimeEST, endTimeEST, appointmentId))
            {
                MessageBox.Show("This appointment overlaps with an existing appointment for this consultant. Please choose a different time.", "Overlap Detected.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Create data payload for AppointmentController
            var appointmentData = new Dictionary<string, object>
            {
                { "AppointmentId", this.appointmentId },
                { "CustomerId", CustomerNameComboBox.SelectedValue }, // Assumes ValueMember is CustomerId
                { "UserId", ConsultantComboBox.SelectedValue },     // Assumes ValueMember is UserId
                { "Description", DescriptionTextBox.Text.Trim() },
                { "Location", LocationComboBox.SelectedItem.ToString() },
                { "Type", VisitTypeComboBox.SelectedItem.ToString() },
                { "Start", startTimeEST }, // Pass the converted EST time
                { "End", endTimeEST }      // Pass the converted EST time
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
            // Clear error provider
            if (sender is ComboBox comboBox && comboBox.SelectedIndex != -1)
            {
                errorProvider.SetError(comboBox, "");
            }
        }
        #endregion

        #region HelperMethods

        private void LoadFormControls()
        {
            AppointmentDatePicker.MinDate = DateTime.Today;

            PopulateTimeComboBox();


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
            DateTime startTimeLocal = (DateTime)row.Cells["Start"].Value;

            // Set simple fields
            DescriptionTextBox.Text = row.Cells["Description"].Value.ToString();
            LocationComboBox.SelectedItem = row.Cells["Location"].Value.ToString();
            VisitTypeComboBox.SelectedItem = row.Cells["Type"].Value.ToString();
            CustomerNameComboBox.SelectedValue = (int)row.Cells["CustomerId"].Value;
            ConsultantComboBox.SelectedValue = (int)row.Cells["UserId"].Value;

            // --- SPLIT DATETIME FOR UI CONTROLS ---
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

            // Validate TextBox
            if (string.IsNullOrWhiteSpace(DescriptionTextBox.Text))
            {
                errorProvider.SetError(DescriptionTextBox, "Description cannot be empty.");
                valid = false;
            }

            return valid;
        }
        #endregion


    }
}
