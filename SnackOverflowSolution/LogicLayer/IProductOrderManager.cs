using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IProductOrderManager
    {
        ProductOrder retrieveProductOrderDetails(int orderID);
		List<ProductOrder> RetrieveProductOrdersByStatus(String Status);

        bool UpdateProductOrderStatus(int productOrderID, string status);

    }
}
