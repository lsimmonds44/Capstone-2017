using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
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
            catch (Exception ex)
            {

                throw new Exception("Data could not be retrieved", ex);
            }
            return result;
        }
    }
}
