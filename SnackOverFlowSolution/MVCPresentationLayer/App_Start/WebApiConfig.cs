using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MVCPresentationLayer
{
    /// <summary>
    /// Ariel Sigo
    /// Updated:
    /// 2017/04/29
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Ariel Sigo
        /// Updated:
        /// 2017/04/29
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "userApi",
                routeTemplate: "api/user/{userName}/{password}",
                defaults: new { controller = "user", userName = RouteParameter.Optional, password = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "pickupApi",
                routeTemplate: "api/pickup/{driverId}",
                defaults: new { controller = "pickup", driverId = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "pickupLineUpdate",
                routeTemplate: "api/pickup/markpickedup/{pickupLineId}",
                defaults: new { controller = "pickup", pickupLineId = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "routeApi",
                routeTemplate: "api/route/{driverId}",
                defaults: new { controller = "route", driverId = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "updateDelivery",
                routeTemplate: "api/delivery/{deliveryId}/{newDeliveryStatus}",
                defaults: new { controller = "delivery", deliveryId = RouteParameter.Optional, newDeliveryStatus = RouteParameter.Optional}
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
