using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeTestApi.Data.Dtos;
using EmployeeTestApi.Data.Models;
using EmployeeTestApi.Data.Repositories.Interfaces;
using EmployeeTestApi.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EmployeeTestApi.Services.Implementations
{
    public class EmployeeService: IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper, ILogger<EmployeeService> logger)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _logger = logger;
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
                _logger.LogError($"GetList filters: {JsonConvert.SerializeObject(filters)} message: {e.Message}");
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
                _logger.LogError($"GetEditModel id: {id} message: {e.Message}");
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
                _logger.LogError($"UpdateEmployee model: {JsonConvert.SerializeObject(model)} message: {e.Message}");
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
                _logger.LogError($"GetDepartments. message: {e.Message}");
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
                _logger.LogError($"CreateEmployee model: {JsonConvert.SerializeObject(model)} message: {e.Message}");
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
                _logger.LogError($"DeleteEmployee id: {id} message: {e.Message}");
                throw;
            }
        }
    }
}