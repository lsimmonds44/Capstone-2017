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
    /// <summary>
    /// Aaron Usher
    /// Created: 
    /// 2017/04/21
    /// 
    /// Class to handle database interactions involving vehicle types.
    /// </summary>
    public class VehicleTypeAccessor
    {
        /// <summary>
        /// Mason Allen
        /// Created: 
        /// 2017/03/01
        /// 
        /// Retrieves a list of all vehicle types from the database.
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
        /// <returns>List of all vehicle types from the database.</returns>
        public static List<VehicleType> RetreiveVehicleTypeList()
        {
            var vehicleTypes = new List<VehicleType>();

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
                    vehicleTypes.Add(new VehicleType()
                    {
                        VehicleTypeID = reader.GetString(0)
                    });
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

            return vehicleTypes;
        }
    }
}
