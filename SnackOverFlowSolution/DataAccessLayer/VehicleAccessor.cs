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

         
    }
}
