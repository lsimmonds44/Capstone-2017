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
    /// 2017/03/24
    /// 
    /// Class to handle database interactions involving repairs.
    /// </summary>
    public class RepairAccessor
    {

        /// <summary>
        /// Robert Forbes
        /// Created: 
        /// 2017/03/24
        /// 
        /// Retrieves all repairs for a given vehicle
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
        /// <param name="vehicleId">The id of the relevant vehicle.</param>
        /// <returns>A list of repairs related to the given vehicle.</returns>
        public static List<Repair> RetreiveAllRepairsForVehicle(int vehicleId)
        {
            var repairs = new List<Repair>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_repair_from_search";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Vehicle_ID", vehicleId);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        repairs.Add(new Repair()
                        {
                            RepairId = reader.GetInt32(0),
                            VehicleId = reader.GetInt32(1)
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

            return repairs;
        }

    }
}
