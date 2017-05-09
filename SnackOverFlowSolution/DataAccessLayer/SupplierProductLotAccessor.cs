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
    /// Created: 
    /// 2017/04/13
    ///
    /// Class to handle database interactions involving supplier product lots.
    public static class SupplierProductLotAccessor
    {

        /// <summary>
        /// Ethan Jorgensen
        /// Created: 
        /// 2017/04/13
        /// 
        /// Adds a new product lot to the database.
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 
        /// 2017/04/28
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <param name="supplierProductLot">The product lot to add.</param>
        /// <returns>Rows affected.</returns>
        public static int CreateSupplierProductLot(SupplierProductLot supplierProductLot)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_create_supplier_product_lot";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SUPPLIER_ID", supplierProductLot.SupplierId);
            cmd.Parameters.AddWithValue("@PRODUCT_ID", supplierProductLot.ProductId);
            cmd.Parameters.AddWithValue("@QUANTITY", supplierProductLot.Quantity);
            cmd.Parameters.AddWithValue("@EXPIRATION_DATE", supplierProductLot.ExpirationDate);

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
        /// Created: 
        /// 2017/04/13
        /// 
        /// Gets a product lot object using the productlotid 
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated:
        /// 2017/04/28
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <param name="productLotID">the id to search on</param>
        /// <returns>A product lot</returns>
        public static SupplierProductLot RetrieveSupplierProductLot(int productLotID)
        {
            SupplierProductLot supplierProductLot = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_supplier_product_lot";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SUPPLIER_PRODUCT_LOT_ID", productLotID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    supplierProductLot = new SupplierProductLot()
                    {
                        SupplierProductLotId = reader.GetInt32(0),
                        SupplierId = reader.GetInt32(1),
                        ProductId = reader.GetInt32(2),
                        Quantity = reader.GetInt32(3),
                        ExpirationDate = reader.GetDateTime(4),
                        Price = reader.IsDBNull(5) ? (decimal?)null : reader.GetDecimal(5)
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

            return supplierProductLot;
        }

        /// <summary>
        /// Ethan Jorgensen
        /// Created:
        /// 2017/04/13
        /// 
        /// Returns a list of SupplierProductLots by supplier
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated:
        /// 2017/04/28
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <param name="supplier">The relevant supplier.</param>
        /// <returns>A list of SupplierProductLots.</returns>
        public static List<SupplierProductLot> RetrieveSupplierProductLotsBySupplier(Supplier supplier)
        {
            var supplierProductLots = new List<SupplierProductLot>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_supplier_product_lot_by_supplier_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SUPPLIER_ID", supplier.SupplierID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SupplierProductLot lot = new SupplierProductLot()
                        {
                            SupplierProductLotId = reader.GetInt32(0),
                            SupplierId = reader.GetInt32(1),
                            ProductId = reader.GetInt32(2),
                            Quantity = reader.GetInt32(3),
                            ExpirationDate = reader.GetDateTime(4),
                            Price = reader.IsDBNull(5) ? (decimal?)null : reader.GetDecimal(5)
                        };

                        supplierProductLots.Add(lot);
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

            return supplierProductLots;
        }

        /// <summary>
        /// Laura Simmonds
        /// Created 2017/05/05
        /// </remarks>
        /// 
        /// <returns>A list of all supplier product lots.</returns>
        public static List<SupplierProductLot> RetrieveSupplierProducts(int supplierId)
        {
            var supplierProducts = new List<SupplierProductLot>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_supplier_product";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SUPPLIER_ID", supplierId);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        supplierProducts.Add(new SupplierProductLot()
                        {
                            ProductName = reader.GetString(0),
                            ProductId = reader.GetInt32(1),
                            Price = reader.IsDBNull(2) ? (decimal?)null : reader.GetDecimal(2)
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

            return supplierProducts;
        }

        /// <summary>
        /// Ethan Jorgensen
        /// Created:
        /// 2017/05/09
        /// 
        /// Updates a supplier product lot
        /// </summary>
        /// <remarks>
        /// Ethan Jorgensen
        /// Updated: 
        /// 2017/05/09
        /// 
        /// Standardize method.
        /// </remarks>
        public static bool UpdateSupplierProductLot(SupplierProductLot oldLot, SupplierProductLot newLot)
        {
            var result = false;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_update_supplier_product_lot";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@old_PRODUCT_LOT_ID", oldLot.SupplierProductLotId);
            cmd.Parameters.AddWithValue("@old_SUPPLIER_ID", oldLot.SupplierId);
            cmd.Parameters.AddWithValue("@new_SUPPLIER_ID", newLot.SupplierId);
            cmd.Parameters.AddWithValue("@old_PRODUCT_ID", oldLot.ProductId);
            cmd.Parameters.AddWithValue("@new_PRODUCT_ID", newLot.ProductId);
            cmd.Parameters.AddWithValue("@old_QUANTITY", oldLot.Quantity);
            cmd.Parameters.AddWithValue("@new_QUANTITY", newLot.Quantity);
            cmd.Parameters.AddWithValue("@old_EXPIRATION_DATE", oldLot.ExpirationDate);
            cmd.Parameters.AddWithValue("@new_EXPIRATION_DATE", newLot.ExpirationDate);

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery() == 1;
            }
            catch
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
        /// Aaron Usher
        /// Updated: 
        /// 2017/04/28
        /// 
        /// Deletes the given supplier product lot from the database.
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 
        /// 2017/04/28
        /// 
        /// Standardize method.
        /// </remarks>
        /// 
        /// <param name="supplierProductLot">The supplier product lot to delete.</param>
        /// <returns>Rows affected.</returns>
        public static int DeleteSupplierProductLot(SupplierProductLot supplierProductLot)
        {
            var rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_delete_supplier_product_lot";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SUPPLIER_PRODUCT_LOT_ID", supplierProductLot.SupplierProductLotId);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }
    }
}
