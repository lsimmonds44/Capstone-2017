using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DataObjects;

namespace DataAccessLayer
{
    public class VehicleTypeAccessor
    {
        /// <summary>
        /// Mason Allen
        /// Created 03/01/2017
        /// 
        /// Retrieves a list of the current vehicle types
        /// </summary>
        /// <returns>List<VehicleType></returns>

        public static List<VehicleType> RetreiveVehicleTypeList()
        {
            var vehicleTypeList = new List<VehicleType>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_vehicle_type_list";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    VehicleType currentVehicleType = new VehicleType()
                    {
                        VehicleTypeID = reader.GetString(0)
                    };
                    vehicleTypeList.Add(currentVehicleType);
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
            return vehicleTypeList;
        }
    }
}
