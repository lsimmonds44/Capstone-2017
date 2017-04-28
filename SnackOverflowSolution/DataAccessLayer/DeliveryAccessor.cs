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
    /// Updated: 2017/04/07
    /// 
    /// Class to handle database interactions involving deliveries.
    /// </summary>
    public static class DeliveryAccessor
    {

        /// <summary>
        /// Aaron Usher
        /// Updated: 2017/04/07
        /// 
        /// Retrieves deliveries based on aspects of passed in deliveries.
        /// </summary>
        /// 
        /// <remarks>
        /// Robert Forbes
        /// Updated: 2017/03/09
        /// 
        /// Database was updated to contain a nullable field so this had to be updated to prevent crashing
        /// 
        /// Also noticed that this seems to return a list of the passed in Delivery not the ones found, not sure if that is intended.
        /// </remarks>
        ///
        /// <param name="delivery">The delivery to search on.</param>
        /// <returns>A list of deliveries.</returns>
        public static List<Delivery> RetrieveDelivery(Delivery delivery)
        {
            var deliveries = new List<Delivery>();
            
            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_DELIVERY_from_search";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@DELIVERY_ID", delivery.DeliveryId);
            cmd.Parameters.AddWithValue("@ROUTE_ID", delivery.RouteId);
            cmd.Parameters.AddWithValue("@DELIVERY_DATE", delivery.DeliveryDate);
            cmd.Parameters.AddWithValue("@VERIFICATION", delivery.Verification);
            cmd.Parameters.AddWithValue("@STATUS_ID", delivery.StatusId);
            cmd.Parameters.AddWithValue("@DELIVERY_TYPE_ID", delivery.DeliveryTypeId);
            cmd.Parameters.AddWithValue("@ORDER_ID", delivery.OrderId);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        deliveries.Add(new Delivery()
                        {
                            DeliveryId = reader.GetInt32(0),
                            RouteId = reader.IsDBNull(1) ? (int?)null : reader.GetInt32(1),
                            DeliveryDate = reader.GetDateTime(2),
                            Verification = reader.IsDBNull(3) ? null : reader.GetSqlBytes(3).Buffer,
                            StatusId = reader.GetString(4),
                            DeliveryTypeId = reader.GetString(5),
                            OrderId = reader.GetInt32(6)
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

            return deliveries;
        }
        /// <summary>
        /// Aaron Usher
        /// Created: 2017/02/17
        /// 
        /// Retrieves every delivery in database.
        /// </summary>
        /// 
        /// <remarks>
        /// Robert Forbes
        /// Updated: 2017/03/09
        /// 
        /// Database was updated to contain a nullable field so this had to be updated to prevent crashing
        /// </remarks>
        /// <returns>All deliveries in the database.</returns>
        public static List<Delivery> RetrieveDeliveries()
        {
            var deliveries = new List<Delivery>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_delivery_list";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        deliveries.Add(new Delivery()
                        {
                            DeliveryId = reader.GetInt32(0),
                            RouteId = reader.IsDBNull(1) ? (int?)null : reader.GetInt32(1),
                            DeliveryDate = reader.GetDateTime(2),
                            Verification = reader.IsDBNull(3) ? null : reader.GetSqlBytes(3).Buffer,
                            StatusId = reader.GetString(4),
                            DeliveryTypeId = reader.GetString(5),
                            OrderId = reader.GetInt32(6)
                        });
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return deliveries;
        }

        /// <summary>
        /// Robert Forbes
        /// Created: 2017/03/09
        /// 
        /// Creates a new delivery
        /// </summary>
        ///
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/07
        /// 
        /// Standardized method; changed signature to use a Delivery instead of the information held in a delivery.
        /// </remarks>
        /// <param name="delivery">Delivery to add to the database.</param>
        /// <returns>Rows affected.</returns>
        public static int CreateDelivery(Delivery delivery)
        {
            
            var rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_create_delivery";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ROUTE_ID", delivery.RouteId);
            cmd.Parameters.AddWithValue("@DELIVERY_DATE", delivery.DeliveryDate);
            cmd.Parameters.AddWithValue("@VERIFICATION", delivery.Verification);
            cmd.Parameters.AddWithValue("@STATUS_ID", delivery.StatusId);
            cmd.Parameters.AddWithValue("@DELIVERY_TYPE_ID", delivery.DeliveryTypeId);
            cmd.Parameters.AddWithValue("@ORDER_ID", delivery.OrderId);
          
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
        /// Created:2017/03/09
        /// 
        /// Creates a new delivery and returns the id of the delivery
        /// </summary>
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/07
        /// 
        /// Standardized method.
        /// </remarks>
        /// <param name="delivery">Delivery to add to the database.</param>
        /// <returns>The delivery Id of the newly created delivery</returns>
        public static int CreateDeliveryAndRetrieveDeliveryId(Delivery delivery)
        {

            var result = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_create_delivery_return_delivery_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ROUTE_ID", delivery.RouteId);
            cmd.Parameters.AddWithValue("@DELIVERY_DATE", delivery.DeliveryDate);
            cmd.Parameters.AddWithValue("@VERIFICATION", delivery.Verification);
            cmd.Parameters.AddWithValue("@STATUS_ID", delivery.StatusId);
            cmd.Parameters.AddWithValue("@DELIVERY_TYPE_ID", delivery.DeliveryTypeId);
            cmd.Parameters.AddWithValue("@ORDER_ID", delivery.OrderId);
            cmd.Parameters.Add("@DELIVERY_ID", SqlDbType.Int).Direction = ParameterDirection.Output;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                result = Convert.ToInt32(cmd.Parameters["@DELIVERY_ID"].Value);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }
        /// <summary>
        /// Aaron Usher
        /// Created: 2017/03/10
        /// 
        /// Updates a delivery, while doing a concurrency check.
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/07
        /// 
        /// Standardized method.
        /// </remarks>
        /// <remarks>
        /// Robert Forbes
        /// Updates: 2017/04/19
        /// 
        /// Updated to check if the verification is null before assigning it
        /// </remarks>
        /// <param name="oldDelivery">The old delivery.</param>
        /// <param name="newDelivery">The new delivery.</param>
        /// <returns>How many rows were affected.</returns>
        public static int UpdateDelivery(Delivery oldDelivery, Delivery newDelivery)
        {
            var rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_update_delivery";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@DELIVERY_ID", oldDelivery.DeliveryId);

            cmd.Parameters.AddWithValue("@old_ROUTE_ID", oldDelivery.RouteId);
            cmd.Parameters.AddWithValue("@old_DELIVERY_DATE", oldDelivery.DeliveryDate);

            //Checking if the verification is null before assigning it as it used to break before doing this
            
            if (oldDelivery.Verification != null)
            {
                cmd.Parameters.AddWithValue("@old_VERIFICATION", oldDelivery.Verification);
            }
            else
            {
                cmd.Parameters.Add("@old_VERIFICATION", SqlDbType.VarBinary);
                cmd.Parameters["@old_VERIFICATION"].Value = DBNull.Value;
            }

            cmd.Parameters.AddWithValue("@old_STATUS_ID", oldDelivery.StatusId);
            cmd.Parameters.AddWithValue("@old_DELIVERY_TYPE_ID", oldDelivery.DeliveryTypeId);
            cmd.Parameters.AddWithValue("@old_ORDER_ID", oldDelivery.OrderId);

            cmd.Parameters.AddWithValue("@new_ROUTE_ID", newDelivery.RouteId);
            cmd.Parameters.AddWithValue("@new_DELIVERY_DATE", newDelivery.DeliveryDate);
            
            if (newDelivery.Verification != null)
            {
                cmd.Parameters.AddWithValue("@new_VERIFICATION", newDelivery.Verification);
            }
            else
            {
                cmd.Parameters.Add("@new_VERIFICATION", SqlDbType.VarBinary);
                cmd.Parameters["@new_VERIFICATION"].Value = DBNull.Value;
            }          

            cmd.Parameters.AddWithValue("@new_STATUS_ID", newDelivery.StatusId);       
            cmd.Parameters.AddWithValue("@new_DELIVERY_TYPE_ID", newDelivery.DeliveryTypeId);       
            cmd.Parameters.AddWithValue("@new_ORDER_ID", newDelivery.OrderId);

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
        /// 2017/04/13
        /// 
        /// Gets all deliveries for the specified route
        /// </summary>
        /// <param name="routeId"></param>
        /// <returns></returns>
        public static List<Delivery> RetrieveDeliveriesForRoute(int? routeId)
        {
            var deliveries = new List<Delivery>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_delivery_from_search";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.Parameters.AddWithValue("@ROUTE_ID", routeId);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        deliveries.Add(new Delivery()
                        {
                            DeliveryId = reader.GetInt32(0),
                            RouteId = reader.IsDBNull(1) ? (int?)null : reader.GetInt32(1),
                            DeliveryDate = reader.GetDateTime(2),
                            Verification = reader.IsDBNull(3) ? null : reader.GetSqlBytes(3).Buffer,
                            StatusId = reader.GetString(4),
                            DeliveryTypeId = reader.GetString(5),
                            OrderId = reader.GetInt32(6)
                        });
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return deliveries;
        }


        /// <summary>
        /// Robert Forbes
        /// 2017/04/13
        /// 
        /// Gets the address assigned to the passed in delivery
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <returns></returns>
        public static UserAddress RetrieveUserAddressForDelivery(int? deliveryId)
        {

            UserAddress userAddress = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_user_address_for_delivery";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@DELIVERY_ID", deliveryId);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    userAddress = new UserAddress()
                    {
                        UserId = reader.GetInt32(0),
                        AddressLineOne = reader.IsDBNull(1) ? null : reader.GetString(1),
                        AddressLineTwo = reader.IsDBNull(2) ? null : reader.GetString(2),
                        City = reader.IsDBNull(3) ? null : reader.GetString(3),
                        State = reader.IsDBNull(4) ? null : reader.GetString(4),
                        Zip = reader.IsDBNull(5) ? null : reader.GetString(5)
                    };
                }
                reader.Close();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

            return userAddress;
        }

        /// <summary>
        /// Robert Forbes
        /// Created:2017/04/19
        /// 
        /// Retrieves a delivery with the passed in id
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <returns></returns>
        public static Delivery RetrieveDeliveryById(int deliveryId)
        {

            Delivery delivery = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_delivery";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@DELIVERY_ID", deliveryId);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    delivery = new Delivery()
                    {
                        DeliveryId = reader.GetInt32(0),
                        RouteId = reader.GetInt32(1),
                        DeliveryDate = reader.GetDateTime(2),
                        Verification = reader.IsDBNull(3) ? null : reader.GetSqlBytes(3).Buffer,
                        StatusId = reader.GetString(4),
                        DeliveryTypeId = reader.GetString(5),
                        OrderId = reader.GetInt32(6)

                    };
                }
                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return delivery;
        }

        /// <summary>
        /// Robert Forbes
        /// 2017/04/23
        /// 
        /// Gets all deliveries for the specified order
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public static List<Delivery> RetrieveDeliveriesForOrder(int? orderId)
        {
            var deliveries = new List<Delivery>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_delivery_from_search";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.Parameters.AddWithValue("@ORDER_ID", orderId);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        deliveries.Add(new Delivery()
                        {
                            DeliveryId = reader.GetInt32(0),
                            RouteId = reader.IsDBNull(1) ? (int?)null : reader.GetInt32(1),
                            DeliveryDate = reader.GetDateTime(2),
                            Verification = reader.IsDBNull(3) ? null : reader.GetSqlBytes(3).Buffer,
                            StatusId = reader.GetString(4),
                            DeliveryTypeId = reader.GetString(5),
                            OrderId = reader.GetInt32(6)
                        });
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return deliveries;
        }
    }
}