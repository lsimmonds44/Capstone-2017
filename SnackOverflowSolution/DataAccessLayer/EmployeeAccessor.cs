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
    /// Updated: 2017/04/07
    /// 
    /// Class to handle database interactions involing employees.
    /// </summary>
    public static class EmployeeAccessor
    {
        /// <summary>
        /// Daniel Brown 
        /// Created: 2017/02/08
        /// 
        /// Retrieve a single employee from the database
        /// </summary>
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/07
        /// 
        /// Standardized method.
        /// </remarks>
        /// <param name="employeeID">The employee ID of the employee to be retrieved</param>
        /// <returns>An employee object</returns>
        public static Employee RetrieveEmployee(int employeeID)
        {

            Employee employee = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_employee";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EMPLOYEE_ID", employeeID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    employee = new Employee();
                    reader.Read();
                        employee.EmployeeId = reader.GetInt32(0);
                        employee.UserId = reader.GetInt32(1);
                        employee.Salary = reader.IsDBNull(2) ? (decimal?)null : reader.GetDecimal(2);
                        employee.Active = reader.GetBoolean(3);
                        employee.DateOfBirth = reader.GetDateTime(4);
                    
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

        /// <summary>
        /// Daniel Brown
        /// Created: 2017/02/08
        /// 
        /// Retrieve a list of all employees
        /// </summary>
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/07
        /// 
        /// Standardized method.
        /// </remarks>
        /// <returns>List of Employee Objects</returns>
        public static List<Employee> RetrieveEmployeeList()
        {
            var employees = new List<Employee>();

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
                            Salary = reader.GetDecimal(2),
                            Active = reader.GetBoolean(3),
                            DateOfBirth = reader.GetDateTime(4)
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
        
        
        /// <summary>
        /// Ariel Sigo
        /// Created: 2017/02/07
        /// 
        /// Updates an employee in the database.
        /// </summary>
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/07
        /// 
        /// Standardized method, and changed signature to take two employees instead of individual parameters.
        /// </remarks>
        /// <param name="oldEmployee">The employee as it was.</param>
        /// <param name="newEmployee">The employee as it should be.</param>
        /// <returns>returns count of rows affected of updated employees</returns>
        public static int UpdateEmployee(Employee oldEmployee, Employee newEmployee)
        {
            var rows = 0;
            
            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_update_employee";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EMPLOYEE_ID", oldEmployee.EmployeeId);
            cmd.Parameters.AddWithValue("@old_USER_ID", oldEmployee.UserId);
            cmd.Parameters.AddWithValue("@new_USER_ID", newEmployee.UserId);
            cmd.Parameters.AddWithValue("@old_SALARY", oldEmployee.Salary);
            cmd.Parameters.AddWithValue("@new_SALARY", newEmployee.Salary);
            cmd.Parameters.AddWithValue("@old_ACTIVE", oldEmployee.Active);
            cmd.Parameters.AddWithValue("@new_ACTIVE", newEmployee.Active);
            cmd.Parameters.AddWithValue("@old_DATE_OF_BIRTH", oldEmployee.DateOfBirth);
            cmd.Parameters.AddWithValue("@new_DATE_OF_BIRTH", newEmployee.DateOfBirth);

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
        /// William Flood
        /// Created: 2017/03/22
        /// 
        /// Creates an employee
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Created: 2017/04/07
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <param name="employee">The employee to add to the database.</param>
        /// <returns>Rows affected.</returns>
        public static int CreateEmployee(Employee employee)
        {
            var rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_create_employee";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@USER_ID", employee.UserId);
            cmd.Parameters.AddWithValue("@SALARY", employee.Salary);
            cmd.Parameters.AddWithValue("@ACTIVE", employee.Active);
            cmd.Parameters.AddWithValue("@DATE_OF_BIRTH", employee.DateOfBirth);
            
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
        /// William Flood
        /// Created: 2017/03/22
        /// 
        /// Retrieves a list of employees based on search criteria
        /// </summary>
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/07
        /// 
        /// Standardized method.
        /// </remarks>
        /// <param name="employee">The employee to search on.</param>
        /// <returns>A list of employees who match the search criteria.</returns>
        public static List<Employee> RetrieveBySearch(Employee employee)
        {
            var employees = new List<Employee>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_employee_from_search";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EMPLOYEE_ID", employee.EmployeeId);
            cmd.Parameters.AddWithValue("@USER_ID", employee.UserId);
            cmd.Parameters.AddWithValue("@SALARY", employee.Salary);
            cmd.Parameters.AddWithValue("@DATE_OF_BIRTH", employee.DateOfBirth);

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
                            Salary = reader.GetDecimal(2),
                            Active = reader.GetBoolean(3),
                            DateOfBirth = reader.GetDateTime(4)
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
        
        /// <summary>
        /// Christian Lopez
        /// Created: 2017/02/24
        /// 
        /// Accesses DB to get Employee by username
        /// </summary>
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/07
        /// 
        /// Standardized method.
        /// </remarks>
        /// <param name="username">The username of the employee.</param>
        /// <returns>The employee with the given username.</returns>
        public static Employee RetrieveEmployeeByUsername(string username)
        {
            Employee employee = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_employee_by_username";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@USER_NAME", username);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    employee = new Employee
                    {
                        EmployeeId = reader.GetInt32(0),
                        UserId = reader.GetInt32(1),
                        Salary = reader.IsDBNull(2) ? (decimal?)null : reader.GetDecimal(2),
                        Active = reader.GetBoolean(3),
                        DateOfBirth = reader.GetDateTime(4)
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

        /// <summary>
        /// Christian Lopez
        /// 2017/04/14
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static Employee RetrieveEmployeeByUserId(int userId)
        {
            Employee employee = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_employee_by_user_id";
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
                    employee = new Employee
                    {
                        EmployeeId = reader.GetInt32(0),
                        UserId = reader.GetInt32(1),
                        Salary = reader.IsDBNull(2) ? (decimal?)null : reader.GetDecimal(2),
                        Active = reader.GetBoolean(3),
                        DateOfBirth = reader.GetDateTime(4)
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
    }
}
