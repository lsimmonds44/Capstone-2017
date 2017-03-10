using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Maintenance Schedule Line class
    /// Created by Mason Allen
    /// Created on 03/09/17
    /// </summary>
    public class MaintenanceScheduleLine
    {
        public int MaintenanceScheduleLineId { get; set; }
        public int MaintenanceScheduleId { get; set; }
        public string Description { get; set; }
        public DateTime MaintenanceDate { get; set; }
    }
}
