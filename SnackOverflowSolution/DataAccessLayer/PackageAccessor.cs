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
        /// 2017/02/02
        /// 
        /// Creates a new package in the database
        /// </summary>
        /// <param name="package">a package object representing the package that will be added to the database</param>
        /// <returns>Rows Affected</returns>
        public static int CreatePackage(Package package)
        {
            // Result represents the number of rows affected
            int result = 0;


            // Getting a SqlCommand object
            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_create_package";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            // Adding parameters for orderid
            cmd.Parameters.Add("@DELIVERY_ID", SqlDbType.Int);
            cmd.Parameters.Add("@ORDER_ID", SqlDbType.Int);

            // Setting parameters for orderid
            cmd.Parameters["@ORDER_ID"].Value = package.OrderId;

            /* 
             * Since deliveryId can be null I'm checking if the package has a null deliveryid
             * and then storing it appropriately
             */
            if (package.DeliveryId != null)
            {
                cmd.Parameters["@DELIVERY_ID"].Value = package.DeliveryId;
            }
            else
            {
                cmd.Parameters["@DELIVERY_ID"].Value = DBNull.Value;
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
        /// 2017/02/02
        /// 
        /// Gets all packages associated with an order
        /// </summary>
        /// <param name="orderId">The order number that the packages are related to</param>
        /// <returns>A list of packages</returns>
        public static List<Package> RetrieveAllPackagesInOrder(int orderId)
        {

            // an empty list to add the found packages to
            List<Package> packages = new List<Package>();

            // Creating an sql command object
            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_packages_in_order_list";
            var cmd = new SqlCommand(cmdText, conn);


            // Adding and setting the parameters for the stored procedure
            cmd.Parameters.Add("@ORDER_ID", SqlDbType.Int);
            cmd.Parameters["@ORDER_ID"].Value = orderId;

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

        public static List<Package> RetrievePackage(Package PackageInstance)
        {
            List<Package> PackageList = new List<Package>();
            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_PACKAGE_from_search";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.Parameters.AddWithValue("@PACKAGE_ID", PackageInstance.PackageId);
            cmd.Parameters.AddWithValue("@DELIVERY_ID", PackageInstance.DeliveryId);
            cmd.Parameters.AddWithValue("@ORDER_ID", PackageInstance.OrderId);

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
                    PackageList.Add(PackageInstance);
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

    }
}
