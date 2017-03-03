using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class CategoryManager : ICategoryManager
    {
        /// <summary>
        /// Ryan Spurgetis
        /// 02/09/2017
        /// 
        /// Takes data for a new Category for Products from view, sends to the Accessor class.
        /// </summary>
        /// <param name="productCategory">Name of category</param>
        /// <param name="prodCategoryDesc">Description of category</param>
        /// <returns></returns>
        public bool NewProductCategory(string productCategory, string prodCategoryDesc)
        {
            var result = false;

            try
            {
                result = (1 == CategoryAccessor.CreateProductCategory(productCategory, prodCategoryDesc));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Problem with adding product category." + ex.Message + ex.StackTrace);
            }

            return result;
        }
    }
}
