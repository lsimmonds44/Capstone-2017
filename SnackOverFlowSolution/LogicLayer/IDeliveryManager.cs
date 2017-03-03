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
    /// Interface to representa a delivery manager.
    /// </summary>
    public interface IDeliveryManager
    {
        List<Delivery> RetrieveDeliveries();

        Vehicle RetrieveVehicleByDelivery(int vehicleID);

    }
}
