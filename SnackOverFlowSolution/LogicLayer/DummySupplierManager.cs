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


        public bool CreateNewSupplier(int userId, bool isApproved, int approvedBy, string farmName, string farmAddress, string farmCity, string farmState, string farmTaxId)
        {
            throw new NotImplementedException();
        }

        public Supplier RetrieveSupplierByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public Supplier RetrieveSupplierBySupplierID(int supplierId)
        {
            throw new NotImplementedException();
        }

        public string RetrieveSupplierName(int userId)
        {
            throw new NotImplementedException();
        }


        public bool ApplyForSupplierAccount(Supplier supplier)
        {
            throw new NotImplementedException();
        }

        public bool UpdateSupplierAccount(Supplier oldSupplier, Supplier newSupplier)
        {
            throw new NotImplementedException();
        }

        public List<string> SupplierAppStatusList()
		{
            throw new NotImplementedException();
        }

        public List<SupplierWithAgreements> RetrieveSuppliersWithAgreements()
        {
            throw new NotImplementedException();
        }


        public bool DenySupplier(Supplier _supplier, int approvedby)
        {
            throw new NotImplementedException();
        }

        public bool ApproveSupplier(Supplier supplier, int approvedby)
        {
            throw new NotImplementedException();
        }


        public SupplierWithAgreements RetrieveSupplierWithAgreementsBySupplierId(int supplierId)
        {
            throw new NotImplementedException();
        }


        public SupplierWithAgreements RetrieveSupplierWithAgreementsByUserId(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
