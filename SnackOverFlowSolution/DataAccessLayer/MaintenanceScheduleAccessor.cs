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
    public class MaintenanceScheduleAccessor
    {
        /// <summary>
        /// Mason Allen
        /// Created: 2017/03/09
        /// 
        /// Creates a new vehicle maintenance schedule
        /// </summary>
        /// <param name="vehicleId">Vehcile Id of the new maintenance schedule</param>
        /// <returns>Int of 1 if successful, 0 if fail</returns>
        public static int CreateMaintenanceSchedule(int vehicleId)
        {
            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_create_maintenance_schedule";
            var cmd = new SqlCommand(cmdText, conn);
            var count = 0;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@VEHICLE_ID", vehicleId);

            try
            {
                conn.Open();
                count = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return count;
        }

        /// <summary>
        /// Created by Mason Allen
        /// Created on 03/09/17
        /// 
        /// Retrives a maintenance schedule by vehicle id
        /// </summary>
        /// <param name="vehicleId">ID of the vehicle you are searching by</param>
        /// <returns>Maintenance schedule</returns>
        public static MaintenanceSchedule RetrieveMaintenanceScheduleByVehicleId(int vehicleId)
        {
            var conn = DBConnection.GetConnection();
            const string cmdText = @"sp_retrieve_maintenance_schedule_by_vehicle_id";
            var cmd = new SqlCommand(cmdText, conn);
            var count = 0;

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@VEHICLE_ID", vehicleId);

            MaintenanceSchedule retrievedMaintenanceSchedule = new MaintenanceSchedule();

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    
                    retrievedMaintenanceSchedule.MaintenanceScheduleId = reader.GetInt32(0);
                    retrievedMaintenanceSchedule.VehicleId = reader.GetInt32(1);
                    
                    reader.Close();
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
            return retrievedMaintenanceSchedule;
        }
    }
}
