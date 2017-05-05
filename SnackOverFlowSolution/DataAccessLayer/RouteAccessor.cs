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
        /// 2017/05/04
        /// 
        /// Gets all routes.
        /// </summary>
        /// 
        /// <param name="driverId">The id of the relevant driver.</param>
        /// <returns>A list of future routes for the the given driver.</returns>
        public static List<Route> RetrieveFutureRoutesForDriver(int? driverId)
        {
            var routes = new List<Route>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_routes_list";
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

        /// <summary>
        /// Robert Forbes
        /// Created: 
        /// 2017/04/13
        /// 
        /// Gets all routes assigned to the driver with the passed in id that occur in the future.
        /// </summary>
        /// 
        /// <returns>A list of all routes.</returns>
        public static List<Route> RetrieveAllRoutes()
        {
            var routes = new List<Route>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_routes_list";
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


        /// <summary>
        /// Robert Forbes
        /// Created:2017/05/04
        /// 
        /// Creates a new route and returns the id of the route
        /// </summary>
        /// <param name="route">route to add to the database.</param>
        /// <returns>The route Id of the newly created route</returns>
        public static int CreateRouteAndRetrieveRouteId(Route route)
        {

            var result = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_create_route_return_route_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@VEHICLE_ID", route.VehicleId);
            cmd.Parameters.AddWithValue("@DRIVER_ID", route.DriverId);
            cmd.Parameters.AddWithValue("@ASSIGNED_DATE", route.AssignedDate);
            cmd.Parameters.Add("@ROUTE_ID", SqlDbType.Int).Direction = ParameterDirection.Output;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                result = Convert.ToInt32(cmd.Parameters["@ROUTE_ID"].Value);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }
    }
}
