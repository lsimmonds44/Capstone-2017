///<summary>
///Robert Forbes
///2017/02/02
///
///</summary>
using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class PackageManager : IPackageManager
    {

        /// <summary>
        /// Robert Forbes
        /// 2017/02/02
        /// 
        /// Calls the data access class to create a package in the database from the passed package object
        /// </summary>
        /// <param name="package">The package to add to the database</param>
        /// <returns>bool representing if the package was successfully added</returns>
        public bool CreatePackage(Package package)
        {
            bool result = false;


            try
            {
                if (PackageAccessor.CreatePackage(package) == 1)
                {
                    result = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Robert Forbes
        /// 2017/02/02
        /// 
        /// Creates a package object from the passed orderId and deliveryID and then calls CreatePackage(Package package)
        /// </summary>
        /// <param name="orderID">The orderID to associate with the package</param>
        /// <param name="deliveryID">The deliveryID to associate with the package</param>
        /// <returns>bool representing if the package was successfully added</returns>
        public bool CreatePackage(int orderID, int? deliveryID)
        {
            bool result = false;
            Package package = new Package()
            {
                OrderId = orderID,
                DeliveryId = deliveryID
            };

            try
            {
                result = CreatePackage(package);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }


        /// <summary>
        /// Robert Forbes
        /// 2017/02/02
        /// 
        /// Gets a list of all packages with the provided orderID
        /// </summary>
        /// <param name="orderID">The order ID that the packages should be associated with</param>
        /// <returns>A list of packages</returns>
        public List<Package> RetrievePackagesInOrder(int orderID)
        {
            List<Package> packages = new List<Package>();

            try
            {
                packages = PackageAccessor.RetrieveAllPackagesByOrder(orderID);

                foreach(Package p in packages){
                    try{
                        p.PackageLineList = PackageLineAccessor.RetrievePackageLinesInPackage(p.PackageId);
                    }
                    catch
                    {
                        // If we cant get the package lines from the db set it to an empty list
                        p.PackageLineList = new List<PackageLine>();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return packages;
        }


        /// <summary>
        /// Robert Forbes
        /// 2017/03/01
        /// 
        /// Retrieves all the packages stored in the database, also retrieves all packge lines for the packages
        /// </summary>
        /// <returns>A list of packages</returns>
        public List<Package> RetrieveAllPackages()
        {
            List<Package> packages = new List<Package>();

            try
            {
                packages = PackageAccessor.RetrieveAllPackages();

                foreach (Package p in packages)
                {
                    try
                    {
                        p.PackageLineList = PackageLineAccessor.RetrievePackageLinesInPackage(p.PackageId);
                    }
                    catch
                    {
                        // If we cant get the package lines from the db set it to an empty list
                        p.PackageLineList = new List<PackageLine>();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return packages;
        }


        /// <summary>
        /// Robert Forbes
        /// 2017/03/09
        /// 
        /// Updates the oldPackage to the values of the newPackage
        /// </summary>
        /// <param name="deliveryId">The new delivery id</param>
        /// <param name="packageId">The package to update</param>
        /// <returns></returns>
        public bool UpdatePackageDelivery(int packageId, int deliveryId)
        {
            bool result = false;

            try
            {
                if (PackageAccessor.UpdatePackageDelivery(packageId, deliveryId) == 1)
                {
                    result = true;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }
    }
}