using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    /// <summary>
    /// Christian Lopez
    /// Created 2017/03/03
    /// 
    /// Handles accessing the DB for warehouse information
    /// </summary>
    public class WarehouseAccessor
    {
        /// <summary>
        /// Chrsitian Lopez
        /// Created 2017/03/03
        /// 
        /// Gets a list of all warehouses in the DB
        /// </summary>
        /// <returns></returns>
        public static List<Warehouse> RetrieveAllWarehouses()
        {
            List<Warehouse> warehouses = new List<Warehouse>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_warehouse_list";
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
                        Warehouse warehouse = new Warehouse
                        {
                            WarehouseID = reader.GetInt32(0),
                            AddressOne = reader.GetString(1),
                            AddressTwo = reader.GetString(2),
                            City = reader.GetString(3),
                            State = reader.GetString(4),
                            Zip = reader.GetString(5)
                        };
                        warehouses.Add(warehouse);
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return warehouses;
        }
    }
}
