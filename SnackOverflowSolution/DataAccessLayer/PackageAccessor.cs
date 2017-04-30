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
    public static class PackageAccessor
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

            cmd.Parameters.Add("@DELIVERY_ID", SqlDbType.Int);
            if(package.DeliveryId != null){
                cmd.Parameters["@DELIVERY_ID"].Value = package.DeliveryId;
            }
            else
            {
                cmd.Parameters["@DELIVERY_ID"].Value = DBNull.Value;
            }
            
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

        /// <summary>
        /// Aaron Usher
        /// Updated: 2017/04/20
        /// 
        /// Retrieves a list of packages based on the given package.
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/20
        /// 
        /// Standardized method; fixed search bug where given package was passed back instead of found packages.
        /// </remarks>
        /// <param name="package">The package to search on.</param>
        /// <returns>List of packages that are similar to the given package.</returns>
        public static List<Package> RetrievePackageFromSearch(Package package)
        {
            var packages = new List<Package>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_PACKAGE_from_search";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PACKAGE_ID", package.PackageId);
            cmd.Parameters.AddWithValue("@DELIVERY_ID", package.DeliveryId);
            cmd.Parameters.AddWithValue("@ORDER_ID", package.OrderId);

            

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
                            DeliveryId = reader.GetInt32(1),
                            OrderId = reader.GetInt32(2)
                        });
                    }
                }
                
            }
            catch (Exception )
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
        /// Created: 2017/03/01
        /// 
        /// Get all packages stored in the database
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/21
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <returns>A list of packages</returns>
        public static List<Package> RetrieveAllPackages()
        {
            var packages = new List<Package>();
            
            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_package_list";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

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

        /// <summary>
        /// Robert Forbes
        /// Created: 2017/03/09
        /// 
        /// Updates the delivery Id of the passed in package
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/21
        /// 
        /// Standardized method; changed delivery id to be nullable and have a default value of null,
        /// based on preexisting comments.
        /// </remarks>
        /// 
        /// <returns>Rows affected</returns>
        public static int UpdatePackageDelivery(int packageId, int? deliveryId = null)
        {
            var rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_update_package_delivery";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PACKAGE_ID", packageId);
            cmd.Parameters.AddWithValue("@new_DELIVERY_ID", deliveryId);

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
        /// Created: 2017/04/13
        /// 
        /// Retrieves all packages with a given delivery id.
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/21
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <param name="deliveryId"></param>
        /// <returns></returns>
        public static List<Package> RetrieveAllPackagesInDelivery(int? deliveryId)
        {
            var packages = new List<Package>();

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
    }
}
