using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;

namespace LogicLayer
{
    /// <summary>
    /// Christian Lopez
    /// Created 2017/03/03
    /// 
    /// Handles the logic between getting data from the 
    /// warehouse accessor and the presentation layer
    /// </summary>
    public class WarehouseManager : IWarehouseManager
    {
        public List<Warehouse> ListWarehouses()
        {
            List<Warehouse> warehouses = null;

            try
            {
                warehouses = WarehouseAccessor.RetrieveAllWarehouses();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }

            return warehouses;
        }
    }
}
