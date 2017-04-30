using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Mason Allen
    /// Created:
    /// 2017/03/09
    /// 
    /// Maintenance Schedule Line class
    /// </summary>
    /// <remarks>
    /// Ariel Sigo
    /// Updated:
    /// 2017/04/29
    /// 
    /// Standardized Comment
    /// </remarks>
    public class MaintenanceScheduleLine
    {
        public int MaintenanceScheduleLineId { get; set; }
        public int MaintenanceScheduleId { get; set; }
        public string Description { get; set; }
        public DateTime MaintenanceDate { get; set; }
    }
}
