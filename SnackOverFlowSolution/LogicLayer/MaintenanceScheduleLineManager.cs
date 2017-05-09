using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;

namespace LogicLayer
{
    public class MaintenanceScheduleLineManager
    {
        public int CreateMaintenanceScheduleLine(MaintenanceScheduleLine newMaintenanceScheduleLine)
        {
            try
            {
                return MaintenanceScheduleLineAccessor.CreateMaintenanceScheduleLine(newMaintenanceScheduleLine);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
