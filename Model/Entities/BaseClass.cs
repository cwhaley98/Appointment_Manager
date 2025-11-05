using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_Manager.Model.Entities
{
    public class BaseClass
    {
        public string CreatedBy { get; set; }

        public string CreateDate { get; set; }

        public string LastUpdate { get; set; }

        public string LastUpdatedBy { get; set; }
    }
}
