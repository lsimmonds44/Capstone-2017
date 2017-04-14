using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
    /// <summary>
    /// Christian Lopez
    /// 2017/04/13
    /// </summary>
    public interface ICompanyOrderManager
    {
        List<CompanyOrder> RetrieveCompanyOrders();

        List<CompanyOrder> RetrieveCompanyOrdersBySupplierId(int supplierId);

        List<CompanyOrderLine> RetrieveCompanyOrderLinesByOderId(int orderId);

        List<CompanyOrderWithLines> RetrieveCompanyOrdersWithLines();

        List<CompanyOrderWithLines> RetrieveCompanyOrdersWithLinesBySupplierId(int supplierId);

        CompanyOrderWithLines RetrieveCompanyOrderWithLinesById(int orderId);
    }
}
