using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_Manager.Model.Entities
{
    public class Country : BaseClass
    {
        public int CountryID { get; set; }

        public string CountryName { get; set; }
    }
}
