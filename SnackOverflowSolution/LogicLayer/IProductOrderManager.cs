using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
    interface IProductOrderManager
    {
        List<ProductOrder> RetrieveProductOrdersByStatus(String Status);
    }
}
