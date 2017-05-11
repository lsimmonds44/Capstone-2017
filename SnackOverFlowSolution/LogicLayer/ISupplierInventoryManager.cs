using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    /// <summary>
    /// Created 2017-03-29 by William Flood
    /// </summary>
    public interface ISupplierInventoryManager
    {
        /// <summary>
        /// 
        /// Created 2017-03-29 by William Flood
        /// Adds a quantity of stock to a supplier's inventory
        /// </summary>
        /// <param name="toAdd"></param>
        /// <returns></returns>
        int CreateSupplierInventory(SupplierInventory toAdd);
    }
}
