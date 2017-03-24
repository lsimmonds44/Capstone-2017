using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public static class DeliveryTypeAccessor
    {
        public static List<string> RetrieveDeliveryTypeList()
        {
            var types = new List<string>();
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
                        types.Add(reader.GetString(0));
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
            return types;
        }
    
    }
}
