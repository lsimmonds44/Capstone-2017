using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayer
{
    /// <summary>
    /// Aaron Usher
    /// Updated: 
    /// 2017/04/28
    ///
    /// Class to handle test database interactions.
    public class TestAccessor
    {
        /// <summary>
        /// Aaron Usher
        /// Updated: 
        /// 2017/04/28
        /// 
        /// Deletes the test user.
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 
        /// 2017/04/28
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <returns>Rows affected.</returns>
        public static int DeleteTestUser()
        {
            int rows = 0;
            
            var conn = DBConnection.GetConnection();
            var cmdText = "sp_delete_test_user";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

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
        /// Updated: 
        /// 2017/04/28
        /// 
        /// Deletes the test employee.
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 
        /// 2017/04/28
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <returns>Rows affected.</returns>
        public static int DeleteTestEmployee()
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_delete_test_employee";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

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
        /// Updated:
        /// 2017/04/28
        /// 
        /// Deletes the test commercial customer.
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 
        /// 2017/04/28
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <param name="commercialCustomer">The commercial customer to delete.</param>
        /// <returns>Rows affected.</returns>
        public static int DeleteTestCommercialCustomer(CommercialCustomer commercialCustomer)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_delete_test_commercial_customer";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@User_ID", commercialCustomer.UserId);
			
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
        /// Updated: 
        /// 2017/04/28
        /// 
        /// Deletes the test product.
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 
        /// 2017/04/28
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <returns>Rows affected.</returns>
        public static int DeleteTestProduct()
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_delete_test_product";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

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
