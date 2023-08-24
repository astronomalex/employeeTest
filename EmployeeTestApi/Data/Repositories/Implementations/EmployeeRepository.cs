using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using EmployeeTestApi.Data.Constants;
using EmployeeTestApi.Data.Dtos;
using EmployeeTestApi.Data.Models;
using EmployeeTestApi.Data.Repositories.Interfaces;
using Microsoft.Data.SqlClient;

namespace EmployeeTestApi.Data.Repositories.Implementations
{
    public class EmployeeRepository: IEmployeeRepository
    {
        private readonly DapperContext _context;

        public EmployeeRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<EmployeeListItemDto>> GetList(EmployeesFilterModel filters)
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@DepartmentFilter", filters.DepartmentName, DbType.String);
                    parameters.Add("@FullNameFilter", filters.EmployeeFullName, DbType.String);
                    parameters.Add("@DateOfBirthFilter", filters.DateOfBirth, DbType.DateTime);
                    parameters.Add("@DateOfEmplFilter", filters.DateOfEmployment, DbType.DateTime);
                    parameters.Add("@SalaryFilter", filters.SalarySearchStr, DbType.String);
                    parameters.Add("@ColumnSort", filters.SortColumnName, DbType.String);
                    parameters.Add("@AscSort", filters.AscDirection, DbType.Boolean);
                    
                    var result =
                        await connection.QueryAsync<EmployeeListItemDto>(StoredProcedures.GetEmployees,
                            commandType: CommandType.StoredProcedure, param: parameters);
                    return result.ToList();
                }
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
                using (var connection = _context.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@Id", id, DbType.Int32);
                    var result =
                        await connection.QuerySingleAsync<EmployeeRm>(StoredProcedures.GetEmployee,
                            commandType: CommandType.StoredProcedure, param: parameters);
                    return result;
                }
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
                using (var connection = _context.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@Id", model.EmployeeId, DbType.Int32);
                    parameters.Add("@DepartmentId", model.DepartmentId, DbType.Int32);
                    parameters.Add("@FirstName", model.FirstName, DbType.String);
                    parameters.Add("@LastName", model.LastName, DbType.String);
                    parameters.Add("@FathersName", model.FathersName, DbType.String);
                    parameters.Add("@DateOfBirth", model.DateOfBirth, DbType.DateTime);
                    parameters.Add("@DateOfEmployment", model.DateOfEmployment, DbType.DateTime);
                    parameters.Add("@Salary", model.Salary, DbType.Decimal);
                    
                    await connection.ExecuteAsync(StoredProcedures.UpdateEmployee,
                            commandType: CommandType.StoredProcedure, param: parameters);
                    
                }
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
                using (var connection = _context.CreateConnection())
                {
                    var result =
                        await connection.QueryAsync<DepartmentRm>(StoredProcedures.GetDepartments,
                            commandType: CommandType.StoredProcedure);
                    return result.ToList();
                }
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
                using (var connection = _context.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@DepartmentId", model.DepartmentId, DbType.Int32);
                    parameters.Add("@FirstName", model.FirstName, DbType.String);
                    parameters.Add("@LastName", model.LastName, DbType.String);
                    parameters.Add("@FathersName", model.FathersName, DbType.String);
                    parameters.Add("@DateOfBirth", model.DateOfBirth, DbType.DateTime);
                    parameters.Add("@DateOfEmployment", model.DateOfEmployment, DbType.DateTime);
                    parameters.Add("@Salary", model.Salary, DbType.Decimal);
                    
                    await connection.ExecuteAsync(StoredProcedures.CreateEmployee,
                        commandType: CommandType.StoredProcedure, param: parameters);
                    
                }
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
                using (var connection = _context.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@Id", id, DbType.Int32);
                    
                    await connection.ExecuteAsync(StoredProcedures.DeleteEmployee,
                        commandType: CommandType.StoredProcedure, param: parameters);
                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            
        }
    }
}