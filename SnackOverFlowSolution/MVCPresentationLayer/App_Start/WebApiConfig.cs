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
                routeTemplate: "api/user/{userName}/{hash}",
                defaults: new { controller = "user", userName = RouteParameter.Optional, hash = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
