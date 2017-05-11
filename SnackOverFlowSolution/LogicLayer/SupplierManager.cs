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
        /// Created: 2017/02/02
        /// 
        /// Creates a new supplier based on the given information
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/21
        /// 
        /// Changed method signature to take a Supplier instead of the fields in a supplier.
        /// </remarks>
        /// 
        /// <param name="supplier">The supplier to create</param>
        /// <returns>Whether or not it was created</returns>
        public bool CreateNewSupplier(Supplier supplier)
        {
            bool wasAdded = false;

            try
            {
                if (1 == SupplierAccessor.CreateNewSupplier(supplier))
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
                s = SupplierAccessor.RetrieveSupplier(supplierId);
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
        /// 
        /// Update 
        /// Bobby Thorne
        /// 5/10/2017
        /// 
        /// Added a foreach loop to fill an ApprovedByName
        /// to be used in the supplier datagrid 
        /// </summary>
        /// <returns></returns>
        public List<Supplier> ListSuppliers()
        {
            List<Supplier> suppliers = null;
            try
            {
                suppliers = SupplierAccessor.RetrieveSuppliers();
                IEmployeeManager employeeManager = new EmployeeManager();
                IUserManager userManager = new UserManager();
                foreach (var supplier in suppliers)
                {
                    if (supplier.ApprovedBy == null)
                    {
                        supplier.ApprovedByName = " ";
                    }
                    else
                    {
                        User user = userManager.RetrieveUser((int)employeeManager.RetrieveEmployee((int)supplier.ApprovedBy).UserId);
                        supplier.ApprovedByName = user.LastName + ", " + user.FirstName; 
                    }
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

            return suppliers;
        }

        /// <summary>
        /// Christian Lopez
        /// Created: 2017/03/02
        /// 
        /// The logic to apply for a supplier account (add a supplier but is not approved)
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/21
        /// 
        /// Changed "ApplyForSupplierAccount" call to "CreateSupplier" call while setting certain fields,
        /// because it does exactly the same thing and isn't as redudant.
        /// </remarks>
        /// 
        /// <returns>Whether or not the account was successfully created.</returns>
        public bool ApplyForSupplierAccount(Supplier supplier)
        {
            bool wasAdded = false;
            supplier.ApprovedBy = null;
            supplier.IsApproved = false;
            try
            {
                if (1 == SupplierAccessor.CreateNewSupplier(supplier))
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

        /// <summary>
        /// Christian Lopez
        /// 2017/04/27
        /// 
        /// Attempts to return a SupplierWithAgreements
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        public SupplierWithAgreements RetrieveSupplierWithAgreementsBySupplierId(int supplierId)
        {
            try
            {
                return SupplierAccessor.RetrieveSupplierWithAggreementsBySupplierId(supplierId);
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
        /// Christian Lopez
        /// 2017/04/27
        /// 
        /// Attempts to return a SupplierWithAgreements by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public SupplierWithAgreements RetrieveSupplierWithAgreementsByUserId(int userId)
        {
            try
            {
                return SupplierAccessor.RetrieveSupplierWithAggreementsByUserId(userId);
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
    }
}
