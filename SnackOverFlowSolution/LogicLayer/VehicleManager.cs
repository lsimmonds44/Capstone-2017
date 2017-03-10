using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class VehicleManager : IVehicleManager
    {
        /// <summary>
        /// Victor Algarin
        /// Created 2017/03/01
        /// 
        /// Retrieves the details for a specific vehicle through an id
        /// </summary>
        public Vehicle RetreiveVehicleById(int vehicleId)
        {
            Vehicle vehicle = null;

            try
            {
                vehicle = VehicleAccessor.RetreiveVehicleByVehicleId(vehicleId);
            }
            catch (Exception)
            {

                throw new ApplicationException("There was a problem retreiving the requested vehicle from the database");
            }

            return vehicle;
        }

        /// <summary>
        /// Mason Allen
        /// Created 03/01/2017
        /// Updated on 03/09/17 to include maintenance schedule creation on new vehicle creation
        /// Creates a new vehicle record
        /// </summary>
        /// <param name="newVehicle"></param>
        /// <returns>An int of 1 for success, 0 for fail</returns>
        public int CreateVehicle(Vehicle newVehicle)
        {
            int newVehicleId;
            int success;
            try
            {
                newVehicleId = VehicleAccessor.CreateVehicle(newVehicle);
                success = MaintenanceScheduleAccessor.CreateMaintenanceSchedule(newVehicleId);
            }
            catch (Exception)
            {
                throw;
                //throw new ApplicationException("There was a problem saving the requested vehicle");
            }
            return success;
        }

        /// <summary>
        /// Eric Walton 
        /// 2017/02/03
        /// Retrieves all vehicles
        /// </summary>
        /// <returns></returns>
        public List<Vehicle> RetrieveAllVehicles()
        {
            var vehicles = new List<Vehicle>();
            try
            {
                vehicles = VehicleAccessor.RetrieveAllVehicles();
            }
            catch (Exception)
            {

                throw;
            }

            return vehicles;
        }
    }
}
