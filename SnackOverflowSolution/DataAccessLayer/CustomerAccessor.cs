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
    public class CustomerAccessor
    {

        /// <summary>
        /// Eric Walton
        /// 2017/06/02
        /// 
        /// Create Commercial Customer method that uses a stored procedure to access the database
        /// </summary>
        /// <param name="cc"></param>
        /// <returns></returns>
        public bool CreateCommercialCustomer(CommercialCustomer cc)
        {
            var result = false;
            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_create_commercial";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@USER_ID", SqlDbType.Int);
            cmd.Parameters.Add("@IS_APPROVED", SqlDbType.Bit);
            cmd.Parameters.Add("@APPROVED_BY", SqlDbType.Int);
            cmd.Parameters.Add("@FEDERAL_TAX_ID", SqlDbType.Int);
            cmd.Parameters.Add("@ACTIVE", SqlDbType.Bit);
            // values
            cmd.Parameters["@USER_ID"].Value = cc.User_Id;
            cmd.Parameters["@IS_APPROVED"].Value = cc.IsApproved;
            cmd.Parameters["@APPROVED_BY"].Value = cc.ApprovedBy;
            cmd.Parameters["@FEDERAL_TAX_ID"].Value = cc.FedTaxId;
            cmd.Parameters["@Active"].Value = cc.Active;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                result = true;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error: " + ex);
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
    } // End of class
} // end of namespace
