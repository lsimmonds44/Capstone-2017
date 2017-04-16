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
        /// Aaron Usher
        /// Updated: 2017/04/14
        /// 
        /// Retrieves a list of all users from the database.
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/14
        /// 
        /// Standardized method and added address fields.
        /// </remarks>
        /// 
        /// <returns>List of all users.</returns>
        public static List<User> RetrieveList()
        {
            var users = new List<User>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_app_user_list";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        users.Add(new User()
                        {
                            UserId = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.IsDBNull(2) ? null : reader.GetString(2),
                            Phone = reader.GetString(3),
                            EmailAddress = reader.GetString(4),
                            EmailPreferences = reader.GetBoolean(5),
                            UserName = reader.GetString(6),
                            Active = reader.GetBoolean(7),
                            AddressLineOne = reader.IsDBNull(8) ? null : reader.GetString(8),
                            AddressLineTwo = reader.IsDBNull(9) ? null : reader.GetString(9),
                            City = reader.IsDBNull(10) ? null : reader.GetString(10),
                            State = reader.IsDBNull(11) ? null : reader.GetString(11),
                            Zip = reader.IsDBNull(12) ? null : reader.GetString(12)
                        });
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return users;
        }



        /// <summary>
        /// Christian Lopez
        /// Created: 2017/02/01
        /// 
        /// Access DB to get User by given username
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/14
        /// 
        /// Standardized method and added address fields.
        /// </remarks>
        /// <param name="username">The username to search on.</param>
        /// <returns>The user with the username.</returns>
        public static User RetrieveUserByUsername(string username)
        {
            User user = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_app_user_by_username";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@USERNAME", username);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    user = new User()
                    {
                        UserId = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.IsDBNull(2) ? null : reader.GetString(2),
                        Phone = reader.GetString(3),
                        EmailAddress = reader.GetString(4),
                        EmailPreferences = reader.GetBoolean(5),
                        UserName = reader.GetString(6),
                        Active = reader.GetBoolean(7),
                        AddressLineOne = reader.IsDBNull(8) ? null : reader.GetString(8),
                        AddressLineTwo = reader.IsDBNull(9) ? null : reader.GetString(9),
                        City = reader.IsDBNull(10) ? null : reader.GetString(10),
                        State = reader.IsDBNull(11) ? null : reader.GetString(11),
                        Zip = reader.IsDBNull(12) ? null : reader.GetString(12)
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return user;
        }

        /// <summary>
        /// Aaron Usher
        /// Updated: 2017/04/14
        /// 
        /// Adds a user to the database.
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/14
        /// 
        /// Standardized method and added address fields.
        /// </remarks>
        /// 
        /// <param name="user">The user to add.</param>
        /// <returns>Rows affected.</returns>
        public static int CreateUser(User user)
        {
            var rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_create_app_user";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FIRST_NAME", user.FirstName);
            cmd.Parameters.AddWithValue("@LAST_NAME", user.LastName);
            cmd.Parameters.AddWithValue("@PHONE", user.Phone);
            cmd.Parameters.AddWithValue("@E_MAIL_ADDRESS", user.EmailAddress);
            cmd.Parameters.AddWithValue("@E_MAIL_PREFERENCES", user.EmailPreferences);
            cmd.Parameters.AddWithValue("@PASSWORD_HASH", user.PasswordHash);
            cmd.Parameters.AddWithValue("@PASSWORD_SALT", user.PasswordSalt);
            cmd.Parameters.AddWithValue("@USER_NAME", user.UserName);
            cmd.Parameters.AddWithValue("@ACTIVE", user.Active);
            cmd.Parameters.AddWithValue("@ADDRESS1", user.AddressLineOne);
            cmd.Parameters.AddWithValue("@ADDRESS2", user.AddressLineTwo);
            cmd.Parameters.AddWithValue("@CITY", user.City);
            cmd.Parameters.AddWithValue("@STATE", user.State);
            cmd.Parameters.AddWithValue("@ZIP", user.Zip);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }

        /// <summary>
        /// Aaron Usher
        /// Updated: 2017/04/14
        /// 
        /// Retrieves the salt of a user based on their username.
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/14
        /// 
        /// Standardized method.
        /// </remarks>
        /// <param name="username">The username to search on.</param>
        /// <returns>Salt of the user.</returns>
        public string RetrieveUserSalt(string username)
        {

            var salt = "";

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_user_salt";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@USERNAME", username);

            try
            {
                conn.Open();
                salt = cmd.ExecuteScalar().ToString();
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return salt;
        }

        /// <summary>
        /// William Flood 
        /// Created: 2017/04/12
        /// 
        /// Retrieves the salt of a user based on their email.
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/14
        /// 
        /// Standardized method.
        /// </remarks>
        /// <param name="email">The email to search on.</param>
        /// <returns>The salt of the user with the given email.</returns>
        public static string RetrieveUserSaltByEmail(string email)
        {

            var salt = "";

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_user_salt_by_email";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmailAddress", email);
            try
            {
                conn.Open();
                salt = cmd.ExecuteScalar().ToString();
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return salt;
        }

        /// <summary>
        /// Aaron Usher
        /// Updated: 2017/04/14
        /// 
        /// Authenticates a user and retrieves their information if it is correct.
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/14
        /// 
        /// Standardized method and added address fields.
        /// </remarks>
        /// <param name="username">The username to search on.</param>
        /// <param name="passwordHash">The password of the user.</param>
        /// <returns>A full User.</returns>
        public static User Login(string username, string passwordHash)
        {
            User user = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_login";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Password_Hash", passwordHash);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    user = new User()
                    {
                        UserId = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.IsDBNull(2) ? null : reader.GetString(2),
                        Phone = reader.GetString(3),
                        EmailAddress = reader.GetString(4),
                        EmailPreferences = reader.GetBoolean(5),
                        UserName = reader.GetString(6),
                        Active = reader.GetBoolean(7),
                        AddressLineOne = reader.IsDBNull(8) ? null : reader.GetString(8),
                        AddressLineTwo = reader.IsDBNull(9) ? null : reader.GetString(9),
                        City = reader.IsDBNull(10) ? null : reader.GetString(10),
                        State = reader.IsDBNull(11) ? null : reader.GetString(11),
                        Zip = reader.IsDBNull(12) ? null : reader.GetString(12)
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

        /// <summary>
        /// William Flood
        /// Created: 2017/04/12
        /// 
        /// Uses a user's email address and hashed password to log in.
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/14
        /// 
        /// Standardized method and added address fields.
        /// </remarks>
        /// <param name="emailAddress">The email address to log in with.</param>
        /// <param name="passwordHash">The hash of the password to log in with.</param>
        /// <returns>The user.</returns>
        public static User WebLogin(string emailAddress, string passwordHash)
        {
            User user = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_web_login";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmailAddress", emailAddress);
            cmd.Parameters.AddWithValue("@Password_Hash", passwordHash);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    user = new User()
                    {
                        UserId = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.IsDBNull(2) ? null : reader.GetString(2),
                        Phone = reader.GetString(3),
                        EmailAddress = reader.GetString(4),
                        EmailPreferences = reader.GetBoolean(5),
                        UserName = reader.GetString(6),
                        Active = reader.GetBoolean(7),
                        AddressLineOne = reader.IsDBNull(8) ? null : reader.GetString(8),
                        AddressLineTwo = reader.IsDBNull(9) ? null : reader.GetString(9),
                        City = reader.IsDBNull(10) ? null : reader.GetString(10),
                        State = reader.IsDBNull(11) ? null : reader.GetString(11),
                        Zip = reader.IsDBNull(12) ? null : reader.GetString(12)
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

        /// <summary>
        /// Bobby Thorne
        /// Created: 2017/03/04
        /// 
        /// Retrieves the username of the of the user with the given email.
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/14
        /// 
        /// Standardized method.
        /// </remarks>
        /// <param name="emailAddress">The email to search on.</param>
        /// <returns>The username.</returns>
        public string RetrieveUsernameByEmail(string emailAddress)
        {
            var username = "";

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_app_username_by_email";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@E_MAIL_ADDRESS", emailAddress);
            try
            {
                conn.Open();
                username = (string)cmd.ExecuteScalar();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

            return username;
        }

        /// <summary>
        /// William Flood
        /// Created: 2017/02/28
        /// 
        /// Updates a user's password in the database.
        /// </summary>
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/14
        /// 
        /// Standardized method.
        /// </remarks>
        /// <param name="username">Username of the user to update.</param>
        /// <param name="oldSalt">The old salt (for a concurrency check).</param>
        /// <param name="oldPasswordHash">The old password hash (for a concurrency check).</param>
        /// <param name="newSalt">The new salt.</param>
        /// <param name="newPasswordHash">The new password hash.</param>
        /// <returns>Rows affected.</returns>
        public int UpdatePassword(string username, string oldSalt, string oldPasswordHash, string newSalt, string newPasswordHash)
        {
            var rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_update_user_password";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@USERNAME", username);
            cmd.Parameters.AddWithValue("@OLD_SALT", oldSalt);
            cmd.Parameters.AddWithValue("@OLD_HASH", oldPasswordHash);
            cmd.Parameters.AddWithValue("@NEW_SALT", newSalt);
            cmd.Parameters.AddWithValue("@NEW_HASH", newPasswordHash);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }

        /// <summary>
        /// Aaron Usher
        /// Updated: 2017/04/14
        /// 
        /// Resets a user's password in the database.
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/14
        /// 
        /// Standardized method.
        /// </remarks>
        /// <param name="username">The username of the user.</param>
        /// <param name="salt">The new salt for the user.</param>
        /// <param name="passwordHash">The new password for the user.</param>
        /// <returns>Rows affected.</returns>
        public int ResetPassword(string username, string salt, string passwordHash)
        {
            var rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_reset_user_password";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@USERNAME", username);
            cmd.Parameters.AddWithValue("@PASSWORD_SALT", salt);
            cmd.Parameters.AddWithValue("@PASSWORD_HASH", passwordHash);
            
            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }

        /// <summary>
        /// Christian Lopez
        /// 2017/03/09
        /// 
        /// Attempts to retrieve a user from the DB associated with the userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static User RetrieveUserByUserId(int userId)
        {
            User user = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_app_user";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@USER_ID", userId);
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    user = new User()
                    {
                        UserId = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.IsDBNull(2) ? null : reader.GetString(2),
                        Phone = reader.GetString(3),
                        EmailAddress = reader.GetString(4),
                        EmailPreferences = reader.GetBoolean(5),
                        UserName = reader.GetString(6),
                        Active = reader.GetBoolean(7),
                        AddressLineOne = reader.IsDBNull(8) ? null : reader.GetString(8),
                        AddressLineTwo = reader.IsDBNull(9) ? null : reader.GetString(9),
                        City = reader.IsDBNull(10) ? null : reader.GetString(10),
                        State = reader.IsDBNull(11) ? null : reader.GetString(11),
                        Zip = reader.IsDBNull(12) ? null : reader.GetString(12)
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return user;
        }

        public static int DeleteUser(int id)
        {
            int rowsAffected = 0;

            const string cmdText = @"sp_delete_app_user";
            var conn = DBConnection.GetConnection();

            using (var cmd = new SqlCommand(cmdText, conn) {CommandType = CommandType.StoredProcedure})
            {
                cmd.Parameters.AddWithValue("@USER_ID", id);

                try
                {
                    conn.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return rowsAffected;
        }
    }
}