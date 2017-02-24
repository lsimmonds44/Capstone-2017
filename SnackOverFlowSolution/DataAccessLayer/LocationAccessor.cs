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
    public static class LocationAccessor
    {
        /// <summary>
        /// Author: Skyler Hiscock
        /// Created: 2016/02/03
        /// Creates a warehouse location
        /// </summary>
        /// <param name="location"></param>
        /// <returns>1 if successful, 0 if it fails</returns>
        public static int CreateLocation(Location location)
        {
            var result = 0;

            // get a connection
            var conn = DBConnection.GetConnection();

            var cmdText = @"sp_create_location";

            // create a command object
            var cmd = new SqlCommand(cmdText, conn);

            // set command type to stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters
            cmd.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar, 250);
            cmd.Parameters.Add("@IS_ACTIVE", SqlDbType.Bit);

            // set parameter values
            cmd.Parameters["@DESCRIPTION"].Value = location.description;
            cmd.Parameters["@IS_ACTIVE"].Value = location.isActive;

            // try-catch-finally

            try
            {
                // open a connection
                conn.Open();

                // execute and capture the result
                result = (int)cmd.ExecuteNonQuery();
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

        public static int UpdateLocation(Location oldLocation, Location newLocation)
        {
            var result = 0;

            // get a connection
            var conn = DBConnection.GetConnection();

            var cmdText = @"sp_update_location";

            // create a command object
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            // add parameters
            cmd.Parameters.Add("@old_LOCATION_ID", SqlDbType.Int);
            cmd.Parameters.Add("@old_DESCRIPTION", SqlDbType.VarChar, 250);
            cmd.Parameters.Add("@new_DESCRIPTION", SqlDbType.VarChar, 250);
            cmd.Parameters.Add("@old_IS_ACTIVE", SqlDbType.Bit);
            cmd.Parameters.Add("@new_IS_ACTIVE", SqlDbType.Bit);

            // set parameter values
            cmd.Parameters["@old_LOCATION_ID"].Value = oldLocation.locationId;
            cmd.Parameters["@old_DESCRIPTION"].Value = oldLocation.description;
            cmd.Parameters["@new_DESCRIPTION"].Value = newLocation.description;
            cmd.Parameters["@old_IS_ACTIVE"].Value = oldLocation.isActive;
            cmd.Parameters["@new_IS_ACTIVE"].Value = newLocation.isActive;

            // try-catch-finally

            try
            {
                // open a connection
                conn.Open();

                // execute and capture the result
                result = (int)cmd.ExecuteNonQuery();
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

        public static Location RetrieveLocationById(int locationId)
        {
            Location location = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_location";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@LOCATION_ID", SqlDbType.Int);
            cmd.Parameters["@LOCATION_ID"].Value = locationId;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    location = new Location()
                    {
                        locationId = reader.GetInt32(0),
                        description = reader.GetString(1),
                        isActive = reader.GetBoolean(2)
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

        public static List<Location> RetrieveAllLocations()
        {
            List<Location> locations = new List<Location>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_locationlist";
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
                            locationId = reader.GetInt32(0),
                            description = reader.GetString(1),
                            isActive = reader.GetBoolean(2)
                        });
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
            return locations;
        }
    }
}