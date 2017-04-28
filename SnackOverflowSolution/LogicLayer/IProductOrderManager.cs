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
        int createProductOrder(ProductOrder productOrder);
        bool UpdateProductOrder(ProductOrder oldOrder, ProductOrder newOrder);
        int SaveOrder(int orderID);
        int LoadOrder(int productOrderID);
        List<int> RetrieveSaveOrders(string username);
    }
}
