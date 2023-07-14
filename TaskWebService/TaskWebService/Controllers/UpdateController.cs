using Dapper;
using Microsoft.AspNetCore.Mvc;
using WebService.Models;

namespace TaskWebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UpdateController : Controller
    {
        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<Employee>>> UpdateEmployees(int id, string? name, string? surname, string?
                                                                phone, int? companyId, string? passportType, string? passportNumber, 
                                                                string? departamentName, string? departamentPhone)
        {
            using var connection = DBConnection.CreateConnection();
            await connection.OpenAsync();
            string sql = "UPDATE employe SET ";

            if (!string.IsNullOrEmpty(name))
                sql += "Name = @Name, ";

            if (!string.IsNullOrEmpty(surname))
                sql += "Surname = @Surname, ";

            if (!string.IsNullOrEmpty(phone))
                sql += "Phone = @Phone, ";

            if (companyId.HasValue)
                sql += "CompanyId = @CompanyId, ";

            if (!string.IsNullOrEmpty(passportType))
                sql += "PassportType = @PassportType, ";

            if (!string.IsNullOrEmpty(passportNumber))
                sql += "PassportNumber = @PassportNumber, ";

            if (!string.IsNullOrEmpty(departamentName))
                sql += "DepartamentName = @DepartamentName, ";

            if (!string.IsNullOrEmpty(departamentPhone))
                sql += "DepartamentPhone = @DepartamentPhone, ";

            sql = sql.TrimEnd(',', ' ');

            sql += " WHERE Id = @Id";
            var employees = await connection.QueryAsync<Employee>(sql, new { Id = id, Name = name, Surname = surname, Phone = phone,
                                            CompanyId = companyId, PassportType = passportType, PassportNumber = passportNumber, 
                                            DepartamentName = departamentName, DepartamentPhone = departamentPhone });
            return Ok(employees);
        }
    }
}
