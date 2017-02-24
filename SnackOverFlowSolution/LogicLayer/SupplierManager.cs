using System;
using DataAccessLayer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

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
        public bool CreateNewSupplier(int userId, bool isApproved, int approvedBy, string farmName,
            string farmCity, string farmState, string farmTaxId)
        {
            bool wasAdded = false;

            try
            {
                if (1 == SupplierAccessor.CreateNewSupplier(userId, isApproved, approvedBy, farmName,
                    farmCity, farmState, farmTaxId))
                {
                    wasAdded = true;
                }
            }
            catch (Exception)
            {

                throw;
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
            catch (Exception)
            {

                throw;
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
            catch (Exception)
            {

                throw;
            }

            if (null == s)
            {
                throw new ApplicationException("Could not find supplier for that supplier ID.");
            }

            return s;
        }


        public string RetrieveSupplierName(int userId)
        {
            string name = null;

            try
            {
                name = SupplierAccessor.RetrieveSupplierName(userId);
            }
            catch (Exception)
            {

                throw;
            }

            return name;
        }
    }
}
