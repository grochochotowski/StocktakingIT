using KropkaNetApi.X_Entities.Enum;
using KropkaNetApi.X_Models.ClientSide.Department;
using KropkaNetApi.X_Models.CompanySide.Employee;
using KropkaNetApi.X_Models.Shared.Account;
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

        [HttpPost("create")]
        //[Authorize(Roles = "Moderator, Admin")]
        public ActionResult Create([FromBody] CreateEmployeeDto dto, RegisterEmployeeDto registerdto)
        {
            var createdEmployeeId = _employeeService.Create(dto, registerdto);

            var result = Created($"{createdEmployeeId}", null) as CreatedResult;
            if (result != null)
            {
                Response.Headers.Add("Access-Control-Expose-Headers", "Location");
            }

            return result;
        }

        // GET api/kropkaNet/employee/all
        [HttpGet("all")]
        //[Authorize(Roles = "Employee, Moderator, Admin")]
        public ActionResult<IEnumerable<DepartmentDto>> GetList(
            [FromQuery] int page,
            [FromQuery] string? filters,
            [FromQuery] string? sortBy,
            [FromQuery] SortDirection sortDireciton
            )
        {
            var employeeDtos = _employeeService.GetList(page, filters, sortBy, sortDireciton);
            return Ok(employeeDtos);
        }
        // GET api/kropkaNet/employee/{id}
        [HttpGet("{employeeId}")]
        //[Authorize(Roles = "Employee, Moderator, Admin")]
        public ActionResult<IEnumerable<EmployeeDto>> GetById(
            [FromRoute] int employeeId,
            [FromQuery] int page,
            [FromQuery] string? filters,
            [FromQuery] string? sortBy,
            [FromQuery] SortDirection sortDireciton
            )
        {
            var employeeDtos = _employeeService.GetById(employeeId, page, filters, sortBy, sortDireciton);
            return Ok(employeeDtos);
        }
        // PUT api/kropkaNet/employee/update/5
        [HttpPut("update/{id}")]
        //[Authorize(Roles = "Employee, Moderator, Admin")]
        public ActionResult Update([FromRoute] int id, [FromBody] CreateEmployeeDto dto)
        {
            var employeeDtos = _employeeService.Update(id, dto);

            return Ok($"{employeeDtos}");
        }

        // PATCH api/kropkaNet/employee/changeposition/{id}
        [HttpPut("changeposition/{id}")]
        //[Authorize(Roles = "Admin")]
        public ActionResult ChangePosition([FromRoute] int employeeId, [FromBody] int positionId)
        {
            _employeeService.ChangePosition(employeeId, positionId);

            return Ok();
        }
        // DELETE api/kropkaNet/department/delete{id}
        [HttpDelete("delete/{id}")]
        //[Authorize(Roles = "Moderator, Admin")]
        public ActionResult<IEnumerable<EmployeeDto>> Delete([FromRoute] int id)
        {
            var employeeDtos = _employeeService.Delete(id);

            return NoContent();
        }
    }
}
