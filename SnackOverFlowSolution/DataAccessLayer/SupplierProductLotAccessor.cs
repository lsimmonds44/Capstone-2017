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
    /// Ethan Jorgensen
    /// 2017-04-13
	///
    /// Contains the access methods for Product Lots
    /// </summary>
    public static class SupplierProductLotAccessor
    {

        /// <summary>
        /// Ethan Jorgensen
        /// 2017-04-13
        /// 
        /// Adds a new product lot to the database.
        /// </summary>
        /// <param name="SupplierProductLot">THe product lot to add.</param>
        /// <returns>Rows affected.</returns>
        public static int CreateSupplierProductLot(SupplierProductLot SupplierProductLot)
        {
            int rows = 0;
            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_create_product_lot";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SUPPLIER_ID", SupplierProductLot.SupplierId);
            cmd.Parameters.AddWithValue("@PRODUCT_ID", SupplierProductLot.ProductId);
            cmd.Parameters.AddWithValue("@QUANTITY", SupplierProductLot.Quantity);

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
        /// Gets a product lot object using the SupplierProductLotId 
        /// </summary>
        /// <param name="SupplierProductLotID">the id to search on</param>
        /// <returns>A product lot</returns>
        public static SupplierProductLot RetrieveSupplierProductLot(int? SupplierProductLotID)
        {
            SupplierProductLot lot = new SupplierProductLot();

            // Getting a SqlCommand object
            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_product_lot";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@PRODUCT_LOT_ID", SupplierProductLotID);

            // Attempting to run the stored procedure
			try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
				reader.Read();

                    var productLot = new SupplierProductLot();
                    productLot.SupplierProductLotId = reader.GetInt32(0);
                    productLot.SupplierId = reader.GetInt32(1);
                    productLot.ProductId = reader.GetInt32(2);
                    productLot.Quantity = reader.GetInt32(3);
                    if (!reader.IsDBNull(4))
                    {
                        productLot.Price = reader.GetDecimal(4);
                    }

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
            List<SupplierProductLot> SupplierProductLots = new List<SupplierProductLot>();

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
                        var productLot = new SupplierProductLot();
                        productLot.SupplierProductLotId = reader.GetInt32(0);
                        productLot.SupplierId = reader.GetInt32(1);
                        productLot.ProductId = reader.GetInt32(2);
                        productLot.Quantity = reader.GetInt32(3);
                        if (!reader.IsDBNull(4))
                        {
                            productLot.Price = reader.GetDecimal(4);
                        }

                        SupplierProductLots.Add(productLot);
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
            return SupplierProductLots;
        }

        /// <summary>
        /// Ethan Jorgensen
        /// 2017-04-13
        /// </summary>
        /// <returns></returns>
        public static List<SupplierProductLot> RetrieveSupplierProductLots()
        {
            List<SupplierProductLot> lots = new List<SupplierProductLot>();

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

                        var productLot = new SupplierProductLot();
                        productLot.SupplierProductLotId = reader.GetInt32(0);
                        productLot.SupplierId = reader.GetInt32(1);
                        productLot.ProductId = reader.GetInt32(2);
                        productLot.Quantity = reader.GetInt32(3);
                        if (!reader.IsDBNull(4))
                        {
                            productLot.Price = reader.GetDecimal(4);
                        }


                        lots.Add(productLot);
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

        public static List<SupplierProductLot> RetrieveActiveSupplierProductLots()
        {
            List<SupplierProductLot> lots = new List<SupplierProductLot>();

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

                        var productLot = new SupplierProductLot();
                        productLot.SupplierProductLotId = reader.GetInt32(0);
                        productLot.SupplierId = reader.GetInt32(1);
                        productLot.ProductId = reader.GetInt32(2);
                        productLot.Quantity = reader.GetInt32(3);
                        if (!reader.IsDBNull(4))
                        {
                            productLot.Price = reader.GetDecimal(4);
                        }


                        lots.Add(productLot);
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
       
        public static bool DeleteSupplierProductLot(SupplierProductLot lot)
        {
            var result = false;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_delete_product_lot";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PRODUCT_LOT_ID", lot.SupplierProductLotId);

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
            SupplierProductLot SupplierProductLot = null;

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
                    var productLot = new SupplierProductLot();
                    productLot.SupplierProductLotId = reader.GetInt32(0);
                    productLot.SupplierId = reader.GetInt32(1);
                    productLot.ProductId = reader.GetInt32(2);
                    productLot.Quantity = reader.GetInt32(3);
                    if (!reader.IsDBNull(4))
                    {
                        productLot.Price = reader.GetDecimal(4);
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

            return SupplierProductLot;
        }
    }
}
