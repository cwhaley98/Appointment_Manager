using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_Manager.Model.Database
{
    public class DBQueries
    {

        // --- Base Appointment Query ---

        /// <summary>
        /// Base query that joins tables for full appointment details.
        /// </summary>
        public static string GetAppointmentsQuery
        {
            get
            {
                return @"
            SELECT 
                appointment.appointmentId, appointment.customerId, appointment.userId,
                title, description, location, contact, type, url, start, end,
                customer.customerName, user.userName, address.phone,
                address.addressId, city.cityId, country.countryId

            FROM 
                appointment
            INNER JOIN 
                customer ON appointment.customerId = customer.customerId
            INNER JOIN 
                user ON appointment.userId = user.userId
            INNER JOIN
                address ON customer.addressId = address.addressId
            
            INNER JOIN
                city ON address.cityId = city.cityId
            INNER JOIN
                country ON city.countryId = country.countryId";
            }
        }
        /// <summary>
        /// Gets appointments for a specific day (in EST).
        /// </summary>
        public static string GetAppointmentsByDayQuery
        {
            get
            {
                // Uses the base query and filters by a 24-hour window (in EST)
                return GetAppointmentsQuery + " WHERE appointment.start >= @StartOfDay " +
                    "AND appointment.start < @EndOfDay " +
                    "ORDER BY start ASC;";
            }
        }

        /// <summary>
        /// Checks for overlapping appointments for a specific user.
        /// </summary>
        public static string CheckOverlapQuery
        {
            get
            {
                // Checks for any appointments for a user that overlap a new time slot
                // Ignores the appointmentId being updated (if @AppointmentId != 0 or "")
                return @"
                    SELECT COUNT(*) FROM appointment
                    WHERE userId = @UserId
                    AND appointmentId != @AppointmentId
                    AND (@Start < end AND @End > start);
                ";
            }
        }

        /// <summary>
        /// Inserts a new appointment record.
        /// </summary>
        public static string InsertAppointmentQuery
        {
            get
            {
                return @"
                    INSERT INTO appointment 
                    (customerId, userId, title, description, location, contact, type, url, start, end, createDate, createdBy, lastUpdate, lastUpdateBy)
                    VALUES
                    (@CustomerId, @UserId, @Title, @Description, @Location, @Contact, @Type, @URL, @Start, @End, NOW(), @CreatedBy, NOW(), @LastUpdateBy);
                ";
            }
        }

        /// <summary>
        /// Updates an existing appointment record.
        /// </summary>
        public static string UpdateAppointmentQuery
        {
            get
            {
                return @"
                    UPDATE appointment
                    SET customerId = @CustomerId,
                        userId = @UserId,
                        title = @Title,
                        description = @Description,
                        location = @Location,
                        contact = @Contact,
                        type = @Type,
                        url = @URL,
                        start = @Start,
                        end = @End,
                        lastUpdate = NOW(),
                        lastUpdateBy = @LastUpdateBy
                    WHERE appointmentId = @AppointmentId;
                ";
            }
        }

        /// <summary>
        /// Deletes an appointment by its ID.
        /// </summary>
        public static string DeleteAppointmentQuery
        {
            get
            {
                return "DELETE FROM appointment " +
                    "WHERE appointmentId = @AppointmentId;";
            }
        }

        // --- Customer & User Lists (for ComboBoxes) ---

        /// <summary>
        /// Gets all customers for ComboBox binding.
        /// </summary>
        public static string GetCustomersListQuery
        {
            get
            {
                return @"SELECT customerId, customerName FROM customer ORDER BY customerName;";
            }
        }

        /// <summary>
        /// Gets a single customer's phone number.
        /// </summary>
        public static string GetCustomerPhoneQuery
        {
            get
            {
                return @"
                    SELECT address.phone 
                    FROM customer
                    INNER JOIN address ON customer.addressId = address.addressId
                    WHERE customer.customerId = @CustomerId;
                ";
            }
        }

        /// <summary>
        /// Gets all users (consultants) for ComboBox binding.
        /// </summary>
        public static string GetUsersListQuery
        {
            get
            {
                return @"SELECT userId, userName FROM user ORDER BY userName;";
            }
        }

        /// <summary>
        /// Gets a user's details by username for login validation.
        /// </summary>
        public static string ValidateUserQuery
        {
            get
            {
                // Selects the password hash and user info
                return "SELECT userId, userName, password FROM user WHERE userName = @username;";
            }
        }

        // --- Alerts & Reports (Req A.6 & A.7) ---

        /// <summary>
        /// Gets upcoming appointments for the logged-in user.
        /// </summary>
        public static string UpcomingAppointmentsQuery
        {
            get
            {
                // Gets appointments for the current user starting within the next 15 minutes (EST)
                return GetAppointmentsQuery + 
                    " WHERE appointment.userId = @UserId " +
                    "AND start >= @Now " +
                    "AND start <= @FifteenMinutesFromNow;";
            }
        }

        /// <summary>
        /// Report: Counts appointment types grouped by month.
        /// </summary>
        public static string ReportTypesByMonthQuery
        {
            get
            {
                return "SELECT MONTHNAME(start) AS Month, type AS Type, COUNT(*) AS Total " +
                    "FROM appointment " +
                    "GROUP BY MONTHNAME(start), type " +
                    "ORDER BY MONTH(start), Type;";
            }
        }

        /// <summary>
        /// Report: Gets the full schedule for a single consultant.
        /// </summary>
        public static string ReportConsultantScheduleQuery
        {
            get
            {
                // Gets all appointments for a specific user, ordered by start time
                return GetAppointmentsQuery + 
                    " WHERE appointment.userId = @UserId " +
                    "ORDER BY start ASC;";
            }
        }

        /// <summary>
        /// Report: Counts appointments grouped by location.
        /// </summary>
        public static string ReportLocationQuery
        {
            get
            {
                // Counts appointments grouped by location
                return "SELECT location, " +
                    "COUNT(*) AS Total " +
                    "FROM appointment " +
                    "GROUP BY location " +
                    "ORDER BY Total DESC;";
            }
        }

        // --- Other Queries (from DBConnection & CustomerController) ---

        public static string CheckTablesExistQuery 
        { get 
            { return 
                    "SELECT " +
                    "COUNT(*) " +
                    "FROM information_schema.tables " +
                    "WHERE table_schema = DATABASE() " +
                    "AND table_name IN ('user', 'country', 'city', 'address', 'customer', 'appointment');"; 
            } 
        }

        public static string GetUserCount 
        { get 
            { return "SELECT " +
                    "COUNT(*) " +
                    "FROM user;"; 
            } 
        
        }
        public static string InitializeDatabaseQuery 
        { get 
            { return "--\r\n-- populate table `country`\r\n--\r\n" +
                    "INSERT INTO `country` VALUES \r\n(1,'US','2019-01-01 00:00:00','test','2019-01-01 00:00:00','test')," +
                    "\r\n(2,'Canada','2019-01-01 00:00:00','test','2019-01-01 00:00:00','test')," +
                    "\r\n(3,'Norway','2019-01-01 00:00:00','test','2019-01-01 00:00:00','test');" +
                    "\r\n--\r\n-- populate table `city`\r\n--\r\nINSERT INTO `city` VALUES " +
                    "\r\n(1,'New York',1,'2019-01-01 00:00:00','test','2019-01-01 00:00:00','test')," +
                    "\r\n(2,'Los Angeles',1,'2019-01-01 00:00:00','test','2019-01-01 00:00:00','test')," +
                    "\r\n(3,'Toronto',2,'2019-01-01 00:00:00','test','2019-01-01 00:00:00','test')," +
                    "\r\n(4,'Vancouver',2,'2019-01-01 00:00:00','test','2019-01-01 00:00:00','test')," +
                    "\r\n(5,'Oslo',3,'2019-01-01 00:00:00','test','2019-01-01 00:00:00','test');" +
                    "\r\n--\r\n-- populate table `address`\r\n--\r\nINSERT INTO `address` VALUES " +
                    "\r\n(1,'123 Main','',1,'11111','555-1212','2019-01-01 00:00:00','test','2019-01-01 00:00:00','test')," +
                    "\r\n(2,'123 Elm','',3,'11112','555-1213','2019-01-01 00:00:00','test','2019-01-01 00:00:00','test')," +
                    "\r\n(3,'123 Oak','',5,'11113','555-1214','2019-01-01 00:00:00','test','2019-01-01 00:00:00','test');" +
                    "\r\n--\r\n-- populate table `customer`\r\n--\r\nINSERT INTO `customer` VALUES " +
                    "\r\n(1,'John Doe',1,1,'2019-01-01 00:00:00','test','2019-01-01 00:00:00','test')," +
                    "\r\n(2,'Alfred E Newman',2,1,'2019-01-01 00:00:00','test','2019-01-01 00:00:00','test')," +
                    "\r\n(3,'Ina Prufung',3,1,'2019-01-01 00:00:00','test','2019-01-01 00:00:00','test');" +
                    "\r\n--\r\n-- populate table `user`\r\n--\r\nINSERT INTO `user` VALUES " +
                    "\r\n(1,'test','test',1,'2019-01-01 00:00:00','test','2019-01-01 00:00:00','test');" +
                    "\r\n--\r\n-- populate table `appointment`\r\n--\r\nINSERT INTO `appointment` VALUES " +
                    "\r\n(1,1,1,'not needed','not needed','not needed','not needed','Presentation','not needed','2019-01-01 00:00:00','2019-01-01 00:00:00','2019-01-01 00:00:00','test','2019-01-01 00:00:00','test')," +
                    "\r\n(2,2,1,'not needed','not needed','not needed','not needed','Scrum','not needed','2019-01-01 00:00:00','2019-01-01 00:00:00','2019-01-01 00:00:00','test','2019-01-01 00:00:00','test');"; 
            } 
        } // Placeholder
        
        // Customer CRUD (Country)
        public static string UpdateCountryQuery 
        { get 
            { return "UPDATE country " +
                    "SET country = @Country, lastUpdate = NOW(), lastUpdateBy = @LastUpdateBy " +
                    "WHERE countryId = @CountryId;"; 
            } 
        }

        public static string GetCountryIdxQuery 
        { get 
            { return "SELECT MAX(countryId) " +
                    "FROM country;"; 
            } 
        }

        public static string InsertCountryQuery 
        { get 
            { return "INSERT INTO country (countryId, country, createDate, createdBy, lastUpdate, lastUpdateBy) " +
                    "VALUES (@CountryId, @Country, NOW(), @CreatedBy, NOW(), @LastUpdateBy);"; 
            } 
        }
        
        // Customer CRUD (City)
        public static string UpdateCityQuery 
        { get 
            { return "UPDATE city SET city = @City, countryId = @CountryId, " +
                    "lastUpdate = NOW(), " +
                    "lastUpdateBy = @LastUpdateBy " +
                    "WHERE cityId = @CityId;"; 
            } 
        }

        public static string GetCityIdxQuery 
        { get 
            { return "SELECT MAX(cityId) " +
                    "FROM city;"; 
            } 
        }

        public static string InsertCityQuery 
        { get 
            { return "INSERT INTO city (cityId, city, countryId, createDate, createdBy, lastUpdate, lastUpdateBy) " +
                    "VALUES (@CityId, @City, @CountryId, NOW(), " +
                    "@CreatedBy, NOW(), @LastUpdateBy);"; 
            } 
        }
        
        // Customer CRUD (Address)
        public static string UpdateAddressQuery 
        { get 
            { return "UPDATE address SET address = @Address, address2 = @Address2, cityId = @CityId, " +
                    "postalCode = @PostalCode, phone = @PhoneNumber, " +
                    "lastUpdate = NOW(), lastUpdateBy = @LastUpdateBy " +
                    "WHERE addressId = @AddressId;"; 
            } 
        }
        public static string GetAddressIdxQuery 
        { get 
            { return "SELECT MAX(addressId) " +
                    "FROM address;"; 
            } 
        }
        public static string InsertAddressQuery 
        { get 
            { return "INSERT INTO address (addressId, address, address2, cityId, postalCode, phone, createDate, createdBy, lastUpdate, lastUpdateBy) " +
                    "VALUES (@AddressId, @Address, @Address2, @CityId, @PostalCode, @PhoneNumber, " +
                    "NOW(), @CreatedBy, NOW(), @LastUpdateBy);"; 
            } 
        }
        
        // Customer CRUD (Customer)
        public static string UpdateCustomerQuery 
        { get 
            { return "UPDATE customer SET customerName = @CustomerName, addressId = @AddressId, active = @ActiveStatus, " +
                    "lastUpdate = NOW(), lastUpdateBy = @LastUpdateBy " +
                    "WHERE customerId = @CustomerId;"; 
            } 
        }
        public static string GetCustomerIdxQuery 
        { get 
            { return "SELECT MAX(customerId) " +
                    "FROM customer;"; 
            } 
        }
        public static string InsertCustomerQuery 
        { get 
            { return "INSERT INTO customer (customerId, customerName, addressId, active, " +
                    "createDate, createdBy, lastUpdate, lastUpdateBy) " +
                    "VALUES (@CustomerId, @CustomerName, @AddressId, @ActiveStatus, NOW(), " +
                    "@CreatedBy, NOW(), @LastUpdateBy);"; 
            } 
        }
        
        // Customer Deletion
        public static string DeleteAppointmentsByCustomerIdQuery 
        { get 
            { return "DELETE FROM appointment " +
                    "WHERE customerId = @CustomerId;"; 
            } 
        }
        public static string DeleteCustomerQuery 
        { get 
            { return "DELETE FROM customer " +
                    "WHERE customerId = @CustomerId;"; 
            } 
        }
        
        // Customer DataGrid View
        public static string GetCustomerTableQuery 
        { get 
            { return "SELECT customer.customerId, customer.customerName, " +
                    "address.address, address.address2, address.postalCode, " +
                    "address.phone, city.city, country.country, " +
                    "customer.addressId, city.cityId, country.countryId " +
                    "FROM customer " +
                    "JOIN address ON customer.addressId = address.addressId " +
                    "JOIN city ON address.cityId = city.cityId " +
                    "JOIN country ON city.countryId = country.countryId " +
                    "ORDER BY customer.customerName ASC;"; 
            } 
        }
    }
}
