using DataAccessLayer;
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
        /// Updated 2017-03-22 by William Flood
        /// Refactored database call to a static method to resolve issue #22
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
            String salt = UserAccessor.RetrieveUserSalt(userName);
            String hash = HashSha256(password + salt);
            try
            {
                userInstance = UserAccessor.Login(userName, hash);
                if (null != userInstance)
                {
                    userFound = true;
                }
                else
                {
                    userFound = false;
                }
                return userFound;
            }
            catch
            {
                throw;
            }

        }


        /// <summary>
        /// William Flood
        /// 4/12/17
        /// Refactored database call to a static method to resolve issue #22
        /// 
        /// This returns true or false from the 1 or 0 that is recieved from 
        /// the UserAccessor layer.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User AuthenticateWebUser(string email, string password)
        {
            String foo = UserAccessor.RetrieveUserSaltByEmail(email);
            String bar = HashSha256(password + foo);
            try
            {
                return UserAccessor.WebLogin(email, bar);
            }
            catch
            {
                throw;
            }

        }

        public User RetrieveUserByUserName(string username)
        {
            User user = null;
            try
            {
                user = UserAccessor.RetrieveUserByUsername(username);
            }
            catch (Exception ex)
            {
                user = null;
                throw new ApplicationException("There was an error.", ex);
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
        /// Update
        /// Bobby Thorne
        /// 3/10/2017
        /// 
        /// This will test the Text Fields to make sure that
        /// bad data is not entered when creating a new user
        /// 
        /// Update
        /// added a catch for phone number and if username is
        /// already used
        /// 
        /// Updated 2017-03-22 by William Flood
        /// Refactored database call to a static method to resolve issue #22
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="confirmPassword"></param>
        /// <returns></returns>
        public string CreateNewUser(User user, string password, string confirmPassword)
        {

            if (user.UserName.Length > 20 || user.UserName.Length < 4)
            {
                return "Invalid Username";
            }
            try
            {
                if (RetrieveUserByUserName(user.UserName) != null)
                {
                    return "Used Username";
                }

            }
            catch { }
            if (user.FirstName.Length < 2 || user.FirstName.Length > 50)
            {
                return "Invalid FirstName";
            }
            if (user.LastName.Length < 2 || user.LastName.Length > 50)
            {
                return "Invalid LastName";
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
            if (!IsPhoneNumber(user.Phone))
            {
                return "Invalid Phone";
            }
            try
            {
                MailAddress m = new MailAddress(user.EmailAddress);
                string username = "";
                username = UserAccessor.RetrieveUsernameByEmail(user.EmailAddress);
                if (username != null)
                {
                    return "Used Email";
                }
            }
            catch
            {
                return "Invalid Email";
            }
            user.PasswordSalt = RandomString(32);
            user.PasswordHash = HashSha256(password + user.PasswordSalt);

            //if (!UserAccessor.UserNameCheck(user.UserName))
            //{
            //    //need to add sp_UserName_Check to see if another user has the same username
            //      return "Used Username"
            //}
            try
            {

                if (1 == UserAccessor.CreateUser(user))
                {
                    return "Created";
                }
            }
            catch
            {
                return "UnableToCreate";
            }

            return "UnableToCreate";

        }

        /// <summary>
        /// Created 2017-03-22 by William Flood
        /// </summary>
        /// <returns></returns>
        public List<User> RetrieveFullUserList()
        {
            try
            {
                var resultList = UserAccessor.RetrieveList();
                return resultList;
            }
            catch
            {
                throw;
            }
        }





        public string LogIn(string p1, string p2)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Created by William Flood
        /// 2017/03/02
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <param name="confirmPassword"></param>
        /// <returns></returns>
        public int ChangePassword(String userName, String oldPassword, String newPassword, String confirmPassword)
        {
            var returnValue = 0;
            if (newPassword.Equals(confirmPassword))
            {

                String oldSalt = UserAccessor.RetrieveUserSalt(userName);
                String oldHash = HashSha256(oldPassword + oldSalt);
                String newSalt = RandomString(32);
                String newHash = HashSha256(newPassword + newSalt);
                try
                {
                    returnValue = UserAccessor.UpdatePassword(userName, oldSalt, oldHash, newSalt, newHash);
                }
                catch
                {
                    throw;
                }
            }
            else
            {
                returnValue = 0;
            }
            return returnValue;
        }

        public String NewPassword()
        {
            return RandomString(5);
        }




        public int ResetPassword(string userName, string password)
        {
            int results = 0;
            String foo = RandomString(32);
            String bar = HashSha256(password + foo);
            try
            {
                results = UserAccessor.ResetPassword(userName, foo, bar);
            }
            catch
            {
                throw;
            }
            return results;
        }

        /// <summary>
        /// Bobby Thorne
        /// 3/4/2017
        /// 
        /// Retrieves username from the user accessor
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public string RetrieveUsernameByEmail(string email)
        {

            return UserAccessor.RetrieveUsernameByEmail(email);
        }

        /// <summary>
        /// Christian Lopez
        /// 2017/03/09
        /// 
        /// Tries to get a user from the Accessor by the userId.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User RetrieveUser(int userId)
        {
            User user = null;
            try
            {
                user = UserAccessor.RetrieveUser(userId);
            }
            catch (SqlException ex)
            {

                throw new ApplicationException("There was a database error.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an unknown error.", ex);
            }
            return user;
        }

        private static bool IsPhoneNumber(string number)
        {

            try
            {
                Int64.Parse(number);
            }
            catch (Exception ex)
            {
                return false;
            }
            if (number.Length == 10) { return true; }
            else { return false; }
        }

        /// <summary>
        /// Christian Lopez
        /// 2017/04/14
        /// 
        /// sees what tables the userId shows up in, and sets
        /// a bool[] in the order of customer, employee, supplier
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool[] GetUserRoles(int userId)
        {
            bool[] roles = new bool[3];
            CommercialCustomer cust = null;
            Employee emp = null;
            Supplier supp = null;
            try
            {
                cust = CustomerAccessor.RetrieveCommercialCustomerByUserId(userId);
                emp = EmployeeAccessor.RetrieveEmployeeByUserId(userId);
                supp = SupplierAccessor.RetrieveSupplierByUserId(userId);
            }
            catch (Exception)
            {
                
                throw;
            }

            roles[0] = (cust != null && cust.IsApproved && cust.Active);
            roles[1] = (emp != null && (bool)emp.Active);
            roles[2] = (supp != null && supp.Active && supp.IsApproved);

            return roles;
        }
    }
}
