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
    public class ProductLotAccessor
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
                    ProductLotID = reader.GetInt32(0),
                    WarehouseID = reader.GetInt32(1),
                    SupplierID = reader.GetInt32(2),
                    LocationID = reader.GetInt32(3),
                    ProductID = reader.GetInt32(4),
                    SupplyManagerID = reader.GetInt32(5),
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
                    ProductLotID = reader.GetInt32(0),
                    WarehouseID = reader.GetInt32(1),
                    SupplierID = reader.GetInt32(2),
                    LocationID = reader.GetInt32(3),
                    ProductID = reader.GetInt32(4),
                    SupplyManagerID = reader.GetInt32(5),
                    Quantity = reader.GetInt32(6),
                    DateReceived = reader.GetDateTime(8),
                    ExpirationDate = reader.GetDateTime(9)
                };
            }
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
            cmd.Parameters["@SUPPLIER_ID"].Value = supplier.SupplierId;

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
            cmd.Parameters["@old_PRODUCT_LOT_ID"].Value = ProductLotInstance.ProductLotID;
            cmd.Parameters["@old_WAREHOUSE_ID"].Value = ProductLotInstance.WarehouseID;
            cmd.Parameters["@old_SUPPLIER_ID"].Value = ProductLotInstance.SupplierID;
            cmd.Parameters["@old_LOCATION_ID"].Value = ProductLotInstance.LocationID;
            cmd.Parameters["@old_PRODUCT_ID"].Value = ProductLotInstance.ProductID;
            cmd.Parameters["@old_SUPPLY_MANAGER_ID"].Value = ProductLotInstance.SupplyManagerID;
            cmd.Parameters["@old_QUANTITY"].Value = ProductLotInstance.Quantity;
            cmd.Parameters["@old_AVAILABLE_QUANTITY"].Value = 0;
            cmd.Parameters["@old_DATE_RECEIVED"].Value = ProductLotInstance.DateReceived;
            cmd.Parameters["@old_EXPIRATION_DATE"].Value = ProductLotInstance.ExpirationDate;
            cmd.Parameters["@new_WAREHOUSE_ID"].Value = ProductLotValidatorInstance.WarehouseID;
            cmd.Parameters["@new_SUPPLIER_ID"].Value = ProductLotValidatorInstance.SupplierID;
            cmd.Parameters["@new_LOCATION_ID"].Value = ProductLotValidatorInstance.LocationID;
            cmd.Parameters["@new_PRODUCT_ID"].Value = ProductLotValidatorInstance.ProductID;
            cmd.Parameters["@new_SUPPLY_MANAGER_ID"].Value = ProductLotValidatorInstance.SupplyManagerID;
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
            cmd.Parameters["@WAREHOUSE_ID"].Value = ProductLotInstance.WarehouseID;
            cmd.Parameters["@SUPPLIER_ID"].Value = ProductLotInstance.SupplierID;
            cmd.Parameters["@LOCATION_ID"].Value = ProductLotInstance.LocationID;
            cmd.Parameters["@PRODUCT_ID"].Value = ProductLotInstance.ProductID;
            cmd.Parameters["@SUPPLY_MANAGER_ID"].Value = ProductLotInstance.SupplyManagerID;
            cmd.Parameters["@QUANTITY"].Value = ProductLotInstance.Quantity;
            cmd.Parameters["@AVAILABLE_QUANTITY"].Value = 0;
            cmd.Parameters["@DATE_RECEIVED"].Value = ProductLotInstance.DateReceived;
            cmd.Parameters["@EXPIRATION_DATE"].Value = ProductLotInstance.ExpirationDate;
        }

        public void SetKeyParameters(SqlCommand cmd)
        {
            cmd.Parameters.Add("@PRODUCT_LOT_ID", SqlDbType.Int);
            cmd.Parameters["@PRODUCT_LOT_ID"].Value = ProductLotInstance.ProductLotID;
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
            cmd.Parameters["@PRODUCT_LOT_ID"].Value = ProductLotInstance.ProductLotID;
            cmd.Parameters["@WAREHOUSE_ID"].Value = ProductLotInstance.WarehouseID;
            cmd.Parameters["@SUPPLIER_ID"].Value = ProductLotInstance.SupplierID;
            cmd.Parameters["@LOCATION_ID"].Value = ProductLotInstance.LocationID;
            cmd.Parameters["@PRODUCT_ID"].Value = ProductLotInstance.ProductID;
            cmd.Parameters["@SUPPLY_MANAGER_ID"].Value = ProductLotInstance.SupplyManagerID;
            cmd.Parameters["@QUANTITY"].Value = ProductLotInstance.Quantity;
            cmd.Parameters["@AVAILABLE_QUANTITY"].Value = 0;
            cmd.Parameters["@DATE_RECEIVED"].Value = ProductLotInstance.DateReceived;
            cmd.Parameters["@EXPIRATION_DATE"].Value = ProductLotInstance.ExpirationDate;
        }
    }
}
