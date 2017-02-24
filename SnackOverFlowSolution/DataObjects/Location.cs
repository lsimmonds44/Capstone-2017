using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Location
    {
        public int? locationId { get; set; }
        public string description { get; set; }

        public bool isActive { get; set; }
    }
}
