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
    public class ProductLotAccessor : IDataAccessor
    {
		
		
        public ProductLot ProductLotInstance { get; set; }
        public ProductLot ProductLotValidatorInstance { get; set; }
        public List<ProductLot> ProductLotList { get; set; }
        public string CreateScript
        {
            get
            {
                return "sp_create_product_lot";
            }
        }

        public string DeactivateScript
        {
            get
            {
                return "sp_delete_product_lot";
            }
        }

        public string RetrieveListScript
        {
            get
            {
                return "sp_retrieve_product_lot_list";
            }
        }

        public string RetrieveSearchScript
        {
            get
            {
                return "sp_retrieve_product_lot_from_search";
            }
        }

        public string RetrieveSingleScript
        {
            get
            {
                return "sp_retrieve_product_lot";
            }
        }

        public string UpdateScript
        {
            get
            {
                return "sp_update_product_lot";
            }
        }

        public void ReadList(SqlDataReader reader)
        {
            ProductLotList = new List<ProductLot>();
            while(reader.Read())
            {
                ProductLotList.Add(new ProductLot()
                {
                    ProductLotId = reader.GetInt32(0),
                    WarehouseId = reader.GetInt32(1),
                    SupplierId = reader.GetInt32(2),
                    LocationId = reader.GetInt32(3),
                    ProductId = reader.GetInt32(4),
                    SupplyManagerId = reader.GetInt32(5),
                    Quantity = reader.GetInt32(6),
                    DateReceived = reader.GetDateTime(8),
                    ExpirationDate = reader.GetDateTime(9)
                });
            }
        }

        public void ReadSingle(SqlDataReader reader)
        {
            ProductLotInstance = null;
            while(reader.Read())
            {

                ProductLotInstance = new ProductLot()
                {
                    ProductLotId = reader.GetInt32(0),
                    WarehouseId = reader.GetInt32(1),
                    SupplierId = reader.GetInt32(2),
                    LocationId = reader.GetInt32(3),
                    ProductId = reader.GetInt32(4),
                    SupplyManagerId = reader.GetInt32(5),
                    Quantity = reader.GetInt32(6),
                    DateReceived = reader.GetDateTime(8),
                    ExpirationDate = reader.GetDateTime(9)
                };
            }
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
            catch (Exception ex)
            {

                throw new ApplicationException("There was an issue connecting to the database: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return productLot;
        }

        /// <summary>
        /// Christian Lopez
        /// 2017/02/27
        /// 
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

        public void SetUpdateParameters(SqlCommand cmd)
        {
            cmd.Parameters.Add("@old_PRODUCT_LOT_ID", SqlDbType.Int);
            cmd.Parameters.Add("@old_WAREHOUSE_ID", SqlDbType.Int);
            cmd.Parameters.Add("@old_SUPPLIER_ID", SqlDbType.Int);
            cmd.Parameters.Add("@old_LOCATION_ID", SqlDbType.Int);
            cmd.Parameters.Add("@old_PRODUCT_ID", SqlDbType.Int);
            cmd.Parameters.Add("@old_SUPPLY_MANAGER_ID", SqlDbType.Int);
            cmd.Parameters.Add("@old_QUANTITY", SqlDbType.Int);
            cmd.Parameters.Add("@old_AVAILABLE_QUANTITY", SqlDbType.Int);
            cmd.Parameters.Add("@old_DATE_RECEIVED", SqlDbType.DateTime);
            cmd.Parameters.Add("@old_EXPIRATION_DATE", SqlDbType.DateTime);
            cmd.Parameters.Add("@new_WAREHOUSE_ID", SqlDbType.Int);
            cmd.Parameters.Add("@new_SUPPLIER_ID", SqlDbType.Int);
            cmd.Parameters.Add("@new_LOCATION_ID", SqlDbType.Int);
            cmd.Parameters.Add("@new_PRODUCT_ID", SqlDbType.Int);
            cmd.Parameters.Add("@new_SUPPLY_MANAGER_ID", SqlDbType.Int);
            cmd.Parameters.Add("@new_QUANTITY", SqlDbType.Int);
            cmd.Parameters.Add("@new_AVAILABLE_QUANTITY", SqlDbType.Int);
            cmd.Parameters.Add("@new_DATE_RECEIVED", SqlDbType.DateTime);
            cmd.Parameters.Add("@new_EXPIRATION_DATE", SqlDbType.DateTime);
            cmd.Parameters["@old_PRODUCT_LOT_ID"].Value = ProductLotInstance.ProductLotId;
            cmd.Parameters["@old_WAREHOUSE_ID"].Value = ProductLotInstance.WarehouseId;
            cmd.Parameters["@old_SUPPLIER_ID"].Value = ProductLotInstance.SupplierId;
            cmd.Parameters["@old_LOCATION_ID"].Value = ProductLotInstance.LocationId;
            cmd.Parameters["@old_PRODUCT_ID"].Value = ProductLotInstance.ProductId;
            cmd.Parameters["@old_SUPPLY_MANAGER_ID"].Value = ProductLotInstance.SupplyManagerId;
            cmd.Parameters["@old_QUANTITY"].Value = ProductLotInstance.Quantity;
            cmd.Parameters["@old_AVAILABLE_QUANTITY"].Value = 0;
            cmd.Parameters["@old_DATE_RECEIVED"].Value = ProductLotInstance.DateReceived;
            cmd.Parameters["@old_EXPIRATION_DATE"].Value = ProductLotInstance.ExpirationDate;
            cmd.Parameters["@new_WAREHOUSE_ID"].Value = ProductLotValidatorInstance.WarehouseId;
            cmd.Parameters["@new_SUPPLIER_ID"].Value = ProductLotValidatorInstance.SupplierId;
            cmd.Parameters["@new_LOCATION_ID"].Value = ProductLotValidatorInstance.LocationId;
            cmd.Parameters["@new_PRODUCT_ID"].Value = ProductLotValidatorInstance.ProductId;
            cmd.Parameters["@new_SUPPLY_MANAGER_ID"].Value = ProductLotValidatorInstance.SupplyManagerId;
            cmd.Parameters["@new_QUANTITY"].Value = ProductLotValidatorInstance.Quantity;
            cmd.Parameters["@new_AVAILABLE_QUANTITY"].Value = 0;
            cmd.Parameters["@new_DATE_RECEIVED"].Value = ProductLotValidatorInstance.DateReceived;
            cmd.Parameters["@new_EXPIRATION_DATE"].Value = ProductLotValidatorInstance.ExpirationDate;
		}

        public void SetCreateParameters(SqlCommand cmd)
        {
            cmd.Parameters.Add("@WAREHOUSE_ID", SqlDbType.Int);
            cmd.Parameters.Add("@SUPPLIER_ID", SqlDbType.Int);
            cmd.Parameters.Add("@LOCATION_ID", SqlDbType.Int);
            cmd.Parameters.Add("@PRODUCT_ID", SqlDbType.Int);
            cmd.Parameters.Add("@SUPPLY_MANAGER_ID", SqlDbType.Int);
            cmd.Parameters.Add("@QUANTITY", SqlDbType.Int);
            cmd.Parameters.Add("@AVAILABLE_QUANTITY", SqlDbType.Int);
            cmd.Parameters.Add("@DATE_RECEIVED", SqlDbType.DateTime);
            cmd.Parameters.Add("@EXPIRATION_DATE", SqlDbType.DateTime);
            cmd.Parameters["@WAREHOUSE_ID"].Value = ProductLotInstance.WarehouseId;
            cmd.Parameters["@SUPPLIER_ID"].Value = ProductLotInstance.SupplierId;
            cmd.Parameters["@LOCATION_ID"].Value = ProductLotInstance.LocationId;
            cmd.Parameters["@PRODUCT_ID"].Value = ProductLotInstance.ProductId;
            cmd.Parameters["@SUPPLY_MANAGER_ID"].Value = ProductLotInstance.SupplyManagerId;
            cmd.Parameters["@QUANTITY"].Value = ProductLotInstance.Quantity;
            cmd.Parameters["@AVAILABLE_QUANTITY"].Value = 0;
            cmd.Parameters["@DATE_RECEIVED"].Value = ProductLotInstance.DateReceived;
            cmd.Parameters["@EXPIRATION_DATE"].Value = ProductLotInstance.ExpirationDate;
        }

        public void SetKeyParameters(SqlCommand cmd)
        {
            cmd.Parameters.Add("@PRODUCT_LOT_ID", SqlDbType.Int);
            cmd.Parameters["@PRODUCT_LOT_ID"].Value = ProductLotInstance.ProductLotId;
        }

        public void SetRetrieveSearchParameters(SqlCommand cmd)
        {
            cmd.Parameters.Add("@PRODUCT_LOT_ID", SqlDbType.Int);
            cmd.Parameters.Add("@WAREHOUSE_ID", SqlDbType.Int);
            cmd.Parameters.Add("@SUPPLIER_ID", SqlDbType.Int);
            cmd.Parameters.Add("@LOCATION_ID", SqlDbType.Int);
            cmd.Parameters.Add("@PRODUCT_ID", SqlDbType.Int);
            cmd.Parameters.Add("@SUPPLY_MANAGER_ID", SqlDbType.Int);
            cmd.Parameters.Add("@QUANTITY", SqlDbType.Int);
            cmd.Parameters.Add("@AVAILABLE_QUANTITY", SqlDbType.Int);
            cmd.Parameters.Add("@DATE_RECEIVED", SqlDbType.DateTime);
            cmd.Parameters.Add("@EXPIRATION_DATE", SqlDbType.DateTime);
            cmd.Parameters["@PRODUCT_LOT_ID"].Value = ProductLotInstance.ProductLotId;
            cmd.Parameters["@WAREHOUSE_ID"].Value = ProductLotInstance.WarehouseId;
            cmd.Parameters["@SUPPLIER_ID"].Value = ProductLotInstance.SupplierId;
            cmd.Parameters["@LOCATION_ID"].Value = ProductLotInstance.LocationId;
            cmd.Parameters["@PRODUCT_ID"].Value = ProductLotInstance.ProductId;
            cmd.Parameters["@SUPPLY_MANAGER_ID"].Value = ProductLotInstance.SupplyManagerId;
            cmd.Parameters["@QUANTITY"].Value = ProductLotInstance.Quantity;
            cmd.Parameters["@AVAILABLE_QUANTITY"].Value = 0;
            cmd.Parameters["@DATE_RECEIVED"].Value = ProductLotInstance.DateReceived;
            cmd.Parameters["@EXPIRATION_DATE"].Value = ProductLotInstance.ExpirationDate;
        }
    }
}
