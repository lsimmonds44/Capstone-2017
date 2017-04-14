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
    /// Created on 2017/02/15
    /// 
    /// This is a stub to test the Product Manager
    /// </summary>
    public class TestProductManager : IProductManager
    {
        public Product retrieveProductById(int productId)
        {
            return new Product
            {
                ProductId = productId,
                Name = "Test Product",
                Description = "This is a test product.",
                ImageName = "TestProduct.jpg",
                UnitPrice = 10.0M,
                UnitOfMeasurement = "Units",
                DeliveryChargePerUnit = 0.1M,
                Active = true
            };
        }

        public int DeleteProduct(int productID)
        {
            throw new NotImplementedException();
        }

        public List<Product> ListProducts()
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


        public List<Product> RetrieveProducts()
        {
            throw new NotImplementedException();
        }


        public List<BrowseProductViewModel> RetrieveProductsBySupplierId(int id)
        {
            throw new NotImplementedException();
        }


        public List<ProductGradePrice> RetrieveGradeForProduct(int productId)
        {
            throw new NotImplementedException();
        }
    }
}
