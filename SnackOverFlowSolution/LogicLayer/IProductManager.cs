using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
    public interface IProductManager
    {
        List<Product> ListProducts();
        Product retrieveProductById(int productId);
    }
}
