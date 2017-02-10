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
    public static class ProductOrderAccessor
    {
        public static List<ProductOrder> RetrieveProductOrdersByStatus(string Status)
        {
            var ProductsByStatusList = new List<ProductOrder>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_PRODUCT_ORDER_by_status";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.Parameters.AddWithValue("@Status", Status);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read()) {
                    var CurrentProductOrder = new ProductOrder()
                    {
                        OrderId = reader.GetInt32(0),
                        CustomerId = reader.GetInt32(1),
                        OrderTypeId = reader.GetString(2),
                        AddressType = reader.GetString(3),
                        DeliveryTypeId = reader.GetString(4),
                        Amount = reader.GetDecimal(5),
                        OrderDate = reader.GetDateTime(6),
                        DateExpected = reader.GetDateTime(7),
                        Discount = reader.GetDecimal(8),
                        OrderStatusId = reader.GetString(9),
                        UserAddressId = reader.GetInt32(10),
                        HasArrived = reader.GetBoolean(11)
                    };
                    ProductsByStatusList.Add(CurrentProductOrder);
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

            return ProductsByStatusList;
        }
    }
}
