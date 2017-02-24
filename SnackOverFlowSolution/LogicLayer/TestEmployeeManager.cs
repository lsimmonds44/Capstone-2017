using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
   public class TestEmployeeManager : IEmployeeManager
    {
        Employee _employee = null;
        public DataObjects.Employee RetrieveEmployeeByUserName(string userName)
        {
            _employee = new Employee();
            _employee.EmployeeId = 10100;

            return _employee;
        }

        public DataObjects.Employee RetrieveEmployee(int employeeID)
        {
            throw new NotImplementedException();
        }

        public List<DataObjects.Employee> RetrieveEmployeeList()
        {
            throw new NotImplementedException();
        }

        public bool UpdateEmployee(int Employee_ID, int oldUser_ID, int newUser_ID, decimal oldSalary, decimal newSalary, bool oldActive, bool newActive, DateTime oldDate_Of_Birth, DateTime newDate_Of_Birth)
        {
            throw new NotImplementedException();
        }


        public int CreateEmployee(Employee employeeInstance)
        {
            throw new NotImplementedException();
        }

        public List<Employee> SearchEmployees(Employee searchParameters)
        {
            throw new NotImplementedException();
        }
    }
}
