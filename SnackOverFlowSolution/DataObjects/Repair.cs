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
    /// 2017/03/24
    /// 
    /// Repair Class
    /// </summary>
    public class Repair
    {

        public List<RepairLine> RepairLineList { get; set; }

        public int? VehicleId { get; set; }

        public int RepairId { get; set; }
    }
}
