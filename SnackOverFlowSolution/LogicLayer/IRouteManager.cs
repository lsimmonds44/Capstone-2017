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
    public interface IRouteManager
    {

        List<Route> RetrieveFutureRoutesForDriver(int? driverId);
        List<Route> RetrieveAllRoutes();
        int CreateRouteAndRetrieveRouteId(Route route);
    }
}
