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
    ///<summary>
    ///Robert Forbes
    ///2017/02/07
    ///
    ///</summary>
    public class PackageLineAccessor
    {

        /// <summary>
        /// Robert Forbes
        /// 2017/02/07
        /// 
        /// Gets all the packages lines for a package
        /// </summary>
        /// <param name="packageID">The packageID the the package lines should belong to</param>
        /// <returns>A list of package lines</returns>
        public static List<PackageLine> RetrievePackageLinesInPackage(int packageID)
        {
            List<PackageLine> lines = new List<PackageLine>();

            // Creating an sql command object
            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_package_lines_in_package_list";
            var cmd = new SqlCommand(cmdText, conn);


            // Adding and setting the parameters for the stored procedure
            cmd.Parameters.Add("@PACKAGE_ID", SqlDbType.Int);
            cmd.Parameters["@PACKAGE_ID"].Value = packageID;

            cmd.CommandType = CommandType.StoredProcedure;

            // Attempting to run the stored procedure
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    // Looping through all returned results until there aren't any left
                    while (reader.Read())
                    {
                        // Creating a package from the current record
                        var line = new PackageLine()
                        {
                            packageLineId = reader.GetInt32(0),
                            packageId = reader.GetInt32(1),
                            productLotId = reader.GetInt32(2),
                            quantity = reader.GetInt32(3),
                            pricePaid = reader.GetDecimal(4)
                        };

                        // Adding the line to the list
                        lines.Add(line);
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

            return lines;
        }


        /// <summary>
        /// Robert Forbes
        /// 2017/02/07
        /// 
        /// Creates a new package line in the database
        /// </summary>
        /// <param name="proposed">The package line to add to the database</param>
        /// <returns>Rows affected</returns>
        public static int CreatePackageLine(PackageLine proposed)
        {
            int result = 0;

            // Getting a SqlCommand object
            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_create_package_line";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            // Adding parameters for orderid
            cmd.Parameters.Add("@PACKAGE_ID", SqlDbType.Int);
            cmd.Parameters.Add("@PRODUCT_LOT_ID", SqlDbType.Int);
            cmd.Parameters.Add("@QUANTITY", SqlDbType.Int);
            cmd.Parameters.Add("@PRICE_PAID", SqlDbType.Decimal);

            cmd.Parameters["@PACKAGE_ID"].Value = proposed.packageId;
            cmd.Parameters["@PRODUCT_LOT_ID"].Value = proposed.productLotId;
            cmd.Parameters["@QUANTITY"].Value = proposed.quantity;
            cmd.Parameters["@PRICE_PAID"].Value = proposed.pricePaid;

            // Attempting to run the stored procedure
            try
            {
                conn.Open();
                // Storing the amount of rows that were affected by the stored procedure
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }
    }
}
