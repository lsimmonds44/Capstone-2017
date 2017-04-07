using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface ISupplierManager
    {
        List<Supplier> ListSuppliers();
        /// <summary>
        /// Christian Lopez
        /// Created on 2017/02/02
        /// 
        /// Method signature to create a new supplier
        /// </summary>
        /// <param name="userId">ID to link the supplier to a User</param>
        /// <param name="isApproved">Wether or not the supplier is approved</param>
        /// <param name="approvedBy">EmployeeId that approved/denied the request</param>
        /// <param name="farmName">The name of the farm</param>
        /// <param name="farmCity">The city where the farm is</param>
        /// <param name="farmState">The state where the farm is</param>
        /// <param name="farmTaxId">The farm tax ID</param>
        /// <returns></returns>
        bool CreateNewSupplier(int userId, bool isApproved, int approvedBy, string farmName, string farmAddress,
            string farmCity, string farmState, string farmTaxId);

        /// <summary>
        /// Christian Lopez
        /// Created on 2017/02/15
        /// 
        /// Method Signature to retrieve a supplier by userId
        /// </summary>
        /// <param name="userId">The id to use</param>
        /// <returns>The supplier in question</returns>
        Supplier RetrieveSupplierByUserId(int userId);

        /// <summary>
        /// Christian Lopez
        /// Created on 2017/02/15
        /// 
        /// Method Signature to retrieve a supplier by supplierId
        /// </summary>
        /// <param name="supplierId">The id to use</param>
        /// <returns>The supplier in question</returns>
        Supplier RetrieveSupplierBySupplierID(int supplierId);

        /// <summary>
        /// Christian Lopez
        /// Created on 2017/02/15
        /// 
        /// Method Signature to retrieve a supplier's name
        /// </summary>
        /// <param name="userId">the userId for the supplier</param>
        /// <returns>The supplier's name</returns>
        string RetrieveSupplierName(int userId);

        /// <summary>
        /// Christian Lopez
        /// Created on 2017/03/02
        /// 
        /// Method signature to apply for supplier account
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="farmName"></param>
        /// <param name="farmCity"></param>
        /// <param name="farmState"></param>
        /// <param name="farmTaxId"></param>
        /// <returns></returns>
        bool ApplyForSupplierAccount(Supplier supplier);

        /// <summary>
        /// Skyler Hiscock
        /// Created on 2017/03/09
        /// 
        /// Method signature to update supplier account
        /// </summary>
        /// <param name="oldSupplier"></param>
        /// <param name="newSupplier"></param>
        /// <returns></returns>
        bool UpdateSupplierAccount(Supplier oldSupplier, Supplier newSupplier);

        /// <summary>
        /// Ryan Spurgetis
        /// 4/6/2017
        /// 
        /// Method signature to display list of supplier application statuses
        /// </summary>
        /// <returns></returns>
        List<string> SupplierAppStatusList();
		
        /// Christian Lopez
        /// 2017/04/06
        /// 
        /// Method signature to retrieve a list of SupplierWithAgreements
        /// </summary>
        /// <returns></returns>
        List<SupplierWithAgreements> RetrieveSuppliersWithAgreements();
    }
}
