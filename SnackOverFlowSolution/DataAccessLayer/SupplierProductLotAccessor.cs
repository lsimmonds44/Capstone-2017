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
    /// Created: 2017/04/13
    ///
    /// Class to handle database interactions involving supplier product lots.
    public static class SupplierProductLotAccessor
    {

        /// <summary>
        /// Ethan Jorgensen
        /// Created: 2017/04/13
        /// 
        /// Adds a new product lot to the database.
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/28
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
        /// Created: 2017/04/13
        /// 
        /// Gets a product lot object using the productlotid 
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/28
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
                        ExpirationDate = reader.GetDateTime(4)

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
        /// Created: 2017/04/13
        /// 
        /// Returns a list of SupplierProductLots by supplier
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/28
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
        /// Ethan Jorgensen
        /// Created: 2017/04/13
        /// 
        /// Gets a list of product lots from the database.
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/28
        /// 
        /// Standardzied method.
        /// </remarks>
        /// 
        /// <returns>A list of all supplier product lots.</returns>
        public static List<SupplierProductLot> RetrieveSupplierProductLots()
        {
            var supplierProductLots = new List<SupplierProductLot>();

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
                        supplierProductLots.Add(new SupplierProductLot()
                        {
                            SupplierProductLotId = reader.GetInt32(0),
                            SupplierId = reader.GetInt32(1),
                            ProductId = reader.GetInt32(2),
                            Quantity = reader.GetInt32(3),
                            ExpirationDate = reader.GetDateTime(4),
                            Price = reader.IsDBNull(5) ? (decimal?)null : reader.GetDecimal(5)
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

            return supplierProductLots;
        }

        /// <summary>
        /// Aaron Usher
        /// Updated: 2017/04/28
        /// 
        /// Deletes the given supplier product lot from the database.
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/28
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
