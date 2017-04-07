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
        public static List<UserCartLine> RetrieveCartForUser(int userID)
        {
            var userCart = new List<UserCartLine>();
            var conn = DBConnection.GetConnection();
            var procedureName = @"sp_retrieve_cart_for_user";
            var com = new SqlCommand(procedureName, conn);
            com.Parameters.AddWithValue("@USER_ID", userID);
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
                        Total = reader.GetDecimal(5),
                        FlatProductDiscount = reader.GetDecimal(6),
                        ScaledProductDiscount = reader.GetDecimal(7),
                        FlatCategoryDiscount = reader.GetDecimal(8),
                        ScaledCategoryDiscount = reader.GetDecimal(9)
                    });
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return userCart;

        }
    }
}
