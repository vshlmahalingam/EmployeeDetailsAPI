using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace EmployeeDetailsAPI.Models
{
    public partial class Employee
    {
        public Employee()
        {
            InverseManager = new HashSet<Employee>();
        }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
         ErrorMessage = "Invalid email format")]
        public string EmailId { get; set; }
        [Required]
        public int? DepartmentId { get; set; }
        public int? ManagerId { get; set; }

        public virtual Department Department { get; set; }
        public virtual Employee Manager { get; set; }
        public virtual ICollection<Employee> InverseManager { get; set; }
    }
}
