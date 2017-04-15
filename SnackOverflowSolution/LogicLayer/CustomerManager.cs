using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        /// <summary>
        /// Bobby Thorne
        /// 4/7/2017
        /// 
        /// Calls the accessor method to approve Commercial Customers and updates who made the change
        /// </summary>
        /// <param name="commercialCustomer"></param>
        /// <param name="approvedBy"></param>
        /// <returns></returns>
        public bool ApproveCommercialCustomer(CommercialCustomer commercialCustomer, int approvedby)
        {

            try
            {
                if (CustomerAccessor.ApproveCommercialCustomer(commercialCustomer, approvedby) > 0)
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
        /// Bobby Thorne
        /// 4/7/2017
        /// 
        /// Calls the accessor method to denie Commercial Customers and updates who made the change
        /// </summary>
        /// <param name="commercialCustomer"></param>
        /// <param name="approvedBy"></param>
        /// <returns></returns>
        public bool DenyCommercialCustomer(CommercialCustomer commercialCustomer, int approvedby)
        {
            try
            {
                if (CustomerAccessor.DenyCommercialCustomer(commercialCustomer, approvedby) > 0)
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
        /// Created by Michael Takrama
        /// 04/15/17
        /// 
        /// Logic to Apply for Commercial Account - MVC
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool ApplyForCommercialAccount(RegisterCommercialViewModel user)
        {
            try
            {
                var um = new UserManager();

                var createdUserResult = um.CreateNewUser(user, user.Password, user.ConfirmPassword);

                if ("Created" != createdUserResult)
                    throw new ApplicationException("ApplyForCommercialAccount" + createdUserResult);
                
                var userData = um.RetrieveUserByUserName(user.UserName);

                var cm = new CommercialCustomer
                {
                    UserId = userData.UserId,
                    FederalTaxId = int.Parse( user.FederalTaxID),  // lossy conversion probable
                    ApprovedBy =  10000, //lower level accessor fix required
                    Active = false
                };

                if (CreateCommercialAccount(cm))
                    return true;

                //creation failed - delete created user.    
    
            }
            catch (Exception ex)
            {
                Debug.WriteLine("CustomerManager-ApplyForCommercialAccount: " + ex.Message);
                throw;
            }

            return false;
        }
    } // end of class
} // end of namespace
