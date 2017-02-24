using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    /// <summary>
    /// Christian Lopez
    /// Created on 2017/02/15
    /// 
    /// The interface for the ProductLot Manager
    /// </summary>
    public interface IProductLotManager
    {
        ProductLot RetrieveNewestProductLotBySupplier(Supplier supplier);
    }
}
