using KropkaNet.Api.Services.ClientSide;
using KropkaNet.Objects.Dtos.ClientSide.Company;
using KropkaNet.Objects.Entities.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KropkaNet.Api.Controllers.ClientSide
{
    [Route("api/kropkaNet/company")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        // POST api/kropkaNet/company/create
        [HttpPost("create")]
        [Authorize]
        public ActionResult Create([FromQuery] int? userId, [FromBody] CreateCompanyDto dto)
        {
            var createdCompanyId = _companyService.Create(userId, dto);

            var result = Created($"{createdCompanyId}", null) as CreatedResult;
            if (result != null)
            {
                Response.Headers.Add("Access-Control-Expose-Headers", "Location");
            }

            return result;
        }

        // GET api/kropkaNet/company/user/{id}
        [HttpGet("user/{userId}")]
        [Authorize]
        public ActionResult<IEnumerable<CompanyDto>> GetListUser(
            [FromRoute] int userId,
            [FromQuery] int page,
            [FromQuery] string? filters,
            [FromQuery] string? sortBy,
            [FromQuery] SortDirection sortDirection
            )
        {
            var companyDtos = _companyService.GetListUser(userId, page, filters, sortBy, sortDirection);
            return Ok(companyDtos);
        }

        // GET api/kropkaNet/company/all
        [HttpGet("all")]
        [Authorize(Roles = "Employee, Moderator, Admin")]
        public ActionResult<IEnumerable<CompanyDto>> GetList(
            [FromQuery] int page,
            [FromQuery] string? filters,
            [FromQuery] string? sortBy,
            [FromQuery] SortDirection sortDirection
            )
        {
            var companyDtos = _companyService.GetList(page, filters, sortBy, sortDirection);
            return Ok(companyDtos);
        }

        // GET api/kropkaNet/company/{id}
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<CompanyDto> GetDetails([FromRoute] int id)
        {
            var companyDto = _companyService.GetDetails(id);
            return Ok(companyDto);
        }

        // PUT api/kropkaNet/company/update/5
        [HttpPut("update/{id}")]
        [Authorize]
        public ActionResult Update([FromRoute] int id, [FromBody] CreateCompanyDto dto)
        {
            var orderId = _companyService.Update(id, dto);

            return Ok($"{orderId}");
        }

        // PATCH api/kropkaNet/company/addUser
        [HttpPatch("addUser")]
        [Authorize]
        public ActionResult AddUser([FromQuery] int userId, [FromQuery] int companyId)
        {
            _companyService.AddUser(userId, companyId);

            return Ok();
        }

        // PATCH api/kropkaNet/company/removeUser
        [HttpPatch("removeUser")]
        [Authorize]
        public ActionResult RemoveUser([FromQuery] int userId, [FromQuery] int companyId)
        {
            _companyService.RemoveUser(userId, companyId);

            return Ok();
        }


        // DELETE api/kropkaNet/company/delete{id}
        [HttpDelete("delete/{id}")]
        [Authorize]
        public ActionResult Delete([FromRoute] int id)
        {
            _companyService.Delete(id);

            return NoContent();
        }
    }
}
