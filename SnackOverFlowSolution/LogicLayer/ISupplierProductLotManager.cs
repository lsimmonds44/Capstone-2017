using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface ISupplierProductLotManager
    {
        bool CreateSupplierProductLot(SupplierProductLot toAdd);

        bool AddProduct(SupplierProductLot p);

        bool DeleteSupplierProductLot(SupplierProductLot lot);

        List<SupplierProductLot> RetrieveSupplierProductLotsBySupplier(Supplier supplier);

        SupplierProductLot RetrieveSupplierProductLotById(int id);

        bool UpdateSupplierProductLot(SupplierProductLot oldLot, SupplierProductLot newLot);
    }
}
