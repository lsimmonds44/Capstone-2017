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
    /// Class to handle database interactions involving routes.
    /// </summary>
    public static class RouteAccessor
    {
        /// <summary>
        /// Robert Forbes
        /// Created: 
        /// 2017/04/13
        /// 
        /// Gets all routes assigned to the driver with the passed in id that occur in the future.
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
        /// <param name="driverId">The id of the relevant driver.</param>
        /// <returns>A list of future routes for the the given driver.</returns>
        public static List<Route> RetrieveFutureRoutesForDriver(int? driverId)
        {
            var routes = new List<Route>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_routes_for_driver_after_date";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@DRIVER_ID", driverId);
            cmd.Parameters.AddWithValue("@TODAYS_DATE", DateTime.Now);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        routes.Add(new Route()
                        {
                            RouteId = reader.GetInt32(0),
                            VehicleId = reader.GetInt32(1),
                            DriverId = reader.GetInt32(2),
                            AssignedDate = reader.GetDateTime(3)
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

            return routes;
        }
        /// <summary>
        /// Aaron Usher
        /// Created: 2017/05/04
        /// 
        /// Retrieves all routes from the database.
        /// </summary>
        ///
        /// <returns>A list of all routes in the database.</returns>
        public static List<Route> RetrieveRoutes()
        {
            var routes = new List<Route>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_route_list";
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
                        routes.Add(new Route()
                        {
                            RouteId = reader.GetInt32(0),
                            VehicleId = reader.GetInt32(1),
                            DriverId = reader.GetInt32(2),
                            AssignedDate = reader.GetDateTime(3)
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

            return routes;
        }

    }
}
