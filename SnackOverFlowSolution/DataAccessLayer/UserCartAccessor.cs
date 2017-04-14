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
    public class UserCartAccessor
    {
        public static List<UserCartLine> RetrieveCartForUser(String userName)
        {
            var userCart = new List<UserCartLine>();
            var conn = DBConnection.GetConnection();
            var procedureName = @"sp_retrieve_cart_for_user";
            var com = new SqlCommand(procedureName, conn);
            com.Parameters.AddWithValue("@USER_NAME", userName);
            com.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                var reader = com.ExecuteReader();
                while (reader.Read())
                {
                    userCart.Add(new UserCartLine(){
                        ProductID = reader.GetInt32(0),
                        UserID = reader.GetInt32(1),
                        Quantity = reader.GetInt32(2),
                        GradeID = reader.GetString(3),
                        Name = reader.GetString(4),
                        BasePrice = reader.GetDecimal(5),
                        FlatProductDiscount = reader.IsDBNull(6)?null:(decimal?)reader.GetDecimal(6),
                        ScaledProductDiscount = reader.IsDBNull(7) ? null : (decimal?)reader.GetDecimal(7),
                        FlatCategoryDiscount = reader.IsDBNull(8) ? null : (decimal?)reader.GetDecimal(8),
                        ScaledCategoryDiscount = reader.IsDBNull(9) ? null : (decimal?)reader.GetDecimal(9)
                    });
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return userCart;

        
        }

        /// <summary>
        /// William Flood
        /// Created: 20178/04/13
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="gradeId"></param>
        /// <param name="quantity"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static int RemoveFromCart(int productId, string gradeId, int quantity, int userId)
        {
            var results = 0;
            var userCart = new List<UserCartLine>();
            var conn = DBConnection.GetConnection();
            var procedureName = @"sp_remove_from_cart";
            var com = new SqlCommand(procedureName, conn);
            com.Parameters.AddWithValue("@PRODUCT_ID", productId);
            com.Parameters.AddWithValue("@GRADE_ID", gradeId);
            com.Parameters.AddWithValue("@QUANTITY", quantity);
            com.Parameters.AddWithValue("@USER_ID", userId);
            com.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                results = com.ExecuteNonQuery();
                
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return results;

        }
    }
}
