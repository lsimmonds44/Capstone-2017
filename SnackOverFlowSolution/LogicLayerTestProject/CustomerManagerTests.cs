using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataObjects;
using LogicLayer;
using System.Collections.Generic;

namespace LogicLayerTestProject
{
    /// <summary>
    /// Created by Daniel Brown
    /// 04/07/2017
    /// 
    /// Test cases intended to test the CustomerManager class methods
    /// </summary>
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

        [TestMethod]
        public void TestRetrieveCommercialCustomerByCorrectUserId()
        {
            //Arrange
            User user;
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

            var result = _customerMgr.RetrieveCommercialCustomerByUserId(testCustomer.UserId);

            //Assert
            Assert.IsTrue(result.UserId == testCustomer.UserId && result.IsApproved == false && result.FederalTaxId == 123456789 && result.Active == true && result.ApprovedBy == 99999);


            RemoveTestUserData();
            RemoveTestCommercialCustomerData(testCustomer);
        
        
        }

        [TestMethod]
        public void TestApproveCommercialCustomer()
        {
            //Arrange
            User user;
            bool isAffected;
            bool expectedIsAffected = true;
            bool expectedApprovedResult = true;
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

            testCustomer = _customerMgr.RetrieveCommercialCustomerByUserId(user.UserId);

            isAffected = _customerMgr.ApproveCommercialCustomer(testCustomer, 11111);

            testCustomer = _customerMgr.RetrieveCommercialCustomerByUserId(testCustomer.UserId);

            Assert.IsTrue(expectedApprovedResult == testCustomer.IsApproved && expectedIsAffected == isAffected);

            RemoveTestUserData();
            RemoveTestCommercialCustomerData(testCustomer);

        }

        [TestMethod]
        public void TestDenyCommercialCustomer()
        {
            //Arrange
            User user;
            bool isAffected;
            bool expectedIsAffected = true;
            bool expectedApprovedResult = false;
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

            testCustomer = new CommercialCustomer() { Active = true, IsApproved = true, FederalTaxId = 123456789, UserId = user.UserId, ApprovedBy = 99999 };

            _customerMgr.CreateCommercialAccount(testCustomer);

            testCustomer = _customerMgr.RetrieveCommercialCustomerByUserId(user.UserId);

            isAffected = _customerMgr.DenyCommercialCustomer(testCustomer, 11111);

            testCustomer = _customerMgr.RetrieveCommercialCustomerByUserId(testCustomer.UserId);

            Assert.IsTrue(expectedApprovedResult == testCustomer.IsApproved && expectedIsAffected == isAffected);

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
