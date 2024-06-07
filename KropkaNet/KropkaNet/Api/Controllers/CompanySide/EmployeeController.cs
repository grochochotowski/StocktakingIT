using KropkaNet.Api.Services.CompanySide;
using KropkaNet.Objects.Dtos.ClientSide.User;
using KropkaNet.Objects.Dtos.CompanySide.Employee;
using KropkaNet.Objects.Entities;
using KropkaNet.Objects.Entities.Enum;
using Microsoft.AspNetCore.Mvc;

namespace KropkaNet.Api.Controllers.CompanySide
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


        // GET api/kropkaNet/employee/all
        [HttpGet("all")]
        //[Authorize(Roles = "Employee, Moderator, Admin")]
        public ActionResult<IEnumerable<EmployeeDto>> GetList(
            [FromQuery] int page,
            [FromQuery] string? filters,
            [FromQuery] string? sortBy,
            [FromQuery] SortDirection sortDireciton
            )
        {
            var employeeDtos = _employeeService.GetList(page, filters, sortBy, sortDireciton);
            return Ok(employeeDtos);
        }

        // GET: api/kropkaNet/user/{stocktakingId}/GetFromStocktaking
        [HttpGet("{stocktakingId}/GetFromStocktaking")]
        //[Authorize]
        public ActionResult<ReturnResult<UserDto>> GetFromStocktaking([FromRoute] int stocktakingId)
        {
            var result = _employeeService.GetFromStocktaking(stocktakingId);
            return Ok(result);
        }

        // GET api/kropkaNet/employee/{id}
        [HttpGet("{employeeId}")]
        //[Authorize(Roles = "Employee, Moderator, Admin")]
        public ActionResult<EmployeeDto> GetById([FromRoute] int employeeId)
        {
            var employeeDto = _employeeService.GetById(employeeId);
            return Ok(employeeDto);
        }
        // PUT api/kropkaNet/employee/update/5
        [HttpPut("update/{id}")]
        //[Authorize(Roles = "Employee, Moderator, Admin")]
        public ActionResult Update([FromRoute] int id, [FromBody] UpdateEmployeeDto dto)
        {
            var employeeDtos = _employeeService.Update(id, dto);

            return Ok($"{employeeDtos}");
        }

        // PATCH api/kropkaNet/employee/changeposition/{employeeId}
        [HttpPut("changeposition/{employeeId}")]
        //[Authorize(Roles = "Admin")]
        public ActionResult ChangePosition([FromRoute] int employeeId, [FromQuery] int positionId)
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
