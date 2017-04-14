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
    /// 
    /// Class to handle database interactions involving product order statuses.
    /// </summary>
    public static class OrderStatusAccessor
    {

        /// <summary>
        /// Robert Forbes
        /// Created: 2017/03/01
        /// 
        /// Retrieves all order statuses from the database.
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/14
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <returns>List of all order statuses.</returns>
        public static List<string> RetrieveAllOrderStatus()
        {
            var orderStatuses = new List<string>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_order_status_list";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        orderStatuses.Add(reader.GetString(0));
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

            return orderStatuses;
        }
    }
}
