using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class TestProductLotManager : IProductLotManager
    {
        public ProductLot RetrieveNewestProductLotBySupplier(Supplier supplier)
        {
            return new ProductLot
            {
                ProductLotId = 10000,
                ProductId = 1,
                SupplierId = supplier.SupplierID,
                WarehouseId = 1,
                LocationId = 1,
                AvailableQuantity = 500,
                Quantity = 1000,
                SupplyManagerId = 10000,
                DateReceived = DateTime.Now,
                ExpirationDate = DateTime.Now.AddDays(7.0)
            };
        }

        public int AddProductLot(ProductLot toAdd)
        {
            throw new NotImplementedException();
        }

        public bool AddProduct(ProductLot p)
        {
            throw new NotImplementedException();
        }


        public List<ProductLot> RetrieveProductLots()
        {
            return new List<ProductLot> {
                RetrieveNewestProductLotBySupplier(new Supplier()),
                RetrieveNewestProductLotBySupplier(new Supplier()),
                RetrieveNewestProductLotBySupplier(new Supplier())};
        }


        public int UpdateProductLotAvailableQuantity(ProductLot oldProductLot, ProductLot newProductLot)
        {
            throw new NotImplementedException();
        }

        public List<ProductLot> RetrieveExpiredProductLots()
        {
            throw new NotImplementedException();
        }

        public bool DeleteProductLot(ProductLot lot)
        {
            throw new NotImplementedException();
        }


        public List<ProductLot> RetrieveProductLotsWithGradeAndPrice()
        {
            throw new NotImplementedException();
        }
    }
}
