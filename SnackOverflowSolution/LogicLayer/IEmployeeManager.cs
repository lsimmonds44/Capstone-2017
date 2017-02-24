using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
       
    public interface IEmployeeManager
    {
		 Employee RetrieveEmployeeByUserName(string userName);
        Employee RetrieveEmployee(int employeeID);

        List<Employee> RetrieveEmployeeList();

        bool UpdateEmployee(int Employee_ID, int oldUser_ID, int newUser_ID, decimal oldSalary, decimal newSalary, bool oldActive, bool newActive, DateTime oldDate_Of_Birth, DateTime newDate_Of_Birth);

        int CreateEmployee(Employee employeeInstance);

        List<Employee> SearchEmployees(Employee searchParameters);
    }
}
