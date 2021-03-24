using EmployeeDetailsAPI.IServices;
using EmployeeDetailsAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDetailsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;
        public EmployeeController(IEmployeeService employee,ILogger<EmployeeController> logger)
        {
            _employeeService = employee;
            this._logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        [Route("api/Employee/GetEmployee")]
        public IEnumerable<Employee> GetEmployee()
        {
            _logger.LogInformation("Getting all Employees");
            return _employeeService.GetEmployee();
        }
 
        [HttpPost]
        [Route("[action]")]
        [Route("api/Employee/AddEmployee")]
        public Employee AddEmployee(Employee employee)
        {
            var result = _employeeService.AddEmployee(employee);
            if (result != null)
            {
                _logger.LogInformation("Added Successfully");
            }
            return result;

        }

        [HttpPut]
        [Route("[action]")]
        [Route("api/Employee/EditEmployee")]
        public Employee EditEmployee(Employee employee)
        {
            var result = _employeeService.UpdateEmployee(employee);
            if (result != null)
            {
                _logger.LogInformation("Updated Successfully");
            }
            return result;
        }

        [HttpDelete]
        [Route("[action]")]
        [Route("api/Employee/DeleteEmployee")]
        public Employee DeleteEmployee(int id)
        {
            var result = _employeeService.DeleteEmployee(id);
            if (result == null)
            {
                _logger.LogWarning($"Employee with ID {id} is not found");
            }
            else
            {
                _logger.LogInformation("Deleted Successfully");
            }
            return result;
        }

        [HttpGet]
        [Route("[action]")]
        [Route("api/Employee/GetEmployeeId")]
        public Employee GetEmployeeId(int id)
        {
            var result = _employeeService.GetEmployeeById(id);
            if (result == null)
            {
                _logger.LogWarning($"Employee with ID {id} is not found");
            }

            return result;
        }

        [HttpGet]
        [Route("[action]")]
        [Route("api/Employee/GetEmployeeStatus")]
        public string GetEmployeeStatus(int id)
        {
            var result = _employeeService.GetEmployeeByStatus(id);
            if (result == null)
            {
                _logger.LogWarning($"Employee with ID {id} is not found");
            }

            return result;
        }
    }
}
