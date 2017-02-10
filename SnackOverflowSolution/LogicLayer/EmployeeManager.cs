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
    } // end class
} // end namespace
