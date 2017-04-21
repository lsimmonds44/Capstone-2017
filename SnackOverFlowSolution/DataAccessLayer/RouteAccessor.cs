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
    /// Created: 2017/04/13
    /// 
    /// Class to handle database interactions involving routes.
    /// </summary>
    public static class RouteAccessor
    {


        /// <summary>
        /// Robert Forbes
        /// 2017/04/13
        /// 
        /// Gets all routes assigned to the driver with the passed in id
        /// </summary>
        /// <param name="driverId"></param>
        /// <returns></returns>
        public static List<Route> RetrieveFutureRoutesForDriver(int? driverId)
        {
            var routes = new List<Route>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_routes_for_driver_after_date";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.Parameters.AddWithValue("@DRIVER_ID", driverId);
            cmd.Parameters.AddWithValue("@TODAYS_DATE", DateTime.Now);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var route = new Route()
                        {
                            RouteId = reader.GetInt32(0),
                            VehicleId = reader.GetInt32(1),
                            DriverId = reader.GetInt32(2),
                            AssignedDate = reader.GetDateTime(3)
                        };

                        routes.Add(route);
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

            return routes;
        }

    }
}
