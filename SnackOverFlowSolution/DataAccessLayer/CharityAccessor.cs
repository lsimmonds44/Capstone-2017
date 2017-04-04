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
    public class CharityAccessor : IDataAccessor
    {
        public Charity CharityInstance { get; set; }
        public List<Charity> CharityList { get; private set; }
        private static CharityAccessor charityAccessorInstance;

        public static CharityAccessor GetCharityAccessorInstance()
        {
            if(null == charityAccessorInstance)
            {
                charityAccessorInstance = new CharityAccessor();
            }
            return charityAccessorInstance;
        }

        public string CreateScript
        {
            get
            {
                return @"sp_create_charity";
            }
        }

        public string DeactivateScript
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string RetrieveListScript
        {
            get
            {
                return @"sp_retrieve_charity_list";
            }
        }

        public string RetrieveSearchScript
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string RetrieveSingleScript
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string UpdateScript
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void ReadList(SqlDataReader reader)
        {
            CharityList = new List<Charity>();
            while (reader.Read()) {
                Charity c = new Charity()
                {
                    CharityID = reader.GetInt32(0),
                    UserID = reader.GetInt32(1),
                    //EmployeeID = reader.GetInt32(2),
                    CharityName = reader.GetString(3),
                    ContactFirstName = reader.GetString(4),
                    ContactLastName = reader.GetString(5),
                    PhoneNumber = reader.GetString(6),
                    Email = reader.GetString(7),
                    ContactHours = reader.GetString(8),
                    Status = reader.GetString(9)
                };
                if (!reader.IsDBNull(2))
                {
                    c.EmployeeID = reader.GetInt32(2);
                }
                else
                {
                    c.EmployeeID = null;
                }
                CharityList.Add(c);

            }
        }

        public void ReadSingle(SqlDataReader reader)
        {
            CharityInstance = new Charity
            {
                CharityID = reader.GetInt32(0),
                UserID = reader.GetInt32(1),
                CharityName = reader.GetString(2),
                ContactFirstName = reader.GetString(3),
                ContactLastName = reader.GetString(4),
                PhoneNumber = reader.GetString(5),
                Email = reader.GetString(6),
                ContactHours = reader.GetString(7),
                Status = reader.GetString(8)
            };
        }

        public void SetCreateParameters(SqlCommand cmd)
        {
            cmd.Parameters.Add("@USER_ID", SqlDbType.Int);
            cmd.Parameters.Add("@EMPLOYEE_ID", SqlDbType.Int);
            cmd.Parameters.Add("@CHARITY_NAME", SqlDbType.NVarChar, 200);
            cmd.Parameters.Add("@CONTACT_FIRST_NAME", SqlDbType.NVarChar, 150);
            cmd.Parameters.Add("@CONTACT_LAST_NAME", SqlDbType.NVarChar, 150);
            cmd.Parameters.Add("@PHONE_NUMBER", SqlDbType.NVarChar, 20);
            cmd.Parameters.Add("@EMAIL", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@CONTACT_HOURS", SqlDbType.NVarChar, 150);
            cmd.Parameters.Add("@STATUS", SqlDbType.NVarChar, 10);
            cmd.Parameters["@USER_ID"].Value = CharityInstance.UserID;
            cmd.Parameters["@EMPLOYEE_ID"].Value = CharityInstance.EmployeeID;
            cmd.Parameters["@CHARITY_NAME"].Value = CharityInstance.CharityName;
            cmd.Parameters["@CONTACT_FIRST_NAME"].Value = CharityInstance.ContactFirstName;
            cmd.Parameters["@CONTACT_LAST_NAME"].Value = CharityInstance.ContactLastName;
            cmd.Parameters["@PHONE_NUMBER"].Value = CharityInstance.PhoneNumber;
            cmd.Parameters["@EMAIL"].Value = CharityInstance.Email;
            cmd.Parameters["@CONTACT_HOURS"].Value = CharityInstance.ContactHours;
            cmd.Parameters["@STATUS"].Value = CharityInstance.Status;
        }

        /// <summary>
        /// Bobby Thorne
        /// 3/24/2017
        /// 
        /// Retrieve Charity By User Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static Charity RetrieveCharityByUserId(int userId)
        {
            Charity s = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_charity_by_user_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@USER_ID", SqlDbType.Int);
            cmd.Parameters["@USER_ID"].Value = userId;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    s = new Charity
                    {
                        CharityID = reader.GetInt32(0),
                        UserID = reader.GetInt32(1),
                        //EmployeeID = reader.GetString(2),
                        CharityName = reader.GetString(3),
                        ContactFirstName = reader.GetString(4),
                        ContactLastName = reader.GetString(5),
                        PhoneNumber = reader.GetString(6),
                        Email = reader.GetString(7),
                        ContactHours = reader.GetString(8),
                        Status = reader.GetString(9)
                    };
                    if (!reader.IsDBNull(2))
                    {
                        s.EmployeeID = reader.GetInt32(2);
                    }
                    else
                    {
                        s.EmployeeID = null;
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Error connecting to DB: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return s;
        }

        public void SetKeyParameters(SqlCommand cmd)
        {
            throw new NotImplementedException();
        }

        public void SetRetrieveSearchParameters(SqlCommand cmd)
        {
            throw new NotImplementedException();
        }

        public void SetUpdateParameters(SqlCommand cmd)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Daniel Brown
        /// Created 03/04/2017
        /// 
        /// Alters the status of a charity to approved
        /// </summary>
        /// <param name="charity"></param>
        /// <returns></returns>
        public int ApproveCharity(Charity charity)
        {

            int rowsAffected = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_update_charity_approve";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@old_CHARITY_ID",SqlDbType.Int);
            cmd.Parameters["@old_CHARITY_ID"].Value = charity.CharityID;

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            finally
            {
                conn.Close();
            }



            return rowsAffected;
        }

        /// <summary>
        /// Daniel Brown
        /// Created 03/04/2017
        /// 
        /// Alters the status of a charity to denied
        /// </summary>
        /// <param name="charity"></param>
        /// <returns></returns>
        public int DenyCharity(Charity charity)
        {

            int rowsAffected = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_update_charity_deny";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@old_CHARITY_ID", SqlDbType.Int);
            cmd.Parameters["@old_CHARITY_ID"].Value = charity.CharityID;

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }



            return rowsAffected;
        }

        /// <summary>
        /// Christian Lopez
        /// Created 2017/03/08
        /// 
        /// Handles the process of adding an application to the DB
        /// </summary>
        /// <param name="charity"></param>
        /// <returns></returns>
        public static int CreateCharityApplication(Charity charity)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_create_charity_application";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@USER_ID", SqlDbType.Int);
            cmd.Parameters.Add("@CHARITY_NAME", SqlDbType.NVarChar, 200);
            cmd.Parameters.Add("@CONTACT_FIRST_NAME", SqlDbType.NVarChar, 150);
            cmd.Parameters.Add("@CONTACT_LAST_NAME", SqlDbType.NVarChar, 150);
            cmd.Parameters.Add("@PHONE_NUMBER", SqlDbType.NVarChar, 20);
            cmd.Parameters.Add("@EMAIL", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@CONTACT_HOURS", SqlDbType.NVarChar, 150);

            cmd.Parameters["@USER_ID"].Value = charity.UserID;
            cmd.Parameters["@CHARITY_NAME"].Value = charity.CharityName;
            cmd.Parameters["@CONTACT_FIRST_NAME"].Value = charity.ContactFirstName;
            cmd.Parameters["@CONTACT_LAST_NAME"].Value = charity.ContactLastName;
            cmd.Parameters["@PHONE_NUMBER"].Value = charity.PhoneNumber;
            cmd.Parameters["@EMAIL"].Value = charity.Email;
            cmd.Parameters["@CONTACT_HOURS"].Value = charity.ContactHours;

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
