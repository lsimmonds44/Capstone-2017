using DataObjects;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
namespace DataAccessLayer
{
    public static class EmployeeOrderResponsibilityAccessor
    {
        public static List<EmployeeOrderResponsibility> RetrieveEmployeeOrderResponsibility(EmployeeOrderResponsibility EmployeeOrderResponsibilityInstance)
        {
            List<EmployeeOrderResponsibility> EmployeeOrderResponsibilityList = new List<EmployeeOrderResponsibility>();
            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_EMPLOYEE_ORDER_RESPONSIBILITY_from_search";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.Parameters.AddWithValue("@ORDER_ID", EmployeeOrderResponsibilityInstance.OrderId);
            cmd.Parameters.AddWithValue("@EMPLOYEE_ID", EmployeeOrderResponsibilityInstance.EmployeeId);
            cmd.Parameters.AddWithValue("@DESCRIPTION", EmployeeOrderResponsibilityInstance.Description);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var foundEmployeeOrderResponsibilityInstance = new EmployeeOrderResponsibility()
                    {
                        OrderId = reader.GetInt32(0),
                        EmployeeId = reader.GetInt32(1),
                        Description = reader.GetString(2)
                    };
                    EmployeeOrderResponsibilityList.Add(EmployeeOrderResponsibilityInstance);
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error: " + ex);
            }
            finally
            {
                conn.Close();
            }
            return EmployeeOrderResponsibilityList;
        }
    }
}
