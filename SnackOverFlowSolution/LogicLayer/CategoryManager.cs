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
    /// Ryan Spurgetis
    /// 
    /// Created:
    /// 2017/02/09
    /// 
    /// Category Manager Interface
    /// </summary>
    public class CategoryManager : ICategoryManager
    {
        /// <summary>
        /// Ryan Spurgetis
        /// 
        /// Created
        /// 2017/02/09
        /// 
        /// Takes data for a new Category for Products from view, sends to the Accessor class.
        /// </summary>
        /// <remarks>
        /// Ariel Sigo
        /// Updated:
        /// 2017/04/29
        /// 
        /// Standardized Comment
        /// </remarks>
        /// <param name="productCategory">Name of category</param>
        /// <param name="prodCategoryDesc">Description of category</param>
        /// <returns>True if succesfful</returns>
        public bool CreateCategory(Category category)
        {
            var result = false;

            try
            {
                result = (1 == CategoryAccessor.CreateCategory(category));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Problem with adding product category." + ex.Message + ex.StackTrace);
            }

            return result;
        }

        /// <summary>
        /// Mason Allen
        /// Created:
        /// 2017/04/13
        /// </summary>
        /// <remarks>
        /// Ariel Sigo
        /// Updated:
        /// 2017/04/29
        /// 
        /// Standardized Comment
        /// </remarks>
        /// <param name="categoryId"></param>
        /// <returns>1 for success, 0 for fail</returns>
        public int DeleteCategory(string categoryId)
        {
            int success = 0;

            try
            {
                success = CategoryAccessor.DeleteCategory(categoryId);
            }
            catch (Exception)
            {
                throw;
            }

            return success;
        }

        /// <summary>
        /// Created by Mason Allen
        /// Created on 5/2/17
        /// Returns a list of all categories in the db
        /// </summary>
        /// <returns></returns>
        public List<Category> RetrieveCategoryList()
        {
            List<Category> categoryList;
            try 
            {
                categoryList = CategoryAccessor.RetrieveCategoryList();
            }
            catch (Exception)
            {
                throw;
            }

            return categoryList;
        }
    }
}
