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
        /// Created: 2017/03/09
        /// 
        /// Creates a new delivery
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/07
        /// 
        /// Method signature changed from taking delivery pieces to a Delivery itself.
        /// </remarks>
        /// <param name="delivery">The delivery to add to the database.</param>
        /// <returns>bool representing if the creation was successful</returns>
        public bool CreateDelivery(Delivery delivery)
        {
            bool result = false;
            try
            {
                if (DeliveryAccessor.CreateDelivery(delivery) == 1)
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
        /// Created: 2017/03/09
        /// 
        /// Creates a new delivery
        /// </summary>
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/07
        /// 
        /// Standardized method. Changed signature to take delivery object instead of individual parameters.
        /// </remarks>
        /// <param name="delivery">The delivery to create.</param>
        /// <returns>The delivery id of the newly created delivery</returns>
        public int CreateDeliveryAndRetrieveDeliveryId(Delivery delivery)
        {
            int result = 0;
            try
            {
                result = DeliveryAccessor.CreateDeliveryAndRetrieveDeliveryId(delivery);
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
