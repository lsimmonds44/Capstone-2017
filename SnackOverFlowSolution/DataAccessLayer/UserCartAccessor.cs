using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    /// <summary>
    /// Aaron Usher
    /// Updated: 2017/04/28
    ///
    /// Class to handle database interactions involving user cart lines.
    public class UserCartAccessor
    {
        /// <summary>
        /// Aaron Usher
        /// Updated: 2017/04/27
        /// 
        /// Retrieves a user's cart lines from the database.
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/27
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <param name="userName">The username of the relevant user.</param>
        /// <returns>A user's cart lines.</returns>
        public static List<UserCartLine> RetrieveCartByUsername(String userName)
        {
            var userCartLines = new List<UserCartLine>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_cart_for_user";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@USER_NAME", userName);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        userCartLines.Add(new UserCartLine()
                        {
                            ProductID = reader.GetInt32(0),
                            UserID = reader.GetInt32(1),
                            Quantity = reader.GetInt32(2),
                            GradeID = reader.GetString(3),
                            Name = reader.GetString(4),
                            BasePrice = reader.GetDecimal(5),
                            FlatProductDiscount = reader.IsDBNull(6) ? null : (decimal?)reader.GetDecimal(6),
                            ScaledProductDiscount = reader.IsDBNull(7) ? null : (decimal?)reader.GetDecimal(7),
                            FlatCategoryDiscount = reader.IsDBNull(8) ? null : (decimal?)reader.GetDecimal(8),
                            ScaledCategoryDiscount = reader.IsDBNull(9) ? null : (decimal?)reader.GetDecimal(9)
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
            return userCartLines;
        }

        /// <summary>
        /// William Flood
        /// Created: 2017/04/13
        /// 
        /// Removes the given items from the cart.
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/28
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <param name="productId"></param>
        /// <param name="gradeId"></param>
        /// <param name="quantity"></param>
        /// <param name="userId"></param>
        /// <returns>Rows affected.</returns>
        public static int RemoveFromCart(int productId, string gradeId, int quantity, int userId)
        {
            var rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_remove_from_cart";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PRODUCT_ID", productId);
            cmd.Parameters.AddWithValue("@GRADE_ID", gradeId);
            cmd.Parameters.AddWithValue("@QUANTITY", quantity);
            cmd.Parameters.AddWithValue("@USER_ID", userId);
            
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
        /// Aaron Usher
        /// Updated: 2017/04/27
        /// 
        /// Creates a user cart line in the database.
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/27
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <param name="userCartLine">The userCartLine to add.</param>
        /// <returns>Rows affected.</returns>
        public static int CreateUserCartLine(UserCartLine userCartLine)
        {
            var results = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_add_to_cart";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PRODUCT_ID", userCartLine.ProductID);
            cmd.Parameters.AddWithValue("@GRADE_ID", userCartLine.GradeID);
            cmd.Parameters.AddWithValue("@QUANTITY", userCartLine.Quantity);
            cmd.Parameters.AddWithValue("@USER_ID", userCartLine.UserID);
            
            try
            {
                conn.Open();
                results = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return results;
        }
    }
}
