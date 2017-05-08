using System;
namespace LogicLayer
{
    public interface ISupplierOrderLineManager
    {
        int CreateOrderLine(DataObjects.SupplierOrderLine supplierOrderLine);
        System.Collections.Generic.List<DataObjects.SupplierOrderLine> RetrieveSupplierOrderLines(int SupplierProductOrderId);
    }
}
