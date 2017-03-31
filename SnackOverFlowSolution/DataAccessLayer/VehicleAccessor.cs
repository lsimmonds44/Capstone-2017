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
    public class VehicleAccessor
    {
        /// <summary>
        /// Victor Algarin
        /// Created 2017/03/01
        /// 
        /// Retrieves details for a specific vehicle through a vehicle id
        /// </summary>
        /// <param name="vehicleId">Pertains to the specific vehicle that will be retreived from the DB</param>
        /// <returns>Vehicle</returns>

        public static Vehicle RetreiveVehicleByVehicleId(int vehicleId)
        {
            var vehicle = new Vehicle();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_vehicle";
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
                    reader.Read();

                    vehicle = new Vehicle();

                    vehicle.VehicleID = reader.GetInt32(0);
                    vehicle.VIN = reader.GetString(1);
                    vehicle.Make = reader.GetString(2);
                    vehicle.Model = reader.GetString(3);
                    vehicle.Mileage = reader.GetInt32(4);
                    vehicle.Year = reader.GetString(5);
                    vehicle.Color = reader.GetString(6);
                    vehicle.Active = reader.GetBoolean(7);
                    if (!reader.IsDBNull(8))
                    {
                        vehicle.LatestRepair = reader.GetDateTime(8);
                    }
                    if (!reader.IsDBNull(9))
                    {
                        vehicle.LastDriver = reader.GetInt32(9);
                    }
                    vehicle.VehicleTypeID = reader.GetString(10);
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
            return vehicle;
        }

        /// <summary>
        /// Mason Allen
        /// Created 2017/03/01 
        /// 
        /// Creates a new vehicle
        /// </summary>
        /// <param name="newVehicle">Vehicle Object to be Created</param>
        /// <returns>Returns an int of 1 if successful, 0 if not</returns>
        public static int CreateVehicle(Vehicle newVehicle)
        {
            var conn = DBConnection.GetConnection();
            const string cmdText = @"sp_create_vehicle";
            var cmd = new SqlCommand(cmdText, conn);
            int newVehicleId = 0;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@VIN", newVehicle.VIN);
            cmd.Parameters.AddWithValue("@MAKE", newVehicle.Make);
            cmd.Parameters.AddWithValue("@MODEL", newVehicle.Model);
            cmd.Parameters.AddWithValue("@MILEAGE", newVehicle.Mileage);
            cmd.Parameters.AddWithValue("@YEAR", newVehicle.Year);
            cmd.Parameters.AddWithValue("@COLOR", newVehicle.Color);
            cmd.Parameters.AddWithValue("@ACTIVE", newVehicle.Active);
            cmd.Parameters.AddWithValue("@LATEST_REPAIR_DATE", newVehicle.LatestRepair);
            cmd.Parameters.AddWithValue("@LAST_DRIVER_ID", newVehicle.LastDriver);
            cmd.Parameters.AddWithValue("@VEHICLE_TYPE_ID", newVehicle.VehicleTypeID);

            try
            {
                conn.Open();
                newVehicleId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            catch (Exception)
            {
                throw;
            }

            finally
            {
                conn.Close();
            }

            return newVehicleId;
        }

        public static List<Vehicle> RetrieveAllVehicles()
        {
            var vehicles = new List<Vehicle>();
            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_vehicle_list";
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

                        var tempVehicle = new Vehicle();
                        tempVehicle.VehicleID = reader.GetInt32(0);
                        tempVehicle.VIN = reader.GetString(1);
                        tempVehicle.Make = reader.GetString(2);
                        tempVehicle.Model = reader.GetString(3);
                        tempVehicle.Mileage = reader.GetInt32(4);
                        tempVehicle.Year = reader.GetString(5);
                        tempVehicle.Color = reader.GetString(6);
                        tempVehicle.Active = reader.GetBoolean(7);
                        if (!reader.IsDBNull(8))
                        {
                            tempVehicle.LatestRepair = reader.GetDateTime(8);
                        }
                        if (!reader.IsDBNull(9))
                        {
                            tempVehicle.LastDriver = reader.GetInt32(9);
                        }
                        tempVehicle.VehicleTypeID = reader.GetString(10);

                        tempVehicle.CheckedOut = reader.GetBoolean(11);

                        if (!reader.IsDBNull(12))
                        {
                            tempVehicle.CheckedOutTimeDate = reader.GetDateTime(12);
                        }

                        vehicles.Add(tempVehicle);
                    }
                    reader.Close();
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
            return vehicles;
        }

        /// <summary>
        /// Laura Simmonds
        /// Created 2017/03/28
        /// 
        /// Changes vehicle status to checked out or checked in and records
        /// the date and time of the change.
        /// </summary>
        /// <returns>Returns 1 if vehicle is checked out, 0 if vehicle is checked in</returns>
        public static int CheckVehicleOutIn(Vehicle oldVehicle)
        {
            var result = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_update_checked_out_status";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@vehicleId", SqlDbType.Int);
            cmd.Parameters["@vehicleId"].Value = oldVehicle.VehicleID;
            Console.WriteLine("vID: " + oldVehicle.VehicleID);
            cmd.Parameters.AddWithValue("@old_Checked_Out_Status", oldVehicle.CheckedOut);
            cmd.Parameters.AddWithValue("@new_Checked_Out_Status", !oldVehicle.CheckedOut);

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
        /// Created: 2017/02/17
        /// 
        /// Gets a vehicle based on a specific deliveryId.
        /// </summary>
        /// <param name="deliveryId">The deliveryId.</param>
        /// <returns>The vehicle.</returns>
        public static Vehicle RetrieveVehicleByDelivery(int deliveryId)
        {
            Vehicle vehicle = null;
            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_vehicle_from_delivery";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.Parameters.AddWithValue("@DELIVERY_ID", deliveryId);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        vehicle = new Vehicle()
                        {
                            VehicleID = reader.GetInt32(0),
                            VIN = reader.GetString(1),
                            Make = reader.GetString(2),
                            Model = reader.GetString(3),
                            Mileage = reader.GetInt32(4),
                            Year = reader.GetString(5),
                            Color = reader.GetString(6),
                            Active = reader.GetBoolean(7),
                            LatestRepair = reader.IsDBNull(8) ? (DateTime?)null : reader.GetDateTime(8),
                            LastDriver = reader.IsDBNull(9) ? (int?)null : reader.GetInt32(9),
                            VehicleTypeID = reader.GetString(10)
                        };
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

            return vehicle;
        }



    } // End of class
} // End of namespace
