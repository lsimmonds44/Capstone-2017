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
    /// 2017/03/24
    /// </summary>
    public class RepairAccessor
    {

        /// <summary>
        /// Robert Forbes
        /// 2017/03/24
        /// 
        /// Retrieves all repairs for a given vehicle
        /// </summary>
        /// <param name="vehicleId">The vehicle id of the vehicle to select repairs for</param>
        /// <returns>A list of repairs</returns>
        public static List<Repair> RetreiveAllRepairsForVehicle(int vehicleId)
        {
            List<Repair> repairs = new List<Repair>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_repair_from_search";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Vehicle_ID", SqlDbType.Int);
            cmd.Parameters["@Vehicle_ID"].Value = vehicleId;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Repair repair = new Repair()
                        {
                            RepairId = reader.GetInt32(0),
                            VehicleId = reader.GetInt32(1)
                        };

                        repairs.Add(repair);
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
            return repairs;
        }

    }
}
