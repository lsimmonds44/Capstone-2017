using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataObjects;
using System.Data.SqlClient;

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
        /// <summary>
        /// Christian Lopez
        /// 2017/03/03
        /// </summary>
        /// <returns></returns>
        public List<Warehouse> ListWarehouses()
        {
            List<Warehouse> warehouses = null;

            try
            {
                warehouses = WarehouseAccessor.RetrieveAllWarehouses();
            }
            catch (SqlException ex)
            {
                
                throw new ApplicationException("There was a database error.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an unkown error.", ex);
            }

            return warehouses;
        }

        public int addWarehouse(Warehouse newWarehouse)
        {
            int success = 0;
            try
            {
                success = WarehouseAccessor.CreateWarehouse(newWarehouse);
            }
            catch (Exception)
            {
                throw new ApplicationException("There was an error saving this vehicle.");
            }
            return success;
        }
    }
}
