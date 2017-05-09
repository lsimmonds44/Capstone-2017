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
    public class SupplierOrderAccessor
    {

        /// Laura Simmonds
        /// Created: 2017/05/04
        /// 
        /// <param name="productOrder">The order to add to the database.</param>
        /// <returns>The auto-generated order id from the database.</returns>
        public static int CreateSupplierOrder(int supplierId, int employeeID)
        {
            int orderId = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_create_supplier_order";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SUPPLIER_ID", supplierId);
            cmd.Parameters.AddWithValue("@EMPLOYEE_ID", employeeID);

            //cmd.Parameters.AddWithValue("@AMOUNT", supplierOrder.Amount);

            try
            {
                conn.Open();
                // int.TryParse(cmd.ExecuteScalar().ToString(), out orderId);
                object Id = cmd.ExecuteScalar();
                decimal someStupidValue = (decimal)Id;
                orderId = (int)someStupidValue;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return orderId;
        }
    }
}
