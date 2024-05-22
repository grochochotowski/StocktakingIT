using KropkaNetApi.X_Models.CompanySide.Employee;
using KropkaNetApi.X_Models.CompanySide.Product;
using KropkaNetApi.Y_Services.CompanySide;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KropkaNetApi.Y_Controllers.CompanySide
{
    [Route("api/company/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }


        // POST: /api/company/employee/register
        [HttpPost("register")]
        public ActionResult Register([FromBody] RegisterEmployeeDto dto)
        {
            _employeeService.Register(dto);
            return Ok();
        }
    }
}
