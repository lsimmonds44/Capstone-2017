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
    /// <summary>
    /// William Flood
    /// Created:
    /// 2017/03/29
    /// 
    /// Class to handle database interactions involving supplier inventories.
    /// </summary>
    public class SupplierInventoryAccessor
    {
        /// <summary>
        /// William Flood
        /// Created: 
        /// 2017/03/29
        /// 
        /// Adds a new supplier inventory to the database.
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated:
        /// 2017/04/21
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <param name="supplierInventory">The supplier inventory to add</param>
        /// <returns>Rows affected.</returns>
        public static int CreateSupplierInventory (SupplierInventory supplierInventory)
        {
            var rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_create_supplier_inventory";
            var cmd = new SqlCommand(cmdText,conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@AGREEMENT_ID", supplierInventory.AgreementID);
            cmd.Parameters.AddWithValue("@DATE_ADDED", supplierInventory.DateAdded);
            cmd.Parameters.AddWithValue("@QUANTITY", supplierInventory.Quantity);
            
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
