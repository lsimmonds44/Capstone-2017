using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class MaintenanceScheduleLineAccessor
    {
        /// <summary>
        /// Creates a new maintenance schedule line
        /// Created by Mason Allen
        /// Created on 03/09/17
        /// </summary>
        /// <param name="newMaintenanceScheduleLine">New maintenance schedule line to be added</param>
        /// <returns>1 for success, 0 for fail</returns>
        public static int CreateMaintenanceScheduleLine(MaintenanceScheduleLine newMaintenanceScheduleLine)
        {
            var conn = DBConnection.GetConnection();
            const string cmdText = @"sp_create_maintenance_schedule_line";
            var cmd = new SqlCommand(cmdText, conn);
            var count = 0;

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MAINTENANCE_SCHEDULE_ID", newMaintenanceScheduleLine.MaintenanceScheduleId);
            cmd.Parameters.AddWithValue("@DESCRIPTION", newMaintenanceScheduleLine.Description);
            cmd.Parameters.AddWithValue("@MAINTENANCE_DATE", newMaintenanceScheduleLine.MaintenanceDate);

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
    }
}
