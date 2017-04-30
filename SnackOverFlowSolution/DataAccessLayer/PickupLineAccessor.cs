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
    /// Created: 
    /// 2017/04/13
    /// 
    /// Class to handle database interactions involving pickup lines.
    /// </summary>
    public static class PickupLineAccessor
    {
        /// <summary>
        /// Robert Forbes
        /// Created: 
        /// 2017/04/13
        /// 
        /// Retrieves pickup lines based on the given pickup id.
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Created: 
        /// 2017/04/21
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <param name="pickupId">The id of the pickup the lines go with.</param>
        /// <returns>List of pickup lines.</returns>
        public static List<PickupLine> RetrievePickupLinesForPickup(int? pickupId)
        {
            var lines = new List<PickupLine>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_pickup_line_from_search";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PICKUP_ID", pickupId);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        lines.Add(new PickupLine()
                        {
                            PickupLineId = reader.GetInt32(0),
                            PickupId = reader.GetInt32(1),
                            ProductLotId = reader.GetInt32(2),
                            Quantity = reader.GetInt32(3),
                            PickupStatus = reader.GetBoolean(4)
                        });
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
        /// Created: 
        /// 2017/04/19
        /// 
        /// Updates a pickup line in the database.
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
        /// <param name="oldPickupLine">The pickup line as it was in the database.</param>
        /// <param name="newPickupLine">The pickup line as it should be.</param>
        /// <returns>Rows affected.</returns>
        public static int UpdatePickupLine(PickupLine oldPickupLine, PickupLine newPickupLine)
        {
            int rows = 0;

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

        /// <summary>
        /// Robert Forbes
        /// Created: 
        /// 2017/04/19
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
        /// <param name="pickupLineId">The id of the pickup line to retrieve.</param>
        /// <returns>The pickup line.</returns>
        public static PickupLine RetrievePickupLine(int? pickupLineId)
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
