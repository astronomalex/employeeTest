using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeTestApi.Data.Dtos;
using EmployeeTestApi.Data.Models;
using EmployeeTestApi.Data.Repositories.Interfaces;
using EmployeeTestApi.Services.Interfaces;

namespace EmployeeTestApi.Services.Implementations
{
    public class EmployeeService: IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<IEnumerable<EmployeeListItemDto>> GetList(EmployeesFilterModel filters)
        {
            try
            {
                return await _employeeRepository.GetList(filters);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<EmployeeRm> GetEditModel(int id)
        {
            try
            {
                return await _employeeRepository.GetEditModel(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task UpdateEmployee(EmployeeRm model)
        {
            try
            {
                await _employeeRepository.UpdateEmployee(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<IEnumerable<DepartmentRm>> GetDepartments()
        {
            try
            {
                var result = await _employeeRepository.GetDepartments();
                return result.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        public async Task CreateEmployee(EmployeeRm model)
        {
            try
            {
                await _employeeRepository.CreateEmployee(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        public async Task DeleteEmployee(int id)
        {
            try
            {
                await _employeeRepository.DeleteEmployee(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}