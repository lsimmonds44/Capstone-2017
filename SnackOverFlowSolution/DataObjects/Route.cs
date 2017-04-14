using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Robert Forbes 2017/04/13
    /// </summary>
    public class Route
    {
        public DateTime AssignedDate { get; set; }
        public int? DriverId { get; set; }
        public int? VehicleId { get; set; }
        public int? RouteId { get; set; }
        public List<Delivery> Deliveries { get; set; }
    }
}
