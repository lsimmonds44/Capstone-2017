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
    public interface IProductLotManager
    {
        int AddProductLot(ProductLot toAdd);
        ProductLot RetrieveNewestProductLotBySupplier(Supplier supplier);
    }
}
