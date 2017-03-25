using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;

namespace LogicLayer
{
    public class OrderLineManager : IOrderLineManager
    {

        /// <summary>
        /// Eric Walton
        /// 2017/10/3
        /// Calls order line accesor create order line and retrieves
        /// the order line id
        /// </summary>
        /// <param name="orderLine"></param>
        /// <returns></returns>
        public int CreateOrderLine(OrderLine orderLine)
        {
            int result = 0;
            try
            {
               result = OrderLineAccessor.CreateOrderLine(orderLine);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }





        public List<OrderLine> RetrieveOrderLineListByProductOrderId(int ProductOrderId, Decimal OrderAmount)
        {
            List<OrderLine> result = null;
            try
            {
                result = OrderLineAccessor.RetrieveOrderLinesByOrderId(ProductOrderId, OrderAmount);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
    } // end of class
} // end of namespace
