using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class EmployeeManager : IEmployeeManager
    {

        public List<Employee> employees { get; set; }


        public Employee RetrieveEmployeeByUserName(string userName)
        {
            _employee = new Employee();
            _employee.EmployeeId = 10000;
            return _employee;
        }

        

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
        /// Ariel Sigo
        /// Created 2017/10/02
        /// 
        /// Refreshes Employee List
        /// </summary>
        private void refreshEmployeeList()
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
                refreshEmployeeList();
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
                result = (1 == EmployeeAccessor.UpdateEmployee(newEmp.EmployeeId, oldEmp.UserId, newEmp.UserId, oldEmp.Salary, newEmp.Salary, oldEmp.Active, newEmp.Active, oldEmp.DateOfBirth, newEmp.DateOfBirth));
                refreshEmployeeList();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }


    }
}
