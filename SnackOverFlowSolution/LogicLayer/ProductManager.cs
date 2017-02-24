using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class ProductManager : IProductManager
    {
        
         ///<summary> 
         ///Dan Brown
         ///Created on 2017/03/10
         ///
         /// Delete an individual product from the product table (following documentation guidlines)
         ///</summary>
         ///<param name="productID"> The ID field of the product to be deleted </param>
         ///<returns> Returns rows affected (int) </returns>
         ///<exception cref="System.ApplicationException"> Thrown if 'ProductAccessor.DeleteProduct' returns 0 rows affected </exception>
         ///<exception cref="System.Exception"> Thrown if there is an error connecting to the 'ProductAccessor' class </exception>
        public int DeleteProduct(int productID)
        {
            int result = 0;

            try
            {
                if (1 == ProductAccessor.DeleteProduct(productID))
                {
                    result = 1;
                }
                else
                {
                    throw new ApplicationException("Product matching that productID was not found. No Product deleted");
                }
            }
            catch (Exception)
            {
                
                throw;
            }


            return result;
        }





        public DataObjects.Product retrieveProductById(int productId)
        {
            throw new NotImplementedException();
        }
    }
}
