using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeTestApi.Data.Dtos;
using EmployeeTestApi.Data.Models;
using EmployeeTestApi.Data.Repositories.Interfaces;
using EmployeeTestApi.Services.Interfaces;

namespace EmployeeTestApi.Services.Implementations
{
    public class EmployeeService: IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<EmployeeListItemDto>> GetList(EmployeesFilterModel filters)
        {
            try
            {
                var result = await _employeeRepository.GetList(filters);
                return _mapper.Map<IEnumerable<EmployeeListItemDto>>(result.ToList());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<EmployeeSm> GetEditModel(int id)
        {
            try
            {
                var result = await _employeeRepository.GetEditModel(id);
                var mappedResult = _mapper.Map<EmployeeSm>(result);

                return mappedResult;
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