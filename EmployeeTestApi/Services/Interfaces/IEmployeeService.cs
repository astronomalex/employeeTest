using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeTestApi.Data.Dtos;
using EmployeeTestApi.Data.Models;

namespace EmployeeTestApi.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeListItemDto>> GetList(EmployeesFilterModel filters);
        Task<EmployeeSm> GetEditModel(int id);
        Task UpdateEmployee(EmployeeRm model);
        Task<IEnumerable<DepartmentRm>> GetDepartments();
        Task CreateEmployee(EmployeeRm model);
        Task DeleteEmployee(int id);
    }
}