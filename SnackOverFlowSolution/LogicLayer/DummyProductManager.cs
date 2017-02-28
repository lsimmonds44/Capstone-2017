using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
    public class DummyProductManager : IProductManager
    {
        int dummyProductID = 10000;
        public List<Product> ListProducts()
        {
            return new List<Product>(new Product[] { new Product { ProductId = 10000 } });
        }


        public int DeleteProduct(int productID)
        {
            throw new NotImplementedException();
        }

        public Product RetrieveProductById(int productId)
        {
            throw new NotImplementedException();
        }


        public int CreateProduct(Product product)
        {
            throw new NotImplementedException();
        }


        public List<BrowseProductViewModel> RetrieveProductsToBrowseProducts()
        {
            throw new NotImplementedException();
        }

        public List<BrowseProductViewModel> FilterProducts(List<string> vendors, List<string> categories, double min, double max)
        {
            throw new NotImplementedException();
        }
    }
}
