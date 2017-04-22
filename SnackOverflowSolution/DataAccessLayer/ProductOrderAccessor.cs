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
    /// Updated: 2017/02/15
    ///
    /// Class to handle database interactions involving product orders.
    /// </summary>
    public class ProductOrderAccessor
    {

        /// <summary>
        /// Eric Walton
        /// Created: 2017/10/3
        ///
        /// Adds an order to the database.
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/21
        /// 
        /// Laura Simmonds
        /// Updated: 2017/04/21
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <param name="productOrder">The order to add to the database.</param>
        /// <returns>The auto-generated order id from the database.</returns>
        public static int CreateProductOrder(ProductOrder productOrder)
        {
            int orderId = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_create_product_order";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@CUSTOMER_ID", productOrder.CustomerId);
            cmd.Parameters.AddWithValue("@EMPLOYEE_ID", productOrder.EmployeeId);
            cmd.Parameters.AddWithValue("@ORDER_TYPE_ID", productOrder.OrderTypeId);
            cmd.Parameters.AddWithValue("@ADDRESS_TYPE", productOrder.AddressType);
            cmd.Parameters.AddWithValue("@ADDRESS1", productOrder.Address1);
            cmd.Parameters.AddWithValue("@CITY", productOrder.City);
            cmd.Parameters.AddWithValue("@STATE", productOrder.State);
            cmd.Parameters.AddWithValue("@ZIP", productOrder.Zip);
            cmd.Parameters.AddWithValue("@DELIVERY_TYPE_ID", productOrder.DeliveryTypeId);
            cmd.Parameters.AddWithValue("@AMOUNT", productOrder.Amount);
            cmd.Parameters.AddWithValue("@ORDER_DATE", productOrder.OrderDate);
            cmd.Parameters.AddWithValue("@DATE_EXPECTED", productOrder.DateExpected);
            cmd.Parameters.AddWithValue("@DISCOUNT", productOrder.Discount);
            cmd.Parameters.AddWithValue("@ORDER_STATUS_ID", productOrder.OrderStatusId);
            cmd.Parameters.AddWithValue("@HAS_ARRIVED", productOrder.HasArrived);

            try
            {
                conn.Open();
                int.TryParse(cmd.ExecuteScalar().ToString(),out orderId);
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


        /// <summary>
        /// Victor Algarin
        /// Created: 2017/02/10
        /// 
        /// Retrieves the product order with the given id.
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/21
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <param name="orderID">The id of the needed product order.</param>
        /// <returns>The product order with the given id.</returns>
        public static ProductOrder RetrieveProductOrder(int orderID)
        {
            ProductOrder order = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_product_order";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ORDER_ID", orderID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    order = new ProductOrder()
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

        /// <summary>
        /// Aaron Usher
        /// Updated: 2017/04/21
        /// 
        /// Retrieves all product orders from the database with the given status.
        /// </summary>
        /// <param name="status">The status to search on.</param>
        /// <returns>All product orders in the database with the given status.</returns>
        public static List<ProductOrder> RetrieveProductOrdersByStatus(string status)
        {
            var productOrders = new List<ProductOrder>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_PRODUCT_ORDER_by_status";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Status", status);
            
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        productOrders.Add(new ProductOrder()
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

            return productOrders;
        }

        /// <summary>
        /// Robert Forbes
        /// Created: 2017/02/16
        /// 
        /// Updates the status for the provided order
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/21
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <param name="productOrderID">The id of the product order to be updated.</param>
        /// <param name="newStatus">The status as it should be.</param>
        /// <returns>Rows affected</returns>
        public static int UpdateProductOrderStatus(int productOrderID, string newStatus)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_update_product_order_status";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ORDER_ID", productOrderID);
            cmd.Parameters.AddWithValue("@NEW_ORDER_STATUS_ID", newStatus);

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
