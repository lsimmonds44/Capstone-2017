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
    /// Class to handle database interactions involving pickups.
    /// </summary>
    public static class PickupAccessor
    {

        /// <summary>
        /// Robert Forbes
        /// Created:
        /// 2017/04/13
        /// 
        /// Retrieves all pickups related to a given driver.
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
        /// <param name="driverId"></param>
        /// <returns></returns>
        public static List<Pickup> RetrievePickupsForDriver(int? driverId)
        {
            var pickups = new List<Pickup>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_pickup_from_search";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@DRIVER_ID", driverId);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        pickups.Add(new Pickup()
                        {
                            PickupId = reader.GetInt32(0),
                            SupplierId = reader.GetInt32(1),
                            WarehouseId = reader.GetInt32(2),
                            DriverId = reader.GetInt32(3),
                            EmployeeId = reader.GetInt32(4)
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

            return pickups;
        }
    }
}
