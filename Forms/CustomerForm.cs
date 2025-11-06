using Appointment_Manager.Controller;
using Appointment_Manager.Controller.Utils;
using Appointment_Manager.Model.Entities;
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
    public partial class CustomerForm : Form
    {
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsUpdate { get; set; }
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Action<bool> RefreshHandler { get; set; }

        private CustomerController customerController = new CustomerController();

        private string addressId = string.Empty;
        private string customerId = string.Empty;
        private string cityId = string.Empty;
        private string countryId = string.Empty;
        public CustomerForm()
        {
            InitializeComponent();
        }
        #region EventHandlers
        private void save_btn_Click(object sender, EventArgs e)
        {
            if (!FieldsAreValid())
            {
                MessageBox.Show("Please correct all highlighted fields.");
                return;
            }
            var customerData = new Dictionary<string, string>
            {
                { "CustomerName", name_textbox.Text },
                { "CustomerAddress", address_textbox.Text },
                { "CustomerAddress2", address2_textbox.Text },
                { "CustomerCity", city_textbox.Text },
                { "CustomerPostalCode", postalcode_textbox.Text },
                { "CustomerCountry", country_textbox.Text },
                { "CustomerPhone", phone_textbox.Text },
                { "AddressId", addressId },
                { "CustomerId", customerId },
                { "CityId", cityId },
                { "CountryId", countryId }
            };

            bool saveWasSuccessful = customerController.SaveCustomer(customerData, IsUpdate);
            if (saveWasSuccessful)
            {
                RefreshHandler?.Invoke(IsUpdate);

                this.Close();
            }
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Helper Methods
        public void UpdateCustomerTitle(bool isUpdate)
        {
            CustomerFormTitle.Text = isUpdate ? "Update Customer" : "Add Customer";
            this.IsUpdate = isUpdate;
        }

        public void PopulateCustomerFields(DataGridViewRow row)
        {
            name_textbox.Text = row.Cells["customerName"].Value.ToString();
            address_textbox.Text = row.Cells["address"].Value.ToString();
            address2_textbox.Text = row.Cells["address2"].Value.ToString();
            city_textbox.Text = row.Cells["city"].Value.ToString();
            postalcode_textbox.Text = row.Cells["postalCode"].Value.ToString();
            country_textbox.Text = row.Cells["country"].Value.ToString();
            phone_textbox.Text = row.Cells["phone"].Value.ToString();

            //Set IDs
            addressId = row.Cells["addressId"].Value.ToString();
            customerId = row.Cells["customerId"].Value.ToString();
            cityId = row.Cells["cityId"].Value.ToString();
            countryId = row.Cells["countryId"].Value.ToString();
        }

        private bool FieldsAreValid()
        {
            // This prevents short-circuiting and forces every validation to run.
            bool valid = true;
            valid &= FormValidations.ValidateTextBox(name_textbox, "string", errorProvider);
            valid &= FormValidations.ValidateTextBox(address_textbox, "string", errorProvider);
            valid &= FormValidations.ValidateTextBox(city_textbox, "string", errorProvider);
            valid &= FormValidations.ValidateTextBox(postalcode_textbox, "string", errorProvider);
            valid &= FormValidations.ValidateTextBox(country_textbox, "string", errorProvider);
            valid &= FormValidations.ValidateTextBox(phone_textbox, "phone", errorProvider);

            return valid;
        }

        #endregion

        #region TextChange Validation
        private void name_textbox_TextChanged(object sender, EventArgs e)
        {
            save_btn.Enabled = FormValidations.ValidateTextBox(name_textbox, "string", errorProvider);
        }

        private void address_textbox_TextChanged(object sender, EventArgs e)
        {
            save_btn.Enabled = FormValidations.ValidateTextBox(address_textbox, "string", errorProvider);
        }

        private void address2_textbox_TextChanged(Object sender, EventArgs e)
        {
        }

        private void city_textbox_TextChanged(object sender, EventArgs e)
        {
            save_btn.Enabled = FormValidations.ValidateTextBox(city_textbox, "string", errorProvider);
        }

        private void postalcode_textbox_TextChanged(object sender, EventArgs e)
        {
            save_btn.Enabled = FormValidations.ValidateTextBox(postalcode_textbox, "string", errorProvider);
        }

        private void country_textbox_TextChanged(object sender, EventArgs e)
        {
            save_btn.Enabled = FormValidations.ValidateTextBox(country_textbox, "string", errorProvider);
        }

        private void phone_textbox_TextChanged(object sender, EventArgs e)
        {
            save_btn.Enabled = FormValidations.ValidateTextBox(phone_textbox, "phone", errorProvider);
        }
        #endregion
    }
}
