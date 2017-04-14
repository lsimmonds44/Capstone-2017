using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MVCPresentationLayer
{
    public static class WebApiConfig
    {
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
                name: "routeApi",
                routeTemplate: "api/route/{driverId}",
                defaults: new { controller = "route", driverId = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
