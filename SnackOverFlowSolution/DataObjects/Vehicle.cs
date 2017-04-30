using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Ariel Sigo
    /// Updated:
    /// 2017/04/29
    /// 
    /// The DTO for Vehicle 
    /// </summary>
    public class Vehicle
    {
        public int VehicleID { get; set; }
        public string VIN { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Mileage { get; set; }
        public string Year { get; set; }
        public string Color { get; set; }
        public bool Active { get; set; }
        public DateTime? LatestRepair { get; set; }
        public int? LastDriver { get; set; }
        public string VehicleTypeID { get; set; }
        public List<Repair> RepairList { get; set; }
        public bool CheckedOut { get; set; }
        public DateTime? CheckedOutTimeDate { get; set; }
    }
}
