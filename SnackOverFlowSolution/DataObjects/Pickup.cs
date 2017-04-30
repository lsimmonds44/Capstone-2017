using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Robert Forbes
    /// 
    /// Created:
    /// 2017/04/13
    /// 
    /// DTO for Pickup
    /// </summary>
    public class Pickup
    {
        public List<PickupLine> PickupLineList { get; set; }
        public int? EmployeeId { get; set; }
        public int? DriverId { get; set; }
        public int? WarehouseId { get; set; }
        public int? SupplierId { get; set; }
        public int? PickupId { get; set; }

        public UserAddress address { get; set; }
    }
}
