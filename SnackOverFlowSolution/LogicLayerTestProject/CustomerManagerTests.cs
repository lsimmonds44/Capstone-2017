using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataObjects;
using LogicLayer;
using System.Collections.Generic;

namespace LogicLayerTestProject
{
    [TestClass]
    public class CustomerManagerTests
    {
        CustomerManager _customerMgr;

        [TestInitialize]
        public void Initialize()
        {
            _customerMgr = new CustomerManager();
        }

        [TestMethod]
        public void TestCreateCommercialAccount()
        {
            

            //Arrange
            User user;
            CommercialCustomer testCustomer;
            bool expected = true;
            UserManager userMgr = new UserManager();


            string password = "password";
            string password2 = "password";


            //Act
            user = new User()
            {
                UserName = "Test",
                FirstName = "Test",
                LastName = "Test",
                Phone = "0000000000",
                EmailAddress = "test@test.com"
            };

            userMgr.CreateNewUser(user, password, password2);
            user = userMgr.RetrieveUserByUserName("Test");

            testCustomer = new CommercialCustomer() { Active = true, IsApproved = false, FederalTaxId = 123456789, UserId= user.UserId, ApprovedBy = 99999};

            bool result = _customerMgr.CreateCommercialAccount(testCustomer);

            //Assert
            Assert.AreEqual(expected, result);
            RemoveTestUserData();
            RemoveTestCommercialCustomerData(testCustomer);
        }

        [TestMethod]
        public void TestRetrieveCommercialCustomers()
        {
            //Arrange
            User user;
            List<CommercialCustomer> result = new List<CommercialCustomer>();
            CommercialCustomer testCustomer;
            UserManager userMgr = new UserManager();


            string password = "password";
            string password2 = "password";


            //Act
            user = new User()
            {
                UserName = "Test",
                FirstName = "Test",
                LastName = "Test",
                Phone = "0000000000",
                EmailAddress = "test@test.com"
            };

            userMgr.CreateNewUser(user, password, password2);
            user = userMgr.RetrieveUserByUserName("Test");

            testCustomer = new CommercialCustomer() { Active = true, IsApproved = false, FederalTaxId = 123456789, UserId = user.UserId, ApprovedBy = 99999 };

            _customerMgr.CreateCommercialAccount(testCustomer);

            result = _customerMgr.RetrieveCommercialCustomers();

            //Assert
            Assert.IsTrue(result.Count > 0);
            RemoveTestUserData();
            RemoveTestCommercialCustomerData(testCustomer);


        }



        [TestCleanup]
        public void TestCleanUp()
        {
            RemoveTestUserData();
        }

        private void RemoveTestUserData()
        {
            TestManager testMgr = new TestManager();
            testMgr.DeleteTestUser();
        }

        private void RemoveTestCommercialCustomerData(CommercialCustomer customer)
        {
            TestManager testMgr = new TestManager();
            testMgr.DeleteTestCommercialCustomer(customer);
        }

    }
}
