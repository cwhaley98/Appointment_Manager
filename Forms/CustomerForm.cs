using Appointment_Manager.Model.Entities;
using Appointment_Manager.Controller;
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
                MessageBox.Show("Please fill in all fields.");
                return;
            }
            var customerData = new Dictionary<string, string>
            {
                { "CustomerName", name_textbox.Text },
                { "CustomerAddress", address_textbox.Text },
                { "CustomerCity", city_textbox.Text },
                { "CustomerPostal", postalcode_textbox.Text },
                { "CustomerCountry", country_textbox.Text },
                { "CustomerPhone", phone_textbox.Text },
                { "AddressId", addressId },
                { "CustomerId", customerId },
                { "CityId", cityId },
                { "CountryId", countryId }
            };

            customerController.SaveCustomer(customerData, IsUpdate);
            RefreshHandler?.Invoke(IsUpdate);

            this.Hide();
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            // We can still call RefreshHandler on cancel if the user needs to see the main form settings restored.
            RefreshHandler?.Invoke(false);
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
            name_textbox.Text = row.Cells["Customer Name"].Value.ToString();
            address_textbox.Text = row.Cells["Address"].Value.ToString();
            city_textbox.Text = row.Cells["City"].Value.ToString();
            postalcode_textbox.Text = row.Cells["Postal Code"].Value.ToString();
            country_textbox.Text = row.Cells["Country"].Value.ToString();
            phone_textbox.Text = row.Cells["Phone"].Value.ToString();

            //Set IDs
            addressId = row.Cells["AddressId"].Value.ToString();
            customerId = row.Cells["CustomerId"].Value.ToString();
            cityId = row.Cells["CityId"].Value.ToString();
            countryId = row.Cells["CountryId"].Value.ToString();
        }

        private bool FieldsAreValid() => Validation.ValidateTextBox(name_textbox, "string", errorProvider)
                && Validation.ValidateTextBox(address_textbox, "string", errorProvider)
                && Validation.ValidateTextBox(city_textbox, "string", errorProvider)
                && Validation.ValidateTextBox(postalcode_textbox, "string", errorProvider)
                && Validation.ValidateTextBox(country_textbox, "string", errorProvider)
                && Validation.ValidateTextBox(phone_textbox, "string", errorProvider);


        #endregion

        #region TextChange Validation
        private void name_textbox_TextChanged(object sender, EventArgs e)
        {
            save_btn.Enabled = Validation.ValidateTextBox(name_textbox, "string", errorProvider);
        }

        private void address_textbox_TextChanged(object sender, EventArgs e)
        {
            save_btn.Enabled = Validation.ValidateTextBox(address_textbox, "string", errorProvider);
        }

        private void city_textbox_TextChanged(object sender, EventArgs e)
        {
            save_btn.Enabled = Validation.ValidateTextBox(city_textbox, "string", errorProvider);
        }

        private void postalcode_textbox_TextChanged(object sender, EventArgs e)
        {
            save_btn.Enabled = Validation.ValidateTextBox(postalcode_textbox, "string", errorProvider);
        }

        private void country_textbox_TextChanged(object sender, EventArgs e)
        {
            save_btn.Enabled = Validation.ValidateTextBox(country_textbox, "string", errorProvider);
        }

        private void phone_textbox_TextChanged(object sender, EventArgs e)
        {
            save_btn.Enabled = Validation.ValidateTextBox(phone_textbox, "string", errorProvider);
        }
        #endregion
    }
}
