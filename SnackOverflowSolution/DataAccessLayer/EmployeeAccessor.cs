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
    public class EmployeeAccessor
    {
        public static Employee RetrieveEmployee(int employeeID)
        {

            Employee employee = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_employee";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@EmployeeID", SqlDbType.Int);
            cmd.Parameters["@EmployeeID"].Value = employeeID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    employee = new Employee()
                    {
                        EmployeeId = reader.GetInt32(0),
                        UserId = reader.GetInt32(1),
                        Salary = reader.GetDecimal(3),
                        Active = reader.GetBoolean(4),
                        DateOfBirth = reader.GetDateTime(5)
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

            return employee;

        }

        public static List<Employee> RetrieveEmployeeList()
        {
            List<Employee> employees = new List<Employee>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_employee_list";

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
                        employees.Add(new Employee()
                        {
                            EmployeeId = reader.GetInt32(0),
                            UserId = reader.GetInt32(1),
                            Salary = reader.GetDecimal(3),
                            Active = reader.GetBoolean(4),
                            DateOfBirth = reader.GetDateTime(5)
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

            return employees;

        }
    }
}
