using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    /// <summary>
    /// Mason Allen
    /// Created on 5/2/17
    /// </summary>
    public class ProductCategoryManager
    {
        /// <summary>
        /// Mason Allen
        /// Created on 5/2/17
        /// Creates a product category record for a new product
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public int CreateProductCategoryRecord(int productId, string categoryId)
        {
            int rows = 0;
            try
            {
                rows = ProductCategoryAccessor.CreateProductCategoryRecord(productId, categoryId);
            }
            catch (Exception)
            {
                throw;
            }

            return rows;
        }
    }
}
