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
    public class TestAccessor
    {

        public static int DeleteTestUser()
        {
            int rowsAffected = 0;
            
            var conn = DBConnection.GetConnection();
            var cmdText = "sp_delete_test_user";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {
                
                throw;
            }
            finally
            {
                conn.Close();
            }


            return rowsAffected;
        }

        public static int DeleteTestEmployee()
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_delete_test_employee";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }


            return rowsAffected;
        }

        public static int DeleteTestCommercialCustomer(CommercialCustomer testCommercialCustomer){
            int rowsAffected = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_delete_test_commercial_customer";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.Parameters.Add("@User_ID", SqlDbType.Int);
            cmd.Parameters["@User_ID"].Value = testCommercialCustomer.UserId;
			
			cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
			
			}
            catch (Exception)
            {
				throw;
            }
            finally
            {
                conn.Close();
            }

            return rowsAffected;
        }

        public static int DeleteTestProduct()
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = "sp_delete_test_product";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return rowsAffected;
        }

    }
}
