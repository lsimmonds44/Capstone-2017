using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
    public class DummySupplierManager : ISupplierManager
    {
        int insertedSupplierID = 10000;
        public List<Supplier> ListSuppliers()
        {
            return new List<Supplier>(new Supplier[] { new Supplier() { SupplierID = insertedSupplierID } });
        }
    }
}
