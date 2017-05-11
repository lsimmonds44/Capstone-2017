using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;

namespace LogicLayer
{
    /// <summary>
    /// Created 2017-03-29 by William Flood
    /// </summary>
    public class SupplierInventoryManager : ISupplierInventoryManager
    {
        /// <summary>
        /// Created 2017-03-29 by William Flood
        /// </summary>
        /// <param name="toAdd"></param>
        /// <returns></returns>
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
