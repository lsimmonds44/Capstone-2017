using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
    public class DummyWarehouseManager : IWarehouseManager
    {

        public List<Warehouse> ListWarehouses()
        {
            return new List<Warehouse>(new Warehouse[]{ new Warehouse() { WarehouseID = 10000 } });
        }
    }
}
