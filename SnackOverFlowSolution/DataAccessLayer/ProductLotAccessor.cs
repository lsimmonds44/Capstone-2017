using System;
﻿using DataObjects;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DataAccessLayer
{
    /// <summary>
	/// Christian Lopez
    /// Created on 2017/02/15
    /// Edited on 2017/02/24 by William Flood
	///
    /// Contains the access methods for Product Lots
    /// </summary>
    public static class ProductLotAccessor
    {
		
        /// <summary>
        /// Aaron Usher
        /// Created: 2017/04/07
        /// 
        /// Adds a new product lot to the database.
        /// </summary>
        /// <param name="productLot">THe product lot to add.</param>
        /// <returns>Rows affected.</returns>
        public static int CreateProductLot(ProductLot productLot)
        {
            int rows = 0;
            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_create_product_lot";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@WAREHOUSE_ID", productLot.WarehouseId);
            cmd.Parameters.AddWithValue("@SUPPLIER_ID", productLot.SupplierId);
            cmd.Parameters.AddWithValue("@LOCATION_ID", productLot.LocationId);
            cmd.Parameters.AddWithValue("@PRODUCT_ID", productLot.ProductId);
            cmd.Parameters.AddWithValue("@SUPPLY_MANAGER_ID", productLot.SupplyManagerId);
            cmd.Parameters.AddWithValue("@QUANTITY", productLot.Quantity);
            cmd.Parameters.AddWithValue("@AVAILABLE_QUANTITY", productLot.AvailableQuantity);
            cmd.Parameters.AddWithValue("@DATE_RECEIVED", productLot.DateReceived);
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
                        ProductLotId = reader.GetInt32(0),
                        WarehouseId = reader.GetInt32(1),
                        SupplierId = reader.GetInt32(2),
                        LocationId = reader.GetInt32(3),
                        ProductId = reader.GetInt32(4),
                        SupplyManagerId = reader.GetInt32(5),
                        Quantity = reader.GetInt32(6),
                        AvailableQuantity = reader.GetInt32(7),
                        DateReceived = reader.GetDateTime(8),
                        ExpirationDate = reader.GetDateTime(9)

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

        /// <summary>
        /// Christian Lopez
        /// Created on 2017/02/15
        /// 
        /// Retrieve the newest product lot based on the supplier
        /// </summary>
        /// <param name="supplier">The Supplier we are interested in</param>
        /// <returns></returns>
        public static ProductLot RetrieveNewestProductLot(Supplier supplier)
        {
            ProductLot productLot = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_product_lot_by_supplier_id";
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
                    // Although sp returns a list, we only want the newest one (largest ID returned first)
                    reader.Read();
                    productLot = new ProductLot
                    {
                        ProductLotId = reader.GetInt32(0),
                        WarehouseId = reader.GetInt32(1),
                        SupplierId = reader.GetInt32(2),
                        LocationId = reader.GetInt32(3),
                        ProductId = reader.GetInt32(4),
                        SupplyManagerId = reader.GetInt32(5),
                        Quantity = reader.GetInt32(6),
                        AvailableQuantity = reader.GetInt32(7),
                        DateReceived = reader.GetDateTime(8),
                        ExpirationDate = reader.GetDateTime(9)
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
            return productLot;
        }

        /// <summary>
        /// Christian Lopez
        /// 2017/03/29
        /// 
        /// Returns a list of ProductLots by supplier
        /// </summary>
        /// <param name="supplier"></param>
        /// <returns></returns>
        public static List<ProductLot> RetrieveProductLotsBySupplier(Supplier supplier)
        {
            List<ProductLot> productLots = new List<ProductLot>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_product_lot_by_supplier_id";
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
                        ProductLot productLot = new ProductLot
                        {
                            ProductLotId = reader.GetInt32(0),
                            WarehouseId = reader.GetInt32(1),
                            SupplierId = reader.GetInt32(2),
                            LocationId = reader.GetInt32(3),
                            ProductId = reader.GetInt32(4),
                            SupplyManagerId = reader.GetInt32(5),
                            Quantity = reader.GetInt32(6),
                            AvailableQuantity = reader.GetInt32(7),
                            DateReceived = reader.GetDateTime(8),
                            ExpirationDate = reader.GetDateTime(9)
                        };
                        productLots.Add(productLot);
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
        /// Created 2017/03/09 by William Flood
        /// 
        /// Returns a list of expired product lots
        /// </summary>
        /// <returns></returns>
        public static List<ProductLot> RetrieveExpiredProductLots()
        {
            List<ProductLot> lots = new List<ProductLot>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_expired_product_lot_list";
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
                        ProductLot lot = new ProductLot
                        {
                            ProductLotId = reader.GetInt32(0),
                            WarehouseId = reader.GetInt32(1),
                            SupplierId = reader.GetInt32(2),
                            LocationId = reader.GetInt32(3),
                            ProductId = reader.GetInt32(4),
                            SupplyManagerId = reader.GetInt32(5),
                            Quantity = reader.GetInt32(6),
                            AvailableQuantity = reader.GetInt32(7),
                            DateReceived = reader.GetDateTime(8),
                            ExpirationDate = reader.GetDateTime(9)
                        };
                        lots.Add(lot);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return lots;
        }

        /// <summary>
        /// Christian Lopez
        /// 2017/02/27
        /// Modified Eric Walton
        /// 2017/03/24
        /// Added Grade and Price changed the while loop to handle a null price.
        /// Gets a list of product lots from the database.
        /// </summary>
        /// <returns></returns>
        public static List<ProductLot> RetrieveProductLots()
        {
            List<ProductLot> lots = new List<ProductLot>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_product_lot_list";
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

                        ProductLot lot = new ProductLot();

                            lot.ProductLotId = reader.GetInt32(0);
                            lot.WarehouseId = reader.GetInt32(1);
                            lot.SupplierId = reader.GetInt32(2);
                            lot.LocationId = reader.GetInt32(3);
                            lot.ProductId = reader.GetInt32(4);
                            lot.SupplyManagerId = reader.GetInt32(5);
                            lot.Quantity = reader.GetInt32(6);
                            lot.AvailableQuantity = reader.GetInt32(7);
                            lot.DateReceived = reader.GetDateTime(8);
                            lot.ExpirationDate = reader.GetDateTime(9);
                            lot.Grade = reader.GetString(10);
                            if (!reader.IsDBNull(11))
                            {
                                lot.Price = reader.GetDecimal(11);
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

        public static List<ProductLot> RetrieveActiveProductLots()
        {
            List<ProductLot> lots = new List<ProductLot>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_active_product_lot_list";
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

                        ProductLot lot = new ProductLot();

                        lot.ProductLotId = reader.GetInt32(0);
                        lot.WarehouseId = reader.GetInt32(1);
                        lot.SupplierId = reader.GetInt32(2);
                        lot.LocationId = reader.GetInt32(3);
                        lot.ProductId = reader.GetInt32(4);
                        lot.SupplyManagerId = reader.GetInt32(5);
                        lot.Quantity = reader.GetInt32(6);
                        lot.AvailableQuantity = reader.GetInt32(7);
                        lot.DateReceived = reader.GetDateTime(8);
                        lot.ExpirationDate = reader.GetDateTime(9);
                        lot.Grade = reader.GetString(10);
                        if (!reader.IsDBNull(11))
                        {
                            lot.Price = reader.GetDecimal(11);
                        }


                        lots.Add(lot);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return lots;
        }
       
        public static bool DeleteProductLot(ProductLot lot)
        {
            var result = false;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_delete_product_lot";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PRODUCT_LOT_ID", lot.ProductLotId);

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
        /// Christian Lopez
        /// 2017/03/29
        /// 
        /// Returns a product lot associated with the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ProductLot RetrieveProductLotById(int id)
        {
            ProductLot productLot = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_product_lot";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PRODUCT_LOT_ID", id);
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    productLot = new ProductLot()
                    {
                        ProductLotId = reader.GetInt32(0),
                        WarehouseId = reader.GetInt32(1),
                        SupplierId = reader.GetInt32(2),
                        LocationId = reader.GetInt32(3),
                        ProductId = reader.GetInt32(4),
                        SupplyManagerId = reader.GetInt32(5),
                        Quantity = reader.GetInt32(6),
                        AvailableQuantity = reader.GetInt32(7),
                        DateReceived = reader.GetDateTime(8),
                        ExpirationDate = reader.GetDateTime(9),
                        Grade = reader.GetString(10)
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

            return productLot;
        }

        /// <summary>
        /// Ryan Spurgetis
        /// 02/17/2017
        /// 
        /// Writes an updated product unit price to the database
        /// </summary>
        /// <param name="productID"></param>
        /// <param name="oldPrice"></param>
        /// <param name="newPrice"></param>
        /// <returns></returns>
        public static int UpdateProductPrice(int productID, decimal newPrice)
        {
            int result = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_update_product_price";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@PRODUCT_LOT_ID", SqlDbType.Int);
            cmd.Parameters.Add("@new_PRICE", SqlDbType.Decimal);

            cmd.Parameters["@PRODUCT_LOT_ID"].Value = productID;
            cmd.Parameters["@new_PRICE"].Value = newPrice;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw new ApplicationException("A problem occurred updating the product price.");
            }
            finally
            {
                conn.Close();
            }

            return result;
        }
    }
}
