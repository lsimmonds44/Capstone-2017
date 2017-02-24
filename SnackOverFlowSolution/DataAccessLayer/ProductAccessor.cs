using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ProductAccessor
    {

        ///<summary> 
        /// Dan Brown
        /// Created on 3/2/17
        ///
        /// Delete an individual product from the SnackOverflowDB product table (following documentation guidlines)
        ///</summary>
        ///<param name="productID"> The ID field of the product to be deleted </param>
        ///<returns> Returns rows affected (int) </returns>
        ///<exception cref="System.Exception"> Thrown if there is an error oppening a connection to the database </exception>
        public static int DeleteProduct(int productID)
        {
            int rowsAffected = 0;

            var cmdText = "@sp_delete_product";
            var conn = DBConnection.GetConnection();

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@PRODUCT_ID", SqlDbType.Int);
            cmd.Parameters["@PRODUCT_ID"].Value = productID;

            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return rowsAffected;
        }



    }
}
