using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IProductLotManager
    {
        int AddProductLot(ProductLot toAdd);
        ProductLot RetrieveNewestProductLotBySupplier(Supplier supplier);

        bool AddProduct(ProductLot p);

        List<ProductLot> RetrieveProductLots();
        List<ProductLot> RetrieveActiveProductLots();

        int UpdateProductLotAvailableQuantity(ProductLot oldProductLot, ProductLot newProductLot);

        List<ProductLot> RetrieveExpiredProductLots();
        bool DeleteProductLot(ProductLot lot);

        List<ProductLot> RetrieveProductLotsBySupplier(Supplier supplier);

        ProductLot RetrieveProductLotById(int id);

        int UpdateProductLotPrice(ProductLot prodLot, decimal newPrice);

    }
}
