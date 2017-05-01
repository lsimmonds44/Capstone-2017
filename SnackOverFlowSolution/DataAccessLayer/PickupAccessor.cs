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
        /// <remarks>
        /// Robert Forbes
        /// 
        /// Updated:
        /// 2017/04/30
        /// 
        /// Added Company order id to database table so all accessors were updated to use the new field
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
                            EmployeeId = reader.GetInt32(4),
                            CompanyOrderId = reader.GetInt32(5)
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

        /// <summary>
        /// Ryan Spurgetis
        /// 4/29/2017
        /// 
        /// Retrieves pickup based on the pickupId field
        /// </summary>
        /// <remarks>
        /// Robert Forbes
        /// 
        /// Updated:
        /// 2017/04/30
        /// 
        /// Added Company order id to database table so had to update all accessors
        /// </remarks>
        /// <param name="pickupId"></param>
        /// <returns></returns>
        public static Pickup RetrievePickupById(int? pickupId)
        {
            Pickup pickup = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_pickup";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PICKUP_ID", pickupId);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    pickup = new Pickup()
                    {
                        PickupId = reader.GetInt32(0),
                        SupplierId = reader.GetInt32(1),
                        WarehouseId = reader.GetInt32(2),
                        DriverId = reader.GetInt32(3),
                        EmployeeId = reader.GetInt32(4),
                        CompanyOrderId = reader.GetInt32(5)
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return pickup;
        }
    }
}
