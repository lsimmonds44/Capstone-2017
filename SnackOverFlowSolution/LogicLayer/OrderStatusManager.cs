using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    /// <summary>
    /// Robert Forbes
    /// 2017/03/01
    /// </summary>
    public class OrderStatusManager : IOrderStatusManager
    {
        /// <summary>
        /// Robert Forbes
        /// 2017/03/01
        /// 
        /// Gets all order status from the database
        /// </summary>
        /// <returns>A list of strings representing order status</returns>
        public List<string> RetrieveAllOrderStatus()
        {
            List<string> status = new List<string>();

            try
            {
                status = OrderStatusAccessor.RetrieveAllOrderStatus();
            }
            catch (Exception)
            {
                throw new ApplicationException("Unable to communicate with the database.");
            }

            return status;
        }
    }
}
