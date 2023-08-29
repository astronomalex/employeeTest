using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeTestApi.Data.Models
{
    public class DepartmentRm
    {
        [Key]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public IEnumerable<EmployeeRm> Employees { get; set; }
    }
}