using Microsoft.AspNetCore.Mvc;
using WebService.Models;

namespace TaskWebService
{
    public interface IEmployeeRepository
    {
        int AddEmployee(Employee employee);
        IActionResult DeleteEmployee(int employeeId);
        Task<ActionResult<IEnumerable<Employee>>> GetEmployeesByCompany(int companyId);
        Task<ActionResult<IEnumerable<Employee>>> GetEmployeesByDepartmentName(string departmentName);
        Task<ActionResult<IEnumerable<Employee>>> UpdateEmployees(int id, string? name, string? surname, string?
                                                                phone, int? companyId, string? passportType, string? passportNumber,
                                                                string? departmentName, string? departmentPhone);
    }
}
