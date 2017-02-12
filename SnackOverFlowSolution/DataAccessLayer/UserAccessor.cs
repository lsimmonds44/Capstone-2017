using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class UserAccessor
    {

        /// <summary>
        /// Bobby Thorne 
        /// 2/11/2017
        /// 
        /// This receives the username and password has and checks for a match
        /// in the database and returns the number of fields that it matches 
        /// with which should be 1 or 0
        /// </summary>
        /// <param name="username"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public static int AuthenticateUser(string username, string passwordHash)
        {
            int results = 0;
            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_authenticate_user";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@USER_NAME", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@PASSWORD_HASH", SqlDbType.NVarChar, 64);
            cmd.Parameters["@USER_NAME"].Value = username;
            cmd.Parameters["@PASSWORD_HASH"].Value = passwordHash;

            try
            {
                conn.Open();
                results = (int)cmd.ExecuteScalar();
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return results;
        }

        /// <summary>
        /// Bobby Thorne 
        /// 2/11/17
        /// 
        /// This accesses the store procedure sp_retrieve_user_by_username
        /// and returns the user info. It will need to change when we
        /// decide to use preferredAddressId since it was returning null
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static User RetrieveUserByUsername(string username)
        {
            User user = null;
            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_user_by_username";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.Parameters.Add("@USER_NAME", SqlDbType.NVarChar, 50);
            cmd.Parameters["@USER_NAME"].Value = username;
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    user = new User()
                    {
                        UserId = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Phone = reader.GetString(3),
                        PreferredAddressId = 1, //reader.GetInt32(4),
                        EmailAddress = reader.GetString(5),
                        EmailPreferences = reader.GetBoolean(6),
                        UserName = reader.GetString(7),
                        Active = reader.GetBoolean(8)
                    };
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return user;
        }

        public static bool UserNameCheck(string userName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Bobby Thorne
        /// 2/11/17
        /// 
        /// Creates a new user when a user first uses the app
        /// </summary>
        /// <param name="user"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public bool CreateNewUser(User user,string passwordHash)
        {
            var result = false;
            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_create_app_user";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FIRST_NAME", SqlDbType.NVarChar,150);
            cmd.Parameters.Add("@LAST_NAME", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@PHONE", SqlDbType.NVarChar, 15);
            cmd.Parameters.Add("@E_MAIL_ADDRESS", SqlDbType.NVarChar, 15);
            cmd.Parameters.Add("@E_MAIL_PREFERENCES", SqlDbType.Bit);
            cmd.Parameters.Add("@PASSWORD_HASH", SqlDbType.NVarChar, 64);
            cmd.Parameters.Add("@PASSWORD_SALT", SqlDbType.NVarChar,64);
            cmd.Parameters.Add("@USER_NAME", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@ACTIVE", SqlDbType.Bit);
            // values
            cmd.Parameters["@FIRST_NAME"].Value = user.FirstName;
            cmd.Parameters["@LAST_NAME"].Value = user.LastName;
            cmd.Parameters["@PHONE"].Value = user.Phone;
            cmd.Parameters["@E_MAIL_ADDRESS"].Value = user.EmailAddress;
            cmd.Parameters["@E_MAIL_PREFERENCES"].Value = user.EmailPreferences;
            cmd.Parameters["@PASSWORD_HASH"].Value = passwordHash;
            cmd.Parameters["@PASSWORD_SALT"].Value = "abc";
            cmd.Parameters["@USER_NAME"].Value = user.UserName;
            cmd.Parameters["@Active"].Value = user.Active;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                result = true;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error: " + ex);
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
    }
}