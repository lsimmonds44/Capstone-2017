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
    public class ProductOrderAccessor
    {
        /// <summary>
        /// Victor Algarin
        /// Created 2017/2/10
        /// 
        /// Retrieves details for a specific order through an order id
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns>ProductOrder</returns>
        public static ProductOrder RetrieveOrderByID(int orderID)
        {
            var order = new ProductOrder();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_product_order";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ORDER_ID", SqlDbType.Int);
            cmd.Parameters["@ORDER_ID"].Value = orderID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    order = new ProductOrder()
                    {
                        OrderID = reader.GetInt32(0),
                        CustomerID = reader.GetInt32(1),
                        OrderTypeId = reader.GetString(2),
                        AddressType = reader.GetString(3),
                        DeliveryType = reader.GetString(4),
                        Amount = reader.GetDecimal(5),
                        DateExpected = reader.GetDateTime(6),
                        Discount = reader.GetDecimal(7),
                        OrderStatusId = reader.GetString(8),
                        UserAddressId = reader.GetInt32(9),
                        HasArrived = reader.GetBoolean(10)
                    };
                    reader.Close();
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

            return order;
        }
    }
}
