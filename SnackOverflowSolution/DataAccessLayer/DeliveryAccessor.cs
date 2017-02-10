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
        public static List<Delivery> RetrieveDelivery(Delivery DeliveryInstance)
        {
            List<Delivery> DeliveryList = new List<Delivery>();
            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_DELIVERY_from_search";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.Parameters.AddWithValue("@DELIVERY_ID", DeliveryInstance.DeliveryId);
            cmd.Parameters.AddWithValue("@ROUTE_ID", DeliveryInstance.RouteId);
            cmd.Parameters.AddWithValue("@DEVLIVERY_DATE", DeliveryInstance.DevliveryDate);
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
                        RouteId = reader.GetInt32(1),
                        DevliveryDate = reader.GetDateTime(2),
                        Verification = reader.IsDBNull(3) ? null : (Stream)reader.GetStream(3),
                        StatusId = reader.GetString(4),
                        DeliveryTypeId = reader.GetString(5),
                        OrderId = reader.GetInt32(6)
                    };
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
    }
}