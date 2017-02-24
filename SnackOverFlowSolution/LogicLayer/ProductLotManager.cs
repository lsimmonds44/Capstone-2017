using DataObjects;
using DataAccessLayer;
using System;
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
            return ProductLotAccessor.CreateProductLot(p);
        }
    }
}
