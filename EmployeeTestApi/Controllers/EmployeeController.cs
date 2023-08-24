using System;
using System.Threading.Tasks;
using EmployeeTestApi.Data.Models;
using EmployeeTestApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        
        [HttpGet("getEmployees")]
        public async Task<IActionResult> GetEmployees([FromQuery] EmployeesFilterModel filters)
        {
            var result = await _employeeService.GetList(filters);
            return Ok(result);
        }

        [HttpGet("getEditModel")]
        public async Task<IActionResult> GetEditModel(int id)
        {
            var result = await _employeeService.GetEditModel(id);
            return Ok(result);
        }

        [HttpPost("updateEmployee")]
        public async Task<IActionResult> UpdateEmployee([FromBody]EmployeeRm employee)
        {
            if (employee == null) throw new ArgumentNullException();
            await _employeeService.UpdateEmployee(employee);
            return Ok();
        }
        
        [HttpGet("getDepartments")]
        public async Task<IActionResult> GetDepartments()
        {
            var result = await _employeeService.GetDepartments();
            return Ok(result);
        }
        
        [HttpPost("createEmployee")]
        public async Task<IActionResult> CreateEmployee([FromBody]EmployeeRm employee)
        {
            if (employee == null) throw new ArgumentNullException();
            await _employeeService.CreateEmployee(employee);
            return Ok();
        }
        
        [HttpPost("deleteEmployee")]
        public async Task<IActionResult> DeleteEmployee([FromBody]int id)
        {
            if (id == 0) throw new ArgumentNullException();
            await _employeeService.DeleteEmployee(id);
            return Ok();
        }
    }
}