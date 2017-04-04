using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class CustomerManager : ICustomerManager
    {
       
        /// <summary>
        /// Eric Walton
        /// 2017/06/02
        /// 
        /// Create Commercial Account method
        /// Trys to create a commercial account 
        /// If successful it returns true 
        /// If unsuccessful it returns false
        /// </summary>
        /// <param name="commercialCustomer"></param>
        /// <returns></returns>
        public bool CreateCommercialAccount(CommercialCustomer commercialCustomer)
        {
            bool result = false;
            try
            {
                result = 1 == CustomerAccessor.CreateCommercialCustomer(commercialCustomer);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Eric Walton
        /// 2017/26/02
        /// Retrieves a list of all commercial customers
        /// If succesful returns list
        /// If unsuccesful throws error
        /// </summary>
        /// <returns></returns>
        public List<CommercialCustomer> RetrieveCommercialCustomers()
        {
            List<CommercialCustomer> commercialCustomers = null;
            try
            {
                commercialCustomers = CustomerAccessor.RetrieveAllCommercialCustomers();
            }
            catch (Exception)
            {
                throw;
            }


            return commercialCustomers;
        }

        /// <summary>
        /// Bobby Thorne
        /// 3/24/2017
        /// 
        /// Retrieves Commercial customer instance by userid
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommercialCustomer RetrieveCommercialCustomerByUserId(int userId)
        {
            CommercialCustomer commercialCustomer = null;

            try
            {
                commercialCustomer = CustomerAccessor.RetrieveCommercialCustomerByUserId(userId);
            }
            catch (Exception)
            {

                throw;
            }

            if (null == commercialCustomer)
            {
                throw new ApplicationException("Could not find customer for that user ID.");
            }

            return commercialCustomer;
        }
    } // end of class
} // end of namespace
