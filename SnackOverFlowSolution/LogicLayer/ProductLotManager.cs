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
    }
}
