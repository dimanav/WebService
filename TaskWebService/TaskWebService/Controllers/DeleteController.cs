using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace TaskWebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeleteController : Controller
    {
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            using var connection = DBConnection.CreateConnection();
            const string sql = "DELETE FROM employe WHERE Id = @Id";
            var affectedRows = connection.Execute(sql, new { Id = id });

            if (affectedRows == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
