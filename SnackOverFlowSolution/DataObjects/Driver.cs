using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Driver
    {
        public int? DriverId { get; set; }
        public string DriverLicenseNumber { get; set; }
        public DateTime? LicenseExpiration { get; set; }
        public bool Active { get; set; }
    }
}
