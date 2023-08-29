using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeTestApi.Data.Models;
using EmployeeTestApi.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace EmployeeTestApi.Data.Repositories.Implementations
{
    public class EmployeeRepository: IEmployeeRepository
    {
        private readonly EfContext _efContext;
        private readonly ILogger<EmployeeRepository> _logger;

        public EmployeeRepository(EfContext efContext, ILogger<EmployeeRepository> logger)
        {
            _efContext = efContext;
            _logger = logger;
        }
        public async Task<IEnumerable<EmployeeRm>> GetList(EmployeesFilterModel filters)
        {
            _logger.LogInformation($"GetList. Filters: {JsonConvert.SerializeObject(filters)}");
            try
            {
                var result = _efContext.Employee
                    .Include(e => e.Department)
                    .Where(e => filters.DepartmentName.IsNullOrEmpty() ||
                                e.Department.DepartmentName.Contains(filters.DepartmentName))
                    .Where(e => filters.EmployeeFullName.IsNullOrEmpty() ||
                                (e.FirstName.Contains(filters.EmployeeFullName) ||
                                 e.LastName.Contains(filters.EmployeeFullName) ||
                                 e.FathersName.Contains(filters.EmployeeFullName)))
                    .Where(e => filters.SalarySearchStr.IsNullOrEmpty() ||
                                e.Salary.ToString().Contains(filters.SalarySearchStr))
                    .Where(e => filters.DateOfBirth.IsNullOrEmpty() ||
                                e.DateOfBirth == DateTime.Parse(filters.DateOfBirth))
                    .Where(e => filters.DateOfEmployment.IsNullOrEmpty() ||
                                e.DateOfBirth == DateTime.Parse(filters.DateOfEmployment));

                if (filters.AscDirection)
                {
                    switch (filters.SortColumnName)
                    {
                        case "Department":
                            return await result.OrderBy(e => e.Department.DepartmentName).ToListAsync();
                        
                        case "FullName":
                            return await result.OrderBy(e => e.LastName)
                                .ThenBy(e => e.FirstName)
                                .ThenBy(e => e.FathersName)
                                .ToListAsync();
                        
                        case "DateOfBirth":
                            return await result.OrderBy(e => e.DateOfBirth).ToListAsync();
                        
                        case "DateOfEmployment":
                            return await result.OrderBy(e => e.DateOfEmployment).ToListAsync();
                        
                        case "Salary":
                            return await result.OrderBy(e => e.Salary).ToListAsync();
                    }
                }
                else
                {
                    switch (filters.SortColumnName)
                    {
                        case "Department":
                            return await result.OrderByDescending(e => e.Department.DepartmentName).ToListAsync();
                        
                        case "FullName":
                            return await result.OrderByDescending(e => e.LastName)
                                .ThenByDescending(e => e.FirstName)
                                .ThenByDescending(e => e.FathersName)
                                .ToListAsync();
                        
                        case "DateOfBirth":
                            return await result.OrderByDescending(e => e.DateOfBirth).ToListAsync();
                        
                        case "DateOfEmployment":
                            return await result.OrderByDescending(e => e.DateOfEmployment).ToListAsync();
                        
                        case "Salary":
                            return await result.OrderByDescending(e => e.Salary).ToListAsync();
                            
                    }
                }
                return await result.ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"GetList filters: {JsonConvert.SerializeObject(filters)} message: {e.Message}");
                throw;
            }
        }

        public async Task<EmployeeRm> GetEditModel(int id)
        {
            _logger.LogInformation($"GetEditModel. id: {id}");
            try
            {
                var result = await _efContext.Employee
                    .Include(e => e.Department)
                    .FirstOrDefaultAsync(e => e.EmployeeId == id);
                return result;
                
            }
            catch (Exception e)
            {
                _logger.LogError($"GetEditModel id: {id} message: {e.Message}");
                throw;
            }
        }

        public async Task UpdateEmployee(EmployeeRm model)
        {
            _logger.LogInformation($"UpdateEmployee. Employee: {JsonConvert.SerializeObject(model)}");
            try
            {
                var employee = await _efContext.Employee.FirstOrDefaultAsync(e => e.EmployeeId == model.EmployeeId);
                if (employee == null) throw new NullReferenceException();

                employee.FirstName = model.FirstName;
                employee.LastName = model.LastName;
                employee.FathersName = model.FathersName;
                employee.Salary = model.Salary;
                employee.DateOfBirth = model.DateOfBirth;
                employee.DateOfEmployment = model.DateOfEmployment;
                employee.DepartmentId = model.DepartmentId;

                await _efContext.SaveChangesAsync();

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
                var result = _efContext.Department.ToList();
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
            _logger.LogInformation($"CreateEmployee. Employee: {JsonConvert.SerializeObject(model)}");
            try
            {
                if (model == null) throw new NullReferenceException();
                await _efContext.Employee.AddAsync(model);
                await _efContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"CreateEmployee model: {JsonConvert.SerializeObject(model)} message: {e.Message}");
                throw;
            }
        }
        
        public async Task DeleteEmployee(int id)
        {
            _logger.LogInformation($"DeleteEmployee. id: {id}");
            try
            {
                var employee = await _efContext.Employee.FirstOrDefaultAsync(e => e.EmployeeId == id);
                if (employee == null) throw new Exception();

                _efContext.Employee.Remove(employee);
                await _efContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"DeleteEmployee id: {id} message: {e.Message}");
                throw;
            }
            
            
        }
    }
}