using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Robert Forbes 
    /// Created:
    /// 2017/04/13
    /// 
    /// Class for Delivery Route
    /// </summary>
    /// <remarks>
    /// Ariel Sigo
    /// Updated:
    /// 2017/04/29
    /// 
    /// Standardized Comment
    /// </remarks>
    public class Route
    {
        public DateTime AssignedDate { get; set; }
        public int? DriverId { get; set; }
        public int? VehicleId { get; set; }
        public int? RouteId { get; set; }
        public List<Delivery> Deliveries { get; set; }
    }
}
