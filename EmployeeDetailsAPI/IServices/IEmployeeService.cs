using EmployeeDetailsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDetailsAPI.IServices
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetEmployee();
        Employee GetEmployeeById(int id);

        String GetEmployeeByStatus(int id);

        Employee AddEmployee(Employee employee);
        Employee UpdateEmployee(Employee employee);
        Employee DeleteEmployee(int id);
    }
}
