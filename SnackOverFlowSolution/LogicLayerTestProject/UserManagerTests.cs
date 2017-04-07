using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicLayer;
using DataObjects;

namespace LogicLayerTestProject
{
    [TestClass]
    public class UserManagerTests
    {
        /// <summary>
        /// Created by Daniel Brown
        /// 03/31/2017
        /// 
        /// Test cases intended to test the UserManager methods
        /// </summary>
      
        private UserManager _userManager;
        private User _user;

        [TestInitialize]
        public void TestInitialize()
        {
            _userManager = new UserManager();
            _user = null;
        }

        [TestMethod]
        public void TestCreateNewUserWithCorrectData()
        {
            // arrange
                User user = new User()
                {
                    UserName = "Test",
                    FirstName = "Test",
                    LastName = "Test",
                    Phone = "0000000000",
                    EmailAddress = "test@test.com"
                };
            

            string password = "password";
            string password2 = "password";

            // act
            string result = _userManager.CreateNewUser(user, password, password2);


            // assert
            Assert.AreEqual("Created", result);
            RemoveTestUserData();
        }


        [TestMethod]
        public void TestCreateUserWithIncorrectShortUsername()
        {
            // arrange
            _user = new User()
            {
                UserName = "Tes",
                FirstName = "Test",
                LastName = "Test",
                Phone = "0000000000",
                EmailAddress = "test@test.com"
            };


            string password = "password";
            string password2 = "password";

            // act
            string result = _userManager.CreateNewUser(_user, password, password2);


            // assert
            Assert.AreEqual("Invalid Username", result);
            RemoveTestUserData();
        }

        [TestMethod]
        public void TestCreateUserWithIncorrectLongUsername()
        {
            // arrange
            _user = new User()
            {
                UserName = "TestTestTestTestTestT",
                FirstName = "Test",
                LastName = "Test",
                Phone = "0000000000",
                EmailAddress = "test@test.com"
            };


            string password = "password";
            string password2 = "password";

            // act
            string result = _userManager.CreateNewUser(_user, password, password2);


            // assert
            Assert.AreEqual("Invalid Username", result);
            RemoveTestUserData();
        }

        [TestMethod]
        public void TestCreateUserWithIncorrectUsedUsername()
        {
            // arrange
            _user = new User()
            {
                UserName = "ADMIN",
                FirstName = "Test",
                LastName = "Test",
                Phone = "0000000000",
                EmailAddress = "test@test.com"
            };


            string password = "password";
            string password2 = "password";

            // act
            string result = _userManager.CreateNewUser(_user, password, password2);


            // assert
            Assert.AreEqual("Used Username", result);
            RemoveTestUserData();
        }

        [TestMethod]
        public void TestCreateUserWithIncorrectShortPhone()
        {
            // arrange
            _user = new User()
            {
                UserName = "Test",
                FirstName = "Test",
                LastName = "Test",
                Phone = "000000000",
                EmailAddress = "test@test.com"
            };
            

            string password = "password";
            string password2 = "password";

            // act
            string result = _userManager.CreateNewUser(_user, password, password2);


            // assert
            Assert.AreEqual("Invalid Phone", result);
            RemoveTestUserData();
        }

        [TestMethod]
        public void TestCreateUserWithIncorrectLongPhone()
        {
            // arrange
            _user = new User()
            {
                UserName = "Test",
                FirstName = "Test",
                LastName = "Test",
                Phone = "00000000000",
                EmailAddress = "test@test.com"
            };


            string password = "password";
            string password2 = "password";

            // act
            string result = _userManager.CreateNewUser(_user, password, password2);


            // assert
            Assert.AreEqual("Invalid Phone", result);
            RemoveTestUserData();
        }

        [TestMethod]
        public void TestCreateUserWithIncorrectShortFirstname()
        {
            // arrange
            _user = new User()
            {
                UserName = "Test",
                FirstName = "T",
                LastName = "Test",
                Phone = "0000000000",
                EmailAddress = "test@test.com"
            };


            string password = "password";
            string password2 = "password";

            // act
            string result = _userManager.CreateNewUser(_user, password, password2);


            // assert
            Assert.AreEqual("Invalid FirstName", result);
            RemoveTestUserData();
        }

        [TestMethod]
        public void TestCreateUserWithIncorrectLongFirstname()
        {
            // arrange
            _user = new User()
            {
                UserName = "Test",
                FirstName = "123456789012345678921234567893123456789412345678951",
                LastName = "Test",
                Phone = "0000000000",
                EmailAddress = "test@test.com"
            };


            string password = "password";
            string password2 = "password";

            // act
            string result = _userManager.CreateNewUser(_user, password, password2);


            // assert
            Assert.AreEqual("Invalid FirstName", result);
            RemoveTestUserData();
        }

        [TestMethod]
        public void TestCreateUserWithIncorrectShortLastname()
        {
            // arrange
            _user = new User()
            {
                UserName = "Test",
                FirstName = "Test",
                LastName = "T",
                Phone = "0000000000",
                EmailAddress = "test@test.com"
            };


            string password = "password";
            string password2 = "password";

            // act
            string result = _userManager.CreateNewUser(_user, password, password2);


            // assert
            Assert.AreEqual("Invalid LastName", result);
            RemoveTestUserData();
        }

        [TestMethod]
        public void TestCreateUserWithIncorrectLongLastname()
        {
            // arrange
            _user = new User()
            {
                UserName = "Test",
                FirstName = "Test",
                LastName = "123456789012345678921234567893123456789412345678951",
                Phone = "0000000000",
                EmailAddress = "test@test.com"
            };


            string password = "password";
            string password2 = "password";

            // act
            string result = _userManager.CreateNewUser(_user, password, password2);


            // assert
            Assert.AreEqual("Invalid LastName", result);
            RemoveTestUserData();
        }

        [TestMethod]
        public void TestCreateUserWithIncorrectShortPassword()
        {
            // arrange
            _user = new User()
            {
                UserName = "Test",
                FirstName = "Test",
                LastName = "Test",
                Phone = "0000000000",
                EmailAddress = "test@test.com"
            };


            string password = "passwo";
            string password2 = "passwo";

            // act
            string result = _userManager.CreateNewUser(_user, password, password2);


            // assert
            Assert.AreEqual("Invalid Password", result);
            RemoveTestUserData();
        }

        [TestMethod]
        public void TestCreateUserWithIncorrectPasswordMismatch()
        {
            // arrange
            _user = new User()
            {
                UserName = "Test",
                FirstName = "Test",
                LastName = "Test",
                Phone = "0000000000",
                EmailAddress = "test@test.com"
            };


            string password = "password";
            string password2 = "passworrd";

            // act
            string result = _userManager.CreateNewUser(_user, password, password2);


            // assert
            Assert.AreEqual("Password No Match", result);
            RemoveTestUserData();
        }

        [TestMethod]
        public void TestCreateUserWithIncorrectExistingEmail()
        {
            // arrange
            _user = new User()
            {
                UserName = "Test",
                FirstName = "Test",
                LastName = "Test",
                Phone = "0000000000",
                EmailAddress = "masn@yahoo"
            };


            string password = "password";
            string password2 = "password";

            // act
            string result = _userManager.CreateNewUser(_user, password, password2);


            // assert
            Assert.AreEqual("Used Email", result);
            RemoveTestUserData();
        }



        [TestMethod]
        public void TestAuthenticateUserWithIncorrectUsername()
        {
            //Arrange
            string username = "AMDIN";
            string password = "ADMIN";

            //Act
            bool result = _userManager.AuthenticateUser(username, password);

            //Assert
            Assert.AreEqual(false, result);
            RemoveTestUserData();
        }

        [TestMethod]
        public void TestAuthenticateUserWithIncorrectPassword()
        {
            //Arrange
            string username = "ADMIN";
            string password = "AMDIN";

            //Act
            bool result = _userManager.AuthenticateUser(username, password);

            //Assert
            Assert.AreEqual(false, result);
            RemoveTestUserData();
        }

        [TestMethod]
        public void TestAuthenticateUserWithCorrectData()
        {
            //Arrange
            string username = "ADMIN";
            string password = "ADMIN";

            //Act
            bool result = _userManager.AuthenticateUser(username, password);

            //Assert
            Assert.AreEqual(true, result);
            RemoveTestUserData();
        }



        [TestMethod]
        public void TestRetrieveUserByIncorrectUsername()
        {
            //Arrange
            User testUser = null;


            //Act
            User result = _userManager.RetrieveUserByUserName("");


            //Assert
            Assert.AreEqual(testUser, result);

        }

        [TestMethod]
        public void TestRetrieveUserByCorrectUsername()
        {
            //Arrange
            User testUser = null;


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


            User result = _userManager.RetrieveUserByUserName("Test");

            
            //Assert
            Assert.IsTrue(result.UserName == "Test" && result.FirstName == "Test" && result.LastName == "Test" && result.Phone == "0000000000" && result.EmailAddress == "test@test.com");
            RemoveTestUserData();

        }

        
        [TestMethod]
        public void TestRetrieveFullUserList()
        {
            //Arrange
            List<User> result = null;

            
            //Add new user to the database
                
            User user = new User()
                {
                    UserName = "Test",
                    FirstName = "Test",
                    LastName = "Test",
                    Phone = "0000000000",
                    EmailAddress = "test@test.com"
                };

                string password = "password";
                string password2 = "password";

                try
                {
                    _userManager.CreateNewUser(user, password, password2);
                }
                catch (Exception)
                {

                }



            //Act
            result = _userManager.RetrieveFullUserList();

            //Assert
            Assert.IsTrue(result.Count > 0);
            RemoveTestUserData();

        }


        [TestMethod]
        public void TestRetrieveUsernameByIncorrectEmail()
        {
            
            // arrange
            string email = "email1352";
            string username = "";

            //act
            string result = _userManager.RetrieveUsernameByEmail(email);

            //Assert
            Assert.AreEqual(username, result);
        }

        [TestMethod]
        public void TestRetrieveUsernameByCorrectEmail()
        {
            
            // arrange
            string email = "test@test.com";
            string username = "Test";

            if (false == _userManager.AuthenticateUser("Test", "password"))
            {
                User user = new User()
                {
                    UserName = "Test",
                    FirstName = "Test",
                    LastName = "Test",
                    Phone = "0000000000",
                    EmailAddress = "test@test.com"
                };


                string password = "password";
                string password2 = "password";

                _userManager.CreateNewUser(user, password, password2);
            }
            
            //act
            string result = _userManager.RetrieveUsernameByEmail(email); 

            //Assert
            Assert.AreEqual(username, result);
            RemoveTestUserData();
        }


        [TestCleanup]
        public void TestCleanUp()
        {
            _userManager = null;
            _user = null;
            RemoveTestUserData();
        }

        private void RemoveTestUserData()
        {
            TestManager testMgr = new TestManager();
            testMgr.DeleteTestUser();
        }
    }
}
