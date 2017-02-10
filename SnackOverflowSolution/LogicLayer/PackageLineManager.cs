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
                if (PackageLineAccessor.CreatePackageLine(line) == 1)
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
