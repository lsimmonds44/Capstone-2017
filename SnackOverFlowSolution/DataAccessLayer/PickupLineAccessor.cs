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
    /// Robert Forbes
    /// 2017/04/13
    /// </summary>
    public static class PickupLineAccessor
    {
        /// <summary>
        /// Robert Forbes
        /// 2017/04/13
        /// </summary>
        /// <param name="pickupId"></param>
        /// <returns></returns>
        public static List<PickupLine> RetrievePickupLinesForPickup(int? pickupId)
        {
            List<PickupLine> lines = new List<PickupLine>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_pickup_line_from_search";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.Parameters.AddWithValue("@PICKUP_ID", pickupId);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var line = new PickupLine()
                        {
                            PickupLineId = reader.GetInt32(0),
                            PickupId = reader.GetInt32(1),
                            ProductLotId = reader.GetInt32(2),
                            Quantity = reader.GetInt32(3),
                            PickupStatus = reader.GetBoolean(4)
                        };
                        lines.Add(line);
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
            return lines;
        }

    }
}
