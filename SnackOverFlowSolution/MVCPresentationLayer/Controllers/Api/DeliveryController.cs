using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MVCPresentationLayer.Controllers.Api
{
    public class DeliveryController : ApiController
    {
        IDeliveryManager _deliveryManager = new DeliveryManager();


        /// <summary>
        /// Robert Forbes
        /// Created on: 2017/04/19
        /// 
        /// Updates the status of a delivery
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <param name="newDeliveryStatus"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public bool UpdateDeliveryStatus(int deliveryId, string newDeliveryStatus)
        {
            try
            {
                Delivery oldDelivery = _deliveryManager.RetrieveDeliveryById(deliveryId);
                Delivery newDelivery = _deliveryManager.RetrieveDeliveryById(deliveryId);
                newDelivery.StatusId = newDeliveryStatus;

                if(_deliveryManager.UpdateDelivery(oldDelivery, newDelivery)){
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
