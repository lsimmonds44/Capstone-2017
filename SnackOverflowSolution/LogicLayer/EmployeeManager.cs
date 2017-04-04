using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class EmployeeManager : IEmployeeManager
    {
        Employee _employee;
        public List<Employee> employees { get; set; }

        /// <summary>
        /// Christian Lopez
        /// Created 2017/02/24
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Employee RetrieveEmployeeByUserName(string userName)
        {
            try
            {
                return EmployeeAccessor.RetrieveEmployeeByUsername(userName);
            }
            catch (SqlException ex)
            {
                
                throw new ApplicationException("There was a database error.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an unknown error.", ex);
            }
        }

        
        /// <summary>
        /// Daniel brown
        /// Created 02/08/2017
        /// 
        /// Retrieve a single employee
        /// </summary>
        /// <param name="employeeID">The Id of the employee to be retrieved</param>
        /// <returns>A single Employee Object</returns>
        public Employee RetrieveEmployee(int employeeID)
        {
            Employee employee = null;

            try
            {
                employee = EmployeeAccessor.RetrieveEmployee(employeeID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return employee;
        }
        
        /// <summary>
        /// Daniel Brown
        /// Created 02/08/2017
        /// 
        /// retrieve a list of all active employees
        /// </summary>
        /// <returns>A list of Employee objects</returns>
        public List<Employee> RetrieveEmployeeList()
        {
            List<Employee> employees = null;
            try
            {
                employees = EmployeeAccessor.RetrieveEmployeeList();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return employees;
        }

        /// <summary>
        /// Created 2017-03-22 by William Flood
        /// 
        /// </summary>
        /// <param name="employeeInstance"></param>
        /// <returns></returns>
        public int CreateEmployee(Employee employeeInstance)
        {
            try
            {
                var rowsAffected = EmployeeAccessor.CreateEmployee(employeeInstance);
                return rowsAffected;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Ariel Sigo
        /// Created 2017/10/02
        /// 
        /// Refreshes Employee List
        /// </summary>
        private void RefreshEmployeeList()
        {
            try
            {
                employees = EmployeeAccessor.RetrieveEmployeeList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        /// <summary>
        /// Ariel Sigo
        /// Created 2017/07/02
        /// 
        /// Updates Employee Information
        /// </summary>
        /// <param name="Employee_ID"></param>
        /// <param name="oldUser_ID"></param>
        /// <param name="newUser_ID"></param>
        /// <param name="oldSalary"></param>
        /// <param name="newSalary"></param>
        /// <param name="oldActive"></param>
        /// <param name="newActive"></param>
        /// <param name="oldDate_Of_Birth"></param>
        /// <param name="newDate_Of_Birth"></param>
        /// <returns>true if update was successful for Employee info</returns>
        public bool UpdateEmployee(int Employee_ID, int oldUser_ID, int newUser_ID, decimal oldSalary, decimal newSalary, bool oldActive, bool newActive, DateTime oldDate_Of_Birth, DateTime newDate_Of_Birth)
        {
            var result = false; // we want to let the presentation layer
            // know whether the operation succeeded
            try
            {
                result = (1 == EmployeeAccessor.UpdateEmployee(Employee_ID, oldUser_ID, newUser_ID, oldSalary, newSalary, oldActive, newActive, oldDate_Of_Birth, newDate_Of_Birth));
                RefreshEmployeeList();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
        /// <summary>
        /// Ariel Sigo  
        /// Created 2017/10/02
        /// 
        /// </summary>
        /// <param name="oldEmp"></param>
        /// <param name="newEmp"></param>
        /// <returns>True if employee update was successful</returns>
        public bool UpdateEmployee(Employee oldEmp, Employee newEmp)
        {
            var result = false; // we want to let the presentation layer
            // know whether the operation succeeded
            try
            {
                result = (1 == EmployeeAccessor.UpdateEmployee((int)newEmp.EmployeeId, (int)oldEmp.UserId, (int)newEmp.UserId, (decimal)oldEmp.Salary, (decimal)newEmp.Salary, (bool)oldEmp.Active, (bool)newEmp.Active, (DateTime)oldEmp.DateOfBirth, (DateTime)newEmp.DateOfBirth));
                RefreshEmployeeList();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Created 2017-03-22 by William Flood
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <returns></returns>
        public List<Employee> SearchEmployees(Employee searchParameters)
        {
            try
            {
                var employeeList = EmployeeAccessor.RetrieveBySearch(searchParameters);
                return employeeList;
            } catch
            {
                throw;
            }
        }
    }
}
