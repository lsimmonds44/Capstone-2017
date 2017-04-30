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
    /// 2017/04/07
    /// 
    /// Class to handle database interactions involving locations.
    /// </summary>
    public static class LocationAccessor
    {
        /// <summary>
        /// Skyler Hiscock
        /// Created: 
        /// 2017/02/03
        /// Creates a warehouse location
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 
        /// 2017/04/07
        /// Standardized method.
        /// </remarks>
        /// 
        /// <param name="location">The location to add to the database.</param>
        /// <returns>Rows affected.</returns>
        public static int CreateLocation(Location location)
        {
            var rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_create_location";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@DESCRIPTION", location.Description);
            cmd.Parameters.AddWithValue("@IS_ACTIVE", location.IsActive);

            try
            {
                conn.Open();
                rows = (int)cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }

        /// <summary>
        /// Aaron Usher
        /// Updated: 
        /// 2017/04/07
        /// 
        /// Class to update a location in the database.
        /// </summary>
        /// <param name="oldLocation">The location as it was.</param>
        /// <param name="newLocation">The location as it should be.</param>
        /// <returns>Rows affected.</returns>
        public static int UpdateLocation(Location oldLocation, Location newLocation)
        {
            var result = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_update_location";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@LOCATION_ID", oldLocation.LocationId);

            cmd.Parameters.AddWithValue("@old_DESCRIPTION", oldLocation.Description);
            cmd.Parameters.AddWithValue("@old_IS_ACTIVE", oldLocation.IsActive);

            cmd.Parameters.AddWithValue("@new_DESCRIPTION", newLocation.Description);
            cmd.Parameters.AddWithValue("@new_IS_ACTIVE", newLocation.IsActive);

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
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

        /// <summary>
        /// Aaron Usher
        /// Updated: 
        /// 2017/04/07
        /// 
        /// Retrieves a location from the database based on its Id.
        /// </summary>
        /// <param name="locationId">The id of the desired location</param>
        /// <returns>The location with the given id.</returns>
        public static Location RetrieveLocationById(int locationId)
        {
            Location location = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_location";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@LOCATION_ID", locationId);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    location = new Location()
                    {
                        LocationId = reader.GetInt32(0),
                        Description = reader.GetString(1),
                        IsActive = reader.GetBoolean(2)
                    };
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

            return location;
        }

        /// <summary>
        /// Bill Flood
        /// Created: 
        /// 2017/03/28
        /// 
        /// Retrieves all locations from the database.
        /// </summary>
        /// <remarks>
        /// Aaron Usher
        /// Updated: 
        /// 2017/04/07
        /// 
        /// Standardized method.
        /// </remarks>
        /// <returns></returns>
        public static List<Location> RetrieveAllLocations()
        {
            var locations = new List<Location>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_location_list";
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
                        locations.Add(new Location()
                        {
                            LocationId = reader.GetInt32(0),
                            Description = reader.GetString(1),
                            IsActive = reader.GetBoolean(2)
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

            return locations;
        }
    }
}