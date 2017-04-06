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
    /// <summary>
    /// Aaron Usher
    /// Updated: 2017/04/03
    /// 
    /// Class to handle database interactions involving customers.
    /// </summary>
    public static class CustomerAccessor
    {

        /// <summary>
        /// Eric Walton
        /// Created: 2017/06/02
        /// 
        /// Create Commercial Customer method that uses a stored procedure to access the database 
        /// to add a commercial customer.
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/03
        /// 
        /// Standardized method.
        /// </remarks>
        /// <param name="commercialCustomer">The commercial customer to add.</param>
        /// <returns>Rows affectd.</returns>
        public static int CreateCommercialCustomer(CommercialCustomer commercialCustomer)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_create_commercial";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@USER_ID", commercialCustomer.UserId);
            cmd.Parameters.AddWithValue("@IS_APPROVED", commercialCustomer.IsApproved);
            cmd.Parameters.AddWithValue("@APPROVED_BY", commercialCustomer.ApprovedBy);
            cmd.Parameters.AddWithValue("@FEDERAL_TAX_ID", commercialCustomer.FederalTaxId);
            cmd.Parameters.AddWithValue("@ACTIVE", commercialCustomer.Active);

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
        /// Bobby Thorne
        /// Created: 2017/03/24
        /// 
        /// Retrieve Commercial Customer by user id
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/03
        /// 
        /// Standardized method.
        /// </remarks>
        /// <param name="userId">User Id to retrieve the commercial customer on.</param>
        /// <returns>The commercial customer with the specified User Id.</returns>
        public static CommercialCustomer RetrieveCommercialCustomerByUserId(int userId)
        {
            CommercialCustomer commercialCustomer = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_commercial_customer_by_user_id";
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
                    commercialCustomer = new CommercialCustomer
                    {
                        CommercialId = reader.GetInt32(0),
                        UserId = reader.GetInt32(1),
                        IsApproved = reader.GetBoolean(2),
                        ApprovedBy = reader.IsDBNull(3) ? null : (int?)reader.GetInt32(3),
                        FederalTaxId = reader.GetInt32(4),
                        Active = reader.GetBoolean(5)
                    };
                }
                reader.Close();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

            return commercialCustomer;
        }

        /// <summary>
        /// Eric Walton
        /// Created: 2017/26/02
        /// Accessor method to Retrieve a list of all Commercial Customers
        /// </summary>
        /// 
        ///  <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/03
        /// 
        /// Standardized method.
        /// </remarks>
        /// <returns>A list of all commercial customers in the database.</returns>
        public static List<CommercialCustomer> RetrieveAllCommercialCustomers()
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
                            CommercialId = reader.GetInt32(0),
                            UserId = reader.GetInt32(1),
                            IsApproved = reader.GetBoolean(2),
                            ApprovedBy = reader.GetInt32(3),
                            FederalTaxId = reader.GetInt32(4),
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
        }
    }
} 
