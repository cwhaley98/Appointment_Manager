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
    public partial class AppointmentForm : Form
    {
        public Appointment MainFormInstance { get; set; }

        private AppointmentController appointmentController = new AppointmentController();

        private CustomerController customerController = new CustomerController();

        private UserController userController = new UserController();

        private string appointmentId;
        public AppointmentForm()
        {
            InitializeComponent();
            LoadForm();
        }
    }
}
