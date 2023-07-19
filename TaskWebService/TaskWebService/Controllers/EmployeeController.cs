using Dapper;
using Microsoft.AspNetCore.Mvc;
using WebService.Models;

namespace TaskWebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController
    {
        IEmployeeRepository employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository) => this.employeeRepository = employeeRepository;
        [HttpPost]
        public int AddEmployee(Employee employee) => employeeRepository.AddEmployee(employee);

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id) => employeeRepository.DeleteEmployee(id);

        [HttpGet("by-companyId/{companyId}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeesByCompany(int companyId) => await employeeRepository.GetEmployeesByCompany(companyId);

        [HttpGet("by-departmentName/{departmentName}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeesByDepartmentName(string departmentName) => await employeeRepository.GetEmployeesByDepartmentName(departmentName);

        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<Employee>>> UpdateEmployees(int id, string? name, string? surname, string?
                                                                phone, int? companyId, string? passportType, string? passportNumber,
                                                                string? departmentName, string? departmentPhone) =>
                                                                await employeeRepository.UpdateEmployees(id, name, surname,
                                                                phone, companyId, passportType, passportNumber,
                                                                departmentName, departmentPhone);
        

    }

    
}
