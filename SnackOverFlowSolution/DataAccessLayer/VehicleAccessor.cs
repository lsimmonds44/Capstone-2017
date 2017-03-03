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
                        LatestRepair = reader.GetDateTime(8),
                        LastDriver = reader.GetInt32(9),
                        VehicleTypeID = reader.GetString(10)
                    };
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
            return vehicle;
        }

        /// <summary>
        /// Created by Mason Allen
        /// Created on 03/01/2017
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
            var count = 0;

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
                count = cmd.ExecuteNonQuery();
            }

            catch (Exception)
            {
                throw;
            }

            finally
            {
                conn.Close();
            }

            return count;
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

                try
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
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
