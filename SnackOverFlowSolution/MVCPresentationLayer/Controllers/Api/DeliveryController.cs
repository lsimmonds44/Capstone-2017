using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace MVCPresentationLayer.Controllers.Api
{
    public class DeliveryController : ApiController
    {
        IDeliveryManager _deliveryManager = new DeliveryManager();
        IProductOrderManager _orderManager = new ProductOrderManager();

        /// <summary>
        /// Robert Forbes
        /// Created on: 2017/04/19
        /// 
        /// Updates the status of a delivery
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <param name="newDeliveryStatus"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public bool UpdateDeliveryStatus(int deliveryId, string newDeliveryStatus, [FromBody] string verificationImage)
        {
            bool result = false;
            try
            {
                Delivery oldDelivery = _deliveryManager.RetrieveDeliveryById(deliveryId);
                Delivery newDelivery = _deliveryManager.RetrieveDeliveryById(deliveryId);
                newDelivery.StatusId = newDeliveryStatus;
                try
                {
                    byte[] verificationAsBytes = Convert.FromBase64String(verificationImage);
                    newDelivery.Verification = verificationAsBytes;
                }
                catch
                {
                    byte[] verificationAsBytes = null;
                    newDelivery.Verification = verificationAsBytes;
                }

                if(_deliveryManager.UpdateDelivery(oldDelivery, newDelivery)){
                    result = true;
                }
                else
                {
                    result = false;
                }

                List<Delivery> ordersDeliveries = _deliveryManager.RetrieveDeliveriesByOrderId((int)oldDelivery.OrderId);
                bool allDelivered = true;
                foreach(Delivery d in ordersDeliveries){
                    if(d.StatusId != "Delivered"){
                        allDelivered = false;
                    }
                }

                if(allDelivered){
                    ProductOrder oldOrder = _orderManager.retrieveProductOrderDetails((int)oldDelivery.OrderId);
                    ProductOrder newOrder = _orderManager.retrieveProductOrderDetails((int)oldDelivery.OrderId);
                    newOrder.OrderStatusId = "Delivered";
                    newOrder.HasArrived = true;
                    _orderManager.UpdateProductOrder(oldOrder, newOrder);
                }
            }
            catch
            {
                result = false;
            }

            return result;
        }
    }
}
