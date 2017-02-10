using DataObjects;
using DataAccessLayer;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class ProductOrderManager
    {
        public List<ProductOrder> RetrieveProductOrdersByStatus(String Status)
        {
            List<ProductOrder> ProductOrderList = ProductOrderAccessor.RetrieveProductOrdersByStatus(Status);
            foreach (ProductOrder ProductOrderFound in ProductOrderList)
            {
                ProductOrderFound.BackorderPreorderList = BackorderPreorderAccessor.RetrieveBackorderPreorder(
                    new BackorderPreorder
                    {
                        OrderId = ProductOrderFound.OrderId
                    }
                );

                ProductOrderFound.DeliveryList = DeliveryAccessor.RetrieveDelivery(
                    new Delivery
                    {
                        OrderId = ProductOrderFound.OrderId
                    }
                );

                ProductOrderFound.EmployeeOrderResponsibilityList = EmployeeOrderResponsibilityAccessor.RetrieveEmployeeOrderResponsibility(
                    new EmployeeOrderResponsibility
                    {
                        OrderId = ProductOrderFound.OrderId
                    }
                );

                ProductOrderFound.PackageList = PackageAccessor.RetrievePackage(
                    new Package
                    {
                        OrderId = ProductOrderFound.OrderId
                    }
                );
            }
            return ProductOrderList;
        }
    }
}
