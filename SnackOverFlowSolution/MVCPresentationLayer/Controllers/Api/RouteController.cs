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
    /// <summary>
    /// Robert Forbes
    /// 2017/04/13
    /// </summary>
    public class RouteController : ApiController
    {
        IRouteManager _routeManager = new RouteManager();


        /// <summary>
        /// Robert Forbes
        /// 2017/04/13
        /// 
        /// API call to return a list of routes
        /// </summary>
        /// <param name="driverId"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public List<Route> RetrieveDriversRoutes(int? driverId)
        {
            try
            {
                return _routeManager.RetrieveFutureRoutesForDriver(driverId);
            }
            catch
            {
                return null;
            }
        }

    }
}
