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
        /// <param name="cc"></param>
        /// <returns></returns>
        public bool CreateCommercialAccount(CommercialCustomer cc)
        {
            CustomerAccessor _customerAccessor = new CustomerAccessor();
            bool result = false;
            try
            {
                result = _customerAccessor.CreateCommercialCustomer(cc);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

    } // end of class
} // end of namespace
