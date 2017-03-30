using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    public class SupplierInventoryAccessor
    {
        public static int CreateSupplierInventory (SupplierInventory toAdd)
        {
            var rowsAffected = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_create_supplier_inventory");
            cmd.Parameters.AddWithValue("@AGREEMENT_ID", toAdd.AgreementID);
            cmd.Parameters.AddWithValue("@DATE_ADDED", toAdd.DateAdded);
            cmd.Parameters.AddWithValue("@QUANTITY", toAdd.Quantity);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close(); // good housekeeping approved!
            }
            return rowsAffected;
        }
    }
}
