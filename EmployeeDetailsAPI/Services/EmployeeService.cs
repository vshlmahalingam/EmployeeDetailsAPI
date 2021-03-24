using EmployeeDetailsAPI.IServices;
using EmployeeDetailsAPI.Models;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace EmployeeDetailsAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        EmployeeAPIDBContext dbContext;
        public EmployeeService(EmployeeAPIDBContext _db)
        {
            this.dbContext = _db;
        }

        public IEnumerable<Employee> GetEmployee()
        {
            var employee = dbContext.Employees.ToList();
            return employee;
        }
        public Employee AddEmployee(Employee employee)
        {
            if (employee != null)
            {
                dbContext.Employees.Add(employee);
                dbContext.SaveChanges();
                sendmail("Welcome");
                return employee;
            }
            return null;
        }

        public Employee UpdateEmployee(Employee employee)
        {
            dbContext.Entry(employee).State = EntityState.Modified;
            dbContext.SaveChanges();
            return employee;
        }

        public Employee DeleteEmployee(int id)
        {
            var employee = dbContext.Employees.FirstOrDefault(x => x.EmployeeId == id);
            dbContext.Entry(employee).State = EntityState.Deleted;
            dbContext.SaveChanges();
            sendmail("Deleted Successfully");
            return employee;
        }

        public Employee GetEmployeeById(int id)
        {
            var employee = dbContext.Employees.FirstOrDefault(x => x.EmployeeId == id);
            return employee;
        }
        public string GetEmployeeByStatus(int id)
        {

            string employeestatus = string.Empty;

            var groupedEmploeesByDepartment = dbContext.Employees.GroupBy(x => new { x.ManagerId }).Select(x => new { Employee = x.Key, EmployeesCount = x.Count() }).Where(x => x.Employee.ManagerId.Equals(id)).ToList();

            if (groupedEmploeesByDepartment.Count > 0)
            {
                var departmentid = dbContext.Employees.Where(x => x.EmployeeId == groupedEmploeesByDepartment[0].Employee.ManagerId).Select(x => x.DepartmentId).ToList();

                if (departmentid[0].ToString() == "2")
                {
                    employeestatus = "Manager";
                }
                else
                {
                    employeestatus = "Head";
                }
            }    
            else
            {
                employeestatus = "Associate";
            }


            return employeestatus;

        }

        public void sendmail(string msg)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("vishalrko214@gmail.com", "vishalrko214@gmail.com"));
            message.To.Add(new MailboxAddress("vishalrko214@gmail.com", "vishalrko214@gmail.com"));
            message.Subject = msg;
            message.Body = new TextPart("plain")
            {
                Text = msg
            };


            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("USERNAME", "PASSWORD");
                client.Send(message);
                client.Disconnect(true);
            }
        }


    }
}
