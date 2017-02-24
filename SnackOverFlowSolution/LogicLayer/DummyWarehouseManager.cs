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
        int WareHouseIDToSend = 10000;
        public List<Warehouse> ListWarehouses()
        {
            return new List<Warehouse>(new Warehouse[] { new Warehouse() { WarehouseID = WareHouseIDToSend = 10000 } });
        }
    }
}
