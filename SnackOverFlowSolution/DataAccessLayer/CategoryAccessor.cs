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
    /// Aaron Usher
    /// Updated: 
    /// 2017/04/03
    /// 
    /// Class to handle database interactions inolving categories.
    /// </summary>
    public static class CategoryAccessor
    {
        /// <summary>
        /// Ryan Spurgetis
        /// Created:
        /// 2017/02/09
        /// 
        /// This method takes data for a new Category for Products from manager class and writes to the database.
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 
        /// 2017/04/03
        /// 
        /// Standardized method. Changed method signature to take a Category instead of information to make a category.
        /// </remarks>
        /// 
        /// <param name="category">The category to add.</param>
        /// <returns>Rows affected.</returns>
        public static int CreateCategory(Category category)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_create_category";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@CATEGORY_ID", category.CategoryID);
            cmd.Parameters.AddWithValue("@DESCRIPTION", category.Description);

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
        /// Mason Allen
        /// 
        /// Created:
        /// 2017/04/13
        /// 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns>1 for success, 0 for fail</returns>
        public static int DeleteCategory(string categoryId)
        {
            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_delete_category";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@CATEGORY_ID", categoryId);

            int rows = 0;

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
