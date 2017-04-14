using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
    public class TestUserManager : IUserManager
    {
        /// <summary>
        /// Christian Lopez
        /// Created on 2017/02/01
        /// 
        /// Return known values as a User
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>Known User</returns>
        public DataObjects.User RetrieveUserByUserName(string userName)
        {
            User newUser = null;

            if (userName.Equals("testUserName"))
            {
                newUser = new User()
                {
                    UserId = 10001,
                    FirstName = "Test",
                    LastName = "User",
                    Phone = "1234567890",
                    EmailAddress = "test@test.com",
                    EmailPreferences = true,
                    UserName = userName,
                    Active = true,
                    AddressLineOne = "818 45th St NE",
                    AddressLineTwo = "test address line two",
                    City = "Test New York",
                    State = "TT",
                    Zip = "66666"
                };
            }
            else
            {
                throw new ApplicationException("Unable to find user " + userName);
            }

            return newUser;
        }


        public string LogIn(string uName, string pass)
        {
            string result = "UserNotFound";
            if (uName == "testUserName" && pass == "password")
            {
                result = "Found";
            }
            return result;
        }

        public User userInstance
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool AuthenticateUser(string text, string password)
        {
            throw new NotImplementedException();
        }

        public List<User> RetrieveFullUserList()
        {
            throw new NotImplementedException();
        }

        public int ChangePassword(string userName, string oldPassword, string newPassword, string confirmPassword)
        {
            throw new NotImplementedException();
        }

        public List<string> roles
        {
            get { throw new NotImplementedException(); }
        }


        public string NewPassword()
        {
            throw new NotImplementedException();
        }

        public int ResetPassword(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public string RetrieveUsernameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Christian Lopez
        /// Created 2017/03/09
        /// 
        /// Test Method for returning a user by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User RetrieveUser(int userId)
        {
            User newUser = null;

            
                newUser = new User()
                {
                    UserId = userId,
                    FirstName = "Test",
                    LastName = "User",
                    Phone = "1234567890",
                    EmailAddress = "test@test.com",
                    EmailPreferences = true,
                    UserName = "test",
                    Active = true,
                    AddressLineOne = "818 45th St NE",
                    AddressLineTwo = "test address line two",
                    City = "Test New York",
                    State = "TT",
                    Zip = "66666"
                };
            
            
                throw new ApplicationException("Unable to find user " + userId);
            

            return newUser;
        }


        public User AuthenticateWebUser(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
