///<summary>
///Robert Forbes
///2017/02/07
///
///</summary>

using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IPackageManager
    {
        bool CreatePackage(Package package);

        bool CreatePackage(int orderID, int? deliveryID);

        List<Package> RetrievePackagesInOrder(int orderID);

        List<Package> RetrieveAllPackages();

        bool UpdatePackageDelivery(int packageId, int deliveryId);
    }
}
