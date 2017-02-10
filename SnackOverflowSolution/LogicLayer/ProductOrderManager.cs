using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{

    public class ProductOrderManager : IProductOrderManager
    {

        public ProductOrder order = new ProductOrder();

        /// <summary>
        /// Victor Algarin
        /// Created 2017/02/08
        /// 
        /// Retrieves the details for a specific order through an order id
        /// </summary>
        public ProductOrder retrieveProductOrderDetails(int orderID)
        {
            try
            {
                order = ProductOrderAccessor.RetrieveOrderByID(orderID);
            }
            catch (Exception)
            {

                throw new ApplicationException("There was a problem retrieving the product order details.");
            }

            return order;
        }
    }
}
