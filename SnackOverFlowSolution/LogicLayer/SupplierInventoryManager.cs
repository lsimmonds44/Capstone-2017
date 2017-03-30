using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;

namespace LogicLayer
{
    public class SupplierInventoryManager : ISupplierInventoryManager
    {
        public int CreateSupplierInventory(SupplierInventory toAdd)
        {
            try
            {
                return SupplierInventoryAccessor.CreateSupplierInventory(toAdd);
            } catch
            {
                throw;
            }

        }
    }
}
