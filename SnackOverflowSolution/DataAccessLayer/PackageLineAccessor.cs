using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// Robert Forbes
    /// Created: 2017/02/07
    ///
    /// Class to handle database interactions involving packages.
    /// </summary>
    public static class PackageLineAccessor
    {

        /// <summary>
        /// Robert Forbes
        /// Created: 2017/02/07
        /// 
        /// Gets all the packages lines for a package
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/21
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <param name="packageID">The packageID of the package that the package lines should belong to</param>
        /// <returns>A list of package lines</returns>
        public static List<PackageLine> RetrievePackageLinesInPackage(int packageID)
        {
            var packageLines = new List<PackageLine>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_package_lines_in_package_list";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PACKAGE_ID", packageID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        packageLines.Add(new PackageLine()
                        {
                            PackageLineId = reader.GetInt32(0),
                            PackageId = reader.GetInt32(1),
                            ProductLotId = reader.GetInt32(2),
                            Quantity = reader.GetInt32(3),
                            PricePaid = reader.GetDecimal(4)
                        });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return packageLines;
        }


        /// <summary>
        /// Robert Forbes
        /// Created: 2017/02/07
        /// 
        /// Creates a new package line in the database
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Created: 2017/04/21
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <param name="packageLine">The package line to add to the database</param>
        /// <returns>Rows affected</returns>
        public static int CreatePackageLine(PackageLine packageLine)
        {
            var rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_create_package_line";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PACKAGE_ID", packageLine.PackageId);
            cmd.Parameters.AddWithValue("@PRODUCT_LOT_ID", packageLine.ProductLotId);
            cmd.Parameters.AddWithValue("@QUANTITY", packageLine.Quantity);
            cmd.Parameters.AddWithValue("@PRICE_PAID", packageLine.PricePaid);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }
    }
}
