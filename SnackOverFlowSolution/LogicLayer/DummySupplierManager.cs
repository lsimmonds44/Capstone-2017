﻿using System;
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


        public bool CreateNewSupplier(int userId, bool isApproved, int approvedBy, string farmName, string farmCity, string farmState, string farmTaxId)
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
    }
}