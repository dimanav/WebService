using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebService.Models;

namespace TaskWebService
{
    public class EmployeeRepository : Controller, IEmployeeRepository
    {
        
        public int AddEmployee(Employee employee)
        {
            using var connection = DBConnection.CreateConnection();
            const string sql = @"INSERT INTO employees (Name, Surname, Phone, CompanyId, PassportType, PassportNumber, DepartmentName, DepartmentPhone) 
               VALUES (@Name, @Surname, @Phone, @CompanyId, @PassportType, @PassportNumber, @DepartmentName, @DepartmentPhone);
               SELECT LAST_INSERT_ID();";

            int id = connection.Query<int>(sql, new
            {
                employee.Name,
                employee.Surname,
                employee.Phone,
                employee.CompanyId,
                PassportType = employee.Passport.PassportType,
                PassportNumber = employee.Passport.PassportNumber,
                DepartmentName = employee.Department.DepartmentName,
                DepartmentPhone = employee.Department.DepartmentPhone
            }).FirstOrDefault();

            return id;

        }

        public IActionResult DeleteEmployee(int id)
        {
            using var connection = DBConnection.CreateConnection();
            const string sql = "DELETE FROM employees WHERE Id = @Id";
            var affectedRows = connection.Execute(sql, new { Id = id });

            if (affectedRows == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeesByCompany(int companyId)
        {
            using var connection = DBConnection.CreateConnection();
            await connection.OpenAsync();
            const string sql = @"SELECT * FROM employees WHERE CompanyId = @CompanyId";
            var employees = await connection.QueryAsync<Employee>(sql, new { CompanyId = companyId });
            return Ok(employees);
        }

        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeesByDepartmentName(string departmentName)
        {
            using var connection = DBConnection.CreateConnection();
            await connection.OpenAsync();
            const string sql = @"SELECT * FROM employees WHERE DepartmentName = @DepartmentName";
            var employees = await connection.QueryAsync<Employee>(sql, new { DepartmentName = departmentName });
            return Ok(employees);
        }

        public async Task<ActionResult<IEnumerable<Employee>>> UpdateEmployees(int id, string? name, string? surname, string?
                                                                phone, int? companyId, string? passportType, string? passportNumber,
                                                                string? departmentName, string? departmentPhone)
        {
            using var connection = DBConnection.CreateConnection();
            await connection.OpenAsync();

            const string sql = @"UPDATE employees SET name = COALESCE(@Name, Name),   
                                Surname = COALESCE(@Surname, Surname), 
                                Phone = COALESCE(@Phone, Phone),
                                CompanyId = COALESCE(@CompanyId, CompanyId),
                                PassportType = COALESCE(@PassportType, PassportType),
                                PassportNumber = COALESCE(@PassportNumber, PassportNumber),
                                DepartmentName = COALESCE(@DepartmentName, DepartmentName),
                                DepartmentPhone = COALESCE(@DepartmentPhone, DepartmentPhone)
                                WHERE Id = @Id";

            
            var employees = await connection.QueryAsync<Employee>(sql, new
            {
                Id = id,
                Name = name,
                Surname = surname,
                Phone = phone,
                CompanyId = companyId,
                PassportType = passportType,
                PassportNumber = passportNumber,
                DepartmentName = departmentName,
                DepartmentPhone = departmentPhone
            });
            return Ok(employees);
        }
    }
}
