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
        CustomerAccessor _customerAccessor = new CustomerAccessor();
        public bool CreateCommercialAccount(CommercialCustomer cc)
        {
            bool result = false;
            try
            {
                result = _customerAccessor.CreateCommercialCustomer(cc);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error: " + ex);
            }
            return result;
        }

    } // end of class
} // end of namespace
