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
    /// Interface to representa a delivery manager.
    /// </summary>
    public interface IDeliveryManager
    {
        List<Delivery> RetrieveDeliveries();

        Vehicle RetrieveVehicleByDelivery(int vehicleID);

        bool CreateDelivery(int? routeId, DateTime deliveryDate, Stream verification, string statusId, string deliveryTypeId, int orderId);

        int CreateDeliveryAndRetrieveDeliveryId(int? routeId, DateTime deliveryDate, Stream verification, string statusId, string deliveryTypeId, int orderId);

    }
}
