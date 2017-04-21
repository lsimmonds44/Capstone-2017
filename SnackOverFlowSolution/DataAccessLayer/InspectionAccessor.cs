using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// Christian Lopez
    /// Created on 2017/02/16
    /// 
    /// Manages accessing the database for Inspection related actions.
    /// </summary>
    public static class InspectionAccessor
    {
        /// <summary>
        /// Christian Lopez
        /// Created: 2017/02/16
        /// 
        /// Creates an inspection from the given data
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/07
        /// 
        /// Standardized method and changed signature from inspection fields to an inspection object.
        /// </remarks>
        /// 
        /// <param name="inspection">The inspection to add to the database.</param>
        /// <returns>The number of rows affected</returns>
        public static int CreateInspection(Inspection inspection)
        {
            var rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_create_inspection";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EMPLOYEE_ID", inspection.EmployeeId);
            cmd.Parameters.AddWithValue("@PRODUCT_LOT_ID", inspection.ProductLotId);
            cmd.Parameters.AddWithValue("@GRADE_ID", inspection.GradeId);
            cmd.Parameters.AddWithValue("@DATE_PERFORMED", inspection.DatePerformed);
            cmd.Parameters.AddWithValue("@EXPIRATION_DATE", inspection.ExpirationDate);

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
