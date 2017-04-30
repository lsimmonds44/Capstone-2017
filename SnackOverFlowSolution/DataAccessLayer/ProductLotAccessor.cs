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
    /// Created: 
    /// 2017/02/15
    ///
    /// Class to handle database interactions involving product lots.
    /// </summary>
    public static class ProductLotAccessor
    {

        /// <summary>
        /// Aaron Usher
        /// Created: 
        /// 2017/04/07
        /// 
        /// Adds a new product lot to the database.
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
        /// <param name="productLot">The product lot to add.</param>
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
        /// Created: 
        /// 2017/02/16
        /// 
        /// Gets a product lot object using the given id 
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
        /// <param name="productLotID">the id to search on</param>
        /// <returns>A product lot</returns>
        public static ProductLot RetrieveProductLot(int? productLotID)
        {
            ProductLot productLot = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_product_lot";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PRODUCT_LOT_ID", productLotID);

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
                        ExpirationDate = reader.GetDateTime(9)

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

            return productLot;
        }

        /// <summary>
        /// Robert Forbes
        /// Created: 
        /// 2017/02/16
        /// 
        /// Updates the available quantity in the product lot table
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
        /// <param name="productLotID">The product lot to be updated</param>
        /// <param name="oldAvailableQuantity">The quantity that already exists in the database</param>
        /// <param name="newAvailableQuantity">The new quantity to insert into the database</param>
        /// <returns>Rows affected</returns>
        public static int UpdateProductLotAvailableQuantity(int? productLotID, int? oldAvailableQuantity, int? newAvailableQuantity)
        {
            var rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_update_product_lot_available_quantity";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PRODUCT_LOT_ID", productLotID);
            cmd.Parameters.AddWithValue("@old_AVAILABLE_QUANTITY", oldAvailableQuantity);
            cmd.Parameters.AddWithValue("@new_AVAILABLE_QUANTITY", newAvailableQuantity);

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
        /// Christian Lopez
        /// Created: 
        /// 2017/02/15
        /// 
        /// Retrieve the newest product lot based on the supplier
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
        /// <param name="supplier">The relevant supplier we are interested in.</param>
        /// <returns>The newest product lot related to the given supplier.</returns>
        public static ProductLot RetrieveNewestProductLotBySupplier(Supplier supplier)
        {
            ProductLot productLot = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_product_lot_by_supplier_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SUPPLIER_ID", supplier.SupplierID);
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
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
        /// Created: 
        /// 2017/03/29
        /// 
        /// Returns all of the product lots related to the given supplier.
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
        /// <param name="supplier">The relevant supplier.</param>
        /// <returns>A list of all product lots related to the given supplier.</returns>
        public static List<ProductLot> RetrieveProductLotsBySupplier(Supplier supplier)
        {
            var productLots = new List<ProductLot>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_product_lot_by_supplier_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SUPPLIER_ID", supplier.SupplierID);
            cmd.Parameters["@SUPPLIER_ID"].Value = supplier.SupplierID;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        productLots.Add(new ProductLot
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
                        });
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

            return productLots;
        }

        /// <summary>
        /// William Flood
        /// Created: 
        /// 2017/03/09
        /// 
        /// Returns a list of all expired product lots.
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
        /// <returns>A list of all expired product lots. </returns>
        public static List<ProductLot> RetrieveExpiredProductLots()
        {
            var productLots = new List<ProductLot>();

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
                        productLots.Add(new ProductLot
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
                        });
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

            return productLots;
        }

        /// <summary>
        /// Christian Lopez
        /// Created: 
        /// 2017/02/27
        /// 
        /// Gets a list of product lots from the database.
        /// </summary>
        /// 
        /// <remarks>
        /// Eric Walton
        /// Updated: 
        /// 2017/03/24
        /// 
        /// Added Grade and Price changed the while loop to handle a null price.
        /// </remarks>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 
        /// 2017/04/21
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <returns>A list of all product lots in the database.</returns>
        public static List<ProductLot> RetrieveProductLots()
        {
            var productLots = new List<ProductLot>();

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

                        productLots.Add(new ProductLot()
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
                            Grade = reader.GetString(10),
                            Price = reader.IsDBNull(11) ? (decimal?)null : reader.GetDecimal(11)
                        });
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

            return productLots;
        }

        /// <summary>
        /// Aaron Usher
        /// Updated: 
        /// 2017/04/21
        /// 
        /// Retrieves a list of all active product lots in the database.
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
        /// <returns>A list of all active product lots in the database.</returns>
        public static List<ProductLot> RetrieveActiveProductLots()
        {
            var productLots = new List<ProductLot>();

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

                        productLots.Add(new ProductLot()
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
                            Grade = reader.GetString(10),
                            Price = reader.IsDBNull(11) ? (decimal?)null : reader.GetDecimal(11)
                        });
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

            return productLots;
        }

        /// <summary>
        /// Aaron Usher
        /// Updated: 
        /// 2017/04/21
        /// 
        /// Deletes a product lot from the database.
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
        /// <param name="productLot">The product lot to delete.</param>
        /// <returns>Rows affected.</returns>
        public static int DeleteProductLot(ProductLot productLot)
        {
            var rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_delete_product_lot";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PRODUCT_LOT_ID", productLot.ProductLotId);

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
        /// Christian Lopez
        /// Created: 
        /// 2017/03/29
        /// 
        /// Returns a product lot associated with the id
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
        /// <param name="id">The id of the needed product lot.</param>
        /// <returns>The relevant product lot.</returns>
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
        /// Created: 
        /// 2017/02/17
        /// 
        /// Writes an updated product unit price to the database
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
        /// <param name="productID">The id of the product to change.</param>
        /// <param name="newPrice">The price as it should be in the database.</param>
        /// <returns>Rows affected.</returns>
        public static int UpdateProductPrice(int productID, decimal newPrice)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_update_product_price";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PRODUCT_LOT_ID", productID);
            cmd.Parameters.AddWithValue("@new_PRICE", newPrice);

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
    }
}
