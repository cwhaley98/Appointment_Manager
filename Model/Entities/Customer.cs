using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_Manager.Model.Entities
{
    public class Customer : BaseClass
    {
        public int CustomerID { get; set; }

        public string CustomerName { get; set; }

        public Address Address { get; set; }

        public bool ActiveStatus { get; set; }
    }
}
