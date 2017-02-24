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
    public class InspectionAccessor
    {
        /// <summary>
        /// Christian Lopez
        /// Created on 2017/02/16
        /// 
        /// Creates an inspection from the given data
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="productLotId"></param>
        /// <param name="gradeId"></param>
        /// <param name="datePerformed"></param>
        /// <param name="expirationDate"></param>
        /// <returns>The number of rows affected</returns>
        public static int CreateInspection(int employeeID, int productLotId, string gradeId,
            DateTime datePerformed, DateTime expirationDate)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_create_inspection";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@EMPLOYEE_ID", SqlDbType.Int);
            cmd.Parameters.Add("@PRODUCT_LOT_ID", SqlDbType.Int);
            cmd.Parameters.Add("@GRADE_ID", SqlDbType.NVarChar, 250);
            cmd.Parameters.Add("@DATE_PERFORMED", SqlDbType.DateTime);
            cmd.Parameters.Add("@EXPIRATION_DATE", SqlDbType.DateTime);
            cmd.Parameters["@EMPLOYEE_ID"].Value = employeeID;
            cmd.Parameters["@PRODUCT_LOT_ID"].Value = productLotId;
            cmd.Parameters["@GRADE_ID"].Value = gradeId;
            cmd.Parameters["@DATE_PERFORMED"].Value = datePerformed;
            cmd.Parameters["@EXPIRATION_DATE"].Value = expirationDate;

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("There was an error saving to the DB: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }


            return rows;
        }
    }
}
