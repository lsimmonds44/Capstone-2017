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
        /// <remarks>
        /// Robert Forbes
        /// Updates: 2017/04/23
        /// 
        /// Fixed nullable values in DB causing an exception
        /// </remarks>
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
                        OrderTypeId = reader.IsDBNull(2) ? null : reader.GetString(2),
                        AddressType = reader.IsDBNull(3) ? null : reader.GetString(3),
                        DeliveryTypeId = reader.IsDBNull(4) ? null : reader.GetString(4),
                        Amount = reader.IsDBNull(5) ? 0 : reader.GetDecimal(5),
                        OrderDate = reader.GetDateTime(6),
                        DateExpected = reader.GetDateTime(7),
                        Discount = reader.GetDecimal(8),
                        OrderStatusId = reader.GetString(9),
                        UserAddressId = reader.IsDBNull(10) ? (int?)null : reader.GetInt32(10),
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
        /// <remarks>
        /// Robert Forbes
        /// 
        /// Created:
        /// 2017/05/04
        /// 
        /// Fixed nullable fields causing exception
        /// </remarks>
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
                            OrderTypeId = reader.IsDBNull(2) ? null : reader.GetString(2),
                            AddressType = reader.IsDBNull(3) ? null : reader.GetString(3),
                            DeliveryTypeId = reader.IsDBNull(4) ? null : reader.GetString(4),
                            Amount = reader.IsDBNull(5) ? null : (decimal?)reader.GetDecimal(5),
                            OrderDate = reader.GetDateTime(6),
                            DateExpected = reader.IsDBNull(7) ? null : (DateTime?)reader.GetDateTime(7),
                            Discount = reader.GetDecimal(8),
                            OrderStatusId = reader.GetString(9),
                            UserAddressId = reader.IsDBNull(10) ? null : (int?)reader.GetInt32(10),
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

        /// <summary>
        /// Robert Forbes
        /// Created: 2017/04/23
        /// 
        /// Updates the provided order
        /// </summary>
        /// 
        /// <param name="oldOrder"></param>
        /// <param name="newOrder"></param>
        /// <returns>Rows affected</returns>
        public static int UpdateProductOrder(ProductOrder oldOrder, ProductOrder newOrder)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_update_product_order";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //Old Values
            cmd.Parameters.AddWithValue("@old_ORDER_ID", oldOrder.OrderId);
            cmd.Parameters.AddWithValue("@old_CUSTOMER_ID", oldOrder.CustomerId);
            cmd.Parameters.AddWithValue("@old_ORDER_DATE", oldOrder.OrderDate);
            cmd.Parameters.AddWithValue("@old_DATE_EXPECTED", oldOrder.DateExpected);
            cmd.Parameters.AddWithValue("@old_DISCOUNT", oldOrder.Discount);
            cmd.Parameters.AddWithValue("@old_ORDER_STATUS_ID", oldOrder.OrderStatusId);
            cmd.Parameters.AddWithValue("@old_HAS_ARRIVED", oldOrder.HasArrived);

            //Old Values Nullables
            cmd.Parameters.Add("@old_ORDER_TYPE_ID", SqlDbType.VarChar, 250);
            if (oldOrder.OrderTypeId == null)
            {
                cmd.Parameters["@old_ORDER_TYPE_ID"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters["@old_ORDER_TYPE_ID"].Value = oldOrder.OrderTypeId;
            }

            cmd.Parameters.Add("@old_ADDRESS_TYPE", SqlDbType.VarChar);
            if (oldOrder.AddressType == null)
            {
                cmd.Parameters["@old_ADDRESS_TYPE"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters["@old_ADDRESS_TYPE"].Value = oldOrder.AddressType;
            }

            cmd.Parameters.Add("@old_DELIVERY_TYPE_ID", SqlDbType.VarChar, 50);
            if (oldOrder.DeliveryTypeId == null)
            {
                cmd.Parameters["@old_DELIVERY_TYPE_ID"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters["@old_DELIVERY_TYPE_ID"].Value = oldOrder.DeliveryTypeId;
            }

            SqlParameter oldAmountParam = new SqlParameter("@old_AMOUNT", SqlDbType.Decimal);
            oldAmountParam.Precision = 6;
            oldAmountParam.Scale = 2;
            cmd.Parameters.Add(oldAmountParam);
            if (oldOrder.Amount == null)
            {
                cmd.Parameters["@old_AMOUNT"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters["@old_AMOUNT"].Value = oldOrder.Amount;
            }

            cmd.Parameters.Add("@old_USER_ADDRESS_ID", SqlDbType.Int);
            if (oldOrder.UserAddressId == null)
            {
                cmd.Parameters["@old_USER_ADDRESS_ID"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters["@old_USER_ADDRESS_ID"].Value = oldOrder.UserAddressId;
            }

            //New Values
            cmd.Parameters.AddWithValue("@new_CUSTOMER_ID", newOrder.CustomerId);
            cmd.Parameters.AddWithValue("@new_ORDER_DATE", newOrder.OrderDate);
            cmd.Parameters.AddWithValue("@new_DATE_EXPECTED", newOrder.DateExpected);
            cmd.Parameters.AddWithValue("@new_DISCOUNT", newOrder.Discount);
            cmd.Parameters.AddWithValue("@new_ORDER_STATUS_ID", newOrder.OrderStatusId);
            cmd.Parameters.AddWithValue("@new_HAS_ARRIVED", newOrder.HasArrived);

            //New Values Nullables
            cmd.Parameters.Add("@new_ORDER_TYPE_ID", SqlDbType.VarChar, 250);
            if (newOrder.OrderTypeId == null)
            {
                cmd.Parameters["@new_ORDER_TYPE_ID"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters["@new_ORDER_TYPE_ID"].Value = newOrder.OrderTypeId;
            }

            cmd.Parameters.Add("@new_ADDRESS_TYPE", SqlDbType.VarChar);
            if (newOrder.AddressType == null)
            {
                cmd.Parameters["@new_ADDRESS_TYPE"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters["@new_ADDRESS_TYPE"].Value = newOrder.AddressType;
            }

            cmd.Parameters.Add("@new_DELIVERY_TYPE_ID", SqlDbType.VarChar, 50);
            if (newOrder.DeliveryTypeId == null)
            {
                cmd.Parameters["@new_DELIVERY_TYPE_ID"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters["@new_DELIVERY_TYPE_ID"].Value = newOrder.DeliveryTypeId;
            }

            SqlParameter newAmountParam = new SqlParameter("@new_AMOUNT", SqlDbType.Decimal);
            newAmountParam.Precision = 6;
            newAmountParam.Scale = 2;
            cmd.Parameters.Add(newAmountParam);
            if (newOrder.Amount == null)
            {
                cmd.Parameters["@new__AMOUNT"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters["@new_AMOUNT"].Value = newOrder.Amount;
            }

            cmd.Parameters.Add("@new_USER_ADDRESS_ID", SqlDbType.Int);
            if (newOrder.UserAddressId == null)
            {
                cmd.Parameters["@new_USER_ADDRESS_ID"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters["@new_USER_ADDRESS_ID"].Value = newOrder.UserAddressId;
            }


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
        /// William Flood
        /// Created: 2017/04/27
        /// 
        /// Saves a previously placed order
        /// </summary>
        /// 
        /// <param name="productOrderID">The id of the product order to be updated.</param>
        /// <param name="newStatus">The status as it should be.</param>
        /// <returns>Rows affected</returns>
        public static int SaveOrder(int productOrderID)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_save_order";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ORDER_ID", productOrderID);

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
        /// William Flood
        /// Created: 2017/04/27
        /// 
        /// Loads a previously placed order
        /// </summary>
        /// 
        /// <param name="productOrderID">The id of the product order to be updated.</param>
        /// <param name="newStatus">The status as it should be.</param>
        /// <returns>Rows affected</returns>
        public static int LoadOrder(int productOrderID)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_load_order";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ORDER_ID", productOrderID);

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
        /// William Flood
        /// Created: 2017/04/27
        /// 
        /// Saves a previously placed order
        /// </summary>
        /// 
        /// <param name="productOrderID">The id of the product order to be updated.</param>
        /// <param name="newStatus">The status as it should be.</param>
        /// <returns>Rows affected</returns>
        public static List<int> RetrieveSaveOrders(string username)
        {
            List<int> orderIDs = new List<int>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_saved_orders_by_user";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@USER_NAME", username);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    orderIDs.Add(reader.GetInt32(0));
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

            return orderIDs;
        }
    }
}
