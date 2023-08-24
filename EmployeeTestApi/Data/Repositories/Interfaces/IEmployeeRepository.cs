using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeTestApi.Data.Dtos;
using EmployeeTestApi.Data.Models;

namespace EmployeeTestApi.Data.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task UpdateEmployee(EmployeeRm model);
        Task<IEnumerable<EmployeeListItemDto>> GetList(EmployeesFilterModel filters);
        Task<EmployeeRm> GetEditModel(int id);
        Task<IEnumerable<DepartmentRm>> GetDepartments();
        Task CreateEmployee(EmployeeRm model);
        Task DeleteEmployee(int id);
    }
}