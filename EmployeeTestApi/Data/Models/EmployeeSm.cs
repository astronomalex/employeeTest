using System;

namespace EmployeeTestApi.Data.Models
{
    public class EmployeeSm
    {
        public int EmployeeId { get; set; }
        
        public int DepartmentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FathersName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfEmployment { get; set; }
        public decimal Salary { get; set; }
    }
}