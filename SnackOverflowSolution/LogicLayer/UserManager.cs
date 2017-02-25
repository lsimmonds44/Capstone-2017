﻿using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class UserManager : IUserManager
    {
        public User userInstance { get; set; }
        public static String HashSha256(string source)
        {
            byte[] data;
            string result = "";
            using (SHA256 sha256Hash = SHA256.Create())
            {
                data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(source));
            }
            var s = new StringBuilder();
            foreach (byte stringByte in data)
            {
                s.Append(stringByte.ToString("x2"));
            }
            result = s.ToString();
            return result;
        }

        public static String RandomString(int size)
        {
            byte[] data = new byte[size];
            string result = "";
            Random rng = new System.Random();
            rng.NextBytes(data);
            var s = new StringBuilder();
            foreach (byte stringByte in data)
            {
                s.Append(stringByte.ToString("x2"));
            }
            result = s.ToString();
            return result;
        }

        public List<String> roles
        {
            get
            {
                var returnList = new List<String>();
                /*foreach(AppUserUserRole roleAssignment in currentUser.APP_USER_USER_ROLE_List)
                {
                    returnList.Add(roleAssignment.USER_ROLE_ID);
                }*/
                returnList.Add("All");
                return returnList;


            }
        }

        /// <summary>
        /// Bobby Thorne
        /// 2/11/17
        /// William Flood
        /// 2/12/17
        /// Salts the password before hashing
        /// 
        /// This returns true or false from the 1 or 0 that is recieved from 
        /// the UserAccessor layer.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool AuthenticateUser(string userName, string password)
        {
            bool userFound;
            UserAccessor accessor = new UserAccessor();
            String foo = accessor.RetrieveUserSalt(userName);
            String bar = HashSha256(password + foo);
            accessor.UserInstance = null;
            if (accessor.Login(userName, bar))
            {
                userInstance = accessor.UserInstance;
                userFound = true;
            }
            else
            {
                userFound = false;
            }
            return userFound;

        }

        public User RetrieveUserByUserName(string username)
        {
            User user = null;
            try
            {
                user = UserAccessor.RetrieveUserByUsername(username);
            }
            catch
            {
                user = null;
                throw;
            }
            return user;
        }

        public Employee editEmployee(int oldEmployee_ID, int oldUser_ID, decimal oldSalary, bool oldActive, DateTime oldDate_Of_Birth)
        {
            Employee employeeEdit = new Employee()

            {
                EmployeeId = oldEmployee_ID,
                UserId = oldUser_ID,
                Salary = oldSalary,
                Active = oldActive,
                DateOfBirth = oldDate_Of_Birth

            };
            return employeeEdit;
        }

        /// <summary>
        /// Bobby Thorne
        /// 2/12/17
        /// 
        /// This will test the Text Fields to make sure that
        /// bad data is not entered when creating a new user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="confirmPassword"></param>
        /// <returns></returns>
        public string CreateNewUser(User user, string password, string confirmPassword)
        {
            UserAccessor userAccessor = new UserAccessor();
            if (user.UserName.Length > 20 || user.UserName.Length < 4)
            {
                return "Invalid Username";
            }
            if (password.Length < 7)
            {
                return "Invalid Password";
            }
            if (password != confirmPassword)
            {
                return "Password No Match";
            }

            if (user.FirstName == null || user.FirstName.Equals("") || user.FirstName.Equals(" "))
            {
                return "Invalid First";
            }
            if (user.LastName == null || user.LastName.Equals("") || user.LastName.Equals(" "))
            {
                return "Invalid Last";
            }
            if (user.Phone.Length > 15)
            {
                return "Invalid Phone";
            }
            try
            {
                MailAddress m = new MailAddress(user.EmailAddress);
            }
            catch
            {
                return "Invalid Email";
            }
            user.PasswordSalt = RandomString(32);
            user.PasswordHash = HashSha256(password + user.PasswordSalt);
            UserAccessor accessor = new UserAccessor();
            accessor.UserInstance = user;

            //if (!UserAccessor.UserNameCheck(user.UserName))
            //{
            //    //need to add sp_UserName_Check to see if another user has the same username
            //      return "Used Username"
            //}
            try
            {

                if (1 == DatabaseMainAccessor.Create(accessor))
                {
                    return "Created";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "UnableToCreate";
        }


        public List<User> RetrieveFullUserList()
        {
            var accessor = new UserAccessor();
            try
            {
                DatabaseMainAccessor.RetrieveList(accessor);
                return accessor.UserList;
            }
            catch
            {
                throw;
            }
        }




        /// <summary>
        /// Christian Lopez
        /// Created on 2017/02/01
        /// 
        /// Retrieve the corresponding User Address
        /// </summary>
        /// <param name="prefferedAddressId"></param>
        /// <returns></returns>
        /// <remarks>Last modified by Christian Lopez 2017/02/25</remarks>
        public UserAddress RetrieveUserAddress(int? prefferedAddressId)
        {

            UserAddress userAddress = null;
            if (prefferedAddressId != null)
            {
                try
                {
                    userAddress = UserAccessor.RetrieveUserAddress(prefferedAddressId);
                }
                catch (Exception)
                {

                    throw new ApplicationException("Unable to access Data");
                }

            }
            return userAddress;
        }


        public string LogIn(string p1, string p2)
        {
            throw new NotImplementedException();
        }
    }
}