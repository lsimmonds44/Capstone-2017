using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IProductManager
    {
        List<Product> ListProducts();

        /// <summary>
        /// Dan Brown
        /// Created on 2017/03/10
        /// 
        /// Delete a product using the productID
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        int DeleteProduct(int productID);
		
        Product RetrieveProductById(int productId);
    }
}
