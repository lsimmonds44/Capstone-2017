using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;

namespace LogicLayer
{
    public class MaintenanceScheduleManager
    {
        /// <summary>
        /// Creates a new maintenance record
        /// Created by Mason Allen
        /// Created on 03/09/17
        /// </summary>
        /// <param name="vehicleId">Vehicle Id of the new maintenance schedule</param>
        /// <returns>int of 1 if successful, 0 if fail</returns>
        public int createMaintenanceSchedule(int vehicleId)
        {
            try
            {
                return MaintenanceScheduleAccessor.CreateMaintenanceSchedule(vehicleId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public MaintenanceSchedule retrieveMaintenanceScheduleByVehicleId(int vehicleId)
        {
            try
            {
                return MaintenanceScheduleAccessor.RetrieveMaintenanceScheduleByVehicleId(vehicleId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
