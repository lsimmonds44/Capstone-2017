using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;

namespace LogicLayer
{
    public class SupplierOrderLineManager : ISupplierOrderLineManager
    {
        /// <summary>
        /// Laura Simmonds
        /// Created:
        /// 2017/05/08
        /// 
        /// Calls supplier order line accessor create order line 
        /// and retrieves the order line id
        /// </summary>
        /// <param name="orderLine"></param>
        /// <returns></returns>
        public int CreateOrderLine(SupplierOrderLine supplierOrderLine)
        {
            int result = 0;
            try
            {
                result = SupplierOrderLineAccessor.CreateSupplierOrderLine(supplierOrderLine);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        ///  Laura Simmonds
        ///  Created:
        ///  2017/05/08
        ///  
        ///  Calls order line accessor to retrieve an orderling by order id
        /// </summary>
        /// <param name="SupplierProductOrderId"></param>
        /// <returns></returns>
        public List<SupplierOrderLine> RetrieveSupplierOrderLines(int SupplierProductOrderId)
        {
            List<SupplierOrderLine> result = null;
            try
            {
                result = SupplierOrderLineAccessor.RetrieveOrderLinesBySupplierOrderId(SupplierProductOrderId);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
    }
}
