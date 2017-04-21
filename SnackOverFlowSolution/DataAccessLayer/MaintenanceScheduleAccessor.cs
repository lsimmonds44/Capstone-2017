using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DataObjects;
using System.Data;

namespace DataAccessLayer
{
    /// <summary>
    /// Aaron Usher
    /// Updated: 2017/04/14
    /// 
    /// Class to handle database interactions involving maintenance schedules.
    /// </summary>
    public static class MaintenanceScheduleAccessor
    {
        /// <summary>
        /// Mason Allen
        /// Created: 2017/03/09
        /// 
        /// Creates a new vehicle maintenance schedule
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/14
        /// 
        /// Standardized method.
        /// </remarks>
        /// <param name="vehicleId">Vehicle Id of the new maintenance schedule</param>
        /// <returns>Rows affected.</returns>
        public static int CreateMaintenanceSchedule(int vehicleId)
        {
            var rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_create_maintenance_schedule";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@VEHICLE_ID", vehicleId);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }

        /// <summary>
        /// Mason Allen
        /// Created: 03/09/17
        /// 
        /// Retrieves a maintenance schedule by vehicle id
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/14
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <param name="vehicleId">ID of the vehicle you are searching by</param>
        /// <returns>Maintenance schedule</returns>
        public static MaintenanceSchedule RetrieveMaintenanceScheduleByVehicleId(int vehicleId)
        {
            MaintenanceSchedule maintenanceSchedule = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_maintenance_schedule_by_vehicle_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@VEHICLE_ID", vehicleId);
            

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();

                    maintenanceSchedule = new MaintenanceSchedule()
                    {
                        MaintenanceScheduleId = reader.GetInt32(0),
                        VehicleId = reader.GetInt32(1)
                    };
                }

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

            return maintenanceSchedule;
        }
    }
}
