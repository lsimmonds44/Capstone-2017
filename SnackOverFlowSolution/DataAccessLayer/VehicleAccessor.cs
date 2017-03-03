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
                        vehicles.Add(new Vehicle()
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
                        });
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


    } // End of class
} // End of namespace
