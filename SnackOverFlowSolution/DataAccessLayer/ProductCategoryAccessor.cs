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
    /// Mason Allen
    /// Created on 5/2/17
    /// </summary>
    public static class ProductCategoryAccessor
    {
        /// <summary>
        /// Mason Allen
        /// Craeted on 5/2/17
        /// Adds a product category record for a new product
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public static int CreateProductCategoryRecord(int productId, string categoryId)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_create_product_category";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PRODUCT_ID", productId);
            cmd.Parameters.AddWithValue("@CATEGORY_ID", categoryId);

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
