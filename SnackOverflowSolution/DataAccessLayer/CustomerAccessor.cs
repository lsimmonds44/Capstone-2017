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
    public class CustomerAccessor
    {

        /// <summary>
        /// Eric Walton
        /// 2017/06/02
        /// 
        /// Create Commercial Customer method that uses a stored procedure to access the database
        /// </summary>
        /// <param name="cc"></param>
        /// <returns></returns>
        public bool CreateCommercialCustomer(CommercialCustomer cc)
        {
            var result = false;
            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_create_commercial";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@USER_ID", SqlDbType.Int);
            cmd.Parameters.Add("@IS_APPROVED", SqlDbType.Bit);
            cmd.Parameters.Add("@APPROVED_BY", SqlDbType.Int);
            cmd.Parameters.Add("@FEDERAL_TAX_ID", SqlDbType.Int);
            cmd.Parameters.Add("@ACTIVE", SqlDbType.Bit);
            // values
            cmd.Parameters["@USER_ID"].Value = cc.User_Id;
            cmd.Parameters["@IS_APPROVED"].Value = cc.IsApproved;
            cmd.Parameters["@APPROVED_BY"].Value = cc.ApprovedBy;
            cmd.Parameters["@FEDERAL_TAX_ID"].Value = cc.FedTaxId;
            cmd.Parameters["@Active"].Value = cc.Active;

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
        } // End of CreteCommercialCustomer

        /// <summary>
        /// Bobby Thorne
        /// 3/24/2017
        /// 
        /// Retrieve Commercial Customer by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static CommercialCustomer RetrieveCommercialCustomerByUserId(int userId)
        {
            CommercialCustomer s = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_commercial_customer_by_user_id";
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
                    s = new CommercialCustomer
                    {
                        Commercial_Id = reader.GetInt32(0),
                        User_Id = reader.GetInt32(1),
                        IsApproved = reader.GetBoolean(2),
                        //ApprovedBy = reader.GetInt32(3),
                        FedTaxId = reader.GetInt32(4),
                        Active = reader.GetBoolean(5)
                    };
                    if (!reader.IsDBNull(3))
                    {
                        s.ApprovedBy = reader.GetInt32(3);
                    }
                    else
                    {
                        s.ApprovedBy = 0;
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

        /// <summary>
        /// Eric Walton
        /// 2017/26/02
        /// Accessor method to Retrieve a list of all Commercial Customers
        /// If succesful returns list
        /// If unsuccessful throws error
        /// </summary>
        /// <returns></returns>
        public List<CommercialCustomer> RetrieveAllCommercialCustomers()
        {
            var commercialCustomers = new List<CommercialCustomer>();
            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_commercial_list";
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
                        commercialCustomers.Add(new CommercialCustomer()
                        {
                            Commercial_Id = reader.GetInt32(0),
                            User_Id = reader.GetInt32(1),
                            IsApproved = reader.GetBoolean(2),
                            ApprovedBy = reader.GetInt32(3),
                            FedTaxId = reader.GetInt32(4),
                            Active = reader.GetBoolean(5)
                        });
                    }
                    reader.Close();
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
            return commercialCustomers;
        } // End of RetrieveAllCommercialCustomers
    } // End of class
} // end of namespace
