using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    /// <summary>
    /// Aaron Usher
    /// Created: 2017/02/17
    /// 
    /// Class to represent a delivery manager.
    /// </summary>
    public class DeliveryManager : IDeliveryManager
    {
        /// <summary>
        /// Aaron Usher
        /// Created: 2017/02/17
        /// 
        /// Retrieves every delivery from the database.
        /// </summary>
        /// <returns>The deliveries.</returns>
        public List<Delivery> RetrieveDeliveries()
        {
            var list = new List<Delivery>();

            try
            {
                list = DeliveryAccessor.RetrieveDeliveries();
            }
            catch (Exception)
            {

                throw;
            }

            return list;
        }

        /// <summary>
        /// Retrieves a vehicle based on a deliveryID.
        /// </summary>
        /// <param name="deliveryID">The deliveryID.</param>
        /// <returns>The vehicle.</returns>
        public Vehicle RetrieveVehicleByDelivery(int deliveryID)
        {
            Vehicle result = null;
            try
            {
                result = VehicleAccessor.RetrieveVehicleByDelivery(deliveryID);
            }
            catch (Exception)
            {

                throw;
            }
            return result;
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
        /// <returns>bool representing if the creation was successful</returns>
        public bool CreateDelivery(int? routeId, DateTime deliveryDate, Stream verification, string statusId, string deliveryTypeId, int orderId)
        {
            bool result = false;
            try
            {
                if (DeliveryAccessor.CreateDelivery(routeId, deliveryDate, verification, statusId, deliveryTypeId, orderId) == 1)
                {
                    result = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
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
        /// <returns>the delivery id of the newly created delivery</returns>
        public int CreateDeliveryAndRetrieveDeliveryId(int? routeId, DateTime deliveryDate, Stream verification, string statusId, string deliveryTypeId, int orderId)
        {
            int result = 0;
            try
            {
                result = DeliveryAccessor.CreateDeliveryAndRetrieveDeliveryId(routeId, deliveryDate, verification, statusId, deliveryTypeId, orderId);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Aaron Usher
        /// Created: 2017/03/24
        /// 
        /// Updates a delivery.
        /// </summary>
        /// <param name="oldDelivery"></param>
        /// <param name="newDelivery"></param>
        /// <returns></returns>
        public bool UpdateDelivery(Delivery oldDelivery, Delivery newDelivery)
        {
            bool result = false;

            try
            {
                result = (1 == DeliveryAccessor.UpdateDelivery(oldDelivery, newDelivery));
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }
    }
}
