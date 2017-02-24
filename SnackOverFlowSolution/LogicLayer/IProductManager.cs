using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
    public interface IProductManager
    {

        /// <summary>
        /// Dan Brown
        /// Created on 2017/03/10
        /// 
        /// Delete a product using the productID
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        int DeleteProduct(int productID);
		
        Product retrieveProductById(int productId);
    }
}
