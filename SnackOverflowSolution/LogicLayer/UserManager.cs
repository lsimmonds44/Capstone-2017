using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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

        public String LogIn(String userName, string password)
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

    }
}
