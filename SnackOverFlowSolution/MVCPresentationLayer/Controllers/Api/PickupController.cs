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
            try
            {
                var oldLine = _pickupManager.RetrievePickupLineById(pickupLineId);
                var newLine = _pickupManager.RetrievePickupLineById(pickupLineId);
                newLine.PickupStatus = true;
                return _pickupManager.UpdatePickupLine(oldLine, newLine);
            }
            catch
            {
                return false;
            }
        }

    }
}
