using System;
﻿using DataObjects;
using DataAccessLayer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		/// Created on 2017/02/15
		/// 
		/// Manages the logic regarding adding a Product Lots
		/// </summary>
        public int AddProductLot(ProductLot toAdd)
        {
            var accessor = new ProductLotAccessor();
            accessor.ProductLotInstance = toAdd;
            try
            {
                return DatabaseMainAccessor.Create(accessor);
            } catch
            {
                throw;
            }
		}
		
        public ProductLot RetrieveNewestProductLotBySupplier(Supplier supplier)
        {
            ProductLot pl = null;
            if (null != supplier)
            {
                try
                {
                    pl = ProductLotAccessor.RetrieveNewestProductLot(supplier);
                }
                catch (Exception)
                {

                    throw;
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
        /// </summary>
        /// <returns></returns>
        public List<ProductLot> RetrieveProductLots()
        {
            List<ProductLot> lots = null;
            try
            {
                lots = ProductLotAccessor.RetrieveProductLots();
                IProductManager productManager = new ProductManager();
                foreach (var lot in lots) {
                    var productInLot = productManager.RetrieveProductById((int)lot.ProductId);
                    lot.ProductName = productInLot.Name;
                }
            }
            catch (Exception)
            {
                
                throw;
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
            catch (Exception)
            {

                throw;
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

            result = ProductLotAccessor.DeleteProductLot(lot);

            return result;
        }




        /// <summary>
        /// Eric Walton
        /// 2017/03/24
        /// Gets only the active product lots (Ones that have more than 0 quantity)
        /// </summary>
        /// <returns></returns>
        /// <remarks>Last Modified 2017/03/29 by Christian Lopez - Extracted method to reuse it</remarks>
        public List<ProductLot> RetrieveActiveProductLots()
        {
            List<ProductLot> lots = null;
            try
            {
                lots = ProductLotAccessor.RetrieveActiveProductLots();
                setProductLotNames(lots);
            }
            catch (Exception)
            {

                throw;
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
            foreach (var lot in lots)
            {
                var productInLot = productManager.RetrieveProductById((int)lot.ProductId);
                lot.ProductName = productInLot.Name;
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
            catch (Exception)
            {
                
                throw;
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
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
