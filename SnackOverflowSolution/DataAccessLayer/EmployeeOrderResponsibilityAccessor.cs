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
    /// <summary>
    /// Aaron Usher
    /// Updated: 2017/04/03
    /// 
    /// Class to handle database interactions involving employee order responsibilities.
    /// </summary>
    public static class EmployeeOrderResponsibilityAccessor
    {
        /// <summary>
        /// Aaron Usher
        /// Updated: 2017/04/03
        /// 
        /// Retrieves a list of employee order responsibilities based on the provided one.
        /// </summary>
        /// <param name="employeeOrderResponsibility">The employee order responsibility to search on.</param>
        /// <returns>A list of employee order responsiblities like the one passed in.</returns>
        public static List<EmployeeOrderResponsibility> RetrieveEmployeeOrderResponsibility(EmployeeOrderResponsibility employeeOrderResponsibility)
        {
            var EmployeeOrderResponsibilityList = new List<EmployeeOrderResponsibility>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_employee_order_responsibility_from_search";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ORDER_ID", employeeOrderResponsibility.OrderId);
            cmd.Parameters.AddWithValue("@EMPLOYEE_ID", employeeOrderResponsibility.EmployeeId);
            cmd.Parameters.AddWithValue("@DESCRIPTION", employeeOrderResponsibility.Description);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var foundEmployeeOrderResponsibilityInstance = new EmployeeOrderResponsibility()
                        {
                            OrderId = reader.GetInt32(0),
                            EmployeeId = reader.GetInt32(1),
                            Description = reader.GetString(2)
                        };
                        EmployeeOrderResponsibilityList.Add(employeeOrderResponsibility);
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

            return EmployeeOrderResponsibilityList;
        }
    }
}
