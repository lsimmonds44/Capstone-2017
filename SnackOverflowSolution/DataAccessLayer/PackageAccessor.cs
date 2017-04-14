/// <summary>
/// Robert Forbes
/// 2017/02/07
/// 
/// Handles database calls for packages
///</summary>
///

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

    public class PackageAccessor
    {

        /// <summary>
        /// Robert Forbes
        /// Created: 2017/02/02
        /// 
        /// Creates a new package in the database
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/14
        /// 
        /// Standardized method.
        /// </remarks>
        /// <param name="package">A package object representing the package that will be added to the database</param>
        /// <returns>Rows affected</returns>
        public static int CreatePackage(Package package)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_create_package";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@DELIVERY_ID", package.DeliveryId);
            cmd.Parameters.AddWithValue("@ORDER_ID", package.OrderId);

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

        /// <summary>
        /// Robert Forbes
        /// 2017/02/02
        /// 
        /// Gets all packages associated with an order
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// 2017/04/14
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <param name="orderId">The order number that the packages are related to</param>
        /// <returns>A list of packages</returns>
        public static List<Package> RetrieveAllPackagesByOrder(int orderId)
        {

            var packages = new List<Package>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_packages_in_order_list";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ORDER_ID", orderId);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        packages.Add(new Package()
                        {
                            PackageId = reader.GetInt32(0),
                            DeliveryId = reader.IsDBNull(1) ? (int?)null : reader.GetInt32(1),
                            OrderId = reader.GetInt32(2)
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

            return packages;
        }

        public static List<Package> RetrievePackageFromSearch(Package package)
        {
            List<Package> PackageList = new List<Package>();
            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_PACKAGE_from_search";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.Parameters.AddWithValue("@PACKAGE_ID", package.PackageId);
            cmd.Parameters.AddWithValue("@DELIVERY_ID", package.DeliveryId);
            cmd.Parameters.AddWithValue("@ORDER_ID", package.OrderId);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var foundPackageInstance = new Package()
                    {
                        PackageId = reader.GetInt32(0),
                        DeliveryId = reader.GetInt32(1),
                        OrderId = reader.GetInt32(2)
                    };
                    PackageList.Add(package);
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error: " + ex);
            }
            finally
            {
                conn.Close();
            }
            return PackageList;
        }

        /// <summary>
        /// Robert Forbes
        /// 2017/03/01
        /// 
        /// Get all packages stored in the database
        /// </summary>
        /// <returns>A list of packages</returns>
        public static List<Package> RetrieveAllPackages()
        {
            // an empty list to add the found packages to
            List<Package> packages = new List<Package>();

            // Creating an sql command object
            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_package_list";
            var cmd = new SqlCommand(cmdText, conn);

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
                        var package = new Package()
                        {
                            PackageId = reader.GetInt32(0),
                            OrderId = reader.GetInt32(2)
                        };

                        // Attempting to set the delivery id since it can be null
                        try
                        {
                            package.DeliveryId = reader.GetInt32(1);
                        }
                        catch
                        {
                            package.DeliveryId = null;
                        }

                        // Adding the package to the list
                        packages.Add(package);
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
            return packages;
        }

        /// <summary>
        /// Robert Forbes
        /// 2017/03/09
        /// 
        /// Updates the delivery Id of the passed in package
        /// </summary>
        /// <returns>int rows affected</returns>
        public static int UpdatePackageDelivery(int packageId, int deliveryId)
        {
            // Result represents the number of rows affected
            int result = 0;


            // Getting a SqlCommand object
            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_update_package_delivery";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@PACKAGE_ID", SqlDbType.Int);
            cmd.Parameters.Add("@new_DELIVERY_ID", SqlDbType.Int);
            cmd.Parameters["@PACKAGE_ID"].Value = packageId;

            /* 
             * Since deliveryId can be null I'm checking if the package has a null deliveryid
             * and then storing it appropriately
             */
            if (deliveryId != null)
            {
                cmd.Parameters["@new_DELIVERY_ID"].Value = deliveryId;
            }
            else
            {
                cmd.Parameters["@new_DELIVERY_ID"].Value = DBNull.Value;
            }

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

        /// <summary>
        /// Robert Forbes
        /// 2017/04/13
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <returns></returns>
        public static List<Package> RetrieveAllPackagesInDelivery(int? deliveryId)
        {

            List<Package> packages = new List<Package>();
            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_package_from_search";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.Parameters.AddWithValue("@DELIVERY_ID", deliveryId);

            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var package = new Package()
                        {
                            PackageId = reader.GetInt32(0),
                            OrderId = reader.GetInt32(2)
                        };

                        try
                        {
                            package.DeliveryId = reader.GetInt32(1);
                        }
                        catch
                        {
                            package.DeliveryId = null;
                        }

                        packages.Add(package);
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
            return packages;

        }
    }
}
