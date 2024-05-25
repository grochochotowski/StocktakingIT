using Azure;
using KropkaNetApi.X_Entities.Enum;
using KropkaNetApi.X_Models.ClientSide.Company;
using KropkaNetApi.X_Models.ClientSide.Department;
using KropkaNetApi.Y_Services.ClientSide;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KropkaNetApi.Y_Controllers.ClientSide
{
    [Route("api/kropkaNet/department")]
    [ApiController]
    [Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpPost("create")]
        [Authorize]
        public ActionResult Create([FromBody] CreateDepartmentDto dto)
        {
            var createdDepartmentId = _departmentService.Create(dto);

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
            [FromQuery] SortDirection sortDireciton
            )
        {
            var departmentDtos = _departmentService.GetList(page, filters, sortBy, sortDireciton);
            return Ok(departmentDtos);
        }
        // GET api/kropkaNet/departemnt/order/{id}
        [HttpGet("user/{orderId}")]
        [Authorize]
        public ActionResult<IEnumerable<DepartmentDto>> GetListOrder(
            [FromRoute] int orderId,
            [FromQuery] int page,
            [FromQuery] string? filters,
            [FromQuery] string? sortBy,
            [FromQuery] SortDirection sortDireciton
            )
        {
            var departmentDtos = _departmentService.GetListOrder(orderId, page, filters, sortBy, sortDireciton);
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
