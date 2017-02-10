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
        Employee _employee = null;

        public Employee RetrieveEmployeeByUserName(string userName)
        {
            _employee = new Employee();
            _employee.EmployeeId = 10200;

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

    }
}
