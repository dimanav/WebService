using Dapper;
using Microsoft.AspNetCore.Mvc;
using WebService.Models;

namespace TaskWebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetByCompanyController : Controller
    {
        [HttpGet("by-companyId/{companyId}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeesByCompany(int companyId)
        {
            using var connection = DBConnection.CreateConnection();
            await connection.OpenAsync();
            const string sql = @"SELECT * FROM employe WHERE CompanyId = @CompanyId";
            var employees = await connection.QueryAsync<Employee>(sql, new { CompanyId = companyId });
            return Ok(employees);
        }
    }
}
