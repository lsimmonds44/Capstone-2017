using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    /// <summary>
    /// Robert Forbes
    /// 2017/04/13
    /// </summary>
    public class RouteManager : IRouteManager
    {

        /// <summary>
        /// Robert Forbes
        /// 2017/04/13
        /// 
        /// Retrieves all routes with assigned dates for today or after
        /// </summary>
        /// <param name="driverId"></param>
        /// <returns></returns>
        public List<Route> RetrieveFutureRoutesForDriver(int? driverId)
        {
            List<Route> routes = new List<Route>();
            
            try
            {
                List<Route> routesToRemove = new List<Route>();
                //Getting all the routes
                routes = RouteAccessor.RetrieveFutureRoutesForDriver(driverId);
                foreach(Route r in routes){
                    //Getting all the deliveries for each route
                    List<Delivery> deliveriesToRemove = new List<Delivery>();
                    r.Deliveries = DeliveryAccessor.RetrieveDeliveriesForRoute(r.RouteId);
                    foreach(Delivery d in r.Deliveries){
                        if(d.StatusId == "Delivered"){
                            deliveriesToRemove.Add(d);
                        }
                        //Getting the address for each delivery
                        d.Address = DeliveryAccessor.RetrieveUserAddressForDelivery(d.DeliveryId);
                        //Getting the packages for each delivery
                        d.PackageList = PackageAccessor.RetrieveAllPackagesInDelivery(d.DeliveryId);
                        foreach(Package p in d.PackageList){
                            //Getting all the lines for each package
                            p.PackageLineList = PackageLineAccessor.RetrievePackageLinesInPackage(p.PackageId);
                            foreach(PackageLine line in p.PackageLineList){
                                //Getting the name of each product for each package line
                                line.ProductName = ProductAccessor.RetrieveProductNameFromProductLotId(line.ProductLotId);
                            }
                        }
                    }
                    foreach (Delivery d in deliveriesToRemove)
                    {
                        r.Deliveries.Remove(d);
                    }
                    if(r.Deliveries.Count == 0){
                        routesToRemove.Add(r);
                    }
                }

                foreach(Route r in routesToRemove){
                    routes.Remove(r);
                }
                
            }
            catch(Exception)
            {
                throw;
            }

            return routes;
        }

        /// <summary>
        /// Robert Forbes
        /// 
        /// Created:
        /// 2017/05/04
        /// </summary>
        /// <returns>A list of all routes in the database</returns>
        public List<Route> RetrieveAllRoutes()
        {
            List<Route> routes= null;

            try
            {
                routes = RouteAccessor.RetrieveAllRoutes();
            }
            catch
            {
                throw new ApplicationException("Failed To Retrieve Routes");
            }

            return routes;
        }

        /// <summary>
        /// Robert Forbes
        /// 
        /// Created:
        /// 2017/05/04
        /// </summary>
        /// <returns>The ID of the newly created route</returns>
        public int CreateRouteAndRetrieveRouteId(Route route)
        {
            int result = 0;

            try
            {
                result = RouteAccessor.CreateRouteAndRetrieveRouteId(route);
            }
            catch
            {
                throw new ApplicationException("Failed To Create Route");
            }

            return result;
        }
    }
}
