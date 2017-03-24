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
    public class RepairLineAccessor
    {

        public static List<RepairLine> RetreiveAllRepairLinesForRepair(int repairId)
        {
            List<RepairLine> lines = new List<RepairLine>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_repair_line_from_search";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@REPAIR_ID", SqlDbType.Int);
            cmd.Parameters["@REPAIR_ID"].Value = repairId;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        RepairLine line = new RepairLine()
                        {
                            RepairLineId = reader.GetInt32(0),
                            RepairId = reader.GetInt32(1),
                            RepairDescription = reader.GetString(2)
                        };

                        lines.Add(line);
                    }

                }
                reader.Close();

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
