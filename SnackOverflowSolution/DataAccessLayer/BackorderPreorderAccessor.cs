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
    /// <summary>
    /// Aaron Usher
    /// Updated: 2017/04/02
    /// 
    /// Class to handle database interactions inolving backorderpreorders.
    /// </summary>
    public static class BackorderPreorderAccessor
    {
        /// <summary>
        /// Aaron Usher
        /// Updated: 2017/04/02
        /// 
        /// Retrieves BackorderPreorders based on the supplied BackorderPreorder.
        /// </summary>
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/02
        /// 
        /// Standaridized naming conventions and form. Fixed terrible bug where the passed in BackorderPreorder
        /// would be repeatedly added to the list.
        /// </remarks>
        /// <param name="backorderPreorder">BackorderPreorder that the search is based on.</param>
        /// <returns>List of backorderPreorders</returns>
        public static List<BackorderPreorder> RetrieveBackorderPreorders(BackorderPreorder backorderPreorder)
        {
            var backorderPreorders = new List<BackorderPreorder>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_BACKORDER_PREORDER_from_search";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@BACKORDER_PREORDER_ID", backorderPreorder.BackorderPreorderId);
            cmd.Parameters.AddWithValue("@ORDER_ID", backorderPreorder.OrderId);
            cmd.Parameters.AddWithValue("@CUSTOMER_ID", backorderPreorder.CustomerId);
            cmd.Parameters.AddWithValue("@AMOUNT", backorderPreorder.Amount);
            cmd.Parameters.AddWithValue("@DATE_PLACED", backorderPreorder.DatePlaced);
            cmd.Parameters.AddWithValue("@DATE_EXPECTED", backorderPreorder.DateExpected);
            cmd.Parameters.AddWithValue("@HAS_ARRIVED", backorderPreorder.HasArrived);
            cmd.Parameters.AddWithValue("@ADDRESS_1", backorderPreorder.Address1);
            cmd.Parameters.AddWithValue("@ADDRESS_2", backorderPreorder.Address2);
            cmd.Parameters.AddWithValue("@CITY", backorderPreorder.City);
            cmd.Parameters.AddWithValue("@STATE", backorderPreorder.State);
            cmd.Parameters.AddWithValue("@ZIP", backorderPreorder.Zip);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        backorderPreorders.Add(new BackorderPreorder()
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

            return backorderPreorders;
        }
    }
}