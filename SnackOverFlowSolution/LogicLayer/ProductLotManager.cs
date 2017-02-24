using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;

namespace LogicLayer
{
    public class ProductLotManager : IProductLotManager
    {
        public int AddProductLot(ProductLot toAdd)
        {
            var accessor = new ProductLotAccessor();
            accessor.ProductLotInstance = toAdd;
            try
            {
                return DatabaseMainAccessor.Create(accessor);
            } catch
            {
                throw;
            }
        }
    }
}
