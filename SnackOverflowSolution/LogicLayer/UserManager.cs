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
        /// 2/11/17
        /// 
        /// This returns true or false from the 1 or 0 that is recieved from 
        /// the UserAccessor layer.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool AuthenticateUser(string username, string password)
        {
            string passwordHash = HashSha256(password);
            if (UserAccessor.AuthenticateUser(username, passwordHash) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static String HashSha256(string source)
        {
            byte[] data;
            string result = "";
            using (SHA256 sha1Hash = SHA256.Create())
            {
                data = sha1Hash.ComputeHash(Encoding.UTF8.GetBytes(source));
            }
            var s = new StringBuilder();
            foreach (byte stringByte in data)
            {
                s.Append(stringByte.ToString("x2"));
            }
            result = s.ToString();
            return result;
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
           
            if(user.FirstName==null || user.FirstName.Equals("")||user.FirstName.Equals(" "))
            {
                return "Invalid First";
            }
            if (user.LastName == null || user.LastName.Equals("") || user.LastName.Equals(" "))
            {
                return "Invalid Last";
            }
            if (user.Phone.Length!=10)
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

            //if (!UserAccessor.UserNameCheck(user.UserName))
            //{
            //    //need to add sp_UserName_Check to see if another user has the same username
            //      return "Used Username"
            //}
            try
            {
                if (userAccessor.CreateNewUser(user, HashSha256(password)))
                {
                    return "Created";
                }
            }catch(Exception ex)
            {
                return "Used Username. Error: " + ex.Message;
            }
            return "UnableToCreate";
        }

        public string LogIn(String userName, string password)
        {

            if (userName.Length > 20 || userName.Length < 4)
            {
                return "Invalid Username";
            }
            if (password.Length < 7)
            {
                //May check more advanced complexity rules later
                return "Invalid Password";
            }
            var hashedPassword = HashSha256(password);
            password = null;
            try
            {
                int result = UserAccessor.AuthenticateUser(userName, hashedPassword);

                if (0 == result)
                {
                    return "UserNotFound";
                }
                else
                {

                }
                return "Found";
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
        public UserAddress RetrieveUserAddress(int? prefferedAddressId)
        {

            UserAddress userAddress = null;

            try
            {
                userAddress = UserAccessor.RetrieveUserAddress(prefferedAddressId);
            }
            catch (Exception)
            {

                throw new ApplicationException("Unable to access Data");
            }

            return userAddress;
        }
    }
}
