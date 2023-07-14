using Dapper;
using Microsoft.AspNetCore.Mvc;
using WebService.Models;

namespace TaskWebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetByDepController : Controller
    {
        [HttpGet("by-departametName/{departamentName}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeesByDepartamentName(string departamentName)
        {
            using var connection = DBConnection.CreateConnection();
            await connection.OpenAsync();
            const string sql = @"SELECT * FROM employe WHERE DepartamentName = @DepartamentName";
            var employees = await connection.QueryAsync<Employee>(sql, new { DepartamentName = departamentName });
            return Ok(employees);
        }
    }
}
