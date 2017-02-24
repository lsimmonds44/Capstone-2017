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
    /// 2017/02/16
    /// </summary>
    public static class ProductLotAccessor
    {

        /// <summary>
        /// Robert Forbes
        /// 2017/02/16
        /// 
        /// Gets a product lot object using the productlotid 
        /// </summary>
        /// <param name="productLotID">the id to search on</param>
        /// <returns>A product lot</returns>
        public static ProductLot RetrieveProductLot(int? productLotID)
        {
            ProductLot lot = new ProductLot();

            // Getting a SqlCommand object
            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_product_lot";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@PRODUCT_LOT_ID", productLotID);

            // Attempting to run the stored procedure
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();

                    lot = new ProductLot()
                    {
                        productLotId = reader.GetInt32(0),
                        warehouseId = reader.GetInt32(1),
                        supplierId = reader.GetInt32(2),
                        locationId = reader.GetInt32(3),
                        productId = reader.GetInt32(4),
                        supplyManagerId = reader.GetInt32(5),
                        quantity = reader.GetInt32(6),
                        availableQuantity = reader.GetInt32(7),
                        dateRecieved = reader.GetDateTime(8),
                        expirationDate = reader.GetDateTime(9)

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

            return lot;
        }

        /// <summary>
        /// Robert Forbes
        /// 2017/02/16
        /// 
        /// Updates the available quantity in the product lot table
        /// </summary>
        /// <param name="productLotID">The product lot to be updated</param>
        /// <param name="oldAvailableQuantity">The quantity that already exists in the database</param>
        /// <param name="newAvailableQuantity">The new quantity to insert into the database</param>
        /// <returns>int rows affected</returns>
        public static int UpdateProductLotAvailableQuantity(int? productLotID, int? oldAvailableQuantity, int? newAvailableQuantity)
        {
            int result = 0;

            // Getting a SqlCommand object
            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_update_product_lot_available_quantity";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@PRODUCT_LOT_ID", productLotID);
            cmd.Parameters.AddWithValue("@old_AVAILABLE_QUANTITY", oldAvailableQuantity);
            cmd.Parameters.AddWithValue("@new_AVAILABLE_QUANTITY", newAvailableQuantity);

            // Attempting to run the stored procedure
            try
            {
                conn.Open();
                // Storing the amount of rows that were affected by the stored procedure
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



    }
}
