using System;
using System.Data;
using System.Data.SqlClient;
using DataObjects;

namespace DataAccessLayer
{
    public class ProductOrderAccessorMvc : ProductOrderAccessor
    {

        /// <summary>
        ///
        /// Michael Takrama
        /// 
        /// Created:
        /// 2017/04/28
        /// 
        /// Product Order Created customized for MVC layer
        /// </summary>
        /// 
        /// <remarks>
        /// Ariel Sigo
        /// Updated:
        /// 2017/04/29
        /// 
        /// Standardized Comment
        /// </remarks>
        /// <param name="productOrder"></param>
        /// <returns></returns>
        public new static int CreateProductOrder(ProductOrder productOrder)
        {
            int orderId = 0;

            var conn = DBConnection.GetConnection();
            const string cmdText = @"sp_create_product_order_from_cart";
            var cmd = new SqlCommand(cmdText, conn) {CommandType = CommandType.StoredProcedure};

            cmd.Parameters.AddWithValue("@CUSTOMER_ID", productOrder.CustomerId);
            cmd.Parameters.AddWithValue("@ORDER_TYPE_ID", (object)productOrder.OrderTypeId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ADDRESS_TYPE", (object)productOrder.AddressType ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ADDRESS1", productOrder.Address1);
            cmd.Parameters.AddWithValue("@CITY", productOrder.City);
            cmd.Parameters.AddWithValue("@STATE", productOrder.State);
            cmd.Parameters.AddWithValue("@ZIP", productOrder.Zip);
            cmd.Parameters.AddWithValue("@DELIVERY_TYPE_ID", (object)productOrder.DeliveryTypeId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DATE_EXPECTED", (object)productOrder.DateExpected ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DISCOUNT", productOrder.Discount);
            cmd.Parameters.AddWithValue("@ORDER_STATUS_ID", productOrder.OrderStatusId);
            cmd.Parameters.AddWithValue("@HAS_ARRIVED", productOrder.HasArrived);
            var returnParam = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;


            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                orderId = (int)returnParam.Value;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return orderId;
        }
        
    }
}