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
        private readonly IEmployeeService employeeService;
        private readonly ILogger<EmployeeController> logger;
        public EmployeeController(IEmployeeService employee,ILogger<EmployeeController> logger)
        {
            employeeService = employee;
            this.logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        [Route("api/Employee/GetEmployee")]
        public IEnumerable<Employee> GetEmployee()
        {
            logger.LogInformation("Getting all Employees");
            return employeeService.GetEmployee();
        }
 
        [HttpPost]
        [Route("[action]")]
        [Route("api/Employee/AddEmployee")]
        public Employee AddEmployee(Employee employee)
        {
            var result = employeeService.AddEmployee(employee);
            if (result != null)
            {
                logger.LogInformation("Added Successfully");
            }
            return result;

        }

        [HttpPut]
        [Route("[action]")]
        [Route("api/Employee/EditEmployee")]
        public Employee EditEmployee(Employee employee)
        {
            var result = employeeService.UpdateEmployee(employee);
            if (result != null)
            { 
                logger.LogInformation("Updated Successfully");
            }
            return result;
        }

        [HttpDelete]
        [Route("[action]")]
        [Route("api/Employee/DeleteEmployee")]
        public Employee DeleteEmployee(int id)
        {
            var result = employeeService.DeleteEmployee(id);
            if (result == null)
            {
                logger.LogWarning($"Employee with ID {id} is not found");
            }
            else
            {
                logger.LogInformation("Deleted Successfully");
            }
            return result;
        }

        [HttpGet]
        [Route("[action]")]
        [Route("api/Employee/GetEmployeeId")]
        public Employee GetEmployeeId(int id)
        {
            var result = employeeService.GetEmployeeById(id);
            if (result == null)
            {
                logger.LogWarning($"Employee with ID {id} is not found");
            }

            return result;
        }

        [HttpGet]
        [Route("[action]")]
        [Route("api/Employee/GetEmployeeStatus")]
        public string GetEmployeeStatus(int id)
        {
            var result = employeeService.GetEmployeeByStatus(id);
            if (result == null)
            {
                logger.LogWarning($"Employee with ID {id} is not found");
            }

            return result;
        }
    }
}
