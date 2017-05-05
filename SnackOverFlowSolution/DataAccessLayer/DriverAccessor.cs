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
    public static class DriverAccessor
    {
        /// <summary>
        /// Robert Forbes
        /// 
        /// Created:
        /// 2017/05/04
        /// </summary>
        /// <returns></returns>
        public static List<Driver> RetrieveAllDrivers()
        {
            List<Driver> drivers = new List<Driver>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_driver_list";
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
                        drivers.Add(new Driver()
                        {
                            DriverId = reader.GetInt32(0),
                            DriverLicenseNumber = reader.GetString(1),
                            LicenseExpiration = reader.GetDateTime(2),
                            Active = reader.GetBoolean(3)
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

            return drivers;
        }
    }
}
