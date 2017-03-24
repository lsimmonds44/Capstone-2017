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
    public static class DeliveryAccessor
    {

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Robert Forbes
        /// Updated: 2017/03/09
        /// 
        /// Database was updated to contain a nullable field so this had to be updated to prevent crashing
        /// 
        /// Also noticed that this seems to return a list of the passed in Delivery not the ones found, not sure if that is intended.
        /// </remarks>
        /// <param name="DeliveryInstance"></param>
        /// <returns></returns>
        public static List<Delivery> RetrieveDelivery(Delivery DeliveryInstance)
        {
            List<Delivery> DeliveryList = new List<Delivery>();
            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_DELIVERY_from_search";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.Parameters.AddWithValue("@DELIVERY_ID", DeliveryInstance.DeliveryId);
            cmd.Parameters.AddWithValue("@ROUTE_ID", DeliveryInstance.RouteId);
            cmd.Parameters.AddWithValue("@DEVLIVERY_DATE", DeliveryInstance.DeliveryDate);
            cmd.Parameters.AddWithValue("@VERIFICATION", DeliveryInstance.Verification);
            cmd.Parameters.AddWithValue("@STATUS_ID", DeliveryInstance.StatusId);
            cmd.Parameters.AddWithValue("@DELIVERY_TYPE_ID", DeliveryInstance.DeliveryTypeId);
            cmd.Parameters.AddWithValue("@ORDER_ID", DeliveryInstance.OrderId);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var foundDeliveryInstance = new Delivery()
                    {
                        DeliveryId = reader.GetInt32(0),
                        DeliveryDate = reader.GetDateTime(2),
                        Verification = reader.IsDBNull(3) ? null : (Stream)reader.GetStream(3),
                        StatusId = reader.GetString(4),
                        DeliveryTypeId = reader.GetString(5),
                        OrderId = reader.GetInt32(6)
                    };

                    try
                    {
                        foundDeliveryInstance.RouteId = reader.GetInt32(1);
                    }
                    catch
                    {
                        foundDeliveryInstance.RouteId = null;
                    }

                    DeliveryList.Add(DeliveryInstance);
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
            return DeliveryList;
        }
        /// <summary>
        /// Aaron Usher
        /// Created: 2017/02/17
        /// 
        /// Retrieves every delivery in database.
        /// </summary>
        /// <remarks>
        /// Robert Forbes
        /// Updated: 2017/03/09
        /// 
        /// Database was updated to contain a nullable field so this had to be updated to prevent crashing
        /// </remarks>
        /// <returns></returns>
        public static List<Delivery> RetrieveDeliveries()
        {
            List<Delivery> DeliveryList = new List<Delivery>();
            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_delivery_list";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var DeliveryInstance = new Delivery()
                    {
                        DeliveryId = reader.GetInt32(0),
                        DeliveryDate = reader.GetDateTime(2),
                        Verification = reader.IsDBNull(3) ? null : (Stream)reader.GetStream(3),
                        StatusId = reader.GetString(4),
                        DeliveryTypeId = reader.GetString(5),
                        OrderId = reader.GetInt32(6)
                    };
                    try
                    {
                        DeliveryInstance.RouteId = reader.GetInt32(1);
                    }
                    catch
                    {
                        DeliveryInstance.RouteId = null;
                    }

                    DeliveryList.Add(DeliveryInstance);
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
            return DeliveryList;
        }

        /// <summary>
        /// Robert Forbes
        /// 2017/03/09
        /// 
        /// Creates a new delivery
        /// </summary>
        /// <param name="routeId"></param>
        /// <param name="deliveryDate"></param>
        /// <param name="verification"></param>
        /// <param name="statusId"></param>
        /// <param name="deliveryTypeId"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public static int CreateDelivery(int? routeId, DateTime deliveryDate, Stream verification, string statusId, string deliveryTypeId, int orderId)
        {
            // Result represents the number of rows affected
            int result = 0;


            // Getting a SqlCommand object
            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_create_delivery";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            // Adding parameters
            cmd.Parameters.Add("@ROUTE_ID", SqlDbType.Int); 
            cmd.Parameters.Add("@DEVLIVERY_DATE", SqlDbType.DateTime);
            cmd.Parameters.Add("@VERIFICATION", SqlDbType.VarBinary);
            cmd.Parameters.Add("@STATUS_ID", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@DELIVERY_TYPE_ID", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@ORDER_ID", SqlDbType.Int);

            cmd.Parameters["@DEVLIVERY_DATE"].Value = deliveryDate;
            cmd.Parameters["@STATUS_ID"].Value = statusId;
            cmd.Parameters["@DELIVERY_TYPE_ID"].Value = deliveryTypeId;
            cmd.Parameters["@ORDER_ID"].Value = orderId;

            /* 
             * Since routeId can be null I'm checking if the passed in routeId is null
             * and then storing it appropriately
             */
            if (routeId != null)
            {
                cmd.Parameters["@ROUTE_ID"].Value = routeId;
            }
            else
            {
                cmd.Parameters["@ROUTE_ID"].Value = DBNull.Value;
            }

            if (verification != null)
            {
                cmd.Parameters["@VERIFICATION"].Value = verification;
            }
            else
            {
                cmd.Parameters["@VERIFICATION"].Value = DBNull.Value;
            }

            // Attempting to run the stored procedure
            try
            {
                conn.Open();
                // Storing the amount of rows that were affected by the stored procedure
                result = cmd.ExecuteNonQuery();
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
        /// Robert Forbes
        /// 2017/03/09
        /// 
        /// Creates a new delivery and returns the id of the delivery
        /// </summary>
        /// <param name="routeId"></param>
        /// <param name="deliveryDate"></param>
        /// <param name="verification"></param>
        /// <param name="statusId"></param>
        /// <param name="deliveryTypeId"></param>
        /// <param name="orderId"></param>
        /// <returns>The delivery Id of the newly created delivery</returns>
        public static int CreateDeliveryAndRetrieveDeliveryId(int? routeId, DateTime deliveryDate, Stream verification, string statusId, string deliveryTypeId, int orderId)
        {
            // Result represents the number of rows affected
            int result = 0;


            // Getting a SqlCommand object
            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_create_delivery_return_delivery_id";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            // Adding parameters
            cmd.Parameters.Add("@ROUTE_ID", SqlDbType.Int);
            cmd.Parameters.Add("@DEVLIVERY_DATE", SqlDbType.DateTime);
            cmd.Parameters.Add("@VERIFICATION", SqlDbType.VarBinary);
            cmd.Parameters.Add("@STATUS_ID", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@DELIVERY_TYPE_ID", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@ORDER_ID", SqlDbType.Int);
            cmd.Parameters.Add("@DELIVERY_ID", SqlDbType.Int).Direction = ParameterDirection.Output;

            cmd.Parameters["@DEVLIVERY_DATE"].Value = deliveryDate;
            cmd.Parameters["@STATUS_ID"].Value = statusId;
            cmd.Parameters["@DELIVERY_TYPE_ID"].Value = deliveryTypeId;
            cmd.Parameters["@ORDER_ID"].Value = orderId;

            /* 
             * Since routeId can be null I'm checking if the passed in routeId is null
             * and then storing it appropriately
             */
            if (routeId != null)
            {
                cmd.Parameters["@ROUTE_ID"].Value = routeId;
            }
            else
            {
                cmd.Parameters["@ROUTE_ID"].Value = DBNull.Value;
            }

            if (verification != null)
            {
                cmd.Parameters["@VERIFICATION"].Value = verification;
            }
            else
            {
                cmd.Parameters["@VERIFICATION"].Value = DBNull.Value;
            }

            // Attempting to run the stored procedure
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
        /// <param name="oldDelivery">The old delivery.</param>
        /// <param name="newDelivery">The new delivery.</param>
        /// <returns>How many rows were affected.</returns>
        public static int UpdateDelivery(Delivery oldDelivery, Delivery newDelivery)
        {
            int rows = 0;
            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_update_delivery";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.Parameters.AddWithValue("@old_DELIVERY_ID", oldDelivery.DeliveryId);
            cmd.Parameters.AddWithValue("@old_ROUTE_ID", oldDelivery.RouteId);
            cmd.Parameters.AddWithValue("@new_ROUTE_ID", newDelivery.RouteId);
            cmd.Parameters.AddWithValue("@old_DEVLIVERY_DATE", oldDelivery.DeliveryDate);
            cmd.Parameters.AddWithValue("@new_DEVLIVERY_DATE", newDelivery.DeliveryDate);
            cmd.Parameters.AddWithValue("@old_VERIFICATION", oldDelivery.Verification);
            cmd.Parameters.AddWithValue("@new_VERIFICATION", newDelivery.Verification);
            cmd.Parameters.AddWithValue("@old_STATUS_ID", oldDelivery.StatusId);
            cmd.Parameters.AddWithValue("@new_STATUS_ID", newDelivery.StatusId);
            cmd.Parameters.AddWithValue("@old_DELIVERY_TYPE_ID", oldDelivery.DeliveryTypeId);
            cmd.Parameters.AddWithValue("@new_DELIVERY_TYPE_ID", newDelivery.DeliveryTypeId);
            cmd.Parameters.AddWithValue("@old_ORDER_ID", oldDelivery.OrderId);
            cmd.Parameters.AddWithValue("@new_ORDER_ID", newDelivery.OrderId);

            cmd.CommandType = CommandType.StoredProcedure;

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