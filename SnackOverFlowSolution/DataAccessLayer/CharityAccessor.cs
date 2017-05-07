using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using System.Data;

namespace DataAccessLayer
{
    /// <summary>
    /// Aaron Usher
    /// Updated: 
    /// 2017/04/04
    /// 
    /// Class to handle database interactions inolving charities.
    /// </summary>
    public static class CharityAccessor
    {
        /// <summary>
        /// Aaron Usher
        /// Created: 
        /// 2017/04/04
        /// 
        /// Retrieves a list of all charities in the database.
        /// </summary>
        /// <returns>List of all charities in the database.</returns>
        public static List<Charity> RetrieveCharities()
        {
            var charities = new List<Charity>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_charity_list";
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
                        charities.Add(new Charity()
                        {
                            CharityID = reader.GetInt32(0),
                            EmployeeID = reader.IsDBNull(1) ? (int?)null : reader.GetInt32(1),
                            CharityName = reader.GetString(2),
                            ContactFirstName = reader.GetString(3),
                            ContactLastName = reader.GetString(4),
                            PhoneNumber = reader.GetString(5),
                            Email = reader.GetString(6),
                            ContactHours = reader.GetString(7),
                            Status = reader.GetString(8)
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
            

            return charities;
        }

        /// <summary>
        /// Bobby Thorne
        /// Created: 
        /// 2017/24/03 
        /// 
        /// Retrieve Charity By User Id
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated:
        /// 2017/04/04
        /// Standardized method.
        /// 
        /// Christian Lopez
        /// 2017/05/07
        /// 
        /// Removed due to business rules separating charity from user
        /// </remarks>
        /// 
        /// <param name="userId">The User Id to search on.</param>
        /// <returns>The charity associated with the given User Id.</returns>
        //public static Charity RetrieveCharityByUserId(int userId)
        //{
        //    Charity charity = null;

        //    var conn = DBConnection.GetConnection();
        //    var cmdText = @"sp_retrieve_charity_by_user_id";
        //    var cmd = new SqlCommand(cmdText, conn);
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    cmd.Parameters.AddWithValue("@USER_ID", userId);

        //    try
        //    {
        //        conn.Open();
        //        var reader = cmd.ExecuteReader();
        //        if (reader.HasRows)
        //        {
        //            reader.Read();
        //            charity = new Charity
        //            {
        //                CharityID = reader.GetInt32(0),
        //                UserID = reader.GetInt32(1),
        //                EmployeeID = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2),
        //                CharityName = reader.GetString(3),
        //                ContactFirstName = reader.GetString(4),
        //                ContactLastName = reader.GetString(5),
        //                PhoneNumber = reader.GetString(6),
        //                Email = reader.GetString(7),
        //                ContactHours = reader.GetString(8),
        //                Status = reader.GetString(9)
        //            };
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }

        //    return charity;
        //}


        /// <summary>
        /// Daniel Brown
        /// Created: 
        /// 2017/24/03
        /// 
        /// Alters the status of a charity to approved
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 
        /// 2017/04/04
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <param name="charity">The charity to be altered.</param>
        /// <returns>Rows affected.</returns>
        public static int ApproveCharity(Charity charity)
        {

            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_update_charity_approve";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("old_CHARITY_ID", charity.CharityID);

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
        /// Daniel Brown
        /// Created: 
        /// 2017/04/03
        /// 
        /// Alters the status of a charity to denied
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 
        /// 2017/04/04
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <param name="charity">The charity to deny.</param>
        /// <returns>Rows affected.</returns>
        public static int DenyCharity(Charity charity)
        {

            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_update_charity_deny";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@old_CHARITY_ID", charity.CharityID);

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
        /// Created: 
        /// 2017/03/08
        /// 
        /// Handles the process of adding a charity application to the DB
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 
        /// 2017/04/04
        /// 
        /// Standardized method.
        /// </remarks>
        /// <param name="charity">Charity to add.</param>
        /// <returns>Rows affected.</returns>
        public static int CreateCharityApplication(Charity charity)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            string cmdText;
            if (charity.EmployeeID == null)
            {
                cmdText = @"sp_create_charity_application";
            }
            else
            {
                cmdText = @"sp_create_charity";
            }
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            if (charity.EmployeeID != null)
            {
                cmd.Parameters.AddWithValue("@EMPLOYEE_ID", charity.EmployeeID);
                cmd.Parameters.AddWithValue("@STATUS", charity.Status);
            }
            cmd.Parameters.AddWithValue("@CHARITY_NAME", charity.CharityName);
            cmd.Parameters.AddWithValue("@CONTACT_FIRST_NAME", charity.ContactFirstName);
            cmd.Parameters.AddWithValue("@CONTACT_LAST_NAME", charity.ContactLastName);
            cmd.Parameters.AddWithValue("@PHONE_NUMBER", charity.PhoneNumber);
            cmd.Parameters.AddWithValue("@EMAIL", charity.Email);
            cmd.Parameters.AddWithValue("@CONTACT_HOURS", charity.ContactHours);

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


        
    }
}
