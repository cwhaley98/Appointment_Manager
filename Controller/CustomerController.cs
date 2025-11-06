using Appointment_Manager.Model.Database;
using Appointment_Manager.Controller.Utils;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Appointment_Manager.Controller
{
    public class CustomerController
    {
        #region Add, Update, Delete Customers

        public bool SaveCustomer(Dictionary<string, string> customerData,  bool isUpdate)
        {
            using (MySqlConnection connection = DBConnection.GetNewConnection())
            try
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int countryId = SaveCountryData(customerData, connection, isUpdate);
                        int cityId = SaveCityData(customerData, connection, countryId, isUpdate);
                        int addressId = SaveAddressData(customerData, cityId, connection, isUpdate);
                        SaveCustomerData(customerData, addressId, connection, isUpdate);

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving customer data: {ex.Message}");
                    return false;
            }
        }

        private int SaveCountryData(Dictionary<string, string> customerData, MySqlConnection connection, bool isUpdate)
        {
            // Implementation for saving country data
            // Return the country ID
            int countryId;
            string query;

            if (isUpdate)
            {
                countryId = int.Parse(customerData["CountryId"]);
                query = DBQueries.UpdateCountryQuery;
            }
            else
            {
                using (var countryIndexCMD = new MySqlCommand(DBQueries.GetCountryIdxQuery, connection))
                {
                    countryId = GetNewId(DBQueries.GetCountryIdxQuery, connection);
                    query = DBQueries.InsertCountryQuery;
                }
            }

            using (var countryInsertCMD = new MySqlCommand(query, connection))
            {
                countryInsertCMD.Parameters.AddWithValue("@CountryId", countryId);
                countryInsertCMD.Parameters.AddWithValue("@Country", customerData["CustomerCountry"]);
                countryInsertCMD.Parameters.AddWithValue("@LastUpdateBy", UserSessions.CurrentUserName);
                if (!isUpdate)
                {
                    countryInsertCMD.Parameters.AddWithValue("@CreatedBy", UserSessions.CurrentUserName);
                }

                countryInsertCMD.Prepare();
                countryInsertCMD.ExecuteNonQuery();

            }

            return countryId;
        }

        private int SaveCityData(Dictionary<string, string> customerData, MySqlConnection connection, int currentCountryId, bool isUpdate)
        {
            int cityId;
            string query;

            if (isUpdate)
            {
                cityId = int.Parse(customerData["CityId"]);
                query = DBQueries.UpdateCityQuery;
            }
            else
            {
                using (var cityIndexCMD = new MySqlCommand(DBQueries.GetCityIdxQuery, connection))
                {
                    cityId = GetNewId(DBQueries.GetCityIdxQuery, connection);
                    query = DBQueries.InsertCityQuery;
                }
            }

            using (var cityInsertCMD = new MySqlCommand(query, connection))
            {
                cityInsertCMD.Parameters.AddWithValue("@CityId", cityId);
                cityInsertCMD.Parameters.AddWithValue("@City", customerData["CustomerCity"]);
                cityInsertCMD.Parameters.AddWithValue("@CountryId", currentCountryId);
                cityInsertCMD.Parameters.AddWithValue("@LastUpdateBy", UserSessions.CurrentUserName);
                if (!isUpdate)
                {
                    cityInsertCMD.Parameters.AddWithValue("@CreatedBy", UserSessions.CurrentUserName);
                }
                cityInsertCMD.Prepare();
                cityInsertCMD.ExecuteNonQuery();
            }
            return cityId;
        }

        private int SaveAddressData(Dictionary<string, string> customerData, int currentCityId, MySqlConnection connection, bool isUpdate)
        {
            // Implementation for saving address data
            // Return the address ID
            int addressId;
            string query;
            if (isUpdate)
            {
                addressId = int.Parse(customerData["AddressId"]);
                query = DBQueries.UpdateAddressQuery;
            }
            else
            {
                using (var addressIndexCMD = new MySqlCommand(DBQueries.GetAddressIdxQuery, connection))
                {
                    addressId = GetNewId(DBQueries.GetAddressIdxQuery, connection);
                    query = DBQueries.InsertAddressQuery;
                }
            }
            using (var addressInsertCMD = new MySqlCommand(query, connection))
            {
                addressInsertCMD.Parameters.AddWithValue("@AddressId", addressId);
                addressInsertCMD.Parameters.AddWithValue("@Address", customerData["CustomerAddress"]);
                addressInsertCMD.Parameters.AddWithValue("@Address2", customerData["CustomerAddress2"]);
                addressInsertCMD.Parameters.AddWithValue("@CityId", currentCityId);
                addressInsertCMD.Parameters.AddWithValue("@PostalCode", customerData["CustomerPostalCode"]);
                addressInsertCMD.Parameters.AddWithValue("@PhoneNumber", customerData["CustomerPhone"]);
                addressInsertCMD.Parameters.AddWithValue("@LastUpdateBy", UserSessions.CurrentUserName);
                if (!isUpdate)
                {
                    addressInsertCMD.Parameters.AddWithValue("@CreatedBy", UserSessions.CurrentUserName);
                }
                addressInsertCMD.Prepare();
                addressInsertCMD.ExecuteNonQuery();
            }
            return addressId;
        }

        private void SaveCustomerData(Dictionary<string, string> customerData, int currentAddressId, MySqlConnection connection, bool isUpdate)
        {
            // Implementation for saving customer data
            int customerId;
            string query;

            if (isUpdate)
            {
                customerId = int.Parse(customerData["CustomerId"]);
                query = DBQueries.UpdateCustomerQuery;
            }
            else
            {
                using (var customerIndexCMD = new MySqlCommand(DBQueries.GetCustomerIdxQuery, connection))
                {
                    customerId = GetNewId(DBQueries.GetCustomerIdxQuery, connection);
                    query = DBQueries.InsertCustomerQuery;
                }
                
            }
            using (var customerInsertCMD = new MySqlCommand(query, connection))
            {
                customerInsertCMD.Parameters.AddWithValue("@CustomerId", customerId);
                customerInsertCMD.Parameters.AddWithValue("@CustomerName", customerData["CustomerName"]);
                customerInsertCMD.Parameters.AddWithValue("@AddressId", currentAddressId);
                customerInsertCMD.Parameters.AddWithValue("@ActiveStatus", 1);
                customerInsertCMD.Parameters.AddWithValue("@LastUpdateBy", UserSessions.CurrentUserName);
                if (!isUpdate)
                {
                    customerInsertCMD.Parameters.AddWithValue("@CreatedBy", UserSessions.CurrentUserName);
                }
                customerInsertCMD.Prepare();
                customerInsertCMD.ExecuteNonQuery();
            }
        }

        public void DeleteCustomer(string customerId)
        {
            using (MySqlConnection connection = DBConnection.GetNewConnection())
            try
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (var deleteAppointmentsCMD = new MySqlCommand(DBQueries.DeleteAppointmentsByCustomerIdQuery, connection))
                        {
                            deleteAppointmentsCMD.Parameters.AddWithValue("@CustomerId", customerId);
                            deleteAppointmentsCMD.ExecuteNonQuery();
                        }

                        using (var deleteCustomerCMD = new MySqlCommand(DBQueries.DeleteCustomerQuery, connection))
                        {
                            deleteCustomerCMD.Parameters.AddWithValue("@CustomerId", customerId);
                            deleteCustomerCMD.Prepare();
                            deleteCustomerCMD.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting customer: {ex.Message}");
            }
        }
        #endregion

        #region Helper Methods

        public string GetCustomerPhone(int customerId)
        {
            string phone = "";
            using (MySqlConnection connection = DBConnection.GetNewConnection())
            {
                using (MySqlCommand command = new MySqlCommand(DBQueries.GetCustomerPhoneQuery, connection))
                {
                    try
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@CustomerId", customerId);

                        // Use ExecuteScalar since we only expect one result
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            phone = result.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error retrieving customer phone: {ex.Message}");
                    }
                }
            }
            return phone;
        }

        private int GetNewId(string query, MySqlConnection connection) =>
            Convert.ToInt32(new MySqlCommand(query, connection).ExecuteScalar()) + 1;
        #endregion

        #region Data Getting Methods

        public DataTable GetAllCustomers(string query)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection connection = DBConnection.GetNewConnection())
            {
                using (MySqlCommand getCustomersCMD = new MySqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        MySqlDataAdapter adapter = new MySqlDataAdapter(getCustomersCMD);
                        adapter.Fill(dt);
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions (e.g., log them)
                        MessageBox.Show("Error retrieving customers: " + ex.Message);
                    }
                }
            }
            return dt;

        }

        public Dictionary<int, string> GetCustomersAsList(string query)
        {
            var customers = new Dictionary<int, string>();


            using (MySqlConnection connection = DBConnection.GetNewConnection())
            {
                using (MySqlCommand command = new MySqlCommand(DBQueries.GetCustomersListQuery, connection))
                {
                    try
                    {
                        connection.Open();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            // Loop through each row in the result set
                            while (reader.Read())
                            {
                                // Add the customerId (Key) and customerName (Value) to the dictionary
                                customers.Add(reader.GetInt32("customerId"), reader.GetString("customerName"));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error retrieving customer list: {ex.Message}");
                    }
                }
            }
            return customers;
        }
        #endregion
    }
}
