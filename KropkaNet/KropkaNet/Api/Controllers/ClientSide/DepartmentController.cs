using KropkaNet.Api.Services.ClientSide;
using KropkaNet.Objects.Dtos.ClientSide.Department;
using KropkaNet.Objects.Entities.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KropkaNet.Api.Controllers.ClientSide
{
    [Route("api/kropkaNet/department")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpPost("create")]
        [Authorize]
        public ActionResult Create([FromQuery] int companyId, [FromBody] CreateDepartmentDto dto)
        {
            var createdDepartmentId = _departmentService.Create(companyId, dto);

            var result = Created($"{createdDepartmentId}", null) as CreatedResult;
            if (result != null)
            {
                Response.Headers.Add("Access-Control-Expose-Headers", "Location");
            }

            return result;
        }

        // GET api/kropkaNet/departemnt/all
        [HttpGet("all")]
        [Authorize(Roles = "Employee, Moderator, Admin")]
        public ActionResult<IEnumerable<DepartmentDto>> GetList(
            [FromQuery] int page,
            [FromQuery] string? filters,
            [FromQuery] string? sortBy,
            [FromQuery] SortDirection sortDirection
            )
        {
            var departmentDtos = _departmentService.GetList(page, filters, sortBy, sortDirection);
            return Ok(departmentDtos);
        }

        // GET api/kropkaNet/company/{companyId}
        [HttpGet("company/{companyId}")]
        [Authorize]
        public ActionResult GetFromCompany(
            [FromRoute] int companyId,
            [FromQuery] string? sortBy,
            [FromQuery] SortDirection sortDirection
            )
        {
            var departmentDtos = _departmentService.GetFromCompany(companyId, sortBy, sortDirection);
            return Ok(departmentDtos);
        }

        // GET api/kropkaNet/departemnt/user/{userId}
        [HttpGet("user/{userId}")]
        [Authorize]
        public IActionResult GetUserDepartments([FromRoute] int userId)
        {
            var departmentDtos = _departmentService.GetUserDepartments(userId);
            return Ok(departmentDtos);
        }

        // PUT api/kropkaNet/department/update/5
        [HttpPut("update/{id}")]
        [Authorize]
        public ActionResult Update([FromRoute] int id, [FromBody] CreateDepartmentDto dto)
        {
            var departmentId = _departmentService.Update(id, dto);

            return Ok($"{departmentId}");
        }
        // DELETE api/kropkaNet/department/delete{id}
        [HttpDelete("delete/{id}")]
        [Authorize]
        public ActionResult<IEnumerable<DepartmentDto>> Delete([FromRoute] int id)
        {
            var departmentDtos = _departmentService.Delete(id);

            return NoContent();
        }
    }
}
