﻿using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        /// 
        /// Update 
        /// Bobby Thorne
        /// 5/7/2017
        /// Adds an ApprovedByName to the CommercailCustomer
        /// To fill the datagrid.
        /// </summary>
        /// <returns></returns>
        public List<CommercialCustomer> RetrieveCommercialCustomers()
        {
            List<CommercialCustomer> commercialCustomers = null;
            try
            {
                UserManager userManager = new UserManager();
                EmployeeManager employeeManager = new EmployeeManager();
                commercialCustomers = CustomerAccessor.RetrieveAllCommercialCustomers();
                foreach (CommercialCustomer e in commercialCustomers)
                {
                    e.name = userManager.RetrieveUser(e.UserId).LastName + ", " + userManager.RetrieveUser(e.UserId).FirstName;
                    if (e.ApprovedBy != null)
                    {
                        var approvalUser = userManager.RetrieveUser(e.ApprovedBy);
                        if(approvalUser.FirstName.Equals("") && approvalUser.LastName.Equals(""))
                        {
                            e.ApprovedByName = "";
                        }
                        else
                        {
                            e.ApprovedByName = approvalUser.LastName + ", " + approvalUser.FirstName;
                        }
                    }
                }
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
            // App User Creation
            var userManager = new UserManager();
            try
            {

                var createdUserResult = userManager.CreateNewUser(user, user.Password, user.ConfirmPassword);

                if ("Created" != createdUserResult)
                    throw new ApplicationException(createdUserResult);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("CustomerManager-ApplyForCommercialAccount: " + ex.Message);
                throw;
            }

            // Commercial Customer Creation
            User userData = null;
            try
            {
                userData = userManager.RetrieveUserByUserName(user.UserName);

                var cm = new CommercialCustomer
                {
                    UserId = userData.UserId,
                    FederalTaxId = int.Parse(user.FederalTaxID),
                    ApprovedBy = 0,
                    Active = true
                };

                if (CreateCommercialAccount(cm))
                    return true;

                //Commercial Customer Creation failed - delete created user
                if (1 == UserAccessor.DeleteUser(userData.UserId))
                    throw new ApplicationException("Fatal Error Occured");

            }
            catch (SqlException ex)
            {
                if (userData != null)
                    UserAccessor.DeleteUser(userData.UserId);

                Debug.WriteLine(ex.Message);

                throw new ApplicationException("Fatal Error Occured");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }

            return false;
        }
    } // end of class
} // end of namespace
