using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class CategoryAccessor
    {
        /// <summary>
        /// Created by Ryan Spurgetis on 2/9/2017
        /// This method takes data for a new Category for Products from manager class and writes to the database.
        /// </summary>
        /// <param name="prodCategory">category name</param>
        /// <param name="categoryDesc">category description</param>
        /// <returns>boolean result</returns>
        public static int CreateProductCategory(string prodCategory, string categoryDesc)
        {
            int result = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_create_category";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CATEGORY_ID", SqlDbType.NVarChar, 200);
            cmd.Parameters.Add("@DESCRIPTION", SqlDbType.NVarChar, 750);
            cmd.Parameters["@CATEGORY_ID"].Value = prodCategory;
            cmd.Parameters["@DESCRIPTION"].Value = categoryDesc;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw new Exception("An error occurred creating product category.");
            }
            finally
            {
                conn.Close();
            }

            return result;
        }
    }
}
