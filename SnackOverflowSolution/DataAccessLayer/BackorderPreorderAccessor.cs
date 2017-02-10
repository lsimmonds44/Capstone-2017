using DataObjects;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
namespace DataAccessLayer
{
    public static class BackorderPreorderAccessor
    {
        public static List<BackorderPreorder> RetrieveBackorderPreorder(BackorderPreorder BackorderPreorderInstance)
        {
            List<BackorderPreorder> BackorderPreorderList = new List<BackorderPreorder>();
            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_BACKORDER_PREORDER_from_search";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.Parameters.AddWithValue("@BACKORDER_PREORDER_ID", BackorderPreorderInstance.BackorderPreorderId);
            cmd.Parameters.AddWithValue("@ORDER_ID", BackorderPreorderInstance.OrderId);
            cmd.Parameters.AddWithValue("@CUSTOMER_ID", BackorderPreorderInstance.CustomerId);
            cmd.Parameters.AddWithValue("@AMOUNT", BackorderPreorderInstance.Amount);
            cmd.Parameters.AddWithValue("@DATE_PLACED", BackorderPreorderInstance.DatePlaced);
            cmd.Parameters.AddWithValue("@DATE_EXPECTED", BackorderPreorderInstance.DateExpected);
            cmd.Parameters.AddWithValue("@HAS_ARRIVED", BackorderPreorderInstance.HasArrived);
            cmd.Parameters.AddWithValue("@ADDRESS_1", BackorderPreorderInstance.Address1);
            cmd.Parameters.AddWithValue("@ADDRESS_2", BackorderPreorderInstance.Address2);
            cmd.Parameters.AddWithValue("@CITY", BackorderPreorderInstance.City);
            cmd.Parameters.AddWithValue("@STATE", BackorderPreorderInstance.State);
            cmd.Parameters.AddWithValue("@ZIP", BackorderPreorderInstance.Zip);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var foundBackorderPreorderInstance = new BackorderPreorder()
                    {
                        BackorderPreorderId = reader.IsDBNull(0) ? null : (int?)reader.GetInt32(0),
                        OrderId = reader.GetInt32(1),
                        CustomerId = reader.GetInt32(2),
                        Amount = reader.GetDecimal(3),
                        DatePlaced = reader.GetDateTime(4),
                        DateExpected = reader.GetDateTime(5),
                        HasArrived = reader.GetBoolean(6),
                        Address1 = reader.GetString(7),
                        Address2 = reader.GetString(8),
                        City = reader.GetString(9),
                        State = reader.GetString(10),
                        Zip = reader.GetString(11)
                    };
                    BackorderPreorderList.Add(BackorderPreorderInstance);
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error: " + ex);
            }
            finally
            {
                conn.Close();
            }
            return BackorderPreorderList;
        }
    }
}