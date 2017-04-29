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
    /// Aaron Usher
    /// Updated: 
    /// 2017/04/21
    /// 
    /// Class to handle database interactions involving repair lines.
    /// </summary>
    public class RepairLineAccessor
    {
        /// <summary>
        /// Aaron Usher
        /// Updated: 
        /// 2017/04/21
        /// 
        /// Retrieves all of the repair lines related to the given repair.
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
        /// <param name="repairID">The id of the related repair.</param>
        /// <returns>All repair lines related to the given repair.</returns>
        public static List<RepairLine> RetreiveAllRepairLinesForRepair(int repairID)
        {
            var repairLines = new List<RepairLine>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_repair_line_from_search";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@REPAIR_ID", repairID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        repairLines.Add(new RepairLine()
                        {
                            RepairLineId = reader.GetInt32(0),
                            RepairId = reader.GetInt32(1),
                            RepairDescription = reader.GetString(2)
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

            return repairLines;
        }

    }
}
