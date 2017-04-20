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


        /// <summary>
        /// Robert Forbes
        /// Created: 2017/04/19
        /// </summary>
        /// <param name="oldPickupLine"></param>
        /// <param name="newPickupLine"></param>
        /// <returns></returns>
        public static int UpdatePickupLine(PickupLine oldPickupLine, PickupLine newPickupLine)
        {
            int rowsAffected = 0;

            var cmdText = @"sp_update_pickup_line";
            var conn = DBConnection.GetConnection();

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@old_PICKUP_LINE_ID", oldPickupLine.PickupLineId);
            cmd.Parameters.AddWithValue("@old_PICKUP_ID", oldPickupLine.PickupId);
            cmd.Parameters.AddWithValue("@old_PRODUCT_LOT_ID", oldPickupLine.ProductLotId);
            cmd.Parameters.AddWithValue("@old_QUANTITY", oldPickupLine.Quantity);
            cmd.Parameters.AddWithValue("@old_PICK_UP_STATUS", oldPickupLine.PickupStatus);
            cmd.Parameters.AddWithValue("@new_PICKUP_ID", newPickupLine.PickupId);
            cmd.Parameters.AddWithValue("@new_PRODUCT_LOT_ID", newPickupLine.ProductLotId);
            cmd.Parameters.AddWithValue("@new_QUANTITY", newPickupLine.Quantity);
            cmd.Parameters.AddWithValue("@new_PICK_UP_STATUS", newPickupLine.PickupStatus);
            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return rowsAffected;
        }

        /// <summary>
        /// Robert Forbes
        /// Created: 2017/04/19
        /// </summary>
        /// <param name="pickupLineId"></param>
        /// <returns></returns>
        public static PickupLine RetrievePickupLineById(int? pickupLineId)
        {
            PickupLine line = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_pickup_line";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.Parameters.AddWithValue("@PICKUP_LINE_ID", pickupLineId);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    line = new PickupLine()
                    {
                        PickupLineId = reader.GetInt32(0),
                        PickupId = reader.GetInt32(1),
                        ProductLotId = reader.GetInt32(2),
                        Quantity = reader.GetInt32(3),
                        PickupStatus = reader.GetBoolean(4)
                    };
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
            return line;
        }

    }
}
