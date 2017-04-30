using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCPresentationLayer
{
    /// <summary>
    /// Ariel Sigo
    /// Updated:
    /// 2017/04/29
    /// Route Configuration 
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// Ariel Sigo
        /// Updated:
        /// 2017/04/29
        /// Registers new routes into the collection
        /// </summary>
        /// <param name="routes"></param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
