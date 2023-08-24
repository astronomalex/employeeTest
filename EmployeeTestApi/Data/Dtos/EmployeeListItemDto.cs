using System;

namespace EmployeeTestApi.Data.Dtos
{
    public class EmployeeListItemDto
    {
                public int EmployeeId { get; set; }
                public string DepartmentName { get; set; }
                public string FullName { get; set; }
                public DateTime DateOfBirth { get; set; }
                public DateTime DateOfEmployment { get; set; }
                public decimal Salary { get; set; }
    }
}