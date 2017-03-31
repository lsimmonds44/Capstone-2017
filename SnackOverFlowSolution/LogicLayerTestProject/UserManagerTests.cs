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
        private UserManager _userManager;

        [TestInitialize]
        public void TestInitialize()
        {
            _userManager = new UserManager();
        }

        [TestMethod]
        public void TestCreateUserWithCorrectData()
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
        }

        [TestMethod]
        public void TestAuthenticateUser()
        {
            //Arrange
            string username = "ADMIN";
            string password = "ADMIN";

            //Act
            bool result = _userManager.AuthenticateUser(username, password);

            //Assert
            Assert.AreEqual(true, result);
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            _userManager = null;
        }


    }
}
