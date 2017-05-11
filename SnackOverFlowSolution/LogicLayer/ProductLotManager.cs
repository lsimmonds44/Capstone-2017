using System;
﻿using DataObjects;
using DataAccessLayer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace LogicLayer
{
    /// <summary>
    /// Christian Lopez
    /// Created on 2017/02/15
    /// 
    /// Manages the logic regarding Product Lots
    /// </summary>
    public class ProductLotManager : IProductLotManager
    {
        /// <summary>
        /// William Flood
        /// Created: 2017/02/15
        /// 
        /// Manages the logic regarding adding a Product Lot.
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated 2017/04/06
        ///
        /// Changed to work with static accessor class; standardized.
        /// </remarks>
        /// <param name="productLot">The product lot to add.</param>
        /// <returns>Whether or not the product lot was created successfully.</returns>
        public bool CreateProductLot(ProductLot productLot)
        {
            bool result = false;
            try
            {
                result = (1 == ProductLotAccessor.CreateProductLot(productLot));
            }
            catch
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Christian Lopez
        /// 2017/02/22
        /// </summary>
        /// <param name="supplier"></param>
        /// <returns></returns>
        public ProductLot RetrieveNewestProductLotBySupplier(Supplier supplier)
        {
            ProductLot pl = null;
            if (null != supplier)
            {
                try
                {
                    pl = ProductLotAccessor.RetrieveNewestProductLotBySupplier(supplier);
                }
                catch (SqlException ex)
                {

                    throw new ApplicationException("There was a database error.", ex);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("There was an unknown error.", ex);
                }
            }
            return pl;
        }


        public bool AddProduct(ProductLot p)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Christian Lopez
        /// 2017/02/27
        /// 
        /// Gets a list of product lots from the database
        /// 
        /// Update 
        /// Bobby Thorne
        /// 5/10/2017
        /// 
        /// Added the farm name to the product lot data object so it can be used in the data grid
        /// </summary>
        /// <returns></returns>
        public List<ProductLot> RetrieveProductLots()
        {
            List<ProductLot> lots = null;
            try
            {
                lots = ProductLotAccessor.RetrieveProductLots();
                IProductManager productManager = new ProductManager();
                ISupplierManager supplierManager = new SupplierManager();
                foreach (var lot in lots)
                {
                    var productInLot = productManager.RetrieveProductById((int)lot.ProductId);
                    lot.ProductName = productInLot.Name;
                    try
                    {
                        
                        int lotSupplierid = (int)lot.SupplierId;
                        Supplier supplier = supplierManager.RetrieveSupplierBySupplierID(lotSupplierid);
                        lot.SupplierName = supplier.FarmName;
                    }
                    catch
                    {
                        lot.SupplierName = "Unable to find farm name";
                    }

                }
            }
            catch (SqlException ex)
            {

                throw new ApplicationException("There was a database error.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an unknown error.", ex);
            }

            return lots;
        }

        /// <summary>
        /// William Flood
        /// 2017/03/09
        /// 
        /// Gets a list of expired product lots from the database
        /// </summary>
        /// <returns></returns>
        public List<ProductLot> RetrieveExpiredProductLots()
        {
            List<ProductLot> lots = null;
            try
            {
                lots = ProductLotAccessor.RetrieveExpiredProductLots();
                IProductManager productManager = new ProductManager();
                foreach (var lot in lots)
                {
                    var productInLot = productManager.RetrieveProductById((int)lot.ProductId);
                    lot.ProductName = productInLot.Name;
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("There was an error", ex);
            }

            return lots;
        }

        /// <summary>
        /// Created by Michael Takrama
        /// 3/2/2017
        /// 
        /// Updates Product Lot Available Quantity
        /// </summary>
        /// <param name="oldProductLot"></param>
        /// <param name="newProductLot"></param>
        /// <returns></returns>
        public int UpdateProductLotAvailableQuantity(ProductLot oldProductLot, ProductLot newProductLot)
        {
            int result = 0;

            result = ProductLotAccessor.UpdateProductLotAvailableQuantity(oldProductLot.ProductLotId, oldProductLot.AvailableQuantity,
                newProductLot.AvailableQuantity);

            return result;
        }

        /// <summary>
        /// Ethan Jorgensen
        /// 2017/03/23
        /// 
        /// Sets a product lot inactive.
        /// </summary>
        /// <returns></returns>
        public bool DeleteProductLot(ProductLot lot)
        {
            bool result = false;


            try
            {
                result = 1 == ProductLotAccessor.DeleteProductLot(lot);
            }
            catch(Exception)
            {
                throw;
            }

            return result;
        }




        /// <summary>
        /// Eric Walton
        /// Created: 2017/03/24
        /// Gets only the active product lots (Ones that have more than 0 quantity)
        /// </summary>
        /// 
        /// <remarks>
        /// Christian Lopez
        /// Updated: 2017/03/29
        /// 
        ///  Extracted method to reuse it
        /// </remarks>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/21
        /// 
        /// Added basic try-catch logic.
        /// </remarks>
        /// 
        /// <returns>A list of active product lots.</returns>
        public List<ProductLot> RetrieveActiveProductLots()
        {
            List<ProductLot> lots = null;
            try
            {
                lots = ProductLotAccessor.RetrieveActiveProductLots();
                setProductLotNames(lots);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Could not retrieve active products", ex.InnerException);
            }

            return lots;
        }

        /// <summary>
        /// Christian Lopez
        /// 2017/03/29
        /// 
        /// Sets the names for the product lots
        /// </summary>
        /// <param name="lots"></param>
        private static void setProductLotNames(List<ProductLot> lots)
        {
            IProductManager productManager = new ProductManager();
            try
            {
                foreach (var lot in lots)
                {
                    var productInLot = productManager.RetrieveProductById((int)lot.ProductId);
                    lot.ProductName = productInLot.Name;
                }
            }
            catch (Exception ex)
            {
                
                throw new ApplicationException("Could not set product names", ex.InnerException);
            }
            
            
        }

        /// <summary>
        /// Christian Lopez
        /// 2017/03/29
        /// 
        /// Retrieves a list of product lots from a given supplier
        /// </summary>
        /// <param name="supplier"></param>
        /// <returns></returns>
        public List<ProductLot> RetrieveProductLotsBySupplier(Supplier supplier)
        {
            List<ProductLot> lots = null;
            try
            {
                lots = ProductLotAccessor.RetrieveProductLotsBySupplier(supplier);
                setProductLotNames(lots);
            }
            catch (SqlException ex)
            {

                throw new ApplicationException("There was a database error.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an unknown error.", ex);
            }
            return lots;
        }

        /// <summary>
        /// Christian Lopez
        /// 2017/03/29
        /// 
        /// Returns a product lot associated with that id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProductLot RetrieveProductLotById(int id)
        {
            try
            {
                ProductLot lot = ProductLotAccessor.RetrieveProductLotById(id);
                ProductManager pm = new ProductManager();
                lot.ProductName = (pm.RetrieveProductById((int)lot.ProductId)).Name;
                return lot;
            }
            catch (SqlException ex)
            {

                throw new ApplicationException("There was a database error.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an unknown error.", ex);
            }

        }

        /// <summary>
        /// Ryan Spurgetis
        /// 2017/03/24
        /// Sends the new price to update product lot price from inspection
        /// </summary>
        /// <param name="prodLot"></param>
        /// <param name="newPrice"></param>
        /// <returns></returns>
        public int UpdateProductLotPrice(ProductLot prodLot, decimal newPrice)
        {
            int result = 0;
            int prodLotId = (int)prodLot.ProductLotId;

            try
            {
                result = ProductLotAccessor.UpdateProductPrice(prodLotId, newPrice);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return result;
        }
    }
}
