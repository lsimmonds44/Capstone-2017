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
    public class PickupController : ApiController
    {
        IPickupManager _pickupManager = new PickupManager();
        ICompanyOrderManager _companyOrderManager = new CompanyOrderManager();

        /// <summary>
        /// Robert Forbes
        /// Created: 2017/04/13
        /// </summary>
        /// <param name="driverId"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public List<Pickup> RetrieveDriverPickups(int driverId)
        {
            try
            {
                return _pickupManager.RetrieveUnpickedupPickupsForDriver(driverId);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Robert Forbes
        /// Created: 2017/04/13
        /// </summary>
        /// <param name="pickupLineId"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public bool MarkPickupLineAsPickedup(int? pickupLineId)
        {
            var result = false;
            try
            {
                var oldLine = _pickupManager.RetrievePickupLineById(pickupLineId);
                var newLine = _pickupManager.RetrievePickupLineById(pickupLineId);
                newLine.PickupStatus = true;
                result =  _pickupManager.UpdatePickupLine(oldLine, newLine);

                //Checking to see if there are any pickup lines that haven't been picked up
                //Before updating the company order to show that the order has been fully picked up
                var pickupLines = _pickupManager.RetrievePickupLinesByPickupId(oldLine.PickupId);
                if(pickupLines.Count(l => l.PickupStatus == false) == 0){
                    var pickup = _pickupManager.RetrievePickupById(oldLine.PickupId);
                    var companyOrder = _companyOrderManager.RetrieveCompanyOrderWithLinesById((int)pickup.CompanyOrderId);
                    _companyOrderManager.UpdateCompanyOrderHasArrived(companyOrder.CompanyOrderID, companyOrder.HasArrived, true);
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
