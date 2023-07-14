using Dapper;
using Microsoft.AspNetCore.Mvc;
using WebService.Models;

namespace TaskWebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddController
    {
        [HttpPost]
        public int AddEmployee(Employee employee)
        {
            using var connection = DBConnection.CreateConnection();
            const string sql = @"INSERT INTO employe (Name, Surname, Phone, CompanyId, PassportType, PassportNumber, DepartamentName, DepartamentPhone) 
               VALUES (@Name, @Surname, @Phone, @CompanyId, @PassportType, @PassportNumber, @DepartamentName, @DepartamentPhone);
               SELECT LAST_INSERT_ID();";

            int id = connection.Query<int>(sql, new
            {
                employee.Name,
                employee.Surname,
                employee.Phone,
                employee.CompanyId,
                PassportType = employee.Passport.PassportType,
                PassportNumber = employee.Passport.PassportNumber,
                DepartamentName = employee.Department.DepartamentName,
                DepartamentPhone = employee.Department.DepartamentPhone
            }).FirstOrDefault();

            return id;

        }
    }
}
