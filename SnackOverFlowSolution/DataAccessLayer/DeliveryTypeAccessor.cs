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
    /// Aaron Usher
    /// Updated: 
    /// 2017/04/03
    /// 
    /// Class to handle database interactions involving delivery types.
    /// </summary>
    public static class DeliveryTypeAccessor
    {
        /// <summary>
        /// Aaron Usher
        /// Updated: 
        /// 2017/04/03
        /// 
        /// Accessor method to retrieve a list of all delivery types.
        /// </summary>
        /// <returns>A list of all delivery types.</returns>
        public static List<string> RetrieveDeliveryTypeList()
        {
            var deliveryTypes = new List<string>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_delivery_type_list";
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
                        deliveryTypes.Add(reader.GetString(0));
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

            return deliveryTypes;
        }
    
    }
}
