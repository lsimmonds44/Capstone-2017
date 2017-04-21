using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    /// <summary>
    /// Aaron Usher
    /// Updated: 2017/04/14
    /// 
    /// Class to handle database interactions involving maintenance schedule lines.
    /// </summary>
    public static class MaintenanceScheduleLineAccessor
    {
        /// <summary>
        /// Mason Allen
        /// Created: 2017/03/09
        ///  
        /// Creates a new maintenance schedule line
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/14
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <param name="maintenanceScheduleLine">New maintenance schedule line to be added</param>
        /// <returns>Rows affected</returns>
        public static int CreateMaintenanceScheduleLine(MaintenanceScheduleLine maintenanceScheduleLine)
        {
            var rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_create_maintenance_schedule_line";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MAINTENANCE_SCHEDULE_ID", maintenanceScheduleLine.MaintenanceScheduleId);
            cmd.Parameters.AddWithValue("@DESCRIPTION", maintenanceScheduleLine.Description);
            cmd.Parameters.AddWithValue("@MAINTENANCE_DATE", maintenanceScheduleLine.MaintenanceDate);

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
    }
}
