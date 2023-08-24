using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EmployeeTestApi.Data.Models
{
    public class EmployeesFilterModel
    {
        public string EmployeeFullName { get; set; }
        public string DepartmentName { get; set; }
        public string SalarySearchStr { get; set; }
        public string DateOfBirth { get; set; }
        public string DateOfEmployment { get; set; }
        public string SortColumnName { get; set; }
        public bool AscDirection { get; set; }
    }
}