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
    public class ProductAccessor : IRetriever
    {

        public Product ProductInstance { get; set; }
        public List<Product> ProductList { get; set; }

        public string RetrieveSingleScript
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string RetrieveListScript
        {
            get
            {
                return @"sp_retrieve_product_list";
            }
        }

        public string RetrieveSearchScript
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        ///<summary> 
        /// Dan Brown
        /// Created on 3/2/17
        ///
        /// Delete an individual product from the SnackOverflowDB product table (following documentation guidlines)
        ///</summary>
        ///<param name="productID"> The ID field of the product to be deleted </param>
        ///<returns> Returns rows affected (int) </returns>
        ///<exception cref="System.Exception"> Thrown if there is an error oppening a connection to the database </exception>
        public static int DeleteProduct(int productID)
        {
            int rowsAffected = 0;

            var cmdText = "@sp_delete_product";
            var conn = DBConnection.GetConnection();

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@PRODUCT_ID", SqlDbType.Int);
            cmd.Parameters["@PRODUCT_ID"].Value = productID;

            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return rowsAffected;
        }

        /// <summary>
        /// Created by Natacha Ilunga 
        /// Created on 2/10/17
        /// 
        /// Retrieves Products from DB to Browse Product View Model
        /// </summary>
        /// <returns></returns>
        public static List<BrowseProductViewModel> RetrieveProductsToBrowseProducts()
        {
            var productsInDB = new List<BrowseProductViewModel>();
            var conn = DBConnection.GetConnection();
            const string cmdText = @"sp_retrieve_products_to_customer";
            var cmd = new SqlCommand(cmdText, conn) { CommandType = CommandType.StoredProcedure };

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var product = new BrowseProductViewModel()
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
                        };

                        productsInDB.Add(product);
                    }
                    reader.Close();
                }
            }
            finally
            {
                conn.Close();
            }

            return productsInDB;
        }


        /// <summary>
        /// Laura Simmonds
        /// Created on 2017/02/15
        /// 
        /// Retrieves a product from the database
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns>product</returns>

        public static Product RetrieveProductbyId(int ProductID)
        {
            {
                var product = new Product();

                var conn = DBConnection.GetConnection();
                var cmdText = @"sp_retrieve_product";
                var cmd = new SqlCommand(cmdText, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PRODUCT_ID", SqlDbType.Int);
                cmd.Parameters["@PRODUCT_ID"].Value = ProductID;

                try
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            // ProductID, Name, Description, UnitPrice, ImageName, DeliveryChargePerUnit

                            product = new Product()
                            {
                                ProductId = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Description = reader.GetString(2),
                                UnitPrice = reader.GetDecimal(3),
                                ImageName = reader.GetString(4),
                                Active = reader.GetBoolean(5),
                                UnitOfMeasurement = reader.GetString(6),
                                DeliveryChargePerUnit = reader.GetDecimal(7)
                            };
                        }
                        reader.Close();
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
        }

        /// <summary>
        /// Created by Michael Takrama 
        /// Created on 2/15/2017
        /// 
        /// Adds New Product
        /// </summary>
        /// <param name="product">Product Object to be Created</param>
        /// <returns>Returns and integer indicating write success</returns>
        public static int CreateProduct(Product product)
        {
            var conn = DBConnection.GetConnection();
            const string cmdText = @"sp_create_product";
            var cmd = new SqlCommand(cmdText, conn);
            var count = 0;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", product.Name);
            cmd.Parameters.AddWithValue("@Description", product.Description);
            cmd.Parameters.AddWithValue("@Unit_Price", product.UnitPrice);
            cmd.Parameters.AddWithValue("@Image_Binary", product.ImageBinary ?? new Byte[0]);
            cmd.Parameters.AddWithValue("@Active", product.Active);
            cmd.Parameters.AddWithValue("@Unit_Of_Measurement", product.UnitOfMeasurement );
            cmd.Parameters.AddWithValue("@Delivery_Charge_Per_Unit", product.DeliveryChargePerUnit);

            try
            {
                conn.Open();
                count = cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }

            return count;
        }

        /// <summary>
        /// Ryan Spurgetis
        /// 02/17/2017
        /// 
        /// Writes an updated product unit price to the database
        /// </summary>
        /// <param name="productID"></param>
        /// <param name="oldPrice"></param>
        /// <param name="newPrice"></param>
        /// <returns></returns>
        public static int UpdateProductPrice(int productID, decimal oldPrice, decimal newPrice)
        {
            int result = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_update_product_price";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ProductID", SqlDbType.Int);
            cmd.Parameters.Add("@OldPrice", SqlDbType.Decimal);
            cmd.Parameters.Add("@NewPrice", SqlDbType.Decimal);

            cmd.Parameters["@ProductID"].Value = productID;
            cmd.Parameters["@OldPrice"].Value = oldPrice;
            cmd.Parameters["@NewPrice"].Value = newPrice;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw new ApplicationException("A problem occurred updating the product price.");
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public void SetRetrieveSearchParameters(SqlCommand cmd)
        {
            throw new NotImplementedException();
        }

        public void ReadSingle(SqlDataReader reader)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Allows DataBaseMainAccessor to read a list of products
        /// </summary>
        /// <param name="reader"></param>
        public void ReadList(SqlDataReader reader)
        {
            ProductList = new List<Product>();
            while(reader.Read())
            {
                ProductList.Add(new Product()
                {
                    ProductId = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Description = reader.GetString(2),
                    ImageName = reader.IsDBNull(4)?null:reader.GetString(4),
                    Active = reader.GetBoolean(5),
                    UnitOfMeasurement = reader.GetString(6),
                    DeliveryChargePerUnit = reader.GetDecimal(7),
                    ImageBinary = reader["IMAGE_BINARY"] as byte[]
                });
            }
        }

        /// <summary>
        /// Christian Lopez
        /// Created 2017/03/08
        /// 
        /// Get a list of all products in the database
        /// </summary>
        public static List<Product> RetrieveProductList() 
        {
            List<Product> products = new List<Product>();

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
                        Product p = new Product()
                        {
                            ProductId = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            UnitPrice = reader.GetDecimal(3),
                            ImageName = reader.GetString(4),
                            Active = reader.GetBoolean(5),
                            UnitOfMeasurement = reader.GetString(6),
                            DeliveryChargePerUnit = reader.GetDecimal(7)
                        };
                        products.Add(p);
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return products;
        }

        /// <summary>
        /// Created by Natacha Ilunga
        /// 03/29/2017
        /// 
        /// Retrieves Products by suplier Id to Supplier Catalog View
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        public static List<BrowseProductViewModel> RetrieveProductsBySupplierIdToViewModel(int supplierId)
        {
            return RetrieveProductsToBrowseProducts().FindAll(s => s.SupplierID == supplierId);
        }

    }
}
