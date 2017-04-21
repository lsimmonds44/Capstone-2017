using System;
using DataObjects;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DataAccessLayer
{
    /// <summary>
	/// Ethan Jorgensen
    /// Created on 2017-04-13
	///
    /// Contains the access methods for Product Lots
    /// </summary>
    public static class SupplierProductLotAccessor
    {

        /// <summary>
        /// Ethan Jorgensen
        /// Created: 2017-04-13
        /// 
        /// Adds a new product lot to the database.
        /// </summary>
        /// <param name="productLot">THe product lot to add.</param>
        /// <returns>Rows affected.</returns>
        public static int CreateSupplierProductLot(SupplierProductLot productLot)
        {
            int rows = 0;
            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_create_supplier_product_lot";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SUPPLIER_ID", productLot.SupplierId);
            cmd.Parameters.AddWithValue("@PRODUCT_ID", productLot.ProductId);
            cmd.Parameters.AddWithValue("@QUANTITY", productLot.Quantity);
            cmd.Parameters.AddWithValue("@EXPIRATION_DATE", productLot.ExpirationDate);

            try
            {
                conn.Open();

                rows = cmd.ExecuteNonQuery();
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
        /// Ethan Jorgensen
        /// 2017-04-13
        /// 
        /// Gets a product lot object using the productlotid 
        /// </summary>
        /// <param name="productLotID">the id to search on</param>
        /// <returns>A product lot</returns>
        public static SupplierProductLot RetrieveSupplierProductLot(int? productLotID)
        {
            SupplierProductLot lot = new SupplierProductLot();

            // Getting a SqlCommand object
            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_supplier_product_lot";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@SUPPLIER_PRODUCT_LOT_ID", productLotID);

            // Attempting to run the stored procedure
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();

                    lot = new SupplierProductLot()
                    {
                        SupplierProductLotId = reader.GetInt32(0),
                        SupplierId = reader.GetInt32(1),
                        ProductId = reader.GetInt32(2),
                        Quantity = reader.GetInt32(3),
                        ExpirationDate = reader.GetDateTime(4)

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
        /// Ethan Jorgensen
        /// 2017-04-13
        /// 
        /// Returns a list of SupplierProductLots by supplier
        /// </summary>
        /// <param name="supplier"></param>
        /// <returns></returns>
        public static List<SupplierProductLot> RetrieveSupplierProductLotsBySupplier(Supplier supplier)
        {
            List<SupplierProductLot> productLots = new List<SupplierProductLot>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_supplier_product_lot_by_supplier_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@SUPPLIER_ID", SqlDbType.Int);
            cmd.Parameters["@SUPPLIER_ID"].Value = supplier.SupplierID;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SupplierProductLot lot = new SupplierProductLot();

                        lot.SupplierProductLotId = reader.GetInt32(0);
                        lot.SupplierId = reader.GetInt32(1);
                        lot.ProductId = reader.GetInt32(2);
                        lot.Quantity = reader.GetInt32(3);
                        lot.ExpirationDate = reader.GetDateTime(4);
                        if (!reader.IsDBNull(5))
                        {
                            lot.Price = reader.GetDecimal(5);
                        }


                        productLots.Add(lot);
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
            return productLots;
        }

        /// <summary>
        /// Ethan Jorgensen
        /// 2017-04-13
        /// Gets a list of product lots from the database.
        /// </summary>
        /// <returns></returns>
        public static List<SupplierProductLot> RetrieveSupplierProductLots()
        {
            List<SupplierProductLot> lots = new List<SupplierProductLot>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_supplier_product_lot_list";
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
                        SupplierProductLot lot = new SupplierProductLot();

                        lot.SupplierProductLotId = reader.GetInt32(0);
                        lot.SupplierId = reader.GetInt32(1);
                        lot.ProductId = reader.GetInt32(2);
                        lot.Quantity = reader.GetInt32(3);
                        lot.ExpirationDate = reader.GetDateTime(4);
                        if (!reader.IsDBNull(5))
                        {
                            lot.Price = reader.GetDecimal(5);
                        }


                        lots.Add(lot);
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

            return lots;
        }

        public static bool DeleteSupplierProductLot(SupplierProductLot lot)
        {
            var result = false;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_delete_supplier_product_lot";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SUPPLIER_PRODUCT_LOT_ID", lot.SupplierProductLotId);

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery() == 1;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        /// <summary>
        /// Ethan Jorgensen
        /// 2017-04-13
        /// 
        /// Returns a product lot associated with the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static SupplierProductLot RetrieveSupplierProductLotById(int id)
        {
            SupplierProductLot lot = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_supplier_product_lot";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SUPPLIER_PRODUCT_LOT_ID", id);
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    lot = new SupplierProductLot();

                    lot.SupplierProductLotId = reader.GetInt32(0);
                    lot.SupplierId = reader.GetInt32(1);
                    lot.ProductId = reader.GetInt32(2);
                    lot.Quantity = reader.GetInt32(3);
                    lot.ExpirationDate = reader.GetDateTime(4);
                    if (!reader.IsDBNull(5))
                    {
                        lot.Price = reader.GetDecimal(5);
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

            return lot;
        }
    }
}
