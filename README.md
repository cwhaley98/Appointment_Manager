# Appointment Scheduling Manager

### Technology Stack
  * C#
  * Windows Form Application (.NET 8)
  * MySQL

### Tools
  * Windows 11
  * Visual Studio Community 2022
  * Git and GitHub

### Summary
The Appointment Scheduling Manager is a Windows Form Application (.NET 8) appointment and calendar scheduling system written in C#. 
This is intergrated with a MySQL database, the GUI is designed for users to add/modify/delete customers from the database, as well as create/modify/delete appointments for customers.

## Functionality
* #### A login form that:
  * Initializes a new database connection
  * Validates if the user exists and if so, compares the entered password with the saved password in the database
  * Executes login fail or success messages based on the user password input
  * Determines the users region location, its detected language (currently only German and English is hardcoded to be detected) and timezone based using a timeZonePollTimer every 5000 milliseconds(5 seconds)

* #### Provides the ability to add, update, and delete customer records in the database, including name, address, and phone number. 

* #### Provides the ability to add, update, and delete appointments, capturing the type of appointment and a link to the specific customer record in the database.

* #### Provides the ability to view the calendar by month and by week.

* #### Provide the ability to select and view days appointments were set on the calendar

* #### Provide the ability to create specific reports of each appointment type by month, reports of appointments for each consultant, and reports of appointments made in each location.

* #### Provides the ability to automatically adjust appointment times based on user time zones and daylight-saving time.

* #### Provide reminders and alerts 15 minutes in advance of an appointment, based on the user’s log-in.

* #### Provides the ability to track user activity by recording timestamps for user log-ins in a .txt file. Each new record appends to the log file if the file already exists.

#### Exception controls for:
  * Scheduling an appointment outside business hours
  * Scheduling overlapping appointments
  * Entering nonexistent or invalid customer data
  * Entering an incorrect username and password
 
## What I learned
  * C# Fundamentals
  * Type safety
  * Windows Forms
  * .NET Framework
  * Model View Controller(MVC) System Design
  * OOP principles like Encapsulation, Data Abstraction, Polymorphism and Inheritance
  * Improved understanding code structure and readability

### Academic
Course: C969 Software II: Advanced C# at WGU
