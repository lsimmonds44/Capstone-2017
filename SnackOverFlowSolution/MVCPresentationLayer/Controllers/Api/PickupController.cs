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
    }
}
