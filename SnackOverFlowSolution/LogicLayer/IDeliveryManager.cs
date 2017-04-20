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

        Delivery RetrieveDeliveryById(int deliveryId);

        bool CreateDelivery(Delivery delivery);

        int CreateDeliveryAndRetrieveDeliveryId(Delivery delivery);

        bool UpdateDelivery(Delivery oldDelivery, Delivery newDelivery);
    }
}
