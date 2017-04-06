using System;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataObjects;
using LogicLayer;

namespace LogicLayerTestProject
{
    [TestClass]
    public class EmployeeManagerTests
    {

        /// <summary>
        /// Created by Daniel Brown
        /// 03/31/2017
        /// 
        /// Test cases intended to test the EmployeeManager methods
        /// </summary>

        private EmployeeManager _employeeMgr;
        private UserManager _userManager;

        [TestInitialize]
        public void TestInitialize()
        {
            _employeeMgr = new EmployeeManager();
            _userManager = new UserManager();
            
            RemoveTestEmployeeData();
            RemoveTestUserData();
        }

        [TestMethod]
        public void TestCreateEmployeeWithCorrectData()
        {
            //Arrange
            User testUser;

            Employee testEmployee; 

            //Act
            testUser = new User()
            {
                UserName = "Test",
                FirstName = "Test",
                LastName = "Test",
                Phone = "0000000000",
                EmailAddress = "test@test.com"
            };

            string password = "password";
            string password2 = "password";
            _userManager.CreateNewUser(testUser, password, password2);

            testUser = _userManager.RetrieveUserByUserName("Test");
            testEmployee = new Employee() { Active = true, DateOfBirth = new DateTime(1000,01,01), Salary = 100, UserId = testUser.UserId };

            int expectedRowsAffected = 1;
            int actualRowsAffected = _employeeMgr.CreateEmployee(testEmployee);

            //Assert
            Assert.AreEqual(expectedRowsAffected, actualRowsAffected);
            RemoveTestEmployeeData();
            RemoveTestUserData();

        }

        [TestMethod]
        [ExpectedException(typeof(SqlException))]
        public void TestCreateEmployeeWithIncorrectUserID()
        {
            //Arrange

            int invalidUserID = 9995682;
            Employee testEmployee;

            //Act

            testEmployee = new Employee() { Active = true, DateOfBirth = DateTime.Now, Salary = 100, UserId = invalidUserID };

            //Assert
            _employeeMgr.CreateEmployee(testEmployee);


        }

        [TestMethod]
        public void TestCreateEmployeeWithNullDateOfBirth()
        {
            //Arrange
            User testUser;

            Employee testEmployee;

            //Act
            testUser = new User()
            {
                UserName = "Test",
                FirstName = "Test",
                LastName = "Test",
                Phone = "0000000000",
                EmailAddress = "test@test.com"
            };

            string password = "password";
            string password2 = "password";
            _userManager.CreateNewUser(testUser, password, password2);

            testUser = _userManager.RetrieveUserByUserName("Test");
            testEmployee = new Employee() { Active = true, DateOfBirth = null, Salary = 100, UserId = testUser.UserId };

            try
            {
                _employeeMgr.CreateEmployee(testEmployee);
                Assert.Fail("Allowed to add an employee with a null DOB");
            }catch(SqlException){
                
            }
            
            RemoveTestUserData();

        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestCreateEmployeeWithNullEmployee()
        {

            //Arrange
            Employee testEmployee;

            //Act
            testEmployee = null;
            _employeeMgr.CreateEmployee(testEmployee);

        }



        [TestMethod]
        public void TestRetrieveEmployeeWithIncorrectUserName()
        {
            //Arrange
            Employee result;
            Employee expectedResult = null;


            //Act

            result = _employeeMgr.RetrieveEmployeeByUserName("ThisIsNotAValidEmployeeUserName");

            //Assert
            Assert.AreEqual(expectedResult, result);


        }

        [TestMethod]
        public void TestRetrieveEmployeeWithCorrectUserName()
        {
            //Arrange
            User testUser;
            Employee testEmployee;
            Employee result;
            


            //Act
            testUser = new User()
            {
                UserName = "Test",
                FirstName = "Test",
                LastName = "Test",
                Phone = "0000000000",
                EmailAddress = "test@test.com"
            };

            string password = "password";
            string password2 = "password";
            _userManager.CreateNewUser(testUser, password, password2);

            testUser = _userManager.RetrieveUserByUserName("Test");
            testEmployee = new Employee() { Active = true, DateOfBirth = new DateTime(1000,01,01), Salary = 100, UserId = testUser.UserId };

            _employeeMgr.CreateEmployee(testEmployee);
            result = _employeeMgr.RetrieveEmployeeByUserName("Test");

            //Assert
            RemoveTestEmployeeData();
            RemoveTestUserData();
            Assert.IsTrue(result.UserId == testUser.UserId && result.Active == true && result.DateOfBirth == new DateTime(1000,01,01) && result.Salary == 100 && result.EmployeeId != null);

        }

        [TestMethod]
        public void TestRetrieveEmployeeWithIncorrectEmployeeID()
        {
            //Arrange
            Employee result;
            Employee expectedResult = null;
            int incorrectEmployeeID = 9999999;


            //Act
            result = _employeeMgr.RetrieveEmployee(incorrectEmployeeID);
            Assert.AreEqual(expectedResult, result);

        }

        [TestMethod]
        public void TestRetrieveEmployeeWithCorrectEmployeeID()
        {
            //Arrange
            User testUser;
            Employee testEmployee;
            Employee expectedResult;
            Employee result;



            //Act
            testUser = new User()
            {
                UserName = "Test",
                FirstName = "Test",
                LastName = "Test",
                Phone = "0000000000",
                EmailAddress = "test@test.com"
            };

            string password = "password";
            string password2 = "password";
            _userManager.CreateNewUser(testUser, password, password2);

            testUser = _userManager.RetrieveUserByUserName("Test");
            testEmployee = new Employee() { Active = true, DateOfBirth = new DateTime(1000, 01, 01), Salary = 100, UserId = testUser.UserId };

            _employeeMgr.CreateEmployee(testEmployee);
            expectedResult = _employeeMgr.RetrieveEmployeeByUserName("Test");
            result = _employeeMgr.RetrieveEmployee((int)expectedResult.EmployeeId);

            //Assert
            RemoveTestEmployeeData();
            RemoveTestUserData();
            Assert.IsTrue(result.Active == true && result.DateOfBirth == new DateTime(1000, 01, 01) && result.Salary == 100 && result.EmployeeId == expectedResult.EmployeeId && result.UserId == expectedResult.UserId);
        }





        [TestCleanup]
        public void TestCleanup()
        {
            _userManager = null;
            _employeeMgr = null;
            try
            {
                RemoveTestEmployeeData();
                RemoveTestUserData();
            }catch{

            }
            
        }

        /// <summary>
        /// Removes the "Test" user from the database
        /// </summary>
        private void RemoveTestUserData()
        {
            TestManager testMgr = new TestManager();
            testMgr.DeleteTestUser();
            testMgr = null;
        }

        /// <summary>
        /// Removes the employee data associated with the "Test" user from the database
        /// Must be run prior to the RemoveTestUserData otherwise the user will be removed but not the associated employee link
        /// </summary>
        /// <param name="userID"></param>
        public void RemoveTestEmployeeData()
        {
            TestManager testMgr = new TestManager();
            testMgr.DeleteTestEmployee();
            testMgr = null;
        }

    }
}
