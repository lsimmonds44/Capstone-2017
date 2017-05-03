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
    /// Aaron Usher
    /// Updated: 
    /// 2017/04/21
    /// 
    /// Class to handle database interactions involving products.
    /// </summary>
    public static class ProductAccessor
    {

        /// <summary> 
        /// Dan Brown
        /// Created: 
        /// 2017/03/02
        ///
        /// Deletes a product from the database.
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 
        /// 2017/04/21
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <param name="productID"> Id of the product to be deleted.</param>
        /// <returns>Rows affected</returns>
        public static int DeleteProduct(int productID)
        {
            int rows = 0;

            var cmdText = "@sp_delete_product";
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PRODUCT_ID", productID);

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
        /// Natacha Ilunga 
        /// Created: 
        /// 2017/02/10
        /// 
        /// Retrieves Products from DB to Browse Product View Model
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 
        /// 2017/04/21
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <returns></returns>
        public static List<BrowseProductViewModel> RetrieveProductsToBrowseProducts()
        {
            var products = new List<BrowseProductViewModel>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_products_to_customer";
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
                        products.Add(new BrowseProductViewModel()
                        {
                            ProductId = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            GradeID = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                            Price = reader.IsDBNull(4) ? double.MinValue : (double)reader.GetDecimal(4),
                            SupplierID = reader.IsDBNull(5) ? int.MinValue : reader.GetInt32(5),
                            Supplier_Name = reader.IsDBNull(6) ? string.Empty : reader.GetString(6),
                            CategoryID = reader.IsDBNull(7) ? string.Empty : reader.GetString(7),
                            Image_Binary = reader["image_binary"] as byte[]
                        });
                    }
                    reader.Close();
                }
            }
            finally
            {
                conn.Close();
            }

            return products;
        }


        /// <summary>
        /// Laura Simmonds
        /// Created: 
        /// 2017/02/15
        /// 
        /// Retrieves a product from the database
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 
        /// 2017/04/21
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <param name="productID"></param>
        /// <returns>product</returns>

        public static Product RetrieveProduct
            (int productID)
        {

            var product = new Product();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_product";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PRODUCT_ID", productID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        product = new Product()
                        {
                            ProductId = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            UnitPrice = reader.GetDecimal(3),
                            ImageName = reader.GetString(4),
                            Active = reader.GetBoolean(5),
                            UnitOfMeasurement = reader.GetString(6),
                            DeliveryChargePerUnit = reader.GetDecimal(7),
                            ImageBinary = new byte[reader.GetStream(8).Length]
                        };
                        reader.GetStream(8).Read(product.ImageBinary, 0, (int)reader.GetStream(8).Length);
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

            return product;

        }

        /// <summary>
        /// Michael Takrama 
        /// Created: 
        /// 2017/02/15
        /// 
        /// Adds a new product to the database.
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated:
        /// 2017/04/21
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <param name="product">The product to be added.</param>
        /// <returns>Rows affected.</returns>
        public static int CreateProduct(Product product)
        {
            var rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_create_product";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Name", product.Name);
            cmd.Parameters.AddWithValue("@Description", product.Description);
            cmd.Parameters.AddWithValue("@Unit_Price", product.UnitPrice);
            cmd.Parameters.AddWithValue("@Image_Binary", product.ImageBinary ?? new Byte[0]);
            cmd.Parameters.AddWithValue("@Active", product.Active);
            cmd.Parameters.AddWithValue("@Unit_Of_Measurement", product.UnitOfMeasurement);
            cmd.Parameters.AddWithValue("@Delivery_Charge_Per_Unit", product.DeliveryChargePerUnit);

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
        /// Mason Allen
        /// Created on 5/2/17
        /// Inserts new product record and returns the generated product id
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static int CreateProductAndRetrieveProductId(Product product)
        {
            int productId = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_create_product_return_product_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Name", product.Name);
            cmd.Parameters.AddWithValue("@Description", product.Description);
            cmd.Parameters.AddWithValue("@Unit_Price", product.UnitPrice);
            cmd.Parameters.AddWithValue("@Image_Binary", product.ImageBinary ?? new Byte[0]);
            cmd.Parameters.AddWithValue("@Active", product.Active);
            cmd.Parameters.AddWithValue("@Unit_Of_Measurement", product.UnitOfMeasurement);
            cmd.Parameters.AddWithValue("@Delivery_Charge_Per_Unit", product.DeliveryChargePerUnit);

            try
            {
                conn.Open();
                productId = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return productId;
        }

        /// <summary>
        /// Ryan Spurgetis
        /// Created: 
        /// 2017/02/17
        /// 
        /// Updates the price of a given product in the database.
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 
        /// 2017/04/21
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <param name="productID">The id of the product to update.</param>
        /// <param name="oldPrice">The price as it was.</param>
        /// <param name="newPrice">The price as it should be.</param>
        /// <returns>Rows affected.</returns>
        public static int UpdateProductPrice(int productID, decimal oldPrice, decimal newPrice)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_update_product_price";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ProductID", productID);
            cmd.Parameters.AddWithValue("@OldPrice", oldPrice);
            cmd.Parameters.AddWithValue("@NewPrice", newPrice);

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
        /// Christian Lopez
        /// Created: 
        /// 2017/03/08
        /// 
        /// Get a list of all products in the database
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 
        /// 2017/04/21
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        ///<returns>List of all products in the database.</returns>
        public static List<Product> RetrieveProductList()
        {
            var products = new List<Product>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_product_list";
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
                        products.Add(new Product()
                        {
                            ProductId = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            UnitPrice = reader.GetDecimal(3),
                            ImageName = reader.GetString(4),
                            Active = reader.GetBoolean(5),
                            UnitOfMeasurement = reader.GetString(6),
                            DeliveryChargePerUnit = reader.GetDecimal(7)
                        });
                    }
                }
                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return products;
        }
        /*This is very, very wrong and needs to be redone as its own stored procedure.*/
        /// <summary>
        /// Natacha Ilunga
        /// 
        /// Created:
        /// 2017/03/29
        /// 
        /// Retrieves Products by suplier Id to Supplier Catalog View
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        public static List<BrowseProductViewModel> RetrieveProductsBySupplierIdToViewModel(int supplierId)
        {
            return RetrieveProductsToBrowseProducts().FindAll(s => s.SupplierID == supplierId);
        }

        /// <summary>
        /// Robert Forbes
        /// Created: 
        /// 2017/04/13
        /// 
        /// Retrieves the name of a product based on the passed in product lot id
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 
        /// 2017/04/21
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <param name="productLotId">The id of the needed product lot.</param>
        /// <returns>The name of the product lot.</returns>
        public static string RetrieveProductNameFromProductLotId(int? productLotId)
        {
            string productName = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_product_name_from_product_lot_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@PRODUCT_LOT_ID", productLotId);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    productName = reader.GetString(0);
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

            return productName;
        }

        /// <summary>
        /// William Flood
        /// Created: 
        /// 2017/04/14
        /// 
        /// Retrieves the price options for a given product from the database.
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 
        /// 2017/04/21
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <param name="productID">The id of the relevant product.</param>
        /// <returns>List of options.</returns>
        public static List<ProductGradePrice> RetrievePriceOptionsForProduct(int productID)
        {
            var options = new List<ProductGradePrice>();

            var conn = DBConnection.GetConnection();
            var procedureName = "sp_retrieve_product_grade_price_from_search";
            var cmd = new SqlCommand(procedureName, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PRODUCT_ID", productID);
           
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        options.Add(new ProductGradePrice
                        {
                            ProductID = reader.GetInt32(0),
                            GradeID = reader.GetString(1),
                            Price = reader.GetDecimal(2)
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

            return options;
        }
    }
}
