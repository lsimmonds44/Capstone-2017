using System;
using DataAccessLayer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using System.Data.SqlClient;

namespace LogicLayer
{
    /// <summary>
    /// Christian Lopez
    /// Created on 2017/02/02
    /// 
    /// Handles the logic for a Supplier
    /// </summary>
    public class SupplierManager : ISupplierManager
    {
        /// <summary>
        /// Christian Lopez
        /// Created on 2017/02/02
        /// 
        /// Creates a new supplier based on the given information
        /// </summary>
        /// <param name="userId">The ID to associate a Supplier with a User</param>
        /// <param name="isApproved">Whether or not a Supplier is approved</param>
        /// <param name="approvedBy">The EmployeeID that reviewed the application</param>
        /// <param name="farmName">The name of the farm</param>
        /// <param name="farmCity">The city where the farm is</param>
        /// <param name="farmState">The state where the farm is</param>
        /// <param name="farmTaxId">The tax ID for the farm</param>
        /// <returns>Whether or not it was updated</returns>
        public bool CreateNewSupplier(int userId, bool isApproved, int approvedBy, string farmName, string farmAddress,
            string farmCity, string farmState, string farmTaxId)
        {
            bool wasAdded = false;

            try
            {
                if (1 == SupplierAccessor.CreateNewSupplier(userId, isApproved, approvedBy, farmName, farmAddress,
                    farmCity, farmState, farmTaxId))
                {
                    wasAdded = true;
                }
            }
            catch (SqlException ex)
            {

                throw new ApplicationException("There was a database error.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an unknown error.", ex);
            }

            return wasAdded;
        }

        /// <summary>
        /// Christian Lopez
        /// Created on 2017/02/15
        /// 
        /// Retrieves a Supplier by the user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Supplier RetrieveSupplierByUserId(int userId)
        {
            Supplier s = null;

            try
            {
                s = SupplierAccessor.RetrieveSupplierByUserId(userId);
            }
            catch (SqlException ex)
            {

                throw new ApplicationException("There was a database error.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an unknown error.", ex);
            }

            if (null == s)
            {
                throw new ApplicationException("Could not find supplier for that user ID.");
            }

            return s;
        }

        /// <summary>
        /// Christian Lopez
        /// Created on 2017/02/15
        /// 
        /// Retrieves a Supplier by the supplier ID
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        public Supplier RetrieveSupplierBySupplierID(int supplierId)
        {
            Supplier s = null;

            try
            {
                s = SupplierAccessor.RetrieveSupplierBySupplierId(supplierId);
            }
            catch (SqlException ex)
            {

                throw new ApplicationException("There was a database error.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an unknown error.", ex);
            }

            if (null == s)
            {
                throw new ApplicationException("Could not find supplier for that supplier ID.");
            }

            return s;
        }

        /// <summary>
        /// Christian Lopez
        /// 2017/02/22
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string RetrieveSupplierName(int userId)
        {
            string name = null;

            try
            {
                name = SupplierAccessor.RetrieveSupplierName(userId);
            }
            catch (SqlException ex)
            {

                throw new ApplicationException("There was a database error.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an unknown error.", ex);
            }

            return name;
        }

        /// <summary>
        /// Christian Lopez
        /// Created 2017/03/03
        /// 
        /// Logic for returning a list of suppliers.
        /// </summary>
        /// <returns></returns>
        public List<Supplier> ListSuppliers()
        {
            List<Supplier> suppliers = null;
            try
            {
                suppliers = SupplierAccessor.RetrieveAllSuppliers();
            }
            catch (SqlException ex)
            {

                throw new ApplicationException("There was a database error.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an unknown error.", ex);
            }

            return suppliers;
        }

        /// <summary>
        /// Christian Lopez
        /// Created 2017/03/02
        /// 
        /// The logic to apply for a supplier account (add a supplier but is not approved)
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="farmName"></param>
        /// <param name="farmCity"></param>
        /// <param name="farmState"></param>
        /// <param name="farmTaxId"></param>
        /// <returns></returns>
        public bool ApplyForSupplierAccount(Supplier supplier)
        {
            bool wasAdded = false;

            try
            {
                if (1 == SupplierAccessor.ApplyForSupplierAccount(supplier))
                {
                    wasAdded = true;
                }
            }
            catch (SqlException ex)
            {

                throw new ApplicationException("There was a database error.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an unknown error.", ex);
            }

            return wasAdded;
        }

        public bool UpdateSupplierAccount(Supplier oldSupplier, Supplier newSupplier)
        {
            bool success = false;

            try
            {
                if(1 == SupplierAccessor.UpdateSupplier(oldSupplier, newSupplier))
                {
                    success = true;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return success;
        }

        /// <summary>
        /// Ryan Spurgetis
        /// 4/6/2017
        /// 
        /// Retrieves a list of supplier application status categories
        /// </summary>
        /// <returns></returns>
        public List<string> SupplierAppStatusList()
        {
            List<string> supplierStatus = null;

            try
            {
                supplierStatus = SupplierAccessor.RetrieveSupplierStatusList();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("An error occured." + ex.Message + ex.StackTrace);
            }

            return supplierStatus;
        }

        /// Christian Lopez
        /// 2017/04/06
        /// 
        /// Attempts to retrieve a list of SupplierWithAgreements
        /// </summary>
        /// <returns></returns>
        public List<SupplierWithAgreements> RetrieveSuppliersWithAgreements()
        {
            try
            {
                return SupplierAccessor.RetrieveAllSuppliersWithAgreements();
            }
            catch (SqlException ex)
            {
                
                throw new ApplicationException("There was a database error.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an unknown error", ex);
            }
        }

        /// <summary>
        /// Bobby Thorne
        /// 4/7/2017
        /// 
        /// Calls accessor method to approve supplier and updates who made the change
        /// </summary>
        /// <param name="supplier"></param>
        /// <param name="approvedBy"></param>
        /// <returns></returns>
        public bool ApproveSupplier(Supplier supplier, int approvedBy)
        {
            try
            {
                if (SupplierAccessor.ApproveSupplier(supplier, approvedBy) > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }

        public bool UpdateSupplierAccount(Supplier oldSupplier, Supplier newSupplier, int approvedby)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Bobby Thorne
        /// 4/7/2017
        /// 
        /// Calls accessor method to deny supplier and updates who made the change
        /// </summary>
        /// <param name="supplier"></param>
        /// <param name="approvedBy"></param>
        /// <returns></returns>
        public bool DenySupplier(Supplier supplier, int approvedBy)
        {
            try
            {
                if (SupplierAccessor.DenySupplier(supplier, approvedBy) > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }
    }
}
