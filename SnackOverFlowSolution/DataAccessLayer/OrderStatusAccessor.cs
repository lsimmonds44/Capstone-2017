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
    /// Robert Forbes
    /// 2017/03/01
    /// </summary>
    public static class OrderStatusAccessor
    {
        public static List<string> RetrieveAllOrderStatus()
        {
            List<string> status = new List<string>();

            // Creating an sql command object
            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_order_status_list";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            // Attempting to run the stored procedure
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    // Looping through all returned results until there aren't any left
                    while (reader.Read())
                    {
                        var newStatus = reader.GetString(0);

                        status.Add(newStatus);
                    }
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
            return status;
        }
    }
}
