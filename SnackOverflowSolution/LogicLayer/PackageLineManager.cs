using DataAccessLayer;
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
    public class PackageLineManager : IPackageLineManager
    {

        /// <summary>
        /// Robert Forbes
        /// 2017/02/07
        /// 
        /// Attempts to run the data access method CreatePackageLine(PackageLine proposed) to create a new package line
        /// </summary>
        /// <param name="line">The line to be added to the database</param>
        /// <returns>bool representing if the package line was successfully added</returns>
        public bool CreatePackageLine(PackageLine line)
        {
            bool result = false;
            try
            {
                ProductLot lot = ProductLotAccessor.RetrieveProductLot(line.productLotId);

                int? newAvailableQuantity = lot.availableQuantity - line.quantity;

                //Trying to take the quantity in the package line out of the product lot table to update stock levels
                if (ProductLotAccessor.UpdateProductLotAvailableQuantity(line.productLotId, lot.availableQuantity, newAvailableQuantity) > 0)
                {
                    //if the product lot quantity could be updated then create the package line
                    if (PackageLineAccessor.CreatePackageLine(line) > 0)
                    {
                        result = true;
                    }
                    else //Trying to add the quantity back to the product lot since the package line couldn't be created
                    {
                        ProductLotAccessor.UpdateProductLotAvailableQuantity(line.productLotId, newAvailableQuantity, lot.availableQuantity);
                    }
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
        /// 2017/02/07
        /// 
        /// Attempts to run the data access method RetrievePackageLinesInPackage(int packageID) to get all package lines for a package
        /// </summary>
        /// <param name="packageID">The packageD that the package lines should relate to</param>
        /// <returns>A list of package lines</returns>
        public List<PackageLine> RetrievePackageLinesInPackage(int packageID)
        {
            List<PackageLine> lines = new List<PackageLine>();

            try
            {

                lines = PackageLineAccessor.RetrievePackageLinesInPackage(packageID);
            }
            catch (Exception)
            {
                throw;
            }

            return lines;
        }
    }
}
